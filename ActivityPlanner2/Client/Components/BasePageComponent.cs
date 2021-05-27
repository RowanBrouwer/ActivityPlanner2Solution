using ActivityPlanner2.Client.ClientServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;

namespace ActivityPlanner2.Client.Components
{
    public class BasePageComponent : ComponentBase
    {
        [Inject]
        protected IHttpService Http { get; set; }
        [Inject]
        protected NavigationManager NavManager { get; set; }
        [Inject]
        protected AuthenticationStateProvider AuthState { get; set; }
    }
}
