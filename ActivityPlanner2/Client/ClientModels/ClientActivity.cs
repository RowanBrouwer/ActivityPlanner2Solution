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
        public IEnumerable<ClientPersonOrganizedActivity> Organizers { get; set; }
        public IEnumerable<ClientPersonInvites> InvitedGuests { get; set; }

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
                InvitedGuests = activity.InvitedGuests == null ? null : activity.InvitedGuests.Cast<ClientPersonInvites>(),
                Organizers = activity.Organizers == null ? null : activity.Organizers.Cast<ClientPersonOrganizedActivity>(),
                GuestsThatAccepted = activity.GuestsThatAccepted == null ? null : activity.GuestsThatAccepted.Cast<ClientBasePerson>(),
                GuestsThatDeclined = activity.GuestsThatDeclined == null ? null : activity.GuestsThatDeclined.Cast<ClientBasePerson>(),
                ActivityName = activity.ActivityName,
                DateOfDeadline = activity.DateOfDeadline.StringToNullableDateTime(),
                DateOfEvent = activity.DateOfEvent.StringToNullableDateTime(),
                Describtion = activity.Describtion
            };
        }

        public static explicit operator ActivityDTO(ClientActivity activity)
        {
            return new()
            {
                Id = activity.Id,
                InvitedGuests = activity.InvitedGuests == null ? null : activity.InvitedGuests.Cast<PersonInvitesDTO>(),
                Organizers = activity.Organizers == null ? null : activity.Organizers.Cast<PersonOrganizedActivityDTO>(),
                GuestsThatAccepted = activity.GuestsThatAccepted == null ? null : activity.GuestsThatAccepted.Cast<BasePersonDTO>(),
                GuestsThatDeclined = activity.GuestsThatDeclined == null ? null : activity.GuestsThatDeclined.Cast<BasePersonDTO>(),
                ActivityName = activity.ActivityName,
                DateOfDeadline = activity.DateOfDeadline.ToString(),
                DateOfEvent = activity.DateOfEvent.ToString(),
                Describtion = activity.Describtion
            };
        }
    }
}
