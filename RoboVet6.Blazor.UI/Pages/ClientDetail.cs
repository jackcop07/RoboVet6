using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RoboVet6.Blazor.UI.Interfaces.Services;
using RoboVet6.Blazor.UI.Models;

namespace RoboVet6.Blazor.UI.Pages
{
    public partial class ClientDetail
    {
        [Parameter]
        public int ClientId { get; set; }

        [Inject]
        public IClientDataService ClientDataService { get; set; }

        public Client Client { get; set; } = new Client();

        bool authenticated;

        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authState = await authenticationStateTask;
            var user = authState.User;

            authenticated = user.Identity?.IsAuthenticated ?? false;

            if (authenticated)
            {
                Client = await ClientDataService.GetClientById(ClientId);
            }
        }
    }
}
