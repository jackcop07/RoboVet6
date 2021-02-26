using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using RoboVet6.Blazor.UI.Interfaces.Services;
using RoboVet6.Blazor.UI.Models;

namespace RoboVet6.Blazor.UI.Services
{
    public class ClientDataService : IClientDataService
    {
        private readonly HttpClient _httpClient;

        public ClientDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Client>> GetAllClients()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Client>>
                (await _httpClient.GetStreamAsync($"https://localhost:44387/api/clients"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Client> GetClientById(int clientId)
        {
            return await JsonSerializer.DeserializeAsync<Client>
                (await _httpClient.GetStreamAsync($"api/employee/{clientId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        }
    }
}
