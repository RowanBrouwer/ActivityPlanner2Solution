using ActivityPlanner2.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityPlanner2.Data.ServerModels
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

        public PersonInvites()
        {
        }

        public static explicit operator PersonInvites(PersonInvitesDTO dto)
        {
            return new()
            {
                Accepted = dto.Accepted,
                ActivityId = dto.ActivityId == 0 ? 0 : dto.ActivityId,
                PersonId = dto.PersonId ?? null
            };
        }

        public static explicit operator PersonInvitesDTO(PersonInvites invite)
        {
            return new()
            {
                Accepted = invite.Accepted,
                ActivityId = invite.ActivityId == 0 ? 0 : invite.ActivityId,
                PersonId = invite.PersonId
            };
        }

    }
}
