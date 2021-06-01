using ActivityPlanner2.Client.ClientServices;
using ActivityPlanner2.Client.ClientModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using Microsoft.AspNetCore.Http;

namespace ActivityPlanner2.Client.Components
{
    public class BasePageComponent : ComponentBase
    {
        [Inject]
        protected IHttpService Http { get; set; }
        [Inject]
        protected NavigationManager NavManager { get; set; }
        [Inject]
        protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        protected ClientBasePerson CurrentUser { get; set; }
    }
}
