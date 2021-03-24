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
    public class SpeciesDataService : ISpeciesDataService
    {
        private readonly HttpClient _httpClient;

        public SpeciesDataService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("RV6Api");
        }

        public async Task<Species> GetSpeciesById(int speciesId)
        {
            return await JsonSerializer.DeserializeAsync<Species>
                (await _httpClient.GetStreamAsync($"api/species/{speciesId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<IEnumerable<Species>> GetAllSpecies()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Species>>
                (await _httpClient.GetStreamAsync("api/species"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task UpdateSpecies(Species speciesToUpdate)
        {
            throw new NotImplementedException();
        }

        public async Task<Species> AddSpecies(Species speciesToAdd)
        {
            throw new NotImplementedException();
        }
    }
}
