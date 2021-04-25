using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoboVet6.Blazor.UI.Models;

namespace RoboVet6.Blazor.UI.Interfaces.Services
{
    public interface IAppointmentDataService
    {
        Task<IEnumerable<Appointment>> GetAllAppointments();
        Task<IEnumerable<Appointment>> GetAppointmentsByDiaryId(int diaryId);
        Task<IEnumerable<Appointment>> GetAppointmentsByAnimalId(int animalId);
        Task<Appointment> GetAppointmentByAppointmentId(int appointmentId);
    }
}
