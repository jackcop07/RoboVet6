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
    public class BreedDataService : IBreedDataService 
    {
        private readonly HttpClient _httpClient;

        public BreedDataService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("RV6Api");
        }


        public async Task<Breed> GetBreedById(int breedId)
        {
            return await JsonSerializer.DeserializeAsync<Breed>
                (await _httpClient.GetStreamAsync($"api/breeds/{breedId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<IEnumerable<Breed>> GetBreedsBySpeciesId(int speciesId)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Breed>>
                (await _httpClient.GetStreamAsync($"api/breeds/species/{speciesId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<IEnumerable<Breed>> GetAllBreeds()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Breed>>
                (await _httpClient.GetStreamAsync("api/breeds"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task UpdateBreed(Breed breedToUpdate)
        {
            throw new NotImplementedException();
        }

        public async Task<Breed> AddBreed(Breed breedToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
