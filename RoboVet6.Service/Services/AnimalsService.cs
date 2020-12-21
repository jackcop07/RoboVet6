using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RoboVet6.DataAccess.Common.Interfaces;
using RoboVet6.Service.Common.Interfaces;
using RoboVet6.Service.Common.Models.API;
using RoboVet6.Service.Common.Models.API.Animal;

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
        public async Task<List<AnimalToReturnDto>> GetAnimalsByClientId(int clientId)
        {
            var clientExists = await _clientRepository.ClientExists(clientId);

            if (clientExists == false)
            {
                return null;
            }

            var animalsFromRepo = await _animalRepository.GetAnimalsByClientId(clientId);

            if (animalsFromRepo.Count == 0)
            {
                return new List<AnimalToReturnDto>();
            }

            var animalsToReturn = _mapper.Map<List<AnimalToReturnDto>>(animalsFromRepo);

            return animalsToReturn;
        }

        public async Task<AnimalToReturnDto> GetAnimalByAnimalId(int animalId)
        {
            var animalExists = await _animalRepository.AnimalExists(animalId);

            if (!animalExists)
            {
                return null;
            }

            var animalFromRepo = await _animalRepository.GetAnimalByAnimalId(animalId);

            var animalToReturn = _mapper.Map<AnimalToReturnDto>(animalFromRepo);

            return animalToReturn;
        }

        public async Task<List<AnimalToReturnDto>> GetAllAnimals(string searchQuery)
        {
            var animalsFromRepo = await _animalRepository.GetAllAnimals(searchQuery);
            if (animalsFromRepo.Count == 0)
            {
                return new List<AnimalToReturnDto>();
            }

            var animalsToReturn = _mapper.Map<List<AnimalToReturnDto>>(animalsFromRepo);

            return animalsToReturn;
        }

        public async Task<AnimalToReturnDto> InsertAnimal(AnimalToInsertDto animal, int clientId)
        {
            var clientExists = await _clientRepository.ClientExists(clientId);

            if (!clientExists)
            {
                return null;
            }

            var animalToInsert = _mapper.Map<Data.Models.Animal>(animal);
            animalToInsert.ClientId = clientId;

            await _animalRepository.InsertAnimal(animalToInsert);

            var animalToReturn = _mapper.Map<AnimalToReturnDto>(animalToInsert);

            return animalToReturn;
        }

        public Task<bool> AnimalExists(int animalId)
        {
            return _animalRepository.AnimalExists(animalId);
        }
    }
}
