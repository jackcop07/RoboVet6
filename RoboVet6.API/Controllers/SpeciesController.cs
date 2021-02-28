using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RoboVet6.Service.Common.Interfaces;
using RoboVet6.Service.Common.Models.API.Species;

namespace RoboVet6.API.Controllers
{
    [Route("api/Species")]
    [ApiController]
    public class SpeciesController : ControllerBase
    {
        private readonly ISpeciesService _speciesService;

        public SpeciesController(ISpeciesService speciesService)
        {
            _speciesService = speciesService
                              ?? throw new ArgumentNullException(nameof(speciesService));
        }


        [HttpGet("{speciesId}", Name = "GetSpeciesBySpeciesId")]
        [ProducesResponseType(200, Type = typeof(SpeciesToReturnDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> GetSpeciesBySpeciesId(int speciesId)
        {
            var result = await _speciesService.GetSpeciesBySpeciesId(speciesId);

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
        [ProducesResponseType(200, Type = typeof(List<SpeciesToReturnDto>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> GetSpecies(string name)
        {
            var result = await _speciesService.GetAllSpecies(name);

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
        [ProducesResponseType(201, Type = typeof(SpeciesToReturnDto))]
        [ProducesResponseType(403)]
        public async Task<IActionResult> InsertClient(SpeciesToInsertDto species)
        {
            var result = await _speciesService.InsertSpecies(species);

            if (result.StatusCode == HttpStatusCode.Created)
            {
                return CreatedAtRoute("GetSpeciesBySpeciesId", new { speciesId = result.Data.Id }, result.Data);
            }

            return StatusCode(500, result.Error);
        }

        [HttpPut("{speciesId}")]
        [ProducesResponseType(200, Type = typeof(SpeciesToReturnDto))]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> UpdateSpecies(int speciesId, SpeciesToUpdateDto updateSpecies)
        {
            var result = await _speciesService.UpdateSpecies(speciesId, updateSpecies);

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
