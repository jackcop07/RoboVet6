using System.Collections.Generic;
using System.Threading.Tasks;
using RoboVet6.Blazor.UI.Models;

namespace RoboVet6.Blazor.UI.Interfaces.Services
{
    public interface IClientDataService
    {
        Task<IEnumerable<Client>> GetAllClients();
        Task<Client> GetClientById(int clientId);
    }
}
