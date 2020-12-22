using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RoboVet6.Service.Common.Models;
using RoboVet6.Service.Common.Models.API;
using RoboVet6.Service.Common.Models.API.Client;

namespace RoboVet6.Service.Common.Interfaces
{
    public interface IClientsService
    {
        Task<ClientToReturnDto> GetClientByClientId(int clientId);
        Task<List<ClientToReturnDto>> GetAllClients(string searchQuery);
        Task<ClientToReturnDto> InsertClient(ClientToInsertDto client);
        Task<bool> ClientExists(int clientId);
        //Task<Client> UpdateClient(int clientId, Client client);

    }
}
