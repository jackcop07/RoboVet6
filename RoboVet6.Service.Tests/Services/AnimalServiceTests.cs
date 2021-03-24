using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using RoboVet6.Data.Models;
using RoboVet6.Data.Models.RoboVet6;
using RoboVet6.DataAccess.Common.Interfaces;
using RoboVet6.Service.Common.Interfaces;
using RoboVet6.Service.Common.Interfaces.Helpers;
using RoboVet6.Service.Common.Models.API.Animal;
using RoboVet6.Service.Services;

namespace RoboVet6.Service.Tests.Services
{
    [TestClass]
    public class AnimalServiceTests
    {
        private Mock<IClientRepository> _clientRepository;
        private Mock<IAnimalRepository> _animalRepository;
        private Mock<IMapper> _mapper;
        private IAnimalsService _service;
        private IAnimalHelper _animalHelper;

        [TestInitialize]
        public void Setup()
        {
            _clientRepository = new Mock<IClientRepository>(); 
            _animalRepository = new Mock<IAnimalRepository>();
            _mapper = new Mock<IMapper>();
            _service = new AnimalsService(_animalRepository.Object, _clientRepository.Object, _mapper.Object, _animalHelper);
        }

        [TestMethod]
        public void GetAnimalsByClientId_Client_Doesnt_Exist()
        {
            //arrange
            _clientRepository.Setup(x => x.ClientExists(1)).ReturnsAsync(false);

            //act
            var result = _service.GetAnimalsByClientId(1).Result;

            //setup
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod]
        public void GetAnimalsByClientId_Returns_Empty_List()
        {
            //arrange
            _clientRepository.Setup(x => x.ClientExists(1)).ReturnsAsync(true);
            _animalRepository.Setup(x => x.GetAnimalsByClientId(1)).ReturnsAsync(new List<AnimalModel>());

            //act
            var result = _service.GetAnimalsByClientId(1).Result;

            //assert
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod]
        public void GetAnimalsByClientId_Returns_List()
        {
            //arrange
            var animalsFromRepo = new List<AnimalModel>
            {
                new AnimalModel
                {
                    Id = 1,
                    Name = "Jimbo",
                    ClientId = 1
                },
                new AnimalModel
                {
                    Id = 2,
                    Name = "Sambo",
                    ClientId = 1
                },
                new AnimalModel
                {
                    Id = 3,
                    Name = "Tombo",
                    ClientId = 1
                }
            };

            var animalsMapped = new List<AnimalToReturnDto>
            {
                new AnimalToReturnDto
                {
                    Id = 1,
                    Name = "Jimbo",
                    ClientId = 1
                },
                new AnimalToReturnDto
                {
                    Id = 2,
                    Name = "Sambo",
                    ClientId = 1
                },
                new AnimalToReturnDto
                {
                    Id = 3,
                    Name = "Tombo",
                    ClientId = 1
                }
            };

            _clientRepository.Setup(x => x.ClientExists(1)).ReturnsAsync(true);
            _animalRepository.Setup(x => x.GetAnimalsByClientId(1)).ReturnsAsync(animalsFromRepo);
            _mapper.Setup(x => x.Map<List<AnimalToReturnDto>>(It.IsAny<List<AnimalModel>>())).Returns(animalsMapped);

            //act
            var result = _service.GetAnimalsByClientId(1).Result;


            //assert
            Assert.AreEqual(JsonConvert.SerializeObject(animalsMapped), JsonConvert.SerializeObject(result.Data));
        }

        [TestMethod]
        public void GetAnimalBuAnimalId_Animal_Doesnt_Exist()
        {
            //arrange
            _animalRepository.Setup(x => x.AnimalExists(1)).ReturnsAsync(false);

            //act
            var result = _service.GetAnimalByAnimalId(1).Result;

            //assert
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod]
        public void GetAnimalByAnimalId_Returns_Animal()
        {
            //arrange
            var animalFromRepo = new AnimalModel
            {
                ClientId = 1,
                Name = "Roger",
                Id = 2
            };

            var animalToReturn = new AnimalToReturnDto
            {
                ClientId = 1,
                Name = "Roger",
                Id = 2
            };

            _animalRepository.Setup(x => x.AnimalExists(1)).ReturnsAsync(true);
            _animalRepository.Setup(x => x.GetAnimalByAnimalId(1)).ReturnsAsync(animalFromRepo);
            _mapper.Setup(x => x.Map<AnimalToReturnDto>(It.IsAny<AnimalModel>())).Returns(animalToReturn);

            //act
            var result = _service.GetAnimalByAnimalId(1).Result;

            //assert
            Assert.AreEqual(JsonConvert.SerializeObject(animalToReturn), JsonConvert.SerializeObject(result.Data));
        }

        [TestMethod]
        public void GetAllAnimals_Returns_Empty_List()
        {
            //arrange
            var emptyListToReturn = new List<AnimalToReturnDto>();
            _animalRepository.Setup(x => x.GetAllAnimals(It.IsAny<string>())).ReturnsAsync(new List<AnimalModel>());

            //act
            var result = _service.GetAllAnimals(It.IsAny<string>()).Result;

            //assert
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);

        }

        [TestMethod]
        public void GetAllAnimals_Returns_Animals()
        {
            //arrange
            var animalsFromRepo = new List<AnimalModel>
            {
                new AnimalModel
                {
                    ClientId = 1,
                    Name = "Steven",
                    Id = 1
                },
                new AnimalModel
                {
                    ClientId = 1,
                    Name = "Sean",
                    Id = 2
                },
                new AnimalModel
                {
                    ClientId = 1,
                    Name = "Sarah",
                    Id = 3
                }
            };

            var animalsToReturn = new List<AnimalToReturnDto>
            {
                new AnimalToReturnDto
                {
                    ClientId = 1,
                    Name = "Steven",
                    Id = 1
                },
                new AnimalToReturnDto
                {
                    ClientId = 1,
                    Name = "Sean",
                    Id = 2
                },
                new AnimalToReturnDto
                {
                    ClientId = 1,
                    Name = "Sarah",
                    Id = 3
                }
            };

            _animalRepository.Setup(x => x.GetAllAnimals("")).ReturnsAsync(animalsFromRepo);
            _mapper.Setup(x => x.Map<List<AnimalToReturnDto>>(It.IsAny<List<AnimalModel>>())).Returns(animalsToReturn);

            //act
            var result = _service.GetAllAnimals("").Result;

            //assert
            Assert.AreEqual(JsonConvert.SerializeObject(animalsToReturn), JsonConvert.SerializeObject(result.Data));
        }

        [TestMethod]
        public void InsertAnimal_Client_Doesnt_Exist()
        {
            //arrange
            _clientRepository.Setup(x => x.ClientExists(1)).ReturnsAsync(false);

            //act
            var result = _service.InsertAnimal(It.IsAny<AnimalToInsertDto>(), It.IsAny<int>()).Result;

            //assert
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod]
        public void InsertAnimal_Client_Exists()
        {
            //arrange
            var animalToInsert = new AnimalModel
            {
                Id = 1,
                Name = "Robert",
                ClientId = 4
            };

            var animalToReturn = new AnimalToReturnDto
            {
                Id = 1,
                Name = "Robert",
                ClientId = 4
            };

            _clientRepository.Setup(x => x.ClientExists(It.IsAny<int>())).ReturnsAsync(true);
            _mapper.Setup(x => x.Map<AnimalModel>(It.IsAny<AnimalToInsertDto>())).Returns(animalToInsert);
            _mapper.Setup(x => x.Map<AnimalToReturnDto>(It.IsAny<AnimalModel>())).Returns(animalToReturn);

            //act
            var result = _service.InsertAnimal(It.IsAny<AnimalToInsertDto>(), 4).Result;


            //assert
            Assert.AreEqual(JsonConvert.SerializeObject(animalToReturn), JsonConvert.SerializeObject(result.Data));
        }

        [TestMethod]
        public void AnimalExists_Returns_True()
        {
            //arrange
            _animalRepository.Setup(x => x.AnimalExists(It.IsAny<int>())).ReturnsAsync(true);

            //act
            var result = _service.AnimalExists(1).Result;

            //assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void AnimalExists_Returns_False()
        {
            //arrange
            _animalRepository.Setup(x => x.AnimalExists(It.IsAny<int>())).ReturnsAsync(false);

            //act
            var result = _service.AnimalExists(1).Result;

            //assert
            Assert.AreEqual(false, result);
        }
    }
}
