using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RoboVet6.Service.Common.Models.API.ApiResponse;
using RoboVet6.Service.Common.Models.API.Species;

namespace RoboVet6.Service.Common.Interfaces
{
    public interface ISpeciesService
    {
        Task<ApiResponse<SpeciesToReturnDto>> GetSpeciesBySpeciesId(int speciesId);
        Task<ApiResponse<List<SpeciesToReturnDto>>> GetAllSpecies(string name);
        Task<ApiResponse<SpeciesToReturnDto>> InsertSpecies(SpeciesToInsertDto speciesToInsert);
        Task<bool> SpeciesExists(int speciesId);
        Task<ApiResponse<SpeciesToReturnDto>> UpdateSpecies(int speciesId, SpeciesToUpdateDto species);
    }
}
