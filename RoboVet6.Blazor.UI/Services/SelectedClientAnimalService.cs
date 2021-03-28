using System;
using Microsoft.AspNetCore.Components;
using RoboVet6.Blazor.UI.Interfaces.Services;
using RoboVet6.Blazor.UI.Models;
using RoboVet6.Blazor.UI.State;

namespace RoboVet6.Blazor.UI.Services
{
    public class SelectedClientAnimalService : ISelectedClientAnimalService
    {
        private readonly AppState _state;

        private Client _selectedClient;

        private Animal _selectedAnimal;


        public SelectedClientAnimalService(AppState state)
        {
            _state = state;
            _selectedClient = new Client();
            _selectedAnimal = new Animal();

        }



        public void UpdateNavBar(bool animalSelected)
        {
            _state.ShowAnimalMenu(animalSelected);
        }

        public void UpdateSelectedClient(Client client)
        {
            _state.SetSelectedClient(client);
        }

        public void UpdateSelectedAnimal(Animal animal)
        {
            _state.SetSelectedAnimal(animal);
        }


        public event Action OnChange;

        public void NotifyDataChanged() =>  OnChange?.Invoke();

    }
}
