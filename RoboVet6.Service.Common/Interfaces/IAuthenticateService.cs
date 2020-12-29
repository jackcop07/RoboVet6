using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RoboVet6.Data.Models.Authentication;
using RoboVet6.Service.Common.Models.API.ApiResponse;

namespace RoboVet6.Service.Common.Interfaces
{
    public interface IAuthenticateService
    {
        Task<ApiAuthenticateResponse> Login(LoginModel model);
        Task<ApiAuthenticateResponse> Register(RegisterModel model);
        Task<ApiAuthenticateResponse> RegisterAdmin(RegisterModel model);
    }
}
