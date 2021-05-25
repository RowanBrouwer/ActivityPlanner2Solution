using ActivityPlanner2.Data.ServerModels;
using ActivityPlanner2.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityPlanner2.Data
{
    public interface IActivityLogic
    {
        public Task<IEnumerable<PersonInvites>> PersonInviteDtoListToPersonInviteList(ActivityDTO value, int Id);
        public Task<IEnumerable<PersonOrganizedActivity>> OrganizersDtoListToOrganizerList(ActivityDTO value, int Id);
    }
}
