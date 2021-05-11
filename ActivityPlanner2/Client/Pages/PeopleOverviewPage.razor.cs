using ActivityPlanner2.Client.Components;
using ActivityPlanner2.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivityPlanner2.Client.Pages
{
    public class PeopleOverviewPageModel : BasePageComponent
    {
        public List<Person> LoadedUsers { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await Task.FromResult(loadData());
        }

        private Task loadData()
        {
            List<Person> newPeople = new List<Person>()
            {
                new Person() { Id = "123", Email = "TestEmail", FirstName = "Rowan", LastName = "Brouwer"}
            };

            return Task.FromResult(LoadedUsers = newPeople);
        }
    }
}
