using ActivityPlanner2.Client.ClientModels;
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
        public Task<IEnumerable<ClientBasePerson>> GetListOfPeople();
        public Task<IEnumerable<ClientBasePerson>> GetListOfInvitesByActivtyId(int id);
        public Task<IEnumerable<ClientBasePerson>> GetListOfOrganizersByActivtyId(int id);
        public Task<IEnumerable<ClientActivity>> GetlistOfInvitedActivitiesByPerson(string id);
        public Task<IEnumerable<ClientActivity>> GetlistOfOrganizedActivitiesByPerson(string id);
        public Task<IEnumerable<ClientActivity>> GetlistOfActivities();
        public Task<ClientActivity> GetActivityById(int id);
        public Task<ClientBasePerson> GetPersonByUserName(string name);
        public Task<ClientBasePerson> GetPersonById(string Id);
        public Task<HttpResponseMessage> AddPerson(ClientBasePerson person);
        public Task<HttpResponseMessage> UpdatePerson(ClientBasePerson person);
        public void Dispose();
    }
}
