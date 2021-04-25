using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RoboVet6.DataAccess.Common.Interfaces;
using RoboVet6.Service.Common.Interfaces.Services;
using RoboVet6.Service.Common.Models.API.ApiResponse;
using RoboVet6.Service.Common.Models.API.Appointment;

namespace RoboVet6.Service.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;
        private readonly IDiaryRepository _diaryRepository;
        private readonly IAnimalRepository _animalRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository, IMapper mapper, IDiaryRepository diaryRepository, IAnimalRepository animalRepository)
        {
            _appointmentRepository = appointmentRepository
                ?? throw new ArgumentNullException(nameof(appointmentRepository));

                _mapper = mapper
                          ?? throw new ArgumentNullException(nameof(mapper));

                _diaryRepository = diaryRepository
                    ?? throw new ArgumentNullException(nameof(diaryRepository));

                _animalRepository = animalRepository
                    ?? throw new ArgumentNullException(nameof(animalRepository));
        }

        public async Task<ApiResponse<AppointmentToReturnDto>> GetAppointmentByAppointmentId(int appointmentId)
        {
            var response = new ApiResponse<AppointmentToReturnDto>();

            try
            {
                var appointmentFromRepo = await _appointmentRepository.GetAppointmentByAppointmentId(appointmentId);

                if (appointmentFromRepo == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                response.Data = _mapper.Map<AppointmentToReturnDto>(appointmentFromRepo);
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

        public async Task<ApiResponse<IEnumerable<AppointmentToReturnDto>>> GetAppointmentsByDiaryId(int diaryId)
        {
            var response = new ApiResponse<IEnumerable<AppointmentToReturnDto>>();

            try
            {
                var diaryExists = await _diaryRepository.DiaryExists(diaryId);

                if (!diaryExists)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                var appointmentsFromRepo = await _appointmentRepository.GetAppointmentsByDiaryId(diaryId);

                if (!appointmentsFromRepo.Any())
                {
                    response.StatusCode = HttpStatusCode.NoContent;
                    return response;
                }

                response.Data = _mapper.Map<IEnumerable<AppointmentToReturnDto>>(appointmentsFromRepo);
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

        public async Task<ApiResponse<IEnumerable<AppointmentToReturnDto>>> GetAppointmentsByAnimalId(int animalId)
        {
            var response = new ApiResponse<IEnumerable<AppointmentToReturnDto>>();

            try
            {
                var animalExists = await _animalRepository.AnimalExists(animalId);

                if (!animalExists)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                var appointmentsFromRepo = await _appointmentRepository.GetAppointmentsByAnimalId(animalId);

                if (!appointmentsFromRepo.Any())
                {
                    response.StatusCode = HttpStatusCode.NoContent;
                    return response;
                }

                response.Data = _mapper.Map<IEnumerable<AppointmentToReturnDto>>(appointmentsFromRepo);
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

        public async Task<ApiResponse<IEnumerable<AppointmentToReturnDto>>> GetAllAppointments()
        {
            var response = new ApiResponse<IEnumerable<AppointmentToReturnDto>>();

            try
            {
                var appointmentsFromRepo = await _appointmentRepository.GetAllAppointments();

                if (!appointmentsFromRepo.Any())
                {
                    response.StatusCode = HttpStatusCode.NoContent;
                    return response;
                }

                response.Data = _mapper.Map<IEnumerable<AppointmentToReturnDto>>(appointmentsFromRepo);
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
    }
}
