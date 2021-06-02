using ActivityPlanner2.Client.ClientModels;
using ActivityPlanner2.Client.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivityPlanner2.Client.Pages
{
    public class PersonEditPageModel : BasePageComponent
    {
        [Parameter] public string Id { get; set; }
        protected ClientBasePerson person;
        protected override async Task OnInitializedAsync()
        {
            person = await Http.GetPersonById(Id);
        }

        protected async Task CallSaveChanges(ClientBasePerson person)
        {
            await Http.UpdatePerson(person);
            NavManager.NavigateTo($"/Person/Detail/{person.Id}");
        }
    }
}
