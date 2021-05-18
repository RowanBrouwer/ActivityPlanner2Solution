using ActivityPlanner2.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ActivityPlanner2.Client.ClientModels
{
    public class ClientPersonInvites
    {
        public string PersonId { get; set; }
        public int ActivityId { get; set; }
        public bool Accepted { get; set; }

        public static explicit operator ClientPersonInvites(PersonInvitesDTO dto)
        {
            return new()
            {
                Accepted = dto.Accepted,
                ActivityId = dto.ActivityId,
                PersonId = dto.PersonId
            };
        }

        public static explicit operator PersonInvitesDTO(ClientPersonInvites invite)
        {
            return new()
            {
                Accepted = invite.Accepted,
                ActivityId = invite.ActivityId,
                PersonId = invite.PersonId
            };
        }
    }
}
