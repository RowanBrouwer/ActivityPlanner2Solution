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
    public interface IPersonRepository
    {
        Task<Person> GetPersonById(string id);
        Task<IEnumerable<Person>> GetListOfPeople();
        Task<IEnumerable<Person>> GetListOfPeopleByName(string name);
        Task AddPerson(BasePersonDTO NewPersonToAdd);
        Task DeletePerson(string id);
        Task UpdatePerson(BasePersonDTO updatedPersonData);
        Task<Person> GetPersonByUserName(string name);
    }
}
