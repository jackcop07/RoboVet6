using System.Collections.Generic;
using System.Threading.Tasks;
using RoboVet6.Blazor.UI.Models;

namespace RoboVet6.Blazor.UI.Interfaces.Services
{
    public interface IBreedDataService
    {
        Task<Breed> GetBreedById(int breedId);
        Task<IEnumerable<Breed>> GetBreedsBySpeciesId(int speciesId);
        Task<IEnumerable<Breed>> GetAllBreeds();
        Task UpdateBreed(Breed breedToUpdate);
        Task<Breed> AddBreed(Breed breedToUpdate);
    }
}
