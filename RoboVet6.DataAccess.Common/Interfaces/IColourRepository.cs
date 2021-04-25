using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RoboVet6.Data.Models.RoboVet6;

namespace RoboVet6.DataAccess.Common.Interfaces
{
    public interface IColourRepository
    {
        Task<List<ColourModel>> GetAllColours(string searchTerm);
        Task<ColourModel> GetColourById(int colourId);
        Task UpdateColour(ColourModel colour);
        Task InsertColour(ColourModel colour);
        Task<bool> ColourExists(int colourId);
    }
}
