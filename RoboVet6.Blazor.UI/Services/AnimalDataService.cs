using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RoboVet6.Blazor.UI.Interfaces.Services;
using RoboVet6.Blazor.UI.Models;

namespace RoboVet6.Blazor.UI.Services
{
    public class AnimalDataService : IAnimalDataService
    {
        private readonly HttpClient _httpClient;

        public AnimalDataService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("RV6Api");
        }

        public async Task<Animal> GetAnimalById(int animalId)
        {
            return await JsonSerializer.DeserializeAsync<Animal>
                (await _httpClient.GetStreamAsync($"api/animals/{animalId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task UpdateAnimal(Animal animalToUpdate)
        {
            var animalJson =
                new StringContent(JsonSerializer.Serialize(animalToUpdate), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync($"api/animals/{animalToUpdate.Id}", animalJson);
        }

        public async Task<Animal> AddAnimal(Animal animalToAdd)
        {
            var animalJson =
                new StringContent(JsonSerializer.Serialize(animalToAdd), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"api/animals/client/{animalToAdd.ClientId}", animalJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Animal>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task<IEnumerable<Animal>> GetAnimalsByClientId(int clientId)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Animal>>
                (await _httpClient.GetStreamAsync($"api/animals/client/{clientId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        }
    }
}
