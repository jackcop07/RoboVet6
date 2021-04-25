using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RoboVet6.Service.Common.Models.API.ApiResponse;
using RoboVet6.Service.Common.Models.API.Appointment;

namespace RoboVet6.Service.Common.Interfaces.Services
{
    public interface IAppointmentService
    {
        Task<ApiResponse<AppointmentToReturnDto>> GetAppointmentByAppointmentId(int appointmentId);
        Task<ApiResponse<IEnumerable<AppointmentToReturnDto>>> GetAppointmentsByDiaryId(int diaryId);
        Task<ApiResponse<IEnumerable<AppointmentToReturnDto>>> GetAppointmentsByAnimalId(int animalId);
        Task<ApiResponse<IEnumerable<AppointmentToReturnDto>>> GetAllAppointments();
    }
}
