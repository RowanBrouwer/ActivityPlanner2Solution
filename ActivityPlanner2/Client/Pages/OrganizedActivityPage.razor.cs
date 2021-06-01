using ActivityPlanner2.Client.ClientModels;
using ActivityPlanner2.Client.Components;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivityPlanner2.Client.Pages
{
    [Authorize]
    public class OrganizedActivityPageModel : BasePageComponent
    {
        public IEnumerable<ClientActivity> OrganizedActivities { get; set; } = new List<ClientActivity>();
        protected override async Task OnInitializedAsync()
        {
            await SetCurrentUser();
            await loadData(CurrentUser.Id);
        }

        private async Task loadData(string userId)
        {
            OrganizedActivities = await Http.GetlistOfOrganizedActivitiesByPerson(userId);
            StateHasChanged();
        }
    }
}
