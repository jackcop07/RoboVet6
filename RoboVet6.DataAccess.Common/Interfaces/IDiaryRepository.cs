using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RoboVet6.Data.Models.RoboVet6;

namespace RoboVet6.DataAccess.Common.Interfaces
{
    public interface IDiaryRepository
    {
        Task<IEnumerable<DiaryModel>> GetAllDiaries(string name);
        Task<DiaryModel> GetDiaryByDiaryId(int diaryId);
        Task UpdateDiary(DiaryModel diary);
        Task InsertDiary(DiaryModel diary);
        Task<bool> DiaryExists(int diaryId);
    }
}
