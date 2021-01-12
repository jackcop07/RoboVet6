using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RoboVet6.Service.Common.Authentication;
using RoboVet6.Service.Common.Interfaces;
using RoboVet6.Service.Common.Models.API.Client;

namespace RoboVet6.API.Controllers
{
    [Route("api/Clients")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientsService _clientsService;
        private readonly ILogger<ClientsController> _logger;

        public ClientsController(IClientsService clientsService, ILogger<ClientsController> logger)
        {
            _clientsService = clientsService
                              ?? throw new ArgumentNullException(nameof(clientsService));
            _logger = logger
                      ?? throw new ArgumentNullException(nameof(logger));
        }
        [Authorize(Roles = UserRoles.User)]
        [HttpGet("{clientId}", Name = "GetClientByClientId")]
        [ProducesResponseType(200, Type = typeof(ClientToReturnDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> GetClientByClientId(int clientId)
        {
            var result = await _clientsService.GetClientByClientId(clientId);

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

        //[Authorize(Roles = UserRoles.User)]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<ClientToReturnDto>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> GetClients(string lastName, string address, string phone, string email)
        {

            var result = await _clientsService.GetAllClients(lastName, address, phone, email);

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

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ClientToReturnDto))]
        [ProducesResponseType(403)]
        public async Task<IActionResult> InsertClient(ClientToInsertDto client)
        {
            var result = await _clientsService.InsertClient(client);

            if (result.StatusCode == HttpStatusCode.Created)
            {
                return CreatedAtRoute("GetClientByClientId", new { clientId = result.Data.Id }, result.Data);
            }

            return StatusCode(500, result.Error);
        }

        [HttpPut ("{clientId}")]
        [ProducesResponseType(200, Type = typeof(ClientToReturnDto))]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> UpdateClient(int clientId, ClientToUpdateDto updateClient)
        {
            var result = await _clientsService.UpdateClient(clientId, updateClient);

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
