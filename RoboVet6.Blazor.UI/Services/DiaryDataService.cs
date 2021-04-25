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
    public class DiaryDataService : IDiaryDataService
    {

        private readonly HttpClient _httpClient;

        public DiaryDataService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("RV6Api");
        }

        public async Task<Diary> GetDiaryByDiaryId(int diaryId)
        {
            return await JsonSerializer.DeserializeAsync<Diary>
                (await _httpClient.GetStreamAsync($"api/diaries/{diaryId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        }

        public async Task<IEnumerable<Diary>> GetAllDiaries()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Diary>>
                (await _httpClient.GetStreamAsync("api/diaries"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        }
    }
}
