using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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

        private int _breedId;
        public int BreedId
        {
            get => _breedId;
            set
            {
                _breedId = value;
                //StateHasChanged();
            }
        }

        public string Debugs { get; set; } = string.Empty;
    

        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateTask;
            var user = authState.User;

            _authenticated = user.Identity?.IsAuthenticated ?? false;

            if (!_authenticated)
            {
                return;
            }

            Saved = false;

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
            //Animal.BreedId = BreedId;

            if (Animal.Id == 0) //new
            {
                Animal.ClientId = ClientId;
                var addedAnimal = await AnimalDataService.AddAnimal(Animal);

                if (addedAnimal != null)
                {
                    StatusClass = "alert-success";
                    Message = "New animal added successfully.";
                    Saved = true;
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new animal. Please try again.";
                    Saved = false;
                }
            }
            else
            {
                await AnimalDataService.UpdateAnimal(Animal);
                StatusClass = "alert-success";
                Message = "Animal updated successfully.";
                Saved = true;
            }
        }

        protected void HandleInvalidSubmit()
        {
            StatusClass = "alert-danger";
            Message = "There are some validation errors. Please try again.";
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
