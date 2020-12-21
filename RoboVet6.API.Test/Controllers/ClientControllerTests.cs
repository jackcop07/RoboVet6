﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using RoboVet6.API.Controllers;
using RoboVet6.Service.Common.Interfaces;
using RoboVet6.Service.Common.Models.API.Animal;
using RoboVet6.Service.Common.Models.API.Client;

namespace RoboVet6.API.Tests.Controllers
{
    [TestClass]
    public class ClientControllerTests
    {
        private Mock<IClientsService> _clientsServiceMock;
        private Mock<ILogger<ClientController>> _loggerMock;
        private ClientController _clientController;

        private List<ClientToReturnDto> clients = new List<ClientToReturnDto>
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
                Address = "30 Innes Neuk",
                City = "Wallfyord",
                Email = "Jack.copeland7@gmail.com",
                Title = "Mr",
                FirstName = "Jack",
                LastName = "Copeland",
                HomePhone = null,
                Id = 1,
                MobilePhone = "07896600692",
                Postcode = "EH21 8EW",
                Animals = new List<AnimalToReturnDto>
                {
                    new AnimalToReturnDto
                    {
                        ClientId = 1,
                        Id = 3,
                        Name = "Slaney"
                    },
                    new AnimalToReturnDto
                    {
                        ClientId = 1,
                        Id = 4,
                        Name = "Kev"
                    }
                }
            },
            new ClientToReturnDto
            {
                Address = "19/6 Wardlaw Street",
                City = "Edinburgh",
                Email = "l.danagher@hotmail.com",
                Title = "Miss",
                FirstName = "Lauren",
                LastName = "Danagher",
                HomePhone = null,
                Id = 2,
                MobilePhone = "07893452617",
                Postcode = "EH11 1TN",
                Animals = new List<AnimalToReturnDto>
                {
                    new AnimalToReturnDto
                    {
                        ClientId = 2,
                        Id = 5,
                        Name = "Rosie"
                    },
                    new AnimalToReturnDto
                    {
                        ClientId = 2,
                        Id = 6,
                        Name = "Katie"
                    }
                }
            }
        };

        [TestInitialize]
        public void Setup()
        {
            _clientsServiceMock = new Mock<IClientsService>();
            _loggerMock = new Mock<ILogger<ClientController>>();
            _clientController = new ClientController(_clientsServiceMock.Object, _loggerMock.Object);
        }

        [TestMethod]
        public void GetClients_Returns_Populated_List()
        {
            //arrange
            _clientsServiceMock.Setup(x => x.GetAllClients("")).ReturnsAsync(clients);
            
            //act
            var result = _clientController.GetClients("").Result;
            var readResult = result as ObjectResult;

            //assert
            Assert.AreEqual(typeof(OkObjectResult), result.GetType());
            Assert.AreEqual(JsonConvert.SerializeObject(result), JsonConvert.SerializeObject(readResult));
        }

        [TestMethod]
        public void GetClients_Returns_Empty_List()
        {
            //arrange
            var emptyClients = new List<ClientToReturnDto>();
            _clientsServiceMock.Setup(x => x.GetAllClients("")).ReturnsAsync(emptyClients);
            
            //act
            var result = _clientController.GetClients("").Result;

            //assert
            Assert.AreEqual(typeof(NoContentResult), result.GetType());
         }

        [TestMethod]
        public void GetClients_Returns_Internal_Server_Error()
        {
            //arrange
            _clientsServiceMock.Setup(x => x.GetAllClients("")).Throws(new ArgumentException("Throwing exception."));

            //act
            var result = _clientController.GetClients("").Result;
            var readResult = result as ObjectResult;

            //assert
            Assert.AreEqual(500, readResult.StatusCode);
        }

        [TestMethod]
        public void GetClienByClientId_Returns_Client()
        {
            //arrange
            var client = new ClientToReturnDto
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

            _clientsServiceMock.Setup(x => x.GetClientByClientId(1)).ReturnsAsync(client);

            //act
            var result = _clientController.GetClientByClientId(1).Result;
            var readResult = result as ObjectResult;

            //assert
            Assert.AreEqual(typeof(OkObjectResult), result.GetType());
            Assert.AreEqual(JsonConvert.SerializeObject(result), JsonConvert.SerializeObject(readResult));
        }

        [TestMethod]
        public void GetClienByClientId_Returns_NotFound()
        {
            //arrange
            _clientsServiceMock.Setup(x => x.GetClientByClientId(1)).ReturnsAsync(() => null);

            //act
            var result = _clientController.GetClientByClientId(1).Result;

            //assert
            Assert.AreEqual(typeof(NotFoundResult), result.GetType());

        }

        [TestMethod]
        public void GetClientByClientId_Returns_Internal_Server_Error()
        {
            //arrange
            _clientsServiceMock.Setup(x => x.GetClientByClientId(1)).Throws(new ArgumentException("Throwing exception."));

            //act
            var result = _clientController.GetClientByClientId(1).Result;
            var readResult = result as ObjectResult;

            //assert
            Assert.AreEqual(500, readResult.StatusCode);
        }

        [TestMethod]
        public void InsertClient_Returns_Inserted()
        {
            //arrange
            var clientToInsert = new ClientToInsertDto
            {
                Address = "12 Livingstone Place",
                City = "Lockerbie",
                Email = "Sampcop49@gmail.com",
                Title = "Mr",
                FirstName = "Sam",
                LastName = "Copeland",
                HomePhone = "01576 202321",
                MobilePhone = "07986633452",
                Postcode = "DG11 2AU",
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
                Postcode = "DG11 2AU",
                Animals = null
            };

            _clientsServiceMock.Setup(x => x.InsertClient(clientToInsert)).ReturnsAsync(clientToReturn);

            //act
            var result = _clientController.InsertClient(clientToInsert).Result;
            var readResult = result as ObjectResult;

            //assert
            Assert.AreEqual(typeof(CreatedAtRouteResult), result.GetType());
            Assert.AreEqual(JsonConvert.SerializeObject(result), JsonConvert.SerializeObject(readResult));
        }


        [TestMethod]
        public void InsertClient_Returns_Internal_Server_Error()
        {
            //arrange
            var clientToInsert = new ClientToInsertDto
            {
                Address = "12 Livingstone Place",
                City = "Lockerbie",
                Email = "Sampcop49@gmail.com",
                Title = "Mr",
                FirstName = "Sam",
                LastName = "Copeland",
                HomePhone = "01576 202321",
                MobilePhone = "07986633452",
                Postcode = "DG11 2AU",
            };
            _clientsServiceMock.Setup(x => x.InsertClient(clientToInsert)).Throws(new ArgumentException("Throwing exception."));

            //act
            var result = _clientController.InsertClient(clientToInsert).Result;
            var readResult = result as ObjectResult;

            //assert
            Assert.AreEqual(500, readResult.StatusCode);
        }
    }
}