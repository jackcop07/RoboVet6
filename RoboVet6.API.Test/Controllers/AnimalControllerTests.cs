using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using RoboVet6.API.Controllers;
using RoboVet6.Service.Common.Interfaces;
using RoboVet6.Service.Common.Models.API.Animal;
using RoboVet6.Service.Common.Models.API.ApiResponse;

namespace RoboVet6.API.Tests.Controllers
{
    [TestClass]
    public class AnimalControllerTests
    {
        private AnimalsController _controller;
        private Mock<IAnimalsService> _service;
        private Mock<ILogger<AnimalsController>> _logger;

        [TestInitialize]
        public void Setup()
        {
            _service = new Mock<IAnimalsService>();
            _logger = new Mock<ILogger<AnimalsController>>();
            _controller = new AnimalsController(_service.Object, _logger.Object);
        }

        [TestMethod]
        public void GetAnimals_Returns_List_Ok()
        {
            //arrange
            var animalsToReturn = new List<AnimalToReturnDto>
            {
                new AnimalToReturnDto
                {
                    Id = 1,
                    Name = "Peter",
                    ClientId = 1
                },
                new AnimalToReturnDto
                {
                    Id = 2,
                    Name = "Ronald",
                    ClientId = 1
                },
                new AnimalToReturnDto
                {
                    Id = 3,
                    Name = "Henry",
                    ClientId = 2
                }
            };

            var mockResponse = new ApiResponse<List<AnimalToReturnDto>>();
            mockResponse.StatusCode = HttpStatusCode.OK;
            mockResponse.Data = animalsToReturn;

            _service.Setup(x => x.GetAllAnimals("")).ReturnsAsync(mockResponse);

            //act
            var result = _controller.GetAnimals("").Result;
            var readResult = result as ObjectResult;

            //assert
            Assert.AreEqual(typeof(OkObjectResult), readResult.GetType());
            Assert.AreEqual(JsonConvert.SerializeObject(animalsToReturn), JsonConvert.SerializeObject(readResult.Value));
        }

        [TestMethod]
        public void GetAnimals_Returns_Empty_List()
        {
            //arrange
            var mockResponse = new ApiResponse<List<AnimalToReturnDto>>();
            mockResponse.StatusCode = HttpStatusCode.NoContent;

            _service.Setup(x => x.GetAllAnimals("")).ReturnsAsync(mockResponse);

            //act
            var result = _controller.GetAnimals("").Result;
            var statusCodeResult = (IStatusCodeActionResult) result;

            //assert
            Assert.AreEqual(StatusCodes.Status204NoContent, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public void GetAnimals_Returns_Internal_Server_Error()
        {
            //arrange
            var mockResponse = new ApiResponse<List<AnimalToReturnDto>>();
            mockResponse.StatusCode = HttpStatusCode.InternalServerError;

            _service.Setup(x => x.GetAllAnimals("")).ReturnsAsync(mockResponse);

            //act
            var result = _controller.GetAnimals("").Result;
            var readResult = result as ObjectResult;

            //assert
            Assert.AreEqual(StatusCodes.Status500InternalServerError, readResult.StatusCode);
        }

        [TestMethod]
        public void GetAnimalByAnimalId_Returns_Animal_Ok()
        {
            //arrange
            var animalToReturn = new AnimalToReturnDto
            {
                Id = 1,
                Name = "Jamie",
                ClientId = 2
            };

            var mockResponse = new ApiResponse<AnimalToReturnDto>();
            mockResponse.Data = animalToReturn;
            mockResponse.StatusCode = HttpStatusCode.OK;

            _service.Setup(x => x.GetAnimalByAnimalId(1)).ReturnsAsync(mockResponse);

            //act
            var result = _controller.GetAnimalByAnimalId(1).Result;
            var readResult = result as ObjectResult;

            //assert
            Assert.AreEqual(StatusCodes.Status200OK, readResult.StatusCode);
            Assert.AreEqual(JsonConvert.SerializeObject(animalToReturn), JsonConvert.SerializeObject(readResult.Value));
        }

        [TestMethod]
        public void GetAnimalByAnimalId_Returns_Animal_NotFound()
        {
            //arrange
            var mockResponse = new ApiResponse<AnimalToReturnDto>();
            mockResponse.StatusCode = HttpStatusCode.NotFound;

            _service.Setup(x => x.GetAnimalByAnimalId(1)).ReturnsAsync(mockResponse);

            //act
            var result = _controller.GetAnimalByAnimalId(1).Result;
            var statusCodeInfo = (IStatusCodeActionResult) result;

            //assert
            Assert.AreEqual(StatusCodes.Status404NotFound, statusCodeInfo.StatusCode);
        }

        [TestMethod]
        public void GetAnimalByAnimalId_Returns_Internal_Server_Error()
        {
            //arrange
            var mockResponse = new ApiResponse<AnimalToReturnDto>();
            mockResponse.StatusCode = HttpStatusCode.InternalServerError;

            _service.Setup(x => x.GetAnimalByAnimalId(1)).ReturnsAsync(mockResponse);

            //act
            var result = _controller.GetAnimalByAnimalId(1).Result;
            var statusCodeInfo = (IStatusCodeActionResult) result;

            //assert
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeInfo.StatusCode);
        }

        [TestMethod]
        public void GetAnimalsByClientId_Returns_Animal_Ok()
        {
            //arrange
            var animalsToReturn = new List<AnimalToReturnDto>
            {
                new AnimalToReturnDto
                {
                    Id = 1,
                    Name = "Jamie",
                    ClientId = 2
                },
                new AnimalToReturnDto
                {
                    Id = 2,
                    Name = "Gary",
                    ClientId = 2
                },
                new AnimalToReturnDto
                {
                    Id = 3,
                    Name = "Scott",
                    ClientId = 3
                },
            };

            var mockService = new ApiResponse<List<AnimalToReturnDto>>();
            mockService.Data = animalsToReturn;
            mockService.StatusCode = HttpStatusCode.OK;

            _service.Setup(x => x.GetAnimalsByClientId(1)).ReturnsAsync(mockService);

            //act
            var result = _controller.GetAnimalsByClientId(1).Result;
            var readResult = result as ObjectResult;

            //assert
            Assert.AreEqual(StatusCodes.Status200OK, readResult.StatusCode);
            Assert.AreEqual(JsonConvert.SerializeObject(animalsToReturn), JsonConvert.SerializeObject(readResult.Value));
        }

        [TestMethod]
        public void GetAnimalsByClientId_Returns_Animal_NoContent()
        {
            //arrange
            var mockResponse = new ApiResponse<List<AnimalToReturnDto>>();
            mockResponse.StatusCode = HttpStatusCode.NoContent;

            _service.Setup(x => x.GetAnimalsByClientId(1)).ReturnsAsync(mockResponse);

            //act
            var result = _controller.GetAnimalsByClientId(1).Result;
            var statusCodeInfo = (IStatusCodeActionResult)result;

            //assert
            Assert.AreEqual(StatusCodes.Status204NoContent, statusCodeInfo.StatusCode);
        }

        [TestMethod]
        public void GetAnimalsByClientId_Returns_Animal_NotFound()
        {
            //arrange
            var mockResponse  =new ApiResponse<List<AnimalToReturnDto>>();
            mockResponse.StatusCode = HttpStatusCode.NotFound;

            _service.Setup(x => x.GetAnimalsByClientId(1)).ReturnsAsync(mockResponse);

            //act
            var result = _controller.GetAnimalsByClientId(1).Result;
            var statusCodeInfo = (IStatusCodeActionResult)result;

            //assert
            Assert.AreEqual(StatusCodes.Status404NotFound, statusCodeInfo.StatusCode);
        }

        [TestMethod]
        public void GetAnimalsByClientlId_Returns_Internal_Server_Error()
        {
            //arrange
            var mockResponse = new ApiResponse<List<AnimalToReturnDto>>();
            mockResponse.StatusCode = HttpStatusCode.InternalServerError;

            _service.Setup(x => x.GetAnimalsByClientId(1)).ReturnsAsync(mockResponse);

            //act
            var result = _controller.GetAnimalsByClientId(1).Result;
            var statusCodeInfo = (IStatusCodeActionResult)result;

            //assert
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeInfo.StatusCode);
        }

        [TestMethod]
        public void InsertAnimal_Returns_Success()
        {
            //arrange
            var animalToInsert = new AnimalToInsertDto
            {
                Name = "Harry"
            };

            var animalToReturn = new AnimalToReturnDto
            {
                Id = 1,
                Name = "Harry",
                ClientId = 1
            };

            var mockResponse = new ApiResponse<AnimalToReturnDto>();
            mockResponse.StatusCode = HttpStatusCode.Created;
            mockResponse.Data = animalToReturn;

            _service.Setup(x => x.InsertAnimal(animalToInsert, 1)).ReturnsAsync(mockResponse);

            //act
            var result = _controller.InsertAnimal(animalToInsert, 1).Result;
            var readResult = result as ObjectResult;

            //assert
            Assert.AreEqual(StatusCodes.Status201Created, readResult.StatusCode);
            Assert.AreEqual(JsonConvert.SerializeObject(animalToReturn), JsonConvert.SerializeObject(readResult.Value));
        }

        [TestMethod]
        public void InsertAnimal_Returns_NotFound()
        {
            //arrange
            var animalToInsert = new AnimalToInsertDto
            {
                Name = "Harry"
            };

            var mockResponse = new ApiResponse<AnimalToReturnDto>();
            mockResponse.StatusCode = HttpStatusCode.NotFound;

            _service.Setup(x => x.InsertAnimal(animalToInsert, 44)).ReturnsAsync(mockResponse);

            //act
            var result = _controller.InsertAnimal(animalToInsert, 44).Result;
            var statusCodeInfo = (IStatusCodeActionResult) result;

            //assert
            Assert.AreEqual(StatusCodes.Status404NotFound, statusCodeInfo.StatusCode);
        }

        [TestMethod]
        public void InsertAnimal_Throws_Internal_Server_Error()
        {
            //arrange
            var mockResponse = new ApiResponse<AnimalToReturnDto>();
            mockResponse.StatusCode = HttpStatusCode.InternalServerError;

            _service.Setup(x => x.InsertAnimal(It.IsAny<AnimalToInsertDto>(), It.IsAny<Int32>()))
                .ReturnsAsync(mockResponse);


            //act
            var result = _controller.InsertAnimal(It.IsAny<AnimalToInsertDto>(), It.IsAny<Int32>()).Result;
            var resultResult = result as ObjectResult;


            //assert
            Assert.AreEqual(StatusCodes.Status500InternalServerError, resultResult.StatusCode);
        }

    }
}
