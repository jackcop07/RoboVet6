using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RoboVet6.Blazor.UI.Interfaces.Services;
using RoboVet6.Blazor.UI.Services;

namespace RoboVet6.Blazor.UI.Pages.Client
{
    public partial class ClientSearchV2
    {
        public IEnumerable<Models.Client> Clients { get; set; } = new List<Models.Client>();

        public List<Models.Client> ClientList { get; set; } = new List<Models.Client>();

        [Inject]
        public IClientDataService ClientDataService { get; set; }

        [Inject]
        public ISelectedClientAnimalService SelectedClientAnimalService { get; set; }

        [Inject]
        protected IMatToaster Toaster { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        bool authenticated;

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        public Models.Animal SelectedAnimal { get; set; } = new Models.Animal();

        [Parameter]
        public EventCallback<bool> ShowAnimalMenu { get; set; }

        public bool NoResults { get; set; }


        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateTask;
            var user = authState.User;

            authenticated = user.Identity?.IsAuthenticated ?? false;

        }

        private void SelectCurrentClient(Models.Client client)
        {
            SelectedClientAnimalService.UpdateSelectedClient(client);

            Toaster.Add($"{client.Title} {client.FirstName} {client.LastName} selected.", MatToastType.Primary);

            SelectedClientAnimalService.UpdateNavBar(true);

            NavigationManager.NavigateTo("/");
        }

        private void SelectCurrentAnimal(Models.Client client, int animalId)
        {
            SelectedClientAnimalService.UpdateSelectedClient(client);

            var animal = client.Animals.FirstOrDefault(x => x.Id == animalId);

            SelectedClientAnimalService.UpdateSelectedAnimal(animal);

            SelectedClientAnimalService.UpdateNavBar(true);

            Toaster.Add($"{client.Title} {client.FirstName} {client.LastName} with {animal.Name} selected.", MatToastType.Primary);
            StateHasChanged();
            NavigationManager.NavigateTo("/");
        }

        int pageSize = 5;
        int pageIndex = 0;

        void OnPage(MatPaginatorPageEvent e)
        {
            pageSize = e.PageSize;
            pageIndex = e.PageIndex;

            ClientList = Clients.Skip(pageSize * pageIndex).Take(pageSize).ToList();
        }

        private Timer _timer;
        public string SearchTerm { get; set; }


        [Parameter]
        public EventCallback<string> OnSearchChanged { get; set; }

        async Task SearchChanged()
        {
            if (_timer != null)
                await _timer.DisposeAsync();

            _timer = new Timer(OnTimerElapsed, null, 500, 0);

            
        }

        private async void OnTimerElapsed(object sender)
        {
            NoResults = false;
            pageIndex = 0;
            Clients = (await ClientDataService.GetAllClients(SearchTerm)).ToList();

            if (!Clients.Any() || Clients == null)
            {
                NoResults = true;
            }
            ClientList = Clients.Skip(pageSize * pageIndex).Take(pageSize).ToList();


            await _timer.DisposeAsync();
            StateHasChanged();
        }

    }
}

