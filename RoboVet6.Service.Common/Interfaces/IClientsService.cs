using System.Collections.Generic;
using System.Threading.Tasks;
using RoboVet6.Service.Common.Models.API.ApiResponse;
using RoboVet6.Service.Common.Models.API.Client;

namespace RoboVet6.Service.Common.Interfaces
{
    public interface IClientsService
    {
        Task<ApiResponse<ClientToReturnDto>> GetClientByClientId(int clientId);
        Task<ApiResponse<List<ClientToReturnDto>>> GetAllClients(string searchQuery);
        Task<ApiResponse<ClientToReturnDto>> InsertClient(ClientToInsertDto client);
        Task<bool> ClientExists(int clientId);
        Task<ApiResponse<ClientToReturnDto>> UpdateClient(int clientId, ClientToUpdateDto client);

    }
}
