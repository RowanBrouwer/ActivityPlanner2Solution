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
        Task<IEnumerable<PersonOrganizedActivity>> GetOrganizedActivitiesByPersonId(string id);
        Task<PersonOrganizedActivity> GetOrganizedActivitiesByPersonIdAndActivityId(string personId, int activityId);
        Task<IEnumerable<PersonOrganizedActivity>> GetOrganizedActivitiesByActivityId(int id);
        Task AddOrganizedActivities(PersonOrganizedActivity Invite);
        Task UpdateOrganizedActivities(PersonOrganizedActivity Invite);
        Task DeleteOrganizedActivitiesByActivityId(int id);
        Task DeleteOrganizedActivitiesByPersonId(string id);
        Task DeleteOrganizedActivitiesByPersonIdAndActivityId(string personId, int activityId);
    }
}
