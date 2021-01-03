using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using RoboVet6.Data.Models.RoboVet6;
using RoboVet6.DataAccess.Common.Interfaces;
using RoboVet6.Service.Common.Interfaces;
using RoboVet6.Service.Common.Models.API.Animal;
using RoboVet6.Service.Common.Models.API.ApiResponse;

namespace RoboVet6.Service.Services
{
    public class AnimalsService : IAnimalsService
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public AnimalsService(IAnimalRepository animalRepository, IClientRepository clientRepository, IMapper mapper)
        {
            _animalRepository = animalRepository
                                ?? throw new ArgumentNullException(nameof(animalRepository));
            _clientRepository = clientRepository
                                ?? throw new ArgumentNullException(nameof(clientRepository));
            _mapper = mapper
                      ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<ApiResponse<List<AnimalToReturnDto>>> GetAnimalsByClientId(int clientId)
        {
            var response = new ApiResponse<List<AnimalToReturnDto>>();

            try
            {
                var clientExists = await _clientRepository.ClientExists(clientId);

                if (clientExists == false)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                var animalsFromRepo = await _animalRepository.GetAnimalsByClientId(clientId);

                if (animalsFromRepo.Count == 0)
                {
                    response.StatusCode = HttpStatusCode.NoContent;
                    return response;
                }

                response.StatusCode = HttpStatusCode.OK;
                response.Data = _mapper.Map<List<AnimalToReturnDto>>(animalsFromRepo);

                return response;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Error = e.Message;
                return response;
            }
        }

        public async Task<ApiResponse<AnimalToReturnDto>> GetAnimalByAnimalId(int animalId)
        {

            var response = new ApiResponse<AnimalToReturnDto>();

            try
            {
                var animalExists = await _animalRepository.AnimalExists(animalId);

                if (!animalExists)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                var animalFromRepo = await _animalRepository.GetAnimalByAnimalId(animalId);

                response.StatusCode = HttpStatusCode.OK;
                response.Data = _mapper.Map<AnimalToReturnDto>(animalFromRepo);

                return response;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Error = e.Message;
                return response;
            }
        }

        public async Task<ApiResponse<List<AnimalToReturnDto>>> GetAllAnimals(string name)
        {
            var response = new ApiResponse<List<AnimalToReturnDto>>();

            try
            {
                var animalsFromRepo = await _animalRepository.GetAllAnimals(name);

                if (animalsFromRepo.Count == 0)
                {
                    response.StatusCode = HttpStatusCode.NoContent;
                    return response;
                }

                response.StatusCode = HttpStatusCode.OK;
                response.Data = _mapper.Map<List<AnimalToReturnDto>>(animalsFromRepo);

                return response;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Error = e.Message;
                return response;
            }
        }

        public async Task<ApiResponse<AnimalToReturnDto>> InsertAnimal(AnimalToInsertDto animal, int clientId)
        {

            var response = new ApiResponse<AnimalToReturnDto>();

            try
            {
                var clientExists = await _clientRepository.ClientExists(clientId);

                if (!clientExists)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                var animalToInsert = _mapper.Map<AnimalModel>(animal);
                animalToInsert.ClientId = clientId;

                await _animalRepository.InsertAnimal(animalToInsert);

                response.StatusCode = HttpStatusCode.Created;
                response.Data = _mapper.Map<AnimalToReturnDto>(animalToInsert);

                return response;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Error = e.Message;
                return response;
            }
        }

        public Task<bool> AnimalExists(int animalId)
        {
            return _animalRepository.AnimalExists(animalId);
        }

        public async Task<ApiResponse<AnimalToReturnDto>> UpdateAnimal(int animalId, AnimalToUpdateDto animal)
        {
            var response = new ApiResponse<AnimalToReturnDto>();

            try
            {
                var animalFromRepo = await _animalRepository.GetAnimalByAnimalId(animalId);

                if (animalFromRepo == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                if (animalFromRepo.ClientId != animal.ClientId)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Error = $"Animal Id: {animalId} doesn't exist for Client Id: {animal.ClientId}.";
                    return response;
                }

                _mapper.Map(animal, animalFromRepo);

                await _animalRepository.UpdateAnimal(animalFromRepo);

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
