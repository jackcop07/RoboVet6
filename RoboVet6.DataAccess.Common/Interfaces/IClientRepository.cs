using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RoboVet6.Data.Models;


namespace RoboVet6.DataAccess.Common.Interfaces
{
    public interface IClientRepository
    {
        Task<List<Client>> GetAllClients(string searchQuery);
        Task<Client> GetClientById(int clientId);
        Task InsertClient(Client client);
        Task UpdateClient(Client client);
        Task<bool> ClientExists(int clientId);
    }
}
