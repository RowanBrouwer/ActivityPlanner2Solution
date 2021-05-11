using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityPlanner2.Shared
{
    public class Activity
    {
        public int Id { get; set; }
        public string ActivityName { get; set; }
        public IEnumerable<PersonOrginizedActivity> Organizers { get; set; }
        public IEnumerable<PersonInvites> InvitedGuests { get; set; }

        public ICollection<Person> GuestsThatAccepted()
            => InvitedGuests != null ?
            InvitedGuests.Any() ?
            InvitedGuests.Where(i => i.Accepted == true)
            .Select(p => p.Person).ToList()
            : null : null;

        public ICollection<Person> GuestsThatDeclined() => InvitedGuests != null ?
            InvitedGuests.Any() ?
            InvitedGuests.Where(i => i.Accepted == false)
            .Select(p => p.Person).ToList()
            : null : null;

        public DateTime DateOfEvent { get; set; }
        public DateTime DateOfDeadline { get; set; }
        public string Describtion { get; set; }
    }
}
