using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RoboVet6.Data.Models.RoboVet6;

namespace RoboVet6.DataAccess.Common.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<AppointmentModel>> GetAllAppointments();
        Task<IEnumerable<AppointmentModel>> GetAppointmentsByAnimalId(int animalId);
        Task<IEnumerable<AppointmentModel>> GetAppointmentsByDiaryId(int diaryId);
        Task<AppointmentModel> GetAppointmentByAppointmentId(int appointmentId);
    }
}
