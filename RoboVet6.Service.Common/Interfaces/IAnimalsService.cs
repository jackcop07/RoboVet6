using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RoboVet6.Service.Common.Models.API;

namespace RoboVet6.Service.Common.Interfaces
{
    public interface IAnimalsService
    {
        Task<List<Animal>> GetAnimalsByClientId(int clientId);
        Task<Animal> GetAnimalByAnimalId(int animalId);
        Task<List<Animal>> GetAllAnimals();
        Task<Animal> InsertAnimal(Animal animal, int clientId);
        Task<bool> AnimalExists(int animalId);
    }
}
