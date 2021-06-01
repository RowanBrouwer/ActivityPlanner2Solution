using ActivityPlanner2.Client.ClientModels;
using ActivityPlanner2.Client.ClientServices;
using ActivityPlanner2.Client.Components;
using ActivityPlanner2.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivityPlanner2.Client.Pages
{
    [Authorize]
    public class PeopleOverviewPageModel : BasePageComponent
    {
        public IEnumerable<ClientBasePerson> LoadedUsers { get; set; } = new List<ClientBasePerson>();
        protected override async Task OnInitializedAsync()
        {
            await Task.FromResult(loadData());
        }

        private async Task loadData()
        {
            LoadedUsers = await Http.GetListOfPeople();
            StateHasChanged();
        }

        protected void RedirectToDetails(string Id)
        {
            NavManager.NavigateTo($"/Person/Detail/{Id}");
        }
    }
}
