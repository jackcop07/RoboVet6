using System;
using System.Net.Http;
using System.Threading.Tasks;
using MatBlazor;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RoboVet6.Blazor.UI.Auth;
using RoboVet6.Blazor.UI.Components;
using RoboVet6.Blazor.UI.Interfaces.Services;
using RoboVet6.Blazor.UI.Models;
using RoboVet6.Blazor.UI.Services;
using RoboVet6.Blazor.UI.State;

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
                options.UserOptions.RoleClaim = "role";
            })
                .AddAccountClaimsPrincipalFactory<ArrayClaimsPrincipalFactory<RemoteUserAccount>>();

            //DI for services created by me
             builder.Services.AddScoped<IClientDataService, ClientDataService>();
             builder.Services.AddScoped<IAnimalDataService, AnimalDataService>();
             builder.Services.AddScoped<ISpeciesDataService, SpeciesDataService>();
             builder.Services.AddScoped<IBreedDataService, BreedDataService>();


            // We register a named HttpClient here for the API
            builder.Services.AddHttpClient("RV6Api",
                    client =>
                    {
                        client.BaseAddress = new Uri("https://localhost:44387/");
                    })
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

            builder.Services.AddSingleton<ISelectedClientAnimalService, SelectedClientAnimalService>();

            builder.Services.AddSingleton<AppState>();



            builder.Services.AddMatBlazor();

            builder.Services.AddMatToaster(config =>
            {
                config.Position = MatToastPosition.BottomRight;
                config.PreventDuplicates = true;
                config.NewestOnTop = true;
                config.ShowCloseButton = true;
                config.MaximumOpacity = 95;
                config.VisibleStateDuration = 3000;
            });

            await builder.Build().RunAsync();
        }
    }
}
