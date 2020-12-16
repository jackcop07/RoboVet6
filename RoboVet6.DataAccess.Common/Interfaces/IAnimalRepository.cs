using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RoboVet6.Data.Models;

namespace RoboVet6.DataAccess.Common.Interfaces
{
    public interface IAnimalRepository
    {
        Task<List<Animal>> GetAllAnimals();
        Task<Animal> GetAnimalByAnimalId(int animalId);
        Task<List<Animal>> GetAnimalsByClientId(int clientId);
        Task InsertAnimal(Animal animal);
        Task UpdateAnimal(Animal animal);
        Task<bool> AnimalExists(int animalId);
    }
}
