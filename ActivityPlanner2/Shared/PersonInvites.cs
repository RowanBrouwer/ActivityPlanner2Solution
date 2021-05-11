using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityPlanner2.Shared
{
    public class PersonInvites
    {
        [ForeignKey("Person")]
        public string PersonId { get; set; }
        public Person Person { get; set; }
        [ForeignKey("Activity")]
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }
        public bool Accepted { get; set; }
    }
}
