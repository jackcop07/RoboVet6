using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RoboVet6.Blazor.UI.Interfaces.Services;
using RoboVet6.Blazor.UI.Models;
using RoboVet6.Blazor.UI.Services;

namespace RoboVet6.Blazor.UI.Pages.Animal
{
    public partial class AnimalEdit
    {
        [Parameter] 
        public int ClientId { get; set; }

        [Parameter]
        public int AnimalId { get; set; }

        public Models.Animal Animal { get; set; } = new Models.Animal();
        public IEnumerable<Species> SpeciesList { get; set; } = new List<Species>();
        public IEnumerable<Breed> Breeds { get; set; } = new List<Breed>();
        public IEnumerable<Breed> BreedList { get; set; } = new List<Breed>();

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        [Inject]
        public IAnimalDataService AnimalDataService { get; set; }

        [Inject]
        public ISpeciesDataService SpeciesDataService { get; set; }

        [Inject]
        public IBreedDataService BreedDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        protected IMatToaster Toaster { get; set; }

        private bool _authenticated;

        private int _speciesId;
        public int SpeciesId
        {
            get => _speciesId;
            set
            {
                _speciesId = value;
                GetBreeds();
            }
        }

        public int BreedId { get; set; }

        public string Debugs { get; set; } = string.Empty;


        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateTask;
            var user = authState.User;

            _authenticated = user.Identity?.IsAuthenticated ?? false;

            if (!_authenticated)
            {
                return;
            }

            SpeciesList = await SpeciesDataService.GetAllSpecies();

            if (AnimalId != 0)
            {
                Animal = await AnimalDataService.GetAnimalById(AnimalId);
                SpeciesId = Animal.SpeciesId;
                BreedId = Animal.BreedId;
                Breeds = await BreedDataService.GetAllBreeds();
                BreedList = Breeds.Where(x => x.SpeciesId == SpeciesId);

            }

        }

        protected async Task HandleValidSubmit()
        {
            Animal.SpeciesId = SpeciesId;

            if (Animal.Id == 0) //new
            {
                Animal.ClientId = ClientId;
                var addedAnimal = await AnimalDataService.AddAnimal(Animal);

                if (addedAnimal != null)
                {
                    Toaster.Add("Animal created successfully.", MatToastType.Success);
                    NavigationManager.NavigateTo($"/animaldetail/{addedAnimal.Id}");
                }
                else
                {
                    Toaster.Add("Animal not created. Please try again.", MatToastType.Danger);
                }
            }
            else
            {
                await AnimalDataService.UpdateAnimal(Animal);

                Toaster.Add("Animal updated successfully", MatToastType.Success);
                NavigationManager.NavigateTo($"/animaldetail/{Animal.Id}");
            }
        }

        protected void HandleInvalidSubmit()
        {
            Toaster.Add("Please correct validation errors.", MatToastType.Danger);
        }

        protected void NavigateToClientSearch()
        {
            NavigationManager.NavigateTo("/client");
        }

        protected void GetBreeds()
        {
            BreedList = Breeds.Where(x => x.SpeciesId == SpeciesId);
            if (Breeds.Any())
            {
                Animal.BreedId = 0;
            }
            
            Debugs = DateTime.Now.ToLongTimeString() + BreedId;
        }
    }
}
