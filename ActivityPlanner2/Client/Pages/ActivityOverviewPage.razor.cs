using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivityPlanner2.Client.Components;
using ActivityPlanner2.Client.ClientModels;
using Microsoft.AspNetCore.Authorization;

namespace ActivityPlanner2.Client.Pages
{
    [Authorize]
    public class ActivityOverviewPageModel : BasePageComponent
    {
        protected IEnumerable<ClientActivity> clientActivities = new List<ClientActivity>();
        protected override async Task OnInitializedAsync()
        {
            await loadData();
        }

        private async Task loadData()
        {
            clientActivities = await Http.GetlistOfActivities();
            StateHasChanged();
        }
    }
}
