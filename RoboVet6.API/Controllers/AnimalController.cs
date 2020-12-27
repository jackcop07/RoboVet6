using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RoboVet6.Service.Common.Authentication;
using RoboVet6.Service.Common.Interfaces;
using RoboVet6.Service.Common.Models.API.Animal;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace RoboVet6.API.Controllers
{
    [ApiController]
    [Route("api/Animal")]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalsService _animalsService;
        private readonly ILogger<AnimalController> _logger;

        public AnimalController(IAnimalsService animalsService, ILogger<AnimalController> logger)
        {
            _animalsService = animalsService 
                              ?? throw new ArgumentNullException(nameof(animalsService));
            _logger = logger
                      ?? throw new ArgumentNullException(nameof(logger));
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpGet]
        public async Task<IActionResult> GetAnimals(string searchQuery)
        {

            var result = await _animalsService.GetAllAnimals(searchQuery);

            if (result.StatusCode == HttpStatusCode.NoContent)
            {
                return NoContent();
            }

            if (result.StatusCode == HttpStatusCode.OK)
            {
                _logger.LogInformation(JsonSerializer.Serialize(result.Data));
                return Ok(result.Data);
            }

            return StatusCode(500, result.Error);

        }

        [Authorize(Roles = UserRoles.User)]
        [HttpGet("{animalId}", Name = "GetAnimalByAnimalId")]
        public async Task<IActionResult> GetAnimalByAnimalId(int animalId)
        {

            var result = await _animalsService.GetAnimalByAnimalId(animalId);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                return Ok(result.Data);
            }

            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }

            return StatusCode(500, result.Error);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost ("{clientId}")]
        public async Task<IActionResult> InsertAnimal(AnimalToInsertDto animal, int clientId)
        {
            
            var result = await _animalsService.InsertAnimal(animal, clientId);

            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }

            if (result.StatusCode == HttpStatusCode.Created)
            {
                return CreatedAtRoute("GetAnimalByAnimalId", new { animalId = result.Data.Id }, result.Data);
            }

            return StatusCode(500, result.Error);

        }

        [Authorize(Roles = UserRoles.User)]
        [HttpGet]
        [Route("client/{clientId}")]
        public async Task<IActionResult> GetAnimalsByClientId(int clientId)
        {

            var result = await _animalsService.GetAnimalsByClientId(clientId);

            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }

            if (result.StatusCode == HttpStatusCode.NoContent)
            {
                return NoContent();
            }

            if (result.StatusCode == HttpStatusCode.OK)
            {
                return Ok(result.Data);
            }

            return StatusCode(500, result.Error);
        }
    }
}
