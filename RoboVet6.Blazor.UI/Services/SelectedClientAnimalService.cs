using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using RoboVet6.Blazor.UI.Components;
using RoboVet6.Blazor.UI.Models;

namespace RoboVet6.Blazor.UI.Services
{
    public class SelectedClientAnimalService
    {
        private Client _selectedClient;

        private Animal _selectedAnimal;

        public SelectedClientAnimalService()
        {
            _selectedClient = new Client();
            _selectedAnimal = new Animal();
        }

        public Client SelectedClient
        {
            get
            {
                return _selectedClient;
            }
            set
            {
                _selectedClient = value;
                NotifyDataChanged();
            }
        }

        public Animal SelectedAnimal
        {
            get
            {
                return _selectedAnimal;
            }
            set
            {
                _selectedAnimal = value;
                NotifyDataChanged();
            }
        }


        public event Action OnChange;

        private void NotifyDataChanged() =>  OnChange?.Invoke();
    }
}
