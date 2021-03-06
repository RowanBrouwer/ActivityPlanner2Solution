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
using System.Diagnostics;

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

            var result = await Http.GetFromJsonAsync<IEnumerable<ClientBasePerson>>(StringCollection.Api_PeopleControler_Uri + "/GetAllPeople");

            return result;
        }

        public async Task<ClientBasePerson> GetPersonByUserName(string name)
        {
            logger.LogInformation($"Calling API-GET for Person {name} at {DateTime.Now.ToShortTimeString()}");

            var result = await Http.GetFromJsonAsync<ClientBasePerson>(StringCollection.Api_PeopleControler_Uri + "/GetPersonByName" + $"/{name}");

            return result;
        }

        public async Task<ClientBasePerson> GetPersonById(string Id)
        {
            logger.LogInformation($"Calling API-GET for Person {Id} at {DateTime.Now.ToShortTimeString()}");

            var result = await Http.GetFromJsonAsync<ClientBasePerson>(StringCollection.Api_PeopleControler_Uri + "/GetPersonById" + $"/{Id}");

            return result;
        }

        public async Task<HttpResponseMessage> AddPerson(ClientBasePerson person)
        {
            logger.LogInformation($"Calling API-POST for Person {person.Id} at {DateTime.Now.ToShortTimeString()}");

            var result = await Http.PostAsJsonAsync(StringCollection.Api_PeopleControler_Uri + "/AddPerson", person);

            return result;
        }

        public async Task<HttpResponseMessage> UpdatePerson(ClientBasePerson person)
        {
            logger.LogInformation($"Calling API-PUT for Person {person.Id} at {DateTime.Now.ToShortTimeString()}");

            BasePersonDTO dto = (BasePersonDTO)person;

            var result = await Http.PutAsJsonAsync(StringCollection.Api_PeopleControler_Uri + "/UpdatePerson" + $"/{dto.Id}", dto);

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

            var result = await Http.GetFromJsonAsync<IEnumerable<ActivityDTO>>("api/Activity/GetAllActivitys");

            List<ClientActivity> activityList = new List<ClientActivity>();

            foreach (var activity in result)
            {
                activityList.Add((ClientActivity)activity);
            }

            return activityList;
        }

        public async Task<IEnumerable<ClientActivity>> GetlistOfOrganizedActivitiesByPerson(string userId)
        {
            logger.LogInformation($"Calling API-GET for Person {userId} at {DateTime.Now.ToShortTimeString()}");

            var result = await Http.GetFromJsonAsync<IEnumerable<ActivityDTO>>($"api/Organized/{userId}");

            List<ClientActivity> activityList = new List<ClientActivity>();

            foreach (var activity in result)
            {
                activityList.Add((ClientActivity)activity);
            }

            return activityList;
        }

        public async Task<IEnumerable<ClientBasePerson>> GetListOfInvitesByActivtyId(int id)
        {
            logger.LogInformation($"Calling API-GET for INVITES of ACTIVITY - {id} at {DateTime.Now.ToShortTimeString()}");

            var result = await Http.GetFromJsonAsync<IEnumerable<BasePersonDTO>>($"api/Activity/GetInvitedPeopleByActivityId/{id}");

            List<ClientBasePerson> InviteList = new List<ClientBasePerson>();

            foreach (var person in result)
            {
                InviteList.Add((ClientBasePerson)person);
            }

            return InviteList;
        }

        public async Task<IEnumerable<ClientBasePerson>> GetListOfOrganizersByActivtyId(int id)
        {
            logger.LogInformation($"Calling API-GET for ORGANIZERS of ACTIVITY - {id} at {DateTime.Now.ToShortTimeString()}");

            var result = await Http.GetFromJsonAsync<IEnumerable<BasePersonDTO>>($"api/Activity/GetOrganizersByActivityId/{id}");

            List<ClientBasePerson> OrganizerList = new List<ClientBasePerson>();

            foreach (var person in result)
            {
                OrganizerList.Add((ClientBasePerson)person);
            }

            return OrganizerList;
        }

        public void Dispose()
        {
            ((IDisposable)Http).Dispose();
        }

        public async Task<ClientActivity> GetActivityById(int id)
        {
            logger.LogInformation($"Calling API-GET for ACTIVITY - {id} at {DateTime.Now.ToShortTimeString()}");

            var result = await Http.GetFromJsonAsync<ActivityDTO>($"api/Activity/GetActivityById/{id}");

            var castresult = (ClientActivity)result;

            return castresult;
        }
    }
}
