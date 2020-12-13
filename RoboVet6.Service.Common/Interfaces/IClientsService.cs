using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RoboVet6.Service.Common.Models;
using RoboVet6.Service.Common.Models.API;

namespace RoboVet6.Service.Common.Interfaces
{
    public interface IClientsService
    {
        Task<Client> GetClientByClientId(int clientId);
        Task<List<Client>> GetAllClients();
        Task<Client> InsertClient(Client client);
        Task<bool> ClientExists(int clientId);
        Task<Client> UpdateClient(int clientId, Client client);

    }
}
