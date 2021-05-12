using ActivityPlanner2.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ActivityPlanner2.Client.ClientServices
{
    public interface IHttpService
    {
        public Task<IEnumerable<Person>> GetListOfPeople();
        public Task<Person> GetPersonById(string Id);
        public Task<HttpResponseMessage> AddPerson(Person person);
        public Task<HttpResponseMessage> UpdateVisitor(Person person);
        public void Dispose();
    }
}
