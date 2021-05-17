using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityPlanner2.Data.ServerModels
{
    public class PersonOrginizedActivity
    {
        public string OrganizerId { get; set; }
        public Person Organizer { get; set; }
        public int OrganizedActivityId { get; set; }
        public Activity OrganizedActivity { get; set; }
    }
}
