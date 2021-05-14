using ActivityPlanner2.Client.Pages;
using ActivityPlanner2.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ActivityPlanner2;

namespace ActivityPlanner2.Client.ClientServices
{ 
    public class HttpService : IHttpService, IDisposable
    {
        readonly HttpClient Http;
        ILogger<HttpService> logger;

        public HttpService(HttpClient Http, ILogger<HttpService> logger)
        {
            this.Http = Http;
            this.logger = logger;
        }

        public async Task<IEnumerable<Person>> GetListOfPeople()
        {
            logger.LogInformation($"Calling API-GET for People List at {DateTime.Now.ToShortTimeString()}");

            var result = await Http.GetFromJsonAsync<IEnumerable<Person>>(StringCollection.Api_PeopleControler_Uri);

            return result;
        }

        public async Task<Person> GetPersonById(string Id)
        {
            logger.LogInformation($"Calling API-GET for Person {Id} at {DateTime.Now.ToShortTimeString()}");

            var result = await Http.GetFromJsonAsync<Person>(StringCollection.Api_PeopleControler_Uri + $"/{Id}");

            return result;
        }

        public async Task<HttpResponseMessage> AddPerson(Person person)
        {
            logger.LogInformation($"Calling API-POST for Person {person.Id} at {DateTime.Now.ToShortTimeString()}");

            var result = await Http.PostAsJsonAsync(StringCollection.Api_PeopleControler_Uri, person);

            return result;
        }

        public async Task<HttpResponseMessage> UpdateVisitor(Person person)
        {
            logger.LogInformation($"Calling API-PUT for Person {person.Id} at {DateTime.Now.ToShortTimeString()}");

            var result = await Http.PutAsJsonAsync(StringCollection.Api_PeopleControler_Uri + $"/{person.Id}", person);

            return result;
        }

        public void Dispose()
        {
            ((IDisposable)Http).Dispose();
        }
    }
}
