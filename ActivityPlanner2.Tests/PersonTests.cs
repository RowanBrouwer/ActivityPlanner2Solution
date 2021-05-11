using ActivityPlanner2.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ActivityPlanner2.Tests
{
    public class PersonTests
    {
        Person PersonWithMiddleName = new Person() { FirstName = "Rowan", MiddleName = "de", LastName = "Brouwer", Id = "one"};
        Person PersonWithoutMiddleName = new Person() { FirstName = "Rowan", LastName = "Brouwer", Id = "two"};

        readonly Activity activity1 = new Activity() { ActivityName = "testing", Id = 1 };
        readonly List<PersonInvites> personInvites = new()
        {
            new PersonInvites() { ActivityId = 1, Accepted = true, PersonId = "one" },
            new PersonInvites() { ActivityId = 1, Accepted = false, PersonId = "one" },
            new PersonInvites() { ActivityId = 1, Accepted = true, PersonId = "one" },
            new PersonInvites() { ActivityId = 1, Accepted = false, PersonId = "one" },
            new PersonInvites() { ActivityId = 1, Accepted = true, PersonId = "one" },
            new PersonInvites() { ActivityId = 1, Accepted = false, PersonId = "one" },
            new PersonInvites() { ActivityId = 1, Accepted = true, PersonId = "one" }
        };

        [Fact]
        public void PersonWithMiddleNameFullNameTest()
        {
            Assert.Equal("Rowan de Brouwer", PersonWithMiddleName.FullName());
        }

        [Fact]
        public void PersonWithoutMiddleNameFullNameTest()
        {
            Assert.Equal("Rowan Brouwer", PersonWithoutMiddleName.FullName());
        }

        [Fact]
        public void PersonPlannedActivities()
        {
            PersonWithMiddleName.Invites = personInvites;

            var planned = PersonWithMiddleName.PlannedActivities();

            foreach (var plan in planned)
            {
                Assert.True(plan.Accepted);
            }
        }
    }
}
