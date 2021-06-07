using ActivityPlanner2.Client.ClientModels;
using ActivityPlanner2.Client.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace ActivityPlanner2.Client.Pages.ActivityPages
{
    public class ActivityDetailPageModel : BasePageComponent
    {
        [Parameter] public int Id { get; set; }
        protected ClientActivity activityDetails { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }


        private async Task LoadData()
        {
            activityDetails = await Http.GetActivityById(Id);
        }

        protected void RedirectToEdit(string Id)
        {
            NavManager.NavigateTo($"/Person/Edit/{Id}");
        }
    }
}
