using System.Collections.Generic;
using System.Threading.Tasks;
using RoboVet6.Service.Common.Models.API.Animal;
using RoboVet6.Service.Common.Models.API.ApiResponse;

namespace RoboVet6.Service.Common.Interfaces
{
    public interface IAnimalsService
    {
        Task<ApiResponse<List<AnimalToReturnDto>>> GetAnimalsByClientId(int clientId);
        Task<ApiResponse<AnimalToReturnDto>> GetAnimalByAnimalId(int animalId);
        Task<ApiResponse<List<AnimalToReturnDto>>> GetAllAnimals(string searchQuery);
        Task<ApiResponse<AnimalToReturnDto>> InsertAnimal(AnimalToInsertDto animalToInsert, int clientId);
        Task<bool> AnimalExists(int animalId);
        Task<ApiResponse<AnimalToReturnDto>> UpdateAnimal(int animalId, AnimalToUpdateDto animal);
    }
}
