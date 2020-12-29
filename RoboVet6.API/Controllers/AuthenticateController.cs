using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RoboVet6.Data.Models.Authentication;
using RoboVet6.Service.Common.Authentication;
using RoboVet6.Service.Common.Interfaces;
using RoboVet6.Service.Common.Models.API.ApiResponse;

namespace RoboVet6.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {

        private readonly IAuthenticateService _authenticateService;

        public AuthenticateController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _authenticateService.Login(model);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                return Ok(new
                {
                    token = result.Token,
                    expiration = result.Expiration
                });
            }

            if (result.StatusCode == HttpStatusCode.Unauthorized)
            {
                return Unauthorized();
            }

            return StatusCode(500);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var result = await _authenticateService.Register(model);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                return Ok(new Response {Status = "Success", Message = "User created successfully!"});
            }

            if (result.StatusCode == HttpStatusCode.Conflict)
            {
                return Conflict(new Response {Status = "Error", Message = "User already exists!"});
            }

            return StatusCode(500, "User creation failed! Please check user details and try again.");

        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var result = await _authenticateService.RegisterAdmin(model);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                return Ok(new Response {Status = "Success", Message = "User created successfully!"});
            }

            if (result.StatusCode == HttpStatusCode.Conflict)
            {
                return Conflict(new Response {Status = "Error", Message = "User already exists!"});
            }

            return StatusCode(500,
                new Response
                    {Status = "Error", Message = "User creation failed! Please check user details and try again."});
        }
    }
}
