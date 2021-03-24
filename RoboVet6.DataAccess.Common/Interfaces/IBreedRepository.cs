using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RoboVet6.Data.Models.RoboVet6;

namespace RoboVet6.DataAccess.Common.Interfaces
{
    public interface IBreedRepository
    {
        Task<List<BreedModel>> GetAllBreeds(string name);
        Task<BreedModel> GetBreedByBreedId(int breedId);
        Task<List<BreedModel>> GetBreedsBySpeciesId(int speciesId);
        Task InsertBreed(BreedModel breed);
        Task UpdateBreed(BreedModel breed);
        Task<bool> BreedExists(int breedId);
    }
}
