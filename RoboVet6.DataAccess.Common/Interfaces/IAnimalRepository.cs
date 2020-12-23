using System.Collections.Generic;
using System.Threading.Tasks;
using RoboVet6.Data.Models.RoboVet6;

namespace RoboVet6.DataAccess.Common.Interfaces
{
    public interface IAnimalRepository
    {
        Task<List<AnimalModel>> GetAllAnimals(string searchQuery);
        Task<AnimalModel> GetAnimalByAnimalId(int animalId);
        Task<List<AnimalModel>> GetAnimalsByClientId(int clientId);
        Task InsertAnimal(AnimalModel animal);
        Task UpdateAnimal(AnimalModel animal);
        Task<bool> AnimalExists(int animalId);
    }
}
