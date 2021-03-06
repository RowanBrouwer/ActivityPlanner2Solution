using ActivityPlanner2.Shared.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityPlanner2.Data.ServerModels
{
    public class Person : IdentityUser
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public  IEnumerable<PersonOrganizedActivity> OrganizedActivities { get; set; }
        public  IEnumerable<PersonInvites> Invites { get; set; }
        public string FullName()
            => string.IsNullOrEmpty(MiddleName) ? 
            $"{FirstName} {LastName}" 
            : $"{FirstName} {MiddleName} {LastName}";

        public IEnumerable<PersonInvites> PlannedActivities() 
            => Invites != null ? 
            Invites.Any() != false ? 
            Invites.Where(i => i.Accepted == true).ToList()
            : null : null;


        public static explicit operator BasePersonDTO(Person person)
        {
            return new()
            {
                Id = person.Id,
                UserName = person.UserName,
                FirstName = person.FirstName,
                MiddleName = person.MiddleName,
                LastName = person.LastName,
            };
        }

        public static explicit operator Person(BasePersonDTO person)
        {
            return new()
            {
                Id = person.Id,
                UserName = person.UserName,
                FirstName = person.FirstName,
                MiddleName = person.MiddleName,
                LastName = person.LastName,
            };
        }
    }
}
