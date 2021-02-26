using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RoboVet6.Service.Common.Interfaces;
using RoboVet6.Service.Common.Models.API.Animal;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace RoboVet6.API.Controllers
{
    [ApiController]
    [Route("api/Animals")]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalsService _animalsService;
        private readonly ILogger<AnimalsController> _logger;

        public AnimalsController(IAnimalsService animalsService, ILogger<AnimalsController> logger)
        {
            _animalsService = animalsService 
                              ?? throw new ArgumentNullException(nameof(animalsService));
            _logger = logger
                      ?? throw new ArgumentNullException(nameof(logger));
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<AnimalToReturnDto>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> GetAnimals(string name)
        {

            var result = await _animalsService.GetAllAnimals(name);

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

        [HttpGet("{animalId}", Name = "GetAnimalByAnimalId")]
        [ProducesResponseType(200, Type = typeof(AnimalToReturnDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
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

        [HttpPost ("client/{clientId}")]
        [ProducesResponseType(201, Type = typeof(AnimalToReturnDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
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

        [HttpGet]
        [Route("client/{clientId}")]
        [ProducesResponseType(200, Type = typeof(List<AnimalToReturnDto>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
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

        [HttpPut ("{animalId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> UpdateAnimal(int animalId, AnimalToUpdateDto animalToUpdate)
        {
            var result = await _animalsService.UpdateAnimal(animalId, animalToUpdate);

            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }

            if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest(result.Error);
            }

            if (result.StatusCode == HttpStatusCode.NoContent)
            {
                return NoContent();
            }

            return StatusCode(500, result.Error);
        }
    }
}
