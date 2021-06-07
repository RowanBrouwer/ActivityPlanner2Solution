using ActivityPlanner2.Data.ServerModels;
using ActivityPlanner2.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityPlanner2.Data
{
    public interface IActivityRepository
    {
        Task<Activity> GetActivityById(int id);
        Task<IEnumerable<Activity>> GetListOfActivities();
        Task<IEnumerable<Activity>> GetListOfInvitedActivitiesByPersonId(string id);
        Task<IEnumerable<Activity>> GetListOfOrganizedActivitiesByPersonId(string id);
        Task<IEnumerable<Activity>> GetListOfActivitiesByName(string name);
        Task AddActivityFromDTO(ActivityDTO activityDTO, Activity activity);
        Task<Activity> AddActivityFromActivity(Activity activity);
        Task DeleteActivity(int Id);
        Task UpdateActivityFromDTO(int id, ActivityDTO value);
        Task UpdateActivityFromActivity(Activity activity);
        Task<IEnumerable<Person>> GetInvitesdByActivityId(int id);
        Task<IEnumerable<Person>> GetOrganizersByActivityid(int id);
    }
}
