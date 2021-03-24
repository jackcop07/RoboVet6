using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RoboVet6.Blazor.UI.Interfaces.Services;
using RoboVet6.Blazor.UI.Models;
using RoboVet6.Blazor.UI.Services;

namespace RoboVet6.Blazor.UI.Pages
{
    public partial class ClientSearch
    {
        public IEnumerable<Models.Client> Clients { get; set; } = new List<Models.Client>();

        public List<Models.Client> ClientList { get; set; } = new List<Models.Client>();

        [Inject]
        public IClientDataService ClientDataService { get; set; }

        [Inject] 
        public SelectedClientAnimalService SelectedClientAnimalService { get; set; }

        [Inject]
        protected IMatToaster Toaster { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        bool authenticated;

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        public Models.Animal SelectedAnimal { get; set; } = new Models.Animal();


        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateTask;
            var user = authState.User;

            authenticated = user.Identity?.IsAuthenticated ?? false;

            if (authenticated)
            {
                Clients = (await ClientDataService.GetAllClients()).ToList();
                ClientList = Clients.ToList();
            }

        }

        private void SelectCurrentClient(Models.Client client)
        {
            SelectedClientAnimalService.SelectedClient = client;

            Toaster.Add($"{client.Title} {client.FirstName} {client.LastName} selected.", MatToastType.Primary);

            NavigationManager.NavigateTo("/");
        }

        private void SelectCurrentAnimal(Models.Client client, int animalId)
        {
            SelectedClientAnimalService.SelectedClient = client;

            var animal = client.Animals.FirstOrDefault(x => x.Id == animalId);

            SelectedClientAnimalService.SelectedAnimal = animal;

            Toaster.Add($"{client.Title} {client.FirstName} {client.LastName} with {animal.Name} selected.", MatToastType.Primary);

            NavigationManager.NavigateTo("/");
        }

    }
}
