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
using RoboVet6.Service.Common.Models.API.Animal;
using RoboVet6.Service.Common.Models.API.Client;
using RoboVet6.Service.Services;

namespace RoboVet6.Service.Tests.Services
{
    [TestClass]
    public class ClientServiceTests
    {
        private Mock<IClientRepository> _repository;
        private Mock<IMapper> _mapper;
        private ClientsService _service;

        [TestInitialize]
        public void Setup()
        {
            _repository = new Mock<IClientRepository>();
            _mapper = new Mock<IMapper>();
            _service = new ClientsService(_repository.Object, _mapper.Object);
        }

        [TestMethod]
        public void GetClientByClientId_Returns_NotFound()
        {
            //arrange
            _repository.Setup(x => x.GetClientById(1)).ReturnsAsync(()=>null);

            //act
            var result = _service.GetClientByClientId(1).Result;

            //assert
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod]
        public void GetClientByClientId_Returns_Client()
        {
            //arrange
            var clientFromRepo = new ClientModel
            {
                Address = "12 Livingstone Place",
                City = "Lockerbie",
                Email = "Sampcop49@gmail.com",
                Title = "Mr",
                FirstName = "Sam",
                LastName = "Copeland",
                HomePhone = "01576 202321",
                Id = 5,
                MobilePhone = "07986633452",
                Postcode = "DG11 2AU",
                Animals = new List<AnimalModel>
                {
                    new AnimalModel
                    {
                        ClientId = 5,
                        Id = 1,
                        Name = "Roger"
                    },
                    new AnimalModel
                    {
                        ClientId = 5,
                        Id = 2,
                        Name = "Biggins"
                    }
                }
            };

            var clientAfterMapping = new ClientToReturnDto
            {
                Address = "12 Livingstone Place",
                City = "Lockerbie",
                Email = "Sampcop49@gmail.com",
                Title = "Mr",
                FirstName = "Sam",
                LastName = "Copeland",
                HomePhone = "01576 202321",
                Id = 5,
                MobilePhone = "07986633452",
                Postcode = "DG11 2AU",
                Animals = new List<AnimalToReturnDto>
                {
                    new AnimalToReturnDto
                    {
                        ClientId = 5,
                        Id = 1,
                        Name = "Roger"
                    },
                    new AnimalToReturnDto
                    {
                        ClientId = 5,
                        Id = 2,
                        Name = "Biggins"
                    }
                }
            };

            _repository.Setup(x => x.GetClientById(1)).ReturnsAsync(clientFromRepo);
            _mapper.Setup(x => x.Map<ClientToReturnDto>(It.IsAny<ClientModel>())).Returns(clientAfterMapping);
            
            //act
            var result = _service.GetClientByClientId(1).Result;

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(JsonConvert.SerializeObject(clientAfterMapping), JsonConvert.SerializeObject(result.Data));
        }

        [TestMethod]
        public void GetAllClients_Returns_NoContent()
        {
            //arrange
            _repository.Setup(x => x.GetAllClients(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(() => null);
            _mapper.Setup(x => x.Map<List<ClientToReturnDto>>(It.IsAny<List<ClientModel>>())).Returns(() => null);

            //act
            var result = _service.GetAllClients(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()).Result;

            //assert
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod]
        public void GetAllClients_Returns_List()
        {
            //arrange
            var clientsFromRepo = new List<ClientModel>
            {
                new ClientModel
                {
                    Address = "12 Livingstone Place",
                    City = "Lockerbie",
                    Email = "Sampcop49@gmail.com",
                    Title = "Mr",
                    FirstName = "Sam",
                    LastName = "Copeland",
                    HomePhone = "01576 202321",
                    Id = 5,
                    MobilePhone = "07986633452",
                    Postcode = "DG11 2AU",
                    Animals = new List<AnimalModel>
                    {
                        new AnimalModel
                        {
                            ClientId = 5,
                            Id = 1,
                            Name = "Roger"
                        },
                        new AnimalModel
                        {
                            ClientId = 5,
                            Id = 2,
                            Name = "Biggins"
                        }
                    }
                },
                new ClientModel
                {
                    Address = "12 Livingstone Place",
                    City = "Lockerbie",
                    Email = "Sampcop49@gmail.com",
                    Title = "Mr",
                    FirstName = "Sam",
                    LastName = "Copeland",
                    HomePhone = "01576 202321",
                    Id = 5,
                    MobilePhone = "07986633452",
                    Postcode = "DG11 2AU",
                    Animals = new List<AnimalModel>
                    {
                        new AnimalModel
                        {
                            ClientId = 5,
                            Id = 1,
                            Name = "Roger"
                        },
                        new AnimalModel
                        {
                            ClientId = 5,
                            Id = 2,
                            Name = "Biggins"
                        }
                    }
                }
            };

            var clientsAfterMapping = new List<ClientToReturnDto>
            {
                new ClientToReturnDto
                {
                    Address = "12 Livingstone Place",
                    City = "Lockerbie",
                    Email = "Sampcop49@gmail.com",
                    Title = "Mr",
                    FirstName = "Sam",
                    LastName = "Copeland",
                    HomePhone = "01576 202321",
                    Id = 5,
                    MobilePhone = "07986633452",
                    Postcode = "DG11 2AU",
                    Animals = new List<AnimalToReturnDto>
                    {
                        new AnimalToReturnDto
                        {
                            ClientId = 5,
                            Id = 1,
                            Name = "Roger"
                        },
                        new AnimalToReturnDto
                        {
                            ClientId = 5,
                            Id = 2,
                            Name = "Biggins"
                        }
                    }
                },
                new ClientToReturnDto
                {
                    Address = "12 Livingstone Place",
                    City = "Lockerbie",
                    Email = "Sampcop49@gmail.com",
                    Title = "Mr",
                    FirstName = "Sam",
                    LastName = "Copeland",
                    HomePhone = "01576 202321",
                    Id = 5,
                    MobilePhone = "07986633452",
                    Postcode = "DG11 2AU",
                    Animals = new List<AnimalToReturnDto>
                    {
                        new AnimalToReturnDto
                        {
                            ClientId = 5,
                            Id = 1,
                            Name = "Roger"
                        },
                        new AnimalToReturnDto
                        {
                            ClientId = 5,
                            Id = 2,
                            Name = "Biggins"
                        }
                    }
                }
            };

            _repository.Setup(x => x.GetAllClients(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(clientsFromRepo);
            _mapper.Setup(x => x.Map<List<ClientToReturnDto>>(It.IsAny<List<ClientModel>>())).Returns(clientsAfterMapping);

            //act
            var result = _service.GetAllClients(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()).Result;

            //assert
            Assert.AreEqual(JsonConvert.SerializeObject(clientsAfterMapping), JsonConvert.SerializeObject(result.Data));
        }

        [TestMethod]
        public void InsertClient_Inserts()
        {
            //arrange
            var clientToInsert = new ClientModel
            {
                Address = "12 Livingstone Place",
                City = "Lockerbie",
                Email = "Sampcop49@gmail.com",
                Title = "Mr",
                FirstName = "Sam",
                LastName = "Copeland",
                HomePhone = "01576 202321",
                Id = 5,
                MobilePhone = "07986633452",
                Postcode = "DG11 2AU"
            };

            var clientToReturn = new ClientToReturnDto
            {
                Address = "12 Livingstone Place",
                City = "Lockerbie",
                Email = "Sampcop49@gmail.com",
                Title = "Mr",
                FirstName = "Sam",
                LastName = "Copeland",
                HomePhone = "01576 202321",
                Id = 5,
                MobilePhone = "07986633452",
                Postcode = "DG11 2AU"
            };

            _mapper.Setup(x => x.Map<ClientModel>(It.IsAny<ClientToInsertDto>())).Returns(clientToInsert);
            _mapper.Setup(x => x.Map<ClientToReturnDto>(It.IsAny<ClientModel>())).Returns(clientToReturn);

            //act
            var result = _service.InsertClient(It.IsAny<ClientToInsertDto>()).Result;


            //assert
            Assert.AreEqual(JsonConvert.SerializeObject(clientToReturn), JsonConvert.SerializeObject(result.Data));
        }


        [TestMethod]
        public void ClientExists_Returns_True()
        {
            //arrange
            _repository.Setup(x => x.ClientExists(1)).ReturnsAsync(true);

            //act
            var result = _service.ClientExists(1).Result;

            //assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void ClientExists_Returns_False()
        {
            //arrange
            _repository.Setup(x => x.ClientExists(1)).ReturnsAsync(false);

            //act
            var result = _service.ClientExists(1).Result;

            //assert
            Assert.AreEqual(false, result);
        }
    }
}
