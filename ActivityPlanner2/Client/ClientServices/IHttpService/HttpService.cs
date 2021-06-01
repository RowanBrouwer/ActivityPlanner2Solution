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
using ActivityPlanner2.Client.ClientModels;
using ActivityPlanner2.Shared.DTOs;

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

        public async Task<IEnumerable<ClientBasePerson>> GetListOfPeople()
        {
            logger.LogInformation($"Calling API-GET for People List at {DateTime.Now.ToShortTimeString()}");

            var result = await Http.GetFromJsonAsync<IEnumerable<ClientBasePerson>>(StringCollection.Api_PeopleControler_Uri);

            return result;
        }

        public async Task<ClientBasePerson> GetCurrentPersonByUserName(string name)
        {
            logger.LogInformation($"Calling API-GET for Person {name} at {DateTime.Now.ToShortTimeString()}");

            var result = await Http.GetFromJsonAsync<ClientBasePerson>(StringCollection.Api_PeopleControler_Uri + $"/{name}");

            return result;
        }

        public async Task<HttpResponseMessage> AddPerson(ClientBasePerson person)
        {
            logger.LogInformation($"Calling API-POST for Person {person.Id} at {DateTime.Now.ToShortTimeString()}");

            var result = await Http.PostAsJsonAsync(StringCollection.Api_PeopleControler_Uri, person);

            return result;
        }

        public async Task<HttpResponseMessage> UpdateVisitor(ClientBasePerson person)
        {
            logger.LogInformation($"Calling API-PUT for Person {person.Id} at {DateTime.Now.ToShortTimeString()}");

            var result = await Http.PutAsJsonAsync(StringCollection.Api_PeopleControler_Uri + $"/{person.Id}", person);

            return result;
        }


        public async Task<IEnumerable<ClientActivity>> GetlistOfInvitedActivitiesByPerson(string userId)
        {
            logger.LogInformation($"Calling API-PUT for Person {userId} at {DateTime.Now.ToShortTimeString()}");

            var result = await Http.GetFromJsonAsync<IEnumerable<ActivityDTO>>($"api/invited/{userId}");

            List<ClientActivity> activityList = new List<ClientActivity>();

            foreach (var activity in result)
            {
                activityList.Add((ClientActivity)activity);
            }

            return activityList;
        }

        public async Task<IEnumerable<ClientActivity>> GetlistOfActivities()
        {
            logger.LogInformation($"Calling API-GET for Activity List at {DateTime.Now.ToShortTimeString()}");

            var result = await Http.GetFromJsonAsync<IEnumerable<ActivityDTO>>("api/Activity");

            List<ClientActivity> activityList = new List<ClientActivity>();

            foreach (var activity in result)
            {
                activityList.Add((ClientActivity)activity);
            }

            return activityList;
        }

        public void Dispose()
        {
            ((IDisposable)Http).Dispose();
        }
    }
}
