using ActivityPlanner2.Data.ServerModels;
using ActivityPlanner2.Data;
using ActivityPlanner2.Server.Controllers;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActivityPlanner2.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using ActivityPlanner2.Tests.Setup;

namespace ActivityPlanner2.Tests
{
    public class ActivityControllerTests
    {
        ActivityController controller;
        (DbContextOptions, IOptions<OperationalStoreOptions>) options = DbSetup.CreateNewContextOptions();

        UserManager<Person> manager;
        ApplicationDbContext db;
        IPersonRepository personContext;
        IActivityRepository activityContext;
        IPersonInviteRepository personInviteRepository;
        IPersonOrganizedActivityRepository organizedContext;
        IActivityLogic Logic;
        readonly string TestPersonId1;
        readonly string TestPersonId2;
        readonly int TestActivityId1;

        public ActivityControllerTests()
        {
            db = DbSetup.CreateContext(options);
            manager = DbSetup.CreateUsermanager(db);
            DbSetup.Seed(manager, db);

            personContext = new PersonRepository(db);
            personInviteRepository = new PersonInviteRepository(db, personContext, activityContext);
            organizedContext = new PersonOrganizedActivityRepository(db, personContext, activityContext);
            Logic = new ActivityLogic(personInviteRepository, organizedContext, db);
            activityContext = new ActivityRepository(db, Logic);
            controller = new ActivityController(personContext, activityContext);

            personContext = new PersonRepository(db);
            personInviteRepository = new PersonInviteRepository(db, personContext, activityContext);
            organizedContext = new PersonOrganizedActivityRepository(db, personContext, activityContext);
            Logic = new ActivityLogic(personInviteRepository, organizedContext, db);
            activityContext = new ActivityRepository(db, Logic);
            controller = new ActivityController(personContext, activityContext);

            TestPersonId1 = db.People.First().Id;
            TestPersonId2 = db.People.Skip(1).First().Id;
            TestActivityId1 = db.Activities.First().Id;
        }

        [Fact]
        public async Task GetApiTest()
        {
            var OkObjectResult = await controller.Get();

            Assert.IsType<OkObjectResult>(OkObjectResult.Result);

            var ACtivityObj = StaticMethods.GetObjectResultContent(OkObjectResult);

            Assert.IsAssignableFrom<IEnumerable<ActivityDTO>>(ACtivityObj);
        }

        [Fact]
        public async Task GetByIdApiTest()
        {
            var OkObjectResult = await controller.Get(TestActivityId1);

            Assert.IsType<OkObjectResult>(OkObjectResult.Result);

            var ACtivityObj = StaticMethods.GetObjectResultContent(OkObjectResult);

            Assert.IsAssignableFrom<ActivityDTO>(ACtivityObj);
        }

        [Fact]
        public async Task PostApiTest()
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
                Organizers = new List<PersonOrganizedActivityDTO>() { new PersonOrganizedActivityDTO() { PersonId = TestPersonId1} }
            };

            var CreatedObjectResult = await controller.Post(activity);

            Assert.IsType<CreatedResult>(CreatedObjectResult);
        }

        [Fact]
        public async Task PutApiTest()
        {
            var activity = await activityContext.GetActivityById(1);

            var activityUpdate = new ActivityDTO()
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

            var NoContentObjectResult = await controller.Put(activity.Id , activityUpdate);

            Assert.IsType<NoContentResult>(NoContentObjectResult);
        }

        [Fact]
        public async Task DeleteApiTest()
        {
            var ActivityToDelete = await activityContext.GetActivityById(1);

            controller.Delete(ActivityToDelete.Id);

            Assert.DoesNotContain(db.Activities, a => a.Id == ActivityToDelete.Id);
        }
    }
}
