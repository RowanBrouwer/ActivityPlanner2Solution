using ActivityPlanner2.Data;
using ActivityPlanner2.Data.ServerModels;
using ActivityPlanner2.Server.Controllers;
using ActivityPlanner2.Shared;
using ActivityPlanner2.Shared.DTOs;
using ActivityPlanner2.Tests.Setup;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Xunit;

namespace ActivityPlanner2.Tests
{
    public class PeopleControllerTests
    {
        PeopleController controller;
        (DbContextOptions, IOptions<OperationalStoreOptions>) options = DbSetup.CreateNewContextOptions();

        UserManager<Person> manager;
        ApplicationDbContext db;
        IPersonRepository context;
        readonly string TestPersonId1;

        public PeopleControllerTests()
        {
            db = DbSetup.CreateContext(options);
            manager = DbSetup.CreateUsermanager(db);
            DbSetup.Seed(manager, db);
            context = new PersonRepository(db);
            controller = new PeopleController(context);
            TestPersonId1 = db.People.First().Id;
        }

        [Fact]
        public async void GetListOfPeopleApiTest()
        {
            var okResult = await controller.GetListOfPeople();

            Assert.IsType<OkObjectResult>(okResult.Result);

            var peopleObj = StaticMethods.GetObjectResultContent(okResult);

            Assert.IsAssignableFrom<IEnumerable<BasePersonDTO>>(peopleObj);
        }

        [Fact]
        public async void GetPersonApiTest()
        {
            var okResult = await controller.GetPerson(TestPersonId1);

            Assert.IsType<OkObjectResult>(okResult.Result);

            var personObj = StaticMethods.GetObjectResultContent(okResult);

            Assert.IsAssignableFrom<BasePersonDTO>(personObj);
        }

        [Fact]
        public async void PostApiTest()
        {
            Person person = new() {Email = "xS1lv3r@xS1lv3r", FirstName = "Silver", LastName = "Dilver", UserName = "xS1lv3r@xS1lv3r"};

            var okResult = await controller.Post((BasePersonDTO)person);

            Assert.IsType<OkObjectResult>(okResult.Result);

            var personObj = StaticMethods.GetObjectResultContent(okResult);

            Assert.IsAssignableFrom<BasePersonDTO>(personObj);

            Assert.Equal(person.Id, personObj.Id);
            Assert.Equal(person.FirstName, personObj.FirstName);
            Assert.Equal(person.MiddleName, personObj.MiddleName);
            Assert.Equal(person.LastName, personObj.LastName);
        }

        [Fact]
        public async void PutApiTest()
        {
            var personToUpdate = await context.GetPersonById(TestPersonId1);

            personToUpdate.FirstName = "PutUpdateTest";

            var okResult = await controller.Put(TestPersonId1 , (BasePersonDTO)personToUpdate);

            Assert.IsType<OkObjectResult>(okResult.Result);

            BasePersonDTO personObj = StaticMethods.GetObjectResultContent(okResult);

            Assert.IsAssignableFrom<BasePersonDTO>(personObj);

            Assert.Equal(personToUpdate.FirstName, personObj.FirstName);
        }

        [Fact]
        public async void DeleteApiTest()
        {
            var noResult = await controller.Delete(TestPersonId1);

            Assert.IsType<NoContentResult>(noResult);

            Assert.DoesNotContain(db.People, p => p.Id == TestPersonId1);
        }
    }
}
