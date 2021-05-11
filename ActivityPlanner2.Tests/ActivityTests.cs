using ActivityPlanner2.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ActivityPlanner2.Tests
{
    public class ActivityTests
    {
        readonly Person person1;
        readonly Person person2;
        readonly Person person3;
        readonly Activity activity1;
        readonly List<PersonInvites> invitedGuests;

        public ActivityTests()
        {
            person1 = new Person() { FirstName = "Rowan", MiddleName = "de", LastName = "Brouwer", Id = "one" };
            person2 = new Person() { FirstName = "Test", LastName = "Person", Id = "two" };
            person3 = new Person() { FirstName = "Rowan", LastName = "Brouwer", Id = "three" };
            activity1 = new Activity() { ActivityName = "testing", Id = 1 };

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
        public void GuestsThatAcceptedTest()
        {
            var result = activity1.GuestsThatAccepted();

            var resultCount = result.Count();

            List<string> IdsToCheck = new List<string>() { person1.Id, person3.Id};

            Assert.Contains(result, item => item.Id == person2.Id);
            Assert.DoesNotContain(result, item => item.Id == person1.Id);
            Assert.DoesNotContain(result, item => item.Id == person3.Id);
        }

        [Fact]
        public void GuestsThatDeclinedTest()
        {
            var result = activity1.GuestsThatDeclined();

            var resultCount = result.Count();

            Assert.Contains(result, item => item.Id == person1.Id);
            Assert.Contains(result, item => item.Id == person3.Id);
            Assert.DoesNotContain(result, item => item.Id == person2.Id);
        }
    }
}
