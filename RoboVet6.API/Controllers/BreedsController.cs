using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RoboVet6.Service.Common.Interfaces;
using RoboVet6.Service.Common.Models.API.Breed;
using RoboVet6.Service.Common.Models.API.Species;

namespace RoboVet6.API.Controllers
{
    [Route("api/breeds")]
    [ApiController]
    public class BreedsController : ControllerBase
    {
        private readonly IBreedService _breedService;

        public BreedsController(IBreedService breedService)
        {
            _breedService = breedService
                              ?? throw new ArgumentNullException(nameof(breedService));
        }


        [HttpGet("{breedId}", Name = "GetBreedByBreedId")]
        [ProducesResponseType(200, Type = typeof(BreedToReturnDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> GetBreedByBreedId(int breedId)
        {
            var result = await _breedService.GetBreedByBreedId(breedId);

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

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<BreedToReturnDto>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> GetBreeds(string name)
        {
            var result = await _breedService.GetAllBreeds(name);

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

        [HttpGet("species/{speciesId}")]
        [ProducesResponseType(200, Type = typeof(List<BreedToReturnDto>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> GetBreedsBySpeciesId(int speciesId)
        {
            var result = await _breedService.GetAllBreedsBySpeciesId(speciesId);

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

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(BreedToReturnDto))]
        [ProducesResponseType(403)]
        public async Task<IActionResult> InsertBreed(BreedToInsertDto breed)
        {
            var result = await _breedService.InsertBreed(breed);

            if (result.StatusCode == HttpStatusCode.Created)
            {
                return CreatedAtRoute("GetBreedByBreedId", new { breedId = result.Data.Id }, result.Data);
            }

            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }

            return StatusCode(500, result.Error);
        }

        [HttpPut("{breedId}")]
        [ProducesResponseType(200, Type = typeof(BreedToReturnDto))]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> UpdateBreed(int breedId, BreedToUpdateDto updateBreed)
        {
            var result = await _breedService.UpdateBreed(breedId, updateBreed);

            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }

            if (result.StatusCode == HttpStatusCode.NoContent)
            {
                return NoContent();
            }

            return StatusCode(500, result.Error);
        }
        
    }
}
