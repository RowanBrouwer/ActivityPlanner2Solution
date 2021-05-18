using ActivityPlanner2.Data.ServerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityPlanner2.Data.ActivityRepository
{
    public class ActivityRepository : IActivityRepository
    {
        public Task AddActivity(Activity NewActivityToAdd)
        {
            throw new NotImplementedException();
        }

        public Task DeleteActivity(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Activity> GetActivityById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Activity>> GetListOfActivities()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Activity>> GetListOfActivitiesByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task UpdateActivity(Activity updatedActivityData)
        {
            throw new NotImplementedException();
        }
    }
}
