using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace RoboVet6.Service.Common.Models.API.ApiResponse
{
    public class ApiAuthenticateResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
