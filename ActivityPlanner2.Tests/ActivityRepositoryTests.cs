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
using ActivityPlanner2.Shared.DTOs;

namespace ActivityPlanner2.Tests
{
    public class ActivityRepositoryTests
    {
        (DbContextOptions, IOptions<OperationalStoreOptions>) options = DbSetup.CreateNewContextOptions();
        readonly UserManager<Person> manager;
        readonly ApplicationDbContext db;
        readonly IPersonRepository personContext;
        readonly IPersonInviteRepository personInviteRepository;
        readonly IPersonOrganizedActivityRepository organizedContext;
        readonly IActivityRepository context;
        readonly IActivityLogic logic;

        readonly string TestPersonId1;
        readonly string TestPersonId2;
        readonly string TestPersonId3;

        readonly int TestActivityId1;

        public ActivityRepositoryTests()
        {
            db = DbSetup.CreateContext(options);
            manager = DbSetup.CreateUsermanager(db);
            DbSetup.Seed(manager, db);
            personContext = new PersonRepository(db);
            personInviteRepository = new PersonInviteRepository(db, personContext, context);
            organizedContext = new PersonOrganizedActivityRepository(db, personContext, context);
            logic = new ActivityLogic(personInviteRepository, organizedContext, db);
            context = new ActivityRepository(db, logic);

            personContext = new PersonRepository(db);
            personInviteRepository = new PersonInviteRepository(db, personContext, context);
            organizedContext = new PersonOrganizedActivityRepository(db, personContext, context);
            logic = new ActivityLogic(personInviteRepository, organizedContext, db);
            context = new ActivityRepository(db, logic);

            TestPersonId1 = db.People.First().Id;
            TestPersonId2 = db.People.Skip(1).First().Id;
            TestPersonId3 = db.People.Skip(2).First().Id;
            TestActivityId1 = db.Activities.First().Id;
        }

        [Fact]
        public void AddActivityFromActivityTest()
        {
            var activity = new Activity() { ActivityName = "RepTest"};

            context.AddActivityFromActivity(activity);

            Assert.Contains(db.Activities, p => p.ActivityName == "RepTest");
        }

        [Fact]
        public void AddActivityFromDTOTest()
        {
            var activity = new ActivityDTO()
            {
                ActivityName = "TestPost",
                DateOfDeadline = DateTime.Now.ToString(),
                DateOfEvent = DateTime.Now.ToString(),
                Describtion = "Just a test",
                InvitedGuests = new List<PersonInvitesDTO>() {
                 new PersonInvitesDTO() { PersonId = TestPersonId1, Accepted = true}
                , new PersonInvitesDTO() { PersonId = TestPersonId2, Accepted = true } },
                Organizers = new List<PersonOrganizedActivityDTO>() { new PersonOrganizedActivityDTO() { PersonId = TestPersonId1 } }
            };

            var ActivityToAdd = new Activity();

            context.AddActivityFromDTO(activity, ActivityToAdd);

            Assert.Contains(db.Activities, p => p.ActivityName == "TestPost");
        }

        [Fact]
        public async Task DeleteActivityTest()
        {
            var activityToDelete = await context.GetActivityById(TestActivityId1);

            string name = activityToDelete.ActivityName;

            await context.DeleteActivity(activityToDelete.Id);

            Assert.DoesNotContain(db.Activities, p => p.ActivityName == name);
        }

        [Fact]
        public async Task GetActivityByIdTest()
        {
            var activityToAdd = new Activity() { ActivityName = "GetByIdTest" };

            await context.AddActivityFromActivity(activityToAdd);

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
        public async Task UpdateActivityFromActivityTest()
        {
            var dbActivity = await context.GetActivityById(TestActivityId1);

            dbActivity.ActivityName = "TestActivityName";

            await context.UpdateActivityFromActivity(dbActivity);

            var dbActivityToCheck = await context.GetActivityById(TestActivityId1);

            Assert.Equal(dbActivity, dbActivityToCheck);
        }

        [Fact]
        public async Task UpdateActivityFromDTOTest()
        {
            var dbActivityForChecking = await context.GetActivityById(TestActivityId1);

            var activity = new ActivityDTO()
            {
                Id = dbActivityForChecking.Id,
                ActivityName = "TestPost",
                DateOfDeadline = DateTime.Now.ToString(),
                DateOfEvent = DateTime.Now.ToString(),
                Describtion = "Just a test",
                InvitedGuests = new List<PersonInvitesDTO>() {
                 new PersonInvitesDTO() { PersonId = TestPersonId1, Accepted = true}
                , new PersonInvitesDTO() { PersonId = TestPersonId2, Accepted = true } },
                Organizers = new List<PersonOrganizedActivityDTO>() { new PersonOrganizedActivityDTO() { PersonId = TestPersonId1 } }
            };

            await context.UpdateActivityFromDTO(TestActivityId1, activity);

            Assert.Contains(db.Activities, a => a == dbActivityForChecking);
        }
    }
}
