using ActivityPlanner2.Data.ServerModels;
using ActivityPlanner2.Shared.DTOs;
using ActivityPlanner2.Client.ClientModels;
using Xunit;

namespace ActivityPlanner2.Tests
{
    public class PersonConversionTests
    {
        [Fact]
        public void FromPersonToDTO()
        {
            var person = new Person();

            var dto = (BasePersonDTO)person;

            Assert.IsType<BasePersonDTO>(dto);
        }

        [Fact]
        public void FromDTOToClientPerson()
        {
            var dto = new BasePersonDTO();

            var clientPerson = (ClientBasePerson)dto;

            Assert.IsType<ClientBasePerson>(clientPerson);
        }

        [Fact]
        public void FromClientPersonToDTO()
        {
            var clientPerson = new ClientBasePerson();

            var dto = (BasePersonDTO)clientPerson;

            Assert.IsType<BasePersonDTO>(dto);
        }

        [Fact]
        public void FromDTOToPerson()
        {
            var dto = new BasePersonDTO();

            var person = (Person)dto;

            Assert.IsType<Person>(person);
        }
    }
}
