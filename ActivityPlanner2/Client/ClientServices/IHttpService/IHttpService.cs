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
        public Task<IEnumerable<ClientActivity>> GetlistOfInvitedActivitiesByPerson(string id);
        public Task<IEnumerable<ClientActivity>> GetlistOfActivities();
        public Task<ClientBasePerson> GetCurrentPersonByUserName(string name);
        public Task<HttpResponseMessage> AddPerson(ClientBasePerson person);
        public Task<HttpResponseMessage> UpdateVisitor(ClientBasePerson person);
        public void Dispose();
    }
}
