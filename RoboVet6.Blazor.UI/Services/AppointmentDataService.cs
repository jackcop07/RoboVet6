using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using RoboVet6.Blazor.UI.Interfaces.Services;
using RoboVet6.Blazor.UI.Models;

namespace RoboVet6.Blazor.UI.Services
{
    public class AppointmentDataService : IAppointmentDataService
    {
        private readonly HttpClient _httpClient;

        public AppointmentDataService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("RV6Api");
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointments()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Appointment>>
                (await _httpClient.GetStreamAsync("api/appointments"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByDiaryId(int diaryId)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Appointment>>
                (await _httpClient.GetStreamAsync($"api/appointments/diary/{diaryId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByAnimalId(int animalId)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Appointment>>
                (await _httpClient.GetStreamAsync($"api/appointments/animal/{animalId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        }

        public async Task<Appointment> GetAppointmentByAppointmentId(int appointmentId)
        {
            return await JsonSerializer.DeserializeAsync<Appointment>
                (await _httpClient.GetStreamAsync($"api/appointments/{appointmentId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        }
    }
}
