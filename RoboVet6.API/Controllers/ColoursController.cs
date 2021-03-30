using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RoboVet6.Service.Common.Interfaces.Services;
using RoboVet6.Service.Common.Models.API.Colour;
using RoboVet6.Service.Services;

namespace RoboVet6.API.Controllers
{
    [ApiController]
    [Route("api/colours")]
    public class ColoursController : ControllerBase
    {
        private readonly IColourService _colourService;

        public ColoursController(IColourService colourService)
        {
            _colourService = colourService
                ?? throw new ArgumentException(nameof(colourService));
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<ColourToReturnDto>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> GetColours(string name)
        {
            var result = await _colourService.GetAllColours(name);

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

        [HttpGet("{colourId}", Name = "GetColourByColourId")]
        [ProducesResponseType(200, Type = typeof(ColourToReturnDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> GetColourByColourId(int colourId)
        {
            var result = await _colourService.GetColourByColourId(colourId);

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
    }
}
