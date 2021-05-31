using ActivityPlanner2.Data.ServerModels;
using ActivityPlanner2.Shared;
using ActivityPlanner2.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityPlanner2.Data
{
    public class PersonRepository : IPersonRepository
    {
        ApplicationDbContext context;
        public PersonRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task AddPerson(BasePersonDTO NewPersonToAdd)
        {
            Person newPerson = (Person)NewPersonToAdd;
            context.People.Add(newPerson);
            await saveChanges();
        }

        public async Task DeletePerson(string id)
        {
            Person userToDelete = await GetPersonById(id);

            var PlannedActivities = context.PersonActivities.Where(p => p.PersonId == id);
            context.RemoveRange(PlannedActivities);

            var OrganizedActivities = context.PersonOrginizers.Where(p => p.OrganizerId == id);
            context.RemoveRange(OrganizedActivities);

            context.People.Remove(userToDelete);

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Person>> GetListOfPeople() 
                            => await Task.FromResult(context.People);

        public async Task<IEnumerable<Person>> GetListOfPeopleByName(string name)
        {
            var result = (context.People
                .Where(v => v.MiddleName == null ?
                ($"{v.FirstName} {v.LastName}").Contains(name) 
                : ($"{v.FirstName} {v.MiddleName} {v.LastName}").Contains(name)));

            return await Task.FromResult(result);
        }

        public async Task<Person> GetPersonById(string id)
        {
            return await context.People.FindAsync(id);
        }

        public async Task<Person> GetPersonByUserName(string name)
        {
            return await Task.FromResult(context.People.First(p => p.UserName == name));
        }

        public async Task UpdatePerson(BasePersonDTO updatedPersonData)
        {
            var PersonFromDb = await GetPersonById(updatedPersonData.Id);

            if (PersonFromDb.FirstName != updatedPersonData.FirstName)
                PersonFromDb.FirstName = updatedPersonData.FirstName;

            if (PersonFromDb.MiddleName != updatedPersonData.MiddleName)
                PersonFromDb.MiddleName = updatedPersonData.MiddleName;

            if (PersonFromDb.LastName != updatedPersonData.LastName)
                PersonFromDb.LastName = updatedPersonData.LastName;

            context.People.Update(PersonFromDb);

            await saveChanges();
        }

        private async Task saveChanges() => await context.SaveChangesAsync();

    }
}
