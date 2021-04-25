using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RoboVet6.Service.Common.Models.API.ApiResponse;
using RoboVet6.Service.Common.Models.API.Diary;

namespace RoboVet6.Service.Common.Interfaces.Services
{
    public interface IDiariesService
    {
        Task<ApiResponse<DiaryToReturnDto>> GetDiaryByDiaryId(int diaryId);
        Task<ApiResponse<IEnumerable<DiaryToReturnDto>>> GetDiaries(string name);
        Task<ApiResponse<DiaryToReturnDto>> InsertDiary(DiaryToInsertDto diary);
        Task<ApiResponse<DiaryToReturnDto>> UpdateDiary(int diaryId, DiaryToUpdateDto diary);
        Task<ApiResponse<bool>> DiaryExists(int diaryId);
    }
}
