using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RoboVet6.Service.Common.Interfaces.Helpers
{
    public interface IAnimalHelper
    {
        Task<bool> BreedExistsForSpecies(int breedId, int speciesId);
    }
}
