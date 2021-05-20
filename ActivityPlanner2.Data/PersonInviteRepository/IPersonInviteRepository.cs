using ActivityPlanner2.Data.ServerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityPlanner2.Data
{
    public interface IPersonInviteRepository
    {
        Task<PersonInvites> GetInviteByPersonId(string id);
        Task<PersonInvites> GetInviteByActivityId(int id);
        Task AddInvite(PersonInvites Invite);
        Task UpdateInvite(PersonInvites Invite);
        Task DeleteInvite(int id);
    }
}
