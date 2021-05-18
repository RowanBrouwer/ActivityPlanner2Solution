using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityPlanner2.Shared.DTOs
{
    public class ActivityDTO
    {
        public int Id { get; set; }
        public string ActivityName { get; set; }
        public IEnumerable<PersonOrganizedActivityDTO> Organizers { get; set; }
        public IEnumerable<PersonInvitesDTO> InvitedGuests { get; set; }
        public IEnumerable<BasePersonDTO> GuestsThatAccepted { get; set; }
        public IEnumerable<BasePersonDTO> GuestsThatDeclined { get; set; }
        public string DateOfEvent { get; set; }
        public string DateOfDeadline { get; set; }
        public string Describtion { get; set; }
    }
}
