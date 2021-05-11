using ActivityPlanner2.Shared;
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
        Task AddPerson(Person NewPersonToAdd);
        Task DeletePerson(string id);
        Task UpdatePerson(Person updatedPersonData);
    }
}
