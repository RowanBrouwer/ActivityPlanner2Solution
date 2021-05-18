using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityPlanner2.Shared.DTOs
{
    public class PersonInvitesDTO
    {
        public string PersonId { get; set; }
        public int ActivityId { get; set; }
        public bool Accepted { get; set; }
    }
}
