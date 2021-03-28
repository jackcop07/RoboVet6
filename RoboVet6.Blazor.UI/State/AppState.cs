using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoboVet6.Blazor.UI.Models;

namespace RoboVet6.Blazor.UI.State
{
    public class AppState
    {
        public event Action OnChange;

        private bool _showAnimalMenu;
        public bool ShowAnimalNav => _showAnimalMenu;

        private Client _client;
        public Client SelectedClient => _client;

        private Animal _animal;
        public Animal SelectedAnimal => _animal;


        public AppState()
        {
            _client = new Client();
            _animal = new Animal();
        }

        public void ShowAnimalMenu(bool show)
        {
            _showAnimalMenu = true;
            NotifyStateChanged();
        }

        public void SetSelectedClient(Client client)
        {
            _client = client;
            NotifyStateChanged();
        }

        public void SetSelectedAnimal(Animal animal)
        {
            _animal = animal;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
