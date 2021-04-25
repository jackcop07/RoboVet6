using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using RoboVet6.Data.Models.RoboVet6;
using RoboVet6.DataAccess.Common.Interfaces;
using RoboVet6.Service.Common.Interfaces.Services;
using RoboVet6.Service.Common.Models.API.ApiResponse;
using RoboVet6.Service.Common.Models.API.Colour;

namespace RoboVet6.Service.Services
{
    public class ColoursService : IColourService
    {
        private readonly IColourRepository _colourRepository;
        private readonly IMapper _mapper;

        public ColoursService(IColourRepository colourRepository, IMapper mapper)
        {
            _colourRepository = colourRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<ColourToReturnDto>> GetColourByColourId(int colourId)
        {
            var response = new ApiResponse<ColourToReturnDto>();

            try
            {
                var colourFromRepo = await _colourRepository.GetColourById(colourId);

                if (colourFromRepo == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                var colourToReturn = _mapper.Map<ColourToReturnDto>(colourFromRepo);

                response.StatusCode = HttpStatusCode.OK;
                response.Data = colourToReturn;
                return response;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<ApiResponse<List<ColourToReturnDto>>> GetAllColours(string name)
        {
            var response = new ApiResponse<List<ColourToReturnDto>>();

            try
            {
                var coloursFromRepo = await _colourRepository.GetAllColours(name);

                if (!coloursFromRepo.Any())
                {
                    response.StatusCode = HttpStatusCode.NoContent;
                    return response;
                }

                var coloursToReturn = _mapper.Map<List<ColourToReturnDto>>(coloursFromRepo);
                response.Data = coloursToReturn;
                response.StatusCode = HttpStatusCode.OK;

                return response;

            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<ApiResponse<ColourToReturnDto>> InsertColour(ColourToInsertDto colourToInsert)
        {
            var response = new ApiResponse<ColourToReturnDto>();

            try
            {
                var colourMapped = _mapper.Map<ColourModel>(colourToInsert);

                await _colourRepository.InsertColour(colourMapped);

                response.StatusCode = HttpStatusCode.Created;
                response.Data = _mapper.Map<ColourToReturnDto>(colourMapped);

                return response;
            }
            catch (Exception e)
            {
                response.Error = e.Message;
                response.StatusCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<bool> ColourExists(int colourId)
        {
            return await _colourRepository.ColourExists(colourId);
        }

        public async Task<ApiResponse<ColourToReturnDto>> UpdateColour(int colourId, ColourToUpdateDto colour)
        {
            var response = new ApiResponse<ColourToReturnDto>();

            try
            {
                var colourFromRepo = await _colourRepository.GetColourById(colourId);

                if (colourFromRepo == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                _mapper.Map(colour, colourFromRepo);

                await _colourRepository.UpdateColour(colourFromRepo);

                response.StatusCode = HttpStatusCode.NoContent;
                return response;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }
    }
}
