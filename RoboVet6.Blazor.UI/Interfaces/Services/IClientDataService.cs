using System.Collections.Generic;
using System.Threading.Tasks;
using RoboVet6.Blazor.UI.Models;

namespace RoboVet6.Blazor.UI.Interfaces.Services
{
    public interface IClientDataService
    {
        Task<IEnumerable<Client>> GetAllClients(string searchTerm);
        Task<Client> GetClientById(int clientId);
        Task UpdateClient(Client clientToUpdate);
        Task<Client> AddClient(Client clientToAdd);
    }
}
