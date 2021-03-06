using ActivityPlanner2.Client.ClientServices;
using Blazor.Extensions.Logging;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ActivityPlanner2.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("ActivityPlanner2.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ActivityPlanner2.ServerAPI"));

            builder.Services.AddScoped<IHttpService, HttpService>();

            builder.Services.AddApiAuthorization();

            builder.Services.AddLogging(builder =>
            {
                builder.AddBrowserConsole().SetMinimumLevel(LogLevel.Information);
            });

            await builder.Build().RunAsync();
        }
    }
}
