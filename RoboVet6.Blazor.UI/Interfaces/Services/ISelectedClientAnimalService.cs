using System;
using RoboVet6.Blazor.UI.Models;

namespace RoboVet6.Blazor.UI.Interfaces.Services
{
    public interface ISelectedClientAnimalService
    {
        event Action OnChange;
        void NotifyDataChanged();
        void UpdateNavBar(bool animalSelected);
        void UpdateSelectedAnimal(Animal animal);
        void UpdateSelectedClient(Client client);
    }
}