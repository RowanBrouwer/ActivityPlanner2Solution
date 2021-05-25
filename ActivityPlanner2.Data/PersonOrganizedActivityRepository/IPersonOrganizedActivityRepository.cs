using ActivityPlanner2.Data.ServerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityPlanner2.Data
{
    public interface IPersonOrganizedActivityRepository
    {
        Task<PersonOrganizedActivity> GetInviteByPersonId(string id);
        Task<PersonOrganizedActivity> GetInviteByActivityId(int id);
        Task AddInvite(PersonOrganizedActivity invite);
        Task UpdateInvite(PersonOrganizedActivity invite);
        Task DeleteInviteByPersonId(string id);
        Task DeleteInviteByActivityId(int id);
    }
}
