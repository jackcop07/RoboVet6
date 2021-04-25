using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoboVet6.Blazor.UI.Models;

namespace RoboVet6.Blazor.UI.Interfaces.Services
{
    public interface IDiaryDataService
    {
        Task<Diary> GetDiaryByDiaryId(int diaryId);
        Task<IEnumerable<Diary>> GetAllDiaries();
    }
}
