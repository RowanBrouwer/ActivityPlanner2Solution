using ActivityPlanner2.Client.ClientModels;
using ActivityPlanner2.Client.Components;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivityPlanner2.Client.Pages.PersonPages
{
    [Authorize]
    public class PersonDetailPageModel : BasePageComponent
    {
        [Parameter] public string Id { get; set; }
        protected ClientBasePerson PersonDetails { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await SetCurrentUser();

            if (!(string.IsNullOrEmpty(Id) && Id == CurrentUser.Id))
            {
                PersonDetails = await Http.GetCurrentPersonById(Id);
            }
            else
            {
                PersonDetails = CurrentUser;
            }            
        }
    }
}
