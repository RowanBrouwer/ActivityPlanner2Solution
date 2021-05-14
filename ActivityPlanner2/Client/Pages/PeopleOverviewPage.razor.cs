using ActivityPlanner2.Client.ClientServices;
using ActivityPlanner2.Client.Components;
using ActivityPlanner2.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivityPlanner2.Client.Pages
{
    public class PeopleOverviewPageModel : BasePageComponent
    {
        public IEnumerable<Person> LoadedUsers { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await Task.FromResult(loadData());
        }

        private async Task<IEnumerable<Person>> loadData()
        {
            return await Http.GetListOfPeople();
        }
    }
}
