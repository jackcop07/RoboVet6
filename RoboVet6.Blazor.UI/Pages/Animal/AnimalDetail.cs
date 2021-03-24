using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RoboVet6.Blazor.UI.Interfaces.Services;
using RoboVet6.Blazor.UI.Models;
using RoboVet6.Blazor.UI.Services;

namespace RoboVet6.Blazor.UI.Pages.Animal
{
    public partial class AnimalDetail
    {
        [Parameter]
        public int AnimalId { get; set; }

        public Models.Animal Animal { get; set; }

        public Species Species { get; set; }

        public Models.Client Client { get; set; }

        public Breed Breed { get; set; }

        [Inject]
        public IClientDataService ClientDataService { get; set; }

        [Inject] 
        public IAnimalDataService AnimalDataService { get; set; }

        [Inject]
        public ISpeciesDataService SpeciesDataService { get; set; }

        [Inject]
        public IBreedDataService BreedDataService { get; set; }

        [Inject]
        public SelectedClientAnimalService SelectedClientAnimalService { get; set; }

        [Inject]
        protected IMatToaster Toaster { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        private bool _authenticated;

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateTask;
            var user = authState.User;

            _authenticated = user.Identity?.IsAuthenticated ?? false;

            if (_authenticated)
            {
                Animal = await AnimalDataService.GetAnimalById(AnimalId);
                Species = await SpeciesDataService.GetSpeciesById(Animal.SpeciesId);
                Client = await ClientDataService.GetClientById(Animal.ClientId);
                Breed = await BreedDataService.GetBreedById(Animal.BreedId);
            }
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
