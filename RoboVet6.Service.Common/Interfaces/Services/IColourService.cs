using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RoboVet6.Service.Common.Models.API.ApiResponse;
using RoboVet6.Service.Common.Models.API.Colour;
using RoboVet6.Service.Common.Models.API.Species;

namespace RoboVet6.Service.Common.Interfaces.Services
{
    public interface IColourService
    {
        Task<ApiResponse<ColourToReturnDto>> GetColourByColourId(int colourId);
        Task<ApiResponse<List<ColourToReturnDto>>> GetAllColours(string name);
        Task<ApiResponse<ColourToReturnDto>> InsertColour(ColourToInsertDto colourToInsert);
        Task<bool> ColourExists(int colourId);
        Task<ApiResponse<ColourToReturnDto>> UpdateColour(int colourId, ColourToUpdateDto colour);
    }
}
