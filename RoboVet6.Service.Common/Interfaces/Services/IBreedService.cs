using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RoboVet6.Service.Common.Models.API.ApiResponse;
using RoboVet6.Service.Common.Models.API.Breed;


namespace RoboVet6.Service.Common.Interfaces
{
    public interface IBreedService
    {
        Task<ApiResponse<BreedToReturnDto>> GetBreedByBreedId(int breedId);
        Task<ApiResponse<List<BreedToReturnDto>>> GetAllBreeds(string name);
        Task<ApiResponse<List<BreedToReturnDto>>> GetAllBreedsBySpeciesId(int speciesId);
        Task<ApiResponse<BreedToReturnDto>> InsertBreed(BreedToInsertDto breedToInsert);
        Task<bool> BreedExists(int breedId);
        Task<ApiResponse<BreedToReturnDto>> UpdateBreed(int breedId, BreedToUpdateDto breed);
    }
}
