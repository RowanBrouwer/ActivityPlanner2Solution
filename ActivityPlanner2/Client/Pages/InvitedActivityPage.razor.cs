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
            var state = await AuthState.GetAuthenticationStateAsync();
            CurrentUser = await Http.GeCurrentPersonByUserName(state.User.Identity.Name);

            await loadData(CurrentUser.Id);
        }
            
        private async Task loadData(string userId)
        {    
           InvitedActivities = await Http.GetlistOfInvitedActivitiesByPerson(userId);
            StateHasChanged();
        }

    }
}
