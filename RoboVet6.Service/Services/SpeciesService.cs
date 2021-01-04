using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RoboVet6.Data.Models.RoboVet6;
using RoboVet6.DataAccess.Common.Interfaces;
using RoboVet6.Service.Common.Interfaces;
using RoboVet6.Service.Common.Models.API.ApiResponse;
using RoboVet6.Service.Common.Models.API.Species;

namespace RoboVet6.Service.Services
{
    public class SpeciesService : ISpeciesService
    {
        private readonly ISpeciesRepository _speciesRepository;
        private readonly IMapper _mapper;

        public SpeciesService(ISpeciesRepository speciesRepository, IMapper mapper)
        {
            _speciesRepository = speciesRepository
                                 ?? throw new ArgumentNullException(nameof(speciesRepository));
            _mapper = mapper
                      ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<ApiResponse<SpeciesToReturnDto>> GetSpeciesBySpeciesId(int speciesId)
        {
            var response = new ApiResponse<SpeciesToReturnDto>();

            try
            {
                var speciesFromRepo = await _speciesRepository.GetSpeciesBySpeciesId(speciesId);

                if (speciesFromRepo == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                var speciesToReturn = _mapper.Map<SpeciesToReturnDto>(speciesFromRepo);

                response.StatusCode = HttpStatusCode.OK;
                response.Data = speciesToReturn;
                return response;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Error = e.Message;
                return response;
            }
        }

        public async Task<ApiResponse<List<SpeciesToReturnDto>>> GetAllSpecies(string name)
        {
            var response = new ApiResponse<List<SpeciesToReturnDto>>();

            try
            {
                var speciesFromRepo = await _speciesRepository.GetAllSpecies(name);

                if (speciesFromRepo.Count == 0)
                {
                    response.StatusCode = HttpStatusCode.NoContent;
                    return response;
                }

                var speciesToReturn = _mapper.Map<List<SpeciesToReturnDto>>(speciesFromRepo);

                response.StatusCode = HttpStatusCode.OK;
                response.Data = speciesToReturn;
                return response;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Error = e.Message;
                return response;
            }
        }

        public async Task<ApiResponse<SpeciesToReturnDto>> InsertSpecies(SpeciesToInsertDto species)
        {
            var response = new ApiResponse<SpeciesToReturnDto>();

            try
            {
                var speciesToInsert = _mapper.Map<SpeciesModel>(species);

                await _speciesRepository.InsertSpecies(speciesToInsert);

                var speciesToReturn = _mapper.Map<SpeciesToReturnDto>(speciesToInsert);

                response.StatusCode = HttpStatusCode.Created;
                response.Data = speciesToReturn;

                return response;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Error = e.Message;
                return response;
            }
        }

        public async Task<bool> SpeciesExists(int speciesId)
        {
            return await _speciesRepository.SpeciesExists(speciesId);
        }

        public async Task<ApiResponse<SpeciesToReturnDto>> UpdateSpecies(int speciesId, SpeciesToUpdateDto species)
        {
            var response = new ApiResponse<SpeciesToReturnDto>();

            try
            {
                var speciesFromRepo = await _speciesRepository.GetSpeciesBySpeciesId(speciesId);

                if (speciesFromRepo == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                _mapper.Map(species, speciesFromRepo);

                await _speciesRepository.UpdateSpecies(speciesFromRepo);

                response.StatusCode = HttpStatusCode.NoContent;
                return response;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Error = e.Message;
                return response;
            }
        }
    }
}
