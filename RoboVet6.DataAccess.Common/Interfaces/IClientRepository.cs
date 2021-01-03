using System.Collections.Generic;
using System.Threading.Tasks;
using RoboVet6.Data.Models.RoboVet6;


namespace RoboVet6.DataAccess.Common.Interfaces
{
    public interface IClientRepository
    {
        Task<List<ClientModel>> GetAllClients(string lastName, string address, string phone, string email);
        Task<ClientModel> GetClientById(int clientId);
        Task InsertClient(ClientModel client);
        Task UpdateClient(ClientModel client);
        Task<bool> ClientExists(int clientId);
    }
}
