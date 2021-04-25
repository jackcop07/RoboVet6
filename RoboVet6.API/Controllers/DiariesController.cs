using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RoboVet6.Service.Common.Interfaces.Services;
using RoboVet6.Service.Common.Models.API.Colour;
using RoboVet6.Service.Common.Models.API.Diary;

namespace RoboVet6.API.Controllers
{
    [ApiController]
    [Route("api/diaries")]
    public class DiariesController : ControllerBase
    {
        private readonly IDiariesService _diariesService;

        public DiariesController(IDiariesService diariesService)
        {
            _diariesService = diariesService
                ?? throw new ArgumentNullException(nameof(diariesService));
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<DiaryToReturnDto>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> GetDiaries(string name)
        {
            var result = await _diariesService.GetDiaries(name);

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

        [HttpGet("{diaryId}", Name = "GetDiaryByDiaryId")]
        [ProducesResponseType(200, Type = typeof(DiaryToReturnDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> GetDiaryByDiaryId(int diaryId)
        {
            var result = await _diariesService.GetDiaryByDiaryId(diaryId);

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
