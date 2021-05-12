using ActivityPlanner2.Client.Pages;
using ActivityPlanner2.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ActivityPlanner2.Client.ClientServices
{ 
    public class HttpService : IHttpService, IDisposable
    {
        readonly HttpClient Http;
        ILogger<HttpService> logger;
        private bool disposedValue;

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

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    Http.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~HttpService()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
