using ActivityPlanner2.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivityPlanner2.Client.ClientModels
{
    public class ClientPersonOrganizedActivity
    {
        public string PersonId { get; set; }
        public int ActivityId { get; set; }

        public static explicit operator ClientPersonOrganizedActivity(PersonInvitesDTO dto)
        {
            return new()
            {
                ActivityId = dto.ActivityId,
                PersonId = dto.PersonId
            };
        }

        public static explicit operator ClientPersonOrganizedActivity(ClientPersonInvites invite)
        {
            return new()
            {
                ActivityId = invite.ActivityId,
                PersonId = invite.PersonId
            };
        }
    }
}
