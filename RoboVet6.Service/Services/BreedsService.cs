using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RoboVet6.Data.Models.RoboVet6;
using RoboVet6.DataAccess.Common.Interfaces;
using RoboVet6.Service.Common.Interfaces;
using RoboVet6.Service.Common.Interfaces.Services;
using RoboVet6.Service.Common.Models.API.ApiResponse;
using RoboVet6.Service.Common.Models.API.Breed;
using RoboVet6.Service.Common.Models.API.Client;

namespace RoboVet6.Service.Services
{
    public class BreedsService : IBreedService
    {
        private readonly IBreedRepository _breedRepository;
        private readonly IMapper _mapper;
        private readonly ISpeciesService _speciesService;

        public BreedsService(IBreedRepository breedRepository, IMapper mapper, ISpeciesService speciesService)
        {
            _breedRepository = breedRepository
                            ?? throw new ArgumentNullException(nameof(breedRepository));

            _mapper = mapper
                    ?? throw new ArgumentNullException(nameof(mapper));

            _speciesService = speciesService
                            ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ApiResponse<BreedToReturnDto>> GetBreedByBreedId(int breedId)
        {
            var response = new ApiResponse<BreedToReturnDto>();

            try
            {
                var breedExists = await _breedRepository.BreedExists(breedId);

                if (!breedExists)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                var breedFromRepo = await _breedRepository.GetBreedByBreedId(breedId);

                response.StatusCode = HttpStatusCode.OK;
                response.Data = _mapper.Map<BreedToReturnDto>(breedFromRepo);

                return response;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Error = e.Message;
                return response;
            }
        }

        public async Task<ApiResponse<List<BreedToReturnDto>>> GetAllBreeds(string name)
        {
            var response = new ApiResponse<List<BreedToReturnDto>>();

            try
            {
                var breedsFromRepo = await _breedRepository.GetAllBreeds(name);

                if (!breedsFromRepo.Any())
                {
                    response.StatusCode = HttpStatusCode.NoContent;
                    return response;
                }

                var breedsToReturn = _mapper.Map<List<BreedToReturnDto>>(breedsFromRepo);
                response.Data = breedsToReturn;
                response.StatusCode = HttpStatusCode.OK;

                return response;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Error = e.Message;
                return response;
            }


        }

        public async Task<ApiResponse<List<BreedToReturnDto>>> GetAllBreedsBySpeciesId(int speciesId)
        {
            var response = new ApiResponse<List<BreedToReturnDto>>();

            try
            {
                var breedsFromRepo = await _breedRepository.GetBreedsBySpeciesId(speciesId);

                if (!breedsFromRepo.Any())
                {
                    response.StatusCode = HttpStatusCode.NoContent;
                    return response;
                }

                var breedsToReturn = _mapper.Map<List<BreedToReturnDto>>(breedsFromRepo);
                response.Data = breedsToReturn;
                response.StatusCode = HttpStatusCode.OK;

                return response;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Error = e.Message;
                return response;
            }
        }

        public async Task<ApiResponse<BreedToReturnDto>> InsertBreed(BreedToInsertDto breed)
        {
            var response = new ApiResponse<BreedToReturnDto>();

            try
            {
                var speciesExists = await _speciesService.SpeciesExists(breed.SpeciesId);

                if (!speciesExists)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }
                var breedToInsert = _mapper.Map<BreedModel>(breed);

                await _breedRepository.InsertBreed(breedToInsert);

                var breedToReturn = _mapper.Map<BreedToReturnDto>(breedToInsert);

                response.Data = breedToReturn;
                response.StatusCode = HttpStatusCode.Created;
                return response;
            }
            catch (Exception e)
            {
                response.Error = e.Message;
                response.StatusCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<bool> BreedExists(int breedId)
        {
            return await _breedRepository.BreedExists(breedId);
        }

        public async Task<ApiResponse<BreedToReturnDto>> UpdateBreed(int breedId, BreedToUpdateDto breed)
        {
            var response = new ApiResponse<BreedToReturnDto>();

            try
            {
                var breedFromRepo = await _breedRepository.GetBreedByBreedId(breedId);

                if (breedFromRepo == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                _mapper.Map(breed, breedFromRepo);

                await _breedRepository.UpdateBreed(breedFromRepo);

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
