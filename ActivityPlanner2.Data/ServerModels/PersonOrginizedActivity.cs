﻿using ActivityPlanner2.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityPlanner2.Data.ServerModels
{
    public class PersonOrginizedActivity
    {
        public string OrganizerId { get; set; }
        public Person Organizer { get; set; }
        public int OrganizedActivityId { get; set; }
        public Activity OrganizedActivity { get; set; }

        public PersonOrginizedActivity()
        {

        }

        public PersonOrginizedActivity(Person person, Activity activity)
        {
            OrganizerId = person.Id;
            Organizer = person;
            OrganizedActivityId = activity.Id;
            OrganizedActivity = activity;
        }

        public static explicit operator PersonOrginizedActivity(PersonOrganizedActivityDTO dto)
        {
            return new()
            {
                OrganizedActivityId = dto.ActivityId,
                OrganizerId = dto.PersonId
            };
        }

        public static explicit operator PersonOrganizedActivityDTO(PersonOrginizedActivity invite)
        {
            return new()
            {
                ActivityId = invite.OrganizedActivityId,
                PersonId = invite.OrganizerId
            };
        }
    }
}
