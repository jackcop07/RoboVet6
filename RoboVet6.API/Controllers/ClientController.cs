using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RoboVet6.Service.Common.Authentication;
using RoboVet6.Service.Common.Interfaces;
using RoboVet6.Service.Common.Models.API.Client;

namespace RoboVet6.API.Controllers
{
    [Route("api/Client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientsService _clientsService;
        private readonly ILogger<ClientController> _logger;

        public ClientController(IClientsService clientsService, ILogger<ClientController> logger)
        {
            _clientsService = clientsService
                              ?? throw new ArgumentNullException(nameof(clientsService));
            _logger = logger
                      ?? throw new ArgumentNullException(nameof(logger));
        }
        [Authorize(Roles = UserRoles.User)]
        [HttpGet("{clientId}", Name = "GetClientByClientId")]
        public async Task<IActionResult> GetClientByClientId(int clientId)
        {
            try
            {
                var result = await _clientsService.GetClientByClientId(clientId);

                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Authorize(Roles = UserRoles.User)]
        [HttpGet]
        public async Task<IActionResult> GetClients(string searchQuery)
        {
            try
            {
                var result = await _clientsService.GetAllClients(searchQuery);

                if (result.Count > 0)
                {
                    return Ok(result);
                }

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<IActionResult> InsertClient(ClientToInsertDto client)
        {
            try
            {
                var clientToReturn = await _clientsService.InsertClient(client);

                return CreatedAtRoute("GetClientByClientId", new {clientId = clientToReturn.Id}, clientToReturn);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        //[HttpPut("{clientId}")]
        //public async Task<IActionResult> UpdateClient(Client client, int clientId)
        //{
        //    try
        //    {
        //        var updatedClient = await _clientsService.UpdateClient(clientId, client);

        //        if (updatedClient != null)
        //        {
        //            return Ok(updatedClient);
        //        }

        //        return NotFound();
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}
    }
}
