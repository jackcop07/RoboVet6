using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RoboVet6.Blazor.UI.Interfaces.Services;
using RoboVet6.Blazor.UI.Models;

namespace RoboVet6.Blazor.UI.Pages
{
    public partial class ClientSearch
    {
        public IEnumerable<Client> Clients { get; set; } = new List<Client>();

        public List<Client> ClientList { get; set; } = new List<Client>();

        [Inject]
        public IClientDataService ClientDataService { get; set; }

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
                Clients = (await ClientDataService.GetAllClients()).ToList();
                ClientList = Clients.ToList();
            }

        }
    }
}
