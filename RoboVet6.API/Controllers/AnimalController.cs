using System;
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
            try
            {
                var animals = await _animalsService.GetAllAnimals(searchQuery);

                if (animals.Count == 0)
                {
                    return NoContent();
                }

                _logger.LogInformation(JsonSerializer.Serialize(animals));
                return Ok(animals);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpGet("{animalId}", Name = "GetAnimalByAnimalId")]
        public async Task<IActionResult> GetAnimalByAnimalId(int animalId)
        {
            try
            {
                var animal = await _animalsService.GetAnimalByAnimalId(animalId);

                if (animal == null)
                {
                    return NotFound();
                }

                return Ok(animal);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost ("{clientId}")]
        public async Task<IActionResult> InsertAnimal(AnimalToInsertDto animal, int clientId)
        {
            try
            {
                var animalToReturn = await _animalsService.InsertAnimal(animal, clientId);

                if (animalToReturn == null)
                {
                    return NotFound();
                }

                return CreatedAtRoute("GetAnimalByAnimalId", new { animalId = animalToReturn.Id }, animalToReturn);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpGet]
        [Route("client/{clientId}")]
        public async Task<IActionResult> GetAnimalsByClientId(int clientId)
        {
            try
            {
                var animalsToReturn = await _animalsService.GetAnimalsByClientId(clientId);

                if (animalsToReturn == null)
                {
                    return NotFound();
                }

                if (animalsToReturn.Count == 0)
                {
                    return NoContent();
                }

                return Ok(animalsToReturn);

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
