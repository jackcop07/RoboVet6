using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RoboVet6.Data.Models.RoboVet6;

namespace RoboVet6.DataAccess.Common
{
    public interface ISpeciesRepository
    {
        Task<List<SpeciesModel>> GetAllSpecies(string name);
        Task<SpeciesModel> GetSpeciesBySpeciesId(int speciesId);
        Task InsertSpecies(SpeciesModel species);
        Task UpdateSpecies(SpeciesModel species);
        Task<bool> SpeciesExists(int speciesId);
    }
}
