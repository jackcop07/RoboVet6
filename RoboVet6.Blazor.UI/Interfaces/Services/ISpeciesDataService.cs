using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoboVet6.Blazor.UI.Models;

namespace RoboVet6.Blazor.UI.Interfaces.Services
{
    public interface ISpeciesDataService
    {
        Task<Species> GetSpeciesById(int speciesId);
        Task<IEnumerable<Species>> GetAllSpecies();
        Task UpdateSpecies(Species speciesToUpdate);
        Task<Species> AddSpecies(Species speciesToAdd);
    }
}
