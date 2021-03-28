using System.Collections.Generic;
using System.Linq;
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
        public ISelectedClientAnimalService SelectedClientAnimalService { get; set; }

        [Inject]
        protected IMatToaster Toaster { get; set; }

        public Models.Client Client { get; set; } = new Models.Client();
        public IEnumerable<Models.Animal> Animals { get; set; } = new List<Models.Animal>();

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
                Animals = Client.Animals;
            }
        }

        protected void NavigateToClientSearch()
        {
            NavigationManager.NavigateTo("/client");
        }

        private void SelectCurrentClient(Models.Client client)
        {
            SelectedClientAnimalService.UpdateSelectedClient(client);

            SelectedClientAnimalService.UpdateNavBar(true);

            Toaster.Add($"{client.Title} {client.FirstName} {client.LastName} selected.", MatToastType.Primary);

            NavigationManager.NavigateTo("/");
        }

        private void SelectCurrentAnimal(Models.Client client, int animalId)
        {
            SelectedClientAnimalService.UpdateSelectedClient(client);

            var animal = client.Animals.FirstOrDefault(x => x.Id == animalId);

            SelectedClientAnimalService.UpdateSelectedAnimal(animal);

            SelectedClientAnimalService.UpdateNavBar(true);

            Toaster.Add($"{client.Title} {client.FirstName} {client.LastName} with {animal.Name} selected.", MatToastType.Primary);

            NavigationManager.NavigateTo("/");
        }
    }
}
