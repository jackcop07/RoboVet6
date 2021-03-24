using System.Threading.Tasks;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RoboVet6.Blazor.UI.Interfaces.Services;
using RoboVet6.Blazor.UI.Services;


namespace RoboVet6.Blazor.UI.Pages
{
    public partial class ClientDetail
    {
        [Parameter]
        public int ClientId { get; set; }

        [Parameter] public EventCallback<string> OnClick { get; set; }

        [Inject]
        public IClientDataService ClientDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public SelectedClientAnimalService SelectedClientAnimalService { get; set; }

        [Inject]
        protected IMatToaster Toaster { get; set; }

        public Models.Client Client { get; set; } = new Models.Client();

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

        protected void NavigateToClientSearch()
        {
            NavigationManager.NavigateTo("/client");
        }

        private void SelectCurrentClient(Models.Client client)
        {
            SelectedClientAnimalService.SelectedClient = client;

            Toaster.Add($"{client.Title} {client.FirstName} {client.LastName} selected.", MatToastType.Primary);

            NavigationManager.NavigateTo("/");
        }
    }
}
