using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RoboVet6.DataAccess.Common.Interfaces;
using RoboVet6.Service.Common.Interfaces;
using RoboVet6.Service.Common.Models.API;

namespace RoboVet6.Service.Services
{
    public class AnimalsService : IAnimalsService
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly IClientRepository _clientRepository;

        public AnimalsService(IAnimalRepository animalRepository, IClientRepository clientRepository)
        {
            _animalRepository = animalRepository;
            _clientRepository = clientRepository;
        }
        public async Task<List<Animal>> GetAnimalsByClientId(int clientId)
        {
            var clientExists = await _clientRepository.ClientExists(clientId);
            if (clientExists == false)
            {
                return null;
            }

            var animalsFromRepo = await _animalRepository.GetAnimalsByClientId(clientId);

            if (animalsFromRepo.Count == 0)
            {
                return new List<Animal>();
            }

            var animalsToReturn = new List<Animal>();

            foreach (var animalFromRepo in animalsFromRepo)
            {
               animalsToReturn.Add(new Animal
               {
                   Id = animalFromRepo.Id,
                   Name = animalFromRepo.Name,
                   ClientId = animalFromRepo.ClientId
               });
            }

            return animalsToReturn;
        }

        public async Task<Animal> GetAnimalByAnimalId(int animalId)
        {
            var animalExists = await _animalRepository.AnimalExists(animalId);

            if (!animalExists)
            {
                return null;
            }

            var animalFromRepo = await _animalRepository.GetAnimalByAnimalId(animalId);

            var animalToReturn = new Animal
            {
                Id = animalFromRepo.Id,
                Name = animalFromRepo.Name,
                ClientId = animalFromRepo.ClientId
            };

            return animalToReturn;
        }

        public async Task<List<Animal>> GetAllAnimals()
        {
            var animalsFromRepo = await _animalRepository.GetAllAnimals();
            if (animalsFromRepo.Count == 0)
            {
                return new List<Animal>();
            }

            var animalsToReturn = new List<Animal>();

            foreach (var animalFromRepo in animalsFromRepo)
            {
                animalsToReturn.Add(new Animal
                {
                    Id = animalFromRepo.Id,
                    Name = animalFromRepo.Name,
                    ClientId = animalFromRepo.ClientId
                });

            }

            return animalsToReturn;
        }

        public async Task<Animal> InsertAnimal(Animal animal, int clientId)
        {
            var clientExists = await _clientRepository.ClientExists(clientId);

            if (!clientExists)
            {
                return null;
            }

            var animalToInsert = new Data.Models.Animal
            {
                Id = animal.Id,
                Name = animal.Name,
                ClientId = clientId
            };

            await _animalRepository.InsertAnimal(animalToInsert);

            var animalToReturn = new Animal
            {
                Id = animalToInsert.Id,
                Name = animalToInsert.Name,
                ClientId = animalToInsert.ClientId

            };

            return animalToReturn;
        }

        public Task<bool> AnimalExists(int animalId)
        {
            return _animalRepository.AnimalExists(animalId);
        }
    }
}
