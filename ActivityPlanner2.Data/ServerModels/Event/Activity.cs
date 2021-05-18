using ActivityPlanner2.Shared.DTOs;
using ActivityPlanner2.Shared.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityPlanner2.Data.ServerModels
{
    public class Activity
    {
        public int Id { get; set; }
        public string ActivityName { get; set; }
        public IEnumerable<PersonOrginizedActivity> Organizers { get; set; }
        public IEnumerable<PersonInvites> InvitedGuests { get; set; }

        public IEnumerable<Person> GuestsThatAccepted()
            => InvitedGuests != null ?
            InvitedGuests.Any() ?
            InvitedGuests.Where(i => i.Accepted == true)
            .Select(p => p.Person).ToList()
            : null : null;

        public IEnumerable<Person> GuestsThatDeclined() => InvitedGuests != null ?
            InvitedGuests.Any() ?
            InvitedGuests.Where(i => i.Accepted == false)
            .Select(p => p.Person).ToList()
            : null : null;

        public DateTime? DateOfEvent { get; set; }
        public DateTime? DateOfDeadline { get; set; }
        public string Describtion { get; set; }

        public static explicit operator ActivityDTO(Activity activity)
        {

            return new()
            {
                Id = activity.Id,
                InvitedGuests = activity.InvitedGuests?.Cast<PersonInvitesDTO>(),
                Organizers = activity.Organizers?.Cast<PersonOrganizedActivityDTO>(),
                GuestsThatAccepted = activity.GuestsThatAccepted()?.Cast<BasePersonDTO>(),
                GuestsThatDeclined = activity.GuestsThatDeclined()?.Cast<BasePersonDTO>(),
                ActivityName = activity.ActivityName,
                DateOfDeadline = activity.DateOfDeadline.ToString(),
                DateOfEvent = activity.DateOfEvent.ToString(),
                Describtion = activity.Describtion
            };
        }

        public static explicit operator Activity(ActivityDTO activity)
        {
            return new()
            {
                Id = activity.Id,
                InvitedGuests = activity.InvitedGuests?.Cast<PersonInvites>(),
                Organizers = activity.Organizers?.Cast<PersonOrginizedActivity>(),
                ActivityName = activity.ActivityName,
                DateOfDeadline = activity.DateOfDeadline.StringToNullableDateTime(),
                DateOfEvent = activity.DateOfEvent.StringToNullableDateTime(),
                Describtion = activity.Describtion
            };
        }
    }
}
