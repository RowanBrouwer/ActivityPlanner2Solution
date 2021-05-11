using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityPlanner2.Shared
{
    public class ActivityInvitation
    {
        public IEnumerable<Person> InvitedPeople { get; set; }
        public DateTime DateOfEvent { get; set; }
        public DateTime DateOfDeadline { get; set; }
        public string ActivityName { get; set; }
        public string ActivityDescribtion { get; set; }
    }
}
