using ActivityPlanner2.Shared.DTOs;
using ActivityPlanner2.Shared.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivityPlanner2.Client.ClientModels
{
    public class ClientActivity
    {
        public int Id { get; set; }
        public string ActivityName { get; set; }
        public IEnumerable<ClientBasePerson> Organizers { get; set; }
        public IEnumerable<ClientBasePerson> InvitedGuests { get; set; }

        public IEnumerable<ClientBasePerson> GuestsThatAccepted { get; set; }

        public IEnumerable<ClientBasePerson> GuestsThatDeclined { get; set; }

        public DateTime? DateOfEvent { get; set; }
        public DateTime? DateOfDeadline { get; set; }
        public string Describtion { get; set; }

        public static explicit operator ClientActivity(ActivityDTO activity)
        {
            return new()
            {
                Id = activity.Id,
                InvitedGuests = (IEnumerable<ClientBasePerson>)activity.InvitedGuests,
                Organizers = (IEnumerable<ClientBasePerson>)activity.Organizers,
                GuestsThatAccepted = (IEnumerable<ClientBasePerson>)activity.GuestsThatAccepted,
                GuestsThatDeclined = (IEnumerable<ClientBasePerson>)activity.GuestsThatDeclined,
                ActivityName = activity.ActivityName,
                DateOfDeadline = activity.DateOfDeadline.StringToNullableDateTime(),
                DateOfEvent = activity.DateOfEvent.StringToNullableDateTime(),
                Describtion = activity.Describtion
            };
        }
    }
}
