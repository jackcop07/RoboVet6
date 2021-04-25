using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RoboVet6.Data.Models.RoboVet6;
using RoboVet6.DataAccess.Common.Interfaces;
using RoboVet6.Service.Common.Interfaces.Services;
using RoboVet6.Service.Common.Models.API.ApiResponse;
using RoboVet6.Service.Common.Models.API.Diary;

namespace RoboVet6.Service.Services
{
    public class DiariesService : IDiariesService
    {
        private readonly IDiaryRepository _diaryRepository;
        private readonly IMapper _mapper;

        public DiariesService(IDiaryRepository diaryRepository, IMapper mapper)
        {
            _diaryRepository = diaryRepository
                               ?? throw new ArgumentNullException(nameof(diaryRepository));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ApiResponse<DiaryToReturnDto>> GetDiaryByDiaryId(int diaryId)
        {
            var response = new ApiResponse<DiaryToReturnDto>();

            try
            {
                var diaryFromRepo = await _diaryRepository.GetDiaryByDiaryId(diaryId);

                if (diaryFromRepo == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                var diaryToReturn = _mapper.Map<DiaryToReturnDto>(diaryFromRepo);

                response.StatusCode = HttpStatusCode.OK;
                response.Data = diaryToReturn;

                return response;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Error = e.Message;

                return response;
            }
        }

        public async Task<ApiResponse<IEnumerable<DiaryToReturnDto>>> GetDiaries(string name)
        {
            var response = new ApiResponse<IEnumerable<DiaryToReturnDto>>();

            try
            {
                var diariesFromRepo = await _diaryRepository.GetAllDiaries(name);

                if (diariesFromRepo == null)
                {
                    response.StatusCode = HttpStatusCode.NoContent;
                    return response;
                }

                var diariesToReturn = _mapper.Map<IEnumerable<DiaryToReturnDto>>(diariesFromRepo);

                response.Data = diariesToReturn;
                response.StatusCode = HttpStatusCode.OK;

                return response;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Error = e.Message;

                return response;
            }
        }

        public async Task<ApiResponse<DiaryToReturnDto>> InsertDiary(DiaryToInsertDto diary)
        {
            var response = new ApiResponse<DiaryToReturnDto>();

            try
            {
                var diaryToInsert = _mapper.Map<DiaryModel>(diary);

                await _diaryRepository.InsertDiary(diaryToInsert);

                var diaryToReturn = _mapper.Map<DiaryToReturnDto>(diaryToInsert);

                response.StatusCode = HttpStatusCode.Created;
                response.Data = diaryToReturn;

                return response;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Error = e.Message;

                return response;
            }
        }

        public async Task<ApiResponse<DiaryToReturnDto>> UpdateDiary(int diaryId, DiaryToUpdateDto diary)
        {
            var response = new ApiResponse<DiaryToReturnDto>();

            try
            {
                var diaryFromRepo = await _diaryRepository.GetDiaryByDiaryId(diaryId);

                if (diaryFromRepo == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                _mapper.Map(diary, diaryFromRepo);

                await _diaryRepository.UpdateDiary(diaryFromRepo);

                response.StatusCode = HttpStatusCode.NoContent;

                return response;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Error = e.Message;

                return response;
            }
        }

        public async Task<ApiResponse<bool>> DiaryExists(int diaryId)
        {
            var response = new ApiResponse<bool>();

            try
            {
                var diaryExists = await _diaryRepository.DiaryExists(diaryId);

                response.Data = diaryExists;

                return response;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Error = e.Message;

                return response;
            }
        }
    }
}
