using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using RoboVet6.Data.Models;
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
        public void GetClientByClientId_Returns_Null()
        {
            //arrange
            _repository.Setup(x => x.GetClientById(1)).ReturnsAsync(()=>null);

            //act
            var result = _service.GetClientByClientId(1).Result;

            //assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetClientByClientId_Returns_Client()
        {
            //arrange
            var clientFromRepo = new Client
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
                Animals = new List<Animal>
                {
                    new Animal
                    {
                        ClientId = 5,
                        Id = 1,
                        Name = "Roger"
                    },
                    new Animal
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
            _mapper.Setup(x => x.Map<ClientToReturnDto>(It.IsAny<Client>())).Returns(clientAfterMapping);
            
            //act
            var result = _service.GetClientByClientId(1).Result;

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(JsonConvert.SerializeObject(clientAfterMapping), JsonConvert.SerializeObject(result));
        }

        [TestMethod]
        public void GetAllClients_Returns_Null()
        {
            //arrange
            _repository.Setup(x => x.GetAllClients(It.IsAny<string>())).ReturnsAsync(() => null);
            _mapper.Setup(x => x.Map<List<ClientToReturnDto>>(It.IsAny<List<Client>>())).Returns(() => null);

            //act
            var result = _service.GetAllClients(It.IsAny<string>()).Result;

            //assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetAllClients_Returns_List()
        {
            //arrange
            var clientsFromRepo = new List<Client>
            {
                new Client
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
                    Animals = new List<Animal>
                    {
                        new Animal
                        {
                            ClientId = 5,
                            Id = 1,
                            Name = "Roger"
                        },
                        new Animal
                        {
                            ClientId = 5,
                            Id = 2,
                            Name = "Biggins"
                        }
                    }
                },
                new Client
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
                    Animals = new List<Animal>
                    {
                        new Animal
                        {
                            ClientId = 5,
                            Id = 1,
                            Name = "Roger"
                        },
                        new Animal
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

            _repository.Setup(x => x.GetAllClients(It.IsAny<string>())).ReturnsAsync(clientsFromRepo);
            _mapper.Setup(x => x.Map<List<ClientToReturnDto>>(It.IsAny<List<Client>>())).Returns(clientsAfterMapping);

            //act
            var result = _service.GetAllClients(It.IsAny<string>()).Result;

            //assert
            Assert.AreEqual(clientsAfterMapping, result);
        }

        [TestMethod]
        public void InsertClient_Inserts()
        {
            //arrange
            var clientToInsert = new Client
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

            _mapper.Setup(x => x.Map<Client>(It.IsAny<ClientToInsertDto>())).Returns(clientToInsert);
            _mapper.Setup(x => x.Map<ClientToReturnDto>(It.IsAny<Client>())).Returns(clientToReturn);

            //act
            var result = _service.InsertClient(It.IsAny<ClientToInsertDto>()).Result;


            //assert
            Assert.AreEqual(JsonConvert.SerializeObject(clientToReturn), JsonConvert.SerializeObject(result));
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
