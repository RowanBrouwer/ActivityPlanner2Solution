using ActivityPlanner2.Data;
using ActivityPlanner2.Shared;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ActivityPlanner2.Tests
{
    public class PersonRepositoryTests
    {
        (DbContextOptions, IOptions<OperationalStoreOptions>) options = DbSetup.CreateNewContextOptions();

        UserManager<Person> manager;
        ApplicationDbContext db;
        IPersonRepository context;

        readonly string TestPersonId1;
        readonly string TestPersonId2;
        readonly string TestPersonId3;


        public PersonRepositoryTests()
        {
            db = DbSetup.CreateContext(options);
            manager = DbSetup.CreateUsermanager(db);
            DbSetup.Seed(manager, db);
            context = new PersonRepository(db);

            TestPersonId1 = db.People.First().Id;
            TestPersonId2 = db.People.Skip(1).First().Id;
            TestPersonId3 = db.People.Skip(2).First().Id;
        }

        [Fact]
        public async void GetListOfPeopleTest()
        {

            var result = await context.GetListOfPeople();
            var value = result.Count();

            Assert.NotEqual(0, value);
        }

        [Fact]
        public async void GetPersonByIdTest()
        {
            var result = await context.GetPersonById(TestPersonId1);

            string fullname = result.FullName();
            string expectedFullname = "Rowan Brouwer";

            Assert.Equal(fullname, expectedFullname);
        }

        [Fact]
        public async void UpdatePersonTest()
        {
            var user = await context.GetPersonById(TestPersonId1);

            user.MiddleName = "Test";

            await context.UpdatePerson(user);

            var userAfterUpdate = await context.GetPersonById(TestPersonId1);
            var ExpectedMiddleName = "Test";

            Assert.Equal(userAfterUpdate.MiddleName, ExpectedMiddleName);
        }

        [Fact]
        public async void DeletePersonById()
        {
            var Person = await context.GetPersonById(TestPersonId1);

            await context.DeletePerson(Person.Id);

            Assert.DoesNotContain(db.People, p => p.Id == TestPersonId1);
        }

        [Fact]
        public async void AddPersonTest()
        {
            Person newPerson = new Person { FirstName = "VeryDistinctName", LastName = "Weird" };

            await context.AddPerson(newPerson);

            Person NewDbPerson = db.People.Find(newPerson.Id);
            string expectedFullname = "VeryDistinctName Weird";
            string actualFullname = NewDbPerson.FullName();

            Assert.Equal(expectedFullname, actualFullname);
        }


        [Fact]
        public async void GetListOfPeopleByName()
        {
            string Name = "Rowan Brouwer";

            var SearchResult = await context.GetListOfPeopleByName(Name);

            Assert.Contains(SearchResult, p => p.FullName() == "Rowan Brouwer");
        }
    }
}
