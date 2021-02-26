using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RoboVet6.Blazor.UI.Interfaces.Services;
using RoboVet6.Blazor.UI.Services;

namespace RoboVet6.Blazor.UI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddOidcAuthentication(options =>
            {
                builder.Configuration.Bind("oidc", options.ProviderOptions);
            });

            //DI for services created by me
            //builder.Services.AddHttpClient<IClientDataService, ClientDataService>(client => client.BaseAddress = new Uri("https://localhost:44387/"));
            builder.Services.AddScoped<IClientDataService, ClientDataService>();
            // We register a named HttpClient here for the API
            builder.Services.AddHttpClient("api")
                .AddHttpMessageHandler(sp =>
                {
                    var handler = sp.GetService<AuthorizationMessageHandler>()
                        .ConfigureHandler(
                            authorizedUrls: new[] { "https://localhost:44387" },
                            scopes: new[] { "test" });
                    return handler;
                });

            builder.Services.AddScoped(
                sp => sp.GetService<IHttpClientFactory>().CreateClient("api"));

            await builder.Build().RunAsync();
        }
    }
}
