using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace RoboVet6.Service.Common.Models.API.ApiResponse
{
    public class ApiResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public T Data { get; set; }
        public string Error { get; set; }
    }
}
