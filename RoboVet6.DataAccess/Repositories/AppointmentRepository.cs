using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RoboVet6.Data.DbContext;
using RoboVet6.Data.Models.RoboVet6;
using RoboVet6.DataAccess.Common.Interfaces;

namespace RoboVet6.DataAccess.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AppointmentModel>> GetAllAppointments()
        {
            return await _context.Appointments.ToListAsync();
        }

        public async Task<IEnumerable<AppointmentModel>> GetAppointmentsByAnimalId(int animalId)
        {
            return await _context.Appointments.Where(x => x.AnimalId == animalId).ToListAsync();
        }

        public async Task<IEnumerable<AppointmentModel>> GetAppointmentsByDiaryId(int diaryId)
        {
            return await _context.Appointments.Where(x => x.DiaryId == diaryId).ToListAsync();
        }

        public async Task<AppointmentModel> GetAppointmentByAppointmentId(int appointmentId)
        {
            return await _context.Appointments.FirstOrDefaultAsync(x => x.Id == appointmentId);
        }
    }
}
