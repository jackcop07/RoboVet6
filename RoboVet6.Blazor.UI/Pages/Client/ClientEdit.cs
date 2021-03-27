using System.Threading.Tasks;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RoboVet6.Blazor.UI.Interfaces.Services;

namespace RoboVet6.Blazor.UI.Pages.Client
{
    public partial class ClientEdit
    {
        [Parameter]
        public int ClientId { get; set; }

        [Inject]
        public IClientDataService ClientDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        protected IMatToaster Toaster { get; set; }

        public Models.Client Client { get; set; } = new Models.Client();

        private bool _authenticated;


        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateTask;
            var user = authState.User;

            _authenticated = user.Identity?.IsAuthenticated ?? false;

            if (!_authenticated)
            {
                return;
            }

            if (ClientId != 0)
            {
                Client = await ClientDataService.GetClientById(ClientId);
            }
        }

        protected async Task HandleValidSubmit()
        {

            if (Client.Id == 0) //new
            {
                var addedClient = await ClientDataService.AddClient(Client);

                if (addedClient != null)
                {
                    Toaster.Add("New client added successfully", MatToastType.Success);

                    NavigationManager.NavigateTo($"/clientdetail/{addedClient.Id}");
                }
                else
                {
                    Toaster.Add("Client not created. Please try again.", MatToastType.Danger);
                }
            }
            else
            {
                await ClientDataService.UpdateClient(Client);

                Toaster.Add("Client updated successfully", MatToastType.Success);

                NavigationManager.NavigateTo($"/clientdetail/{Client.Id}");
            }
        }

        protected void HandleInvalidSubmit()
        {
            Toaster.Add("There are some validation errors. Please try again.", MatToastType.Danger);
        }

        protected void NavigateToClientSearch()
        {
            NavigationManager.NavigateTo("/client");
        }
    }
}
