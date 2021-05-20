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
        Task<IEnumerable<Activity>> GetListOfActivitiesByPersonId(string id);
        Task<IEnumerable<Activity>> GetListOfActivitiesByName(string name);
        Task AddActivity(Activity NewActivityToAdd);
        Task DeleteActivity(int Id);
        Task UpdateActivity(Activity updatedActivityData);
    }
}
