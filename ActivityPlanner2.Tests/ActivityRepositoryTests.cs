using ActivityPlanner2.Data.ServerModels;
using ActivityPlanner2.Data;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ActivityPlanner2.Tests
{
    public class ActivityRepositoryTests
    {
        (DbContextOptions, IOptions<OperationalStoreOptions>) options = DbSetup.CreateNewContextOptions();

        UserManager<Person> manager;
        ApplicationDbContext db;
        IActivityRepository context;

        readonly string TestPersonId1;
        readonly string TestPersonId2;
        readonly string TestPersonId3;

        readonly int TestActivityId1;

        public ActivityRepositoryTests()
        {
            db = DbSetup.CreateContext(options);
            manager = DbSetup.CreateUsermanager(db);
            DbSetup.Seed(manager, db);
            context = new ActivityRepository(db);

            TestPersonId1 = db.People.First().Id;
            TestPersonId2 = db.People.Skip(1).First().Id;
            TestPersonId3 = db.People.Skip(2).First().Id;
            TestActivityId1 = db.Activities.First().Id;
        }

        [Fact]
        public void AddActivityTest()
        {
            var activity = new Activity() { ActivityName = "RepTest"};

            context.AddActivity(activity);

            Assert.Contains(db.Activities, p => p.ActivityName == "RepTest");
        }

        [Fact]
        public async Task DeleteActivityTest()
        {
            var activityToDelete = await context.GetActivityById(TestActivityId1);

            string name = activityToDelete.ActivityName;

            await context.DeleteActivity(activityToDelete);

            Assert.DoesNotContain(db.Activities, p => p.ActivityName == name);
        }

        [Fact]
        public async Task GetActivityByIdTest()
        {
            var activityToAdd = new Activity() { ActivityName = "GetByIdTest" };

            await context.AddActivity(activityToAdd);

            var foundActivity = await context.GetActivityById(activityToAdd.Id);

            Assert.Equal(activityToAdd, foundActivity);
        }

        [Fact]
        public async Task GetListOfActivitiesTest()
        {
            var ActivityList = await context.GetListOfActivities();

            Assert.Equal(4, ActivityList.Count());
        }

        [Fact]
        public async Task GetListOfActivitiesByNameTest()
        {
            var NameBasedActivityList = await context.GetListOfActivitiesByName("Football");

            Assert.Single(NameBasedActivityList);

            Assert.Contains(NameBasedActivityList, a => a.ActivityName == "Football");
        }

        [Fact]
        public async Task GetListOfActivitiesByPersonIdTest()
        {
            var PersonBasedList = await context.GetListOfActivitiesByPersonId(TestPersonId1);

            Assert.Equal(4, PersonBasedList.Count());

            Assert.Contains(PersonBasedList, a => a.ActivityName == "Football");
        }

        [Fact]
        public async Task UpdateActivityTest()
        {
            var dbActivity = await context.GetActivityById(TestActivityId1);

            dbActivity.ActivityName = "TestActivityName";

            await context.UpdateActivity(dbActivity);

            var dbActivityToCheck = await context.GetActivityById(TestActivityId1);

            Assert.Equal(dbActivity, dbActivityToCheck);
        }
    }
}
