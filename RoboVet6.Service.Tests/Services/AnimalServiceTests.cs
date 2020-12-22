using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using RoboVet6.Data.Models;
using RoboVet6.DataAccess.Common.Interfaces;
using RoboVet6.Service.Common.Interfaces;
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

        [TestInitialize]
        public void Setup()
        {
            _clientRepository = new Mock<IClientRepository>(); 
            _animalRepository = new Mock<IAnimalRepository>();
            _mapper = new Mock<IMapper>();
            _service = new AnimalsService(_animalRepository.Object, _clientRepository.Object, _mapper.Object);
        }

        [TestMethod]
        public void GetAnimalsByClientId_Client_Doesnt_Exist()
        {
            //arrange
            _clientRepository.Setup(x => x.ClientExists(1)).ReturnsAsync(false);

            //act
            var result = _service.GetAnimalsByClientId(1).Result;

            //setup
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void GetAnimalsByClientId_Returns_Empty_List()
        {
            //arrange
            var expectedResult = new List<AnimalToReturnDto>();

            _clientRepository.Setup(x => x.ClientExists(1)).ReturnsAsync(true);
            _animalRepository.Setup(x => x.GetAnimalsByClientId(1)).ReturnsAsync(new List<Animal>());

            //act
            var result = _service.GetAnimalsByClientId(1).Result;

            //assert
            CollectionAssert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void GetAnimalsByClientId_Returns_List()
        {
            //arrange
            var animalsFromRepo = new List<Animal>
            {
                new Animal
                {
                    Id = 1,
                    Name = "Jimbo",
                    ClientId = 1
                },
                new Animal
                {
                    Id = 2,
                    Name = "Sambo",
                    ClientId = 1
                },
                new Animal
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
            _mapper.Setup(x => x.Map<List<AnimalToReturnDto>>(It.IsAny<List<Animal>>())).Returns(animalsMapped);

            //act
            var result = _service.GetAnimalsByClientId(1).Result;


            //assert
            Assert.AreEqual(JsonConvert.SerializeObject(animalsMapped), JsonConvert.SerializeObject(result));
        }
    }
}
