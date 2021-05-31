using ActivityPlanner2.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivityPlanner2.Client.ClientModels
{
    public class ClientBasePerson
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public static explicit operator ClientBasePerson(BasePersonDTO person)
        {
            return new()
            {
                Id = person.Id,
                UserName = person.UserName,
                FirstName = person.FirstName,
                MiddleName = person.MiddleName,
                LastName = person.LastName
            };
        }

        public static explicit operator BasePersonDTO(ClientBasePerson person)
        {
            return new()
            {
                Id = person.Id,
                UserName = person.UserName,
                FirstName = person.FirstName,
                MiddleName = person.MiddleName,
                LastName = person.LastName
            };
        }
    }
}
