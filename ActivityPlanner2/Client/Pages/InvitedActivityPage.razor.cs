using ActivityPlanner2.Client.ClientModels;
using ActivityPlanner2.Client.Components;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivityPlanner2.Client.Pages
{
    [Authorize]
    public class InvitedActivityPageModel : BasePageComponent
    {

        public IEnumerable<ClientActivity> InvitedActivities { get; set; } = new List<ClientActivity>();
        protected override async Task OnInitializedAsync()
        {
            await Task.FromResult(loadData());
        }

        private async Task loadData()
        {
            var state = await AuthState.GetAuthenticationStateAsync();

           // InvitedActivities = await Http.GetlistOfInvitedActivitiesByPerson();
            StateHasChanged();
        }

    }
}
