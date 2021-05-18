using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ActivityPlanner2.Data.ServerModels;
using ActivityPlanner2.Shared.DTOs;
using ActivityPlanner2.Client.ClientModels;
using System.Threading.Tasks;
using Xunit;
using Humanizer;

namespace ActivityPlanner2.Tests
{
    public class ActivityConversionTests
    {
        readonly Person person1;
        readonly Person person2;
        readonly Person person3;
        readonly Activity activity1;
        IEnumerable<PersonInvites> invitedGuests;

        public ActivityConversionTests()
        {
            person1 = new Person() { FirstName = "Rowan", MiddleName = "de", LastName = "Brouwer", Id = "one" };
            person2 = new Person() { FirstName = "Test", LastName = "Person", Id = "two" };
            person3 = new Person() { FirstName = "Rowan", LastName = "Brouwer", Id = "three" };
            
            activity1 = new Activity() { ActivityName = "testing", Id = 1,  };

            invitedGuests = new List<PersonInvites>()
            {
                new PersonInvites()
                {
                PersonId = person1.Id,
                Person = person1,
                ActivityId = activity1.Id,
                Activity = activity1,
                Accepted = false
                },
                new PersonInvites()
                {
                PersonId = person2.Id,
                Person = person2,
                ActivityId = activity1.Id,
                Activity = activity1,
                Accepted = true
                },
                new PersonInvites()
                {
                PersonId = person3.Id,
                Person = person3,
                ActivityId = activity1.Id,
                Activity = activity1,
                Accepted = false
                }
            };
            activity1.InvitedGuests = invitedGuests;
        }

        [Fact]
        public void ActivityToDTO()
        {
            var activity = new Activity();

            var dto = (ActivityDTO)activity;

            Assert.IsType<ActivityDTO>(dto);
        }

        [Fact]
        public void DTOTOClientActivity()
        {
            var dto = new ActivityDTO();

            var clientActivity = (ClientActivity)dto;

            Assert.IsType<ClientActivity>(clientActivity);
        }

        [Fact]
        public void ClientActivityToDTO()
        {
            var clientActivity = new ClientActivity();

            var dto = (ActivityDTO)clientActivity;

            Assert.IsType<ActivityDTO>(dto);
        }

        [Fact]
        public void DTOToActivity()
        {
            var dto = new ActivityDTO();

            var activity = (Activity)dto;

            Assert.IsType<Activity>(activity);
        }

        [Fact]
        public void ActivityWithDataToDTO()
        {
            var dto = (ActivityDTO)activity1;

            Assert.IsType<ActivityDTO>(dto);
        }

        [Fact]
        public void DTOWithDataToClientActivity()
        {
            var dto = (ActivityDTO)activity1;

            var clientActivity = (ClientActivity)dto;

            Assert.IsType<ClientActivity>(clientActivity);
        }

        [Fact]
        public void ClientActivityWithDataToDTO()
        {
            var dto = (ActivityDTO)activity1;

            var clientActivity = (ClientActivity)dto;

            var dtoback = (ActivityDTO)clientActivity;

            Assert.IsType<ActivityDTO>(dtoback);
        }

        [Fact]
        public void DTOWithDatatoActivity()
        {
            var dto = (ActivityDTO)activity1;

            var activity = (Activity)dto;

            Assert.IsType<Activity>(activity);
        }
    }
}
