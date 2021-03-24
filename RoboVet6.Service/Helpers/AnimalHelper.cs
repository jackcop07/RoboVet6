using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoboVet6.DataAccess.Common.Interfaces;
using RoboVet6.Service.Common.Interfaces;
using RoboVet6.Service.Common.Interfaces.Helpers;
using RoboVet6.Service.Common.Interfaces.Services;

namespace RoboVet6.Service.Helpers
{
    public class AnimalHelper : IAnimalHelper
    {
        private readonly IBreedService _breedService;
        private readonly ISpeciesService _speciesService;

        public AnimalHelper(IBreedService breedService, ISpeciesService speciesService)
        {
            _breedService = breedService;
            _speciesService = speciesService;
        }
        public async Task<bool> BreedExistsForSpecies(int breedId, int speciesId)
        {
            //Make sure that the breed exists
            var breedExists = await _breedService.BreedExists(breedId);

            //Make sure the species exists
            var speciesExists = await _speciesService.SpeciesExists(speciesId);

            if (!breedExists || !speciesExists)
            {
                return false;
            }

            var breedsForSpeciesResponse = await _breedService.GetAllBreedsBySpeciesId(speciesId);

            var breedsForSpeciesList = breedsForSpeciesResponse.Data;

            //If the list is empty return false
            if (!breedsForSpeciesList.Any())
            {
                return false;
            }

            //Check to see if the breed exists for the species
            return breedsForSpeciesList.Exists(x => x.Id == breedId);

        }
    }
}
