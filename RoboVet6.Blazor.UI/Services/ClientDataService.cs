using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RoboVet6.Blazor.UI.Interfaces.Services;
using RoboVet6.Blazor.UI.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace RoboVet6.Blazor.UI.Services
{
    public class ClientDataService : IClientDataService
    {
        private readonly HttpClient _httpClient;

        public ClientDataService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("RV6Api");
        }

        public async Task<IEnumerable<Client>> GetAllClients(string searchTerm)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Client>>
                (await _httpClient.GetStreamAsync($"api/clients?lastName={searchTerm}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Client> GetClientById(int clientId)
        {
            return await JsonSerializer.DeserializeAsync<Client>
                (await _httpClient.GetStreamAsync($"api/clients/{clientId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        }

        public async Task UpdateClient(Client clientToUpdate)
        {
            var clientJson =
                new StringContent(JsonSerializer.Serialize(clientToUpdate), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync($"api/clients/{clientToUpdate.Id}", clientJson);
        }

        public async Task<Client> AddClient(Client clientToAdd)
        {
            var clientJson =
                new StringContent(JsonSerializer.Serialize(clientToAdd), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/clients", clientJson);

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<Client>(await response.Content.ReadAsStringAsync());
            }

            return null;
        }
    }
}
