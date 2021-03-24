using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoboVet6.Blazor.UI.Models;

namespace RoboVet6.Blazor.UI.Interfaces.Services
{
    public interface IAnimalDataService
    {
        Task<Animal> GetAnimalById(int animalId);
        Task UpdateAnimal(Animal animalToUpdate);
        Task<Animal> AddAnimal(Animal animalToAdd);
        Task<IEnumerable<Animal>> GetAnimalsByClientId(int clientId);
    }
}
