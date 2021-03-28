using System;
using System.Collections.Generic;
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
            pageIndex = 0;
            Clients = (await ClientDataService.GetAllClients(SearchTerm)).ToList();
            ClientList = Clients.Skip(pageSize * pageIndex).Take(pageSize).ToList();


            await _timer.DisposeAsync();
            StateHasChanged();
        }

    }
}

