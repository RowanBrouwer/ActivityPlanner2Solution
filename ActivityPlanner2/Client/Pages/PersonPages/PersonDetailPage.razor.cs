using ActivityPlanner2.Client.ClientModels;
using ActivityPlanner2.Client.Components;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivityPlanner2.Client.Pages
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
                PersonDetails = await Http.GetPersonById(Id);
            }
            else
            {
                PersonDetails = await Http.GetPersonById(CurrentUser.Id);
            }            
        }

        protected void RedirectToEdit(string Id)
        {
            NavManager.NavigateTo($"/Person/Edit/{Id}");
        }
    }
}
