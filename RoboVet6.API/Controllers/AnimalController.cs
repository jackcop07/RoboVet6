using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RoboVet6.Service.Common.Interfaces;
using RoboVet6.Service.Common.Models.API;
using RoboVet6.Service.Common.Models.API.Animal;

namespace RoboVet6.API.Controllers
{
    [ApiController]
    [Route("api/Animal")]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalsService _animalsService;

        public AnimalController(IAnimalsService animalsService)
        {
            _animalsService = animalsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAnimals()
        {
            try
            {
                var animals = await _animalsService.GetAllAnimals();

                if (animals.Count == 0)
                {
                    return NoContent();
                }

                return Ok(animals);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

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

        [HttpPost ("{clientId}")]
        public async Task<IActionResult> InsertAnimal(Animal animal, int clientId)
        {
            try
            {
                var animalToReturn = await _animalsService.InsertAnimal(animal, clientId);

                if (animalToReturn == null)
                {
                    return NotFound();
                }

                return CreatedAtRoute("GetAnimalByAnimalId", new { animalId = animal.Id }, animalToReturn);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
