using ActivityPlanner2.Client.ClientServices;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ActivityPlanner2.Client.Components
{
    public class BasePageComponent : ComponentBase
    {
        [Inject]
        protected IHttpService Http { get; set; }
        [Inject]
        protected NavigationManager NavManager { get; set; }
    }
}
