using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using RoboVet6.Data.Models.RoboVet6;
using RoboVet6.DataAccess.Common.Interfaces;
using RoboVet6.Service.Common.Interfaces;
using RoboVet6.Service.Common.Models.API.ApiResponse;
using RoboVet6.Service.Common.Models.API.Client;


namespace RoboVet6.Service.Services
{
    public class ClientsService : IClientsService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientsService(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository 
                        ?? throw new ArgumentNullException(nameof(clientRepository));
            _mapper = mapper
                        ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<ApiResponse<ClientToReturnDto>> GetClientByClientId(int clientId)
        {
            var response = new ApiResponse<ClientToReturnDto>();

            try
            {
                var clientFromRepo = await _clientRepository.GetClientById(clientId);


                if (clientFromRepo == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                response.StatusCode = HttpStatusCode.OK;
                response.Data = _mapper.Map<ClientToReturnDto>(clientFromRepo);

                return response;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Error = e.Message;
                return response;
            }
   
        }

        public async Task<ApiResponse<List<ClientToReturnDto>>> GetAllClients(string searchQuery)
        {
            var response = new ApiResponse<List<ClientToReturnDto>>();
            try
            {
                var clientsFromRepo = await _clientRepository.GetAllClients(searchQuery);
                
                if (clientsFromRepo?.Any() != true)
                {
                    response.StatusCode = HttpStatusCode.NoContent;
                    return response;
                }

                var clientsToReturn = _mapper.Map<List<ClientToReturnDto>>(clientsFromRepo);

                response.StatusCode = HttpStatusCode.OK;
                response.Data = clientsToReturn;

                return response;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Error = e.Message;
                return response;
            }
        }

        public async Task<ApiResponse<ClientToReturnDto>> InsertClient(ClientToInsertDto client)
        {
            var response = new ApiResponse<ClientToReturnDto>();

            try
            {
                var clientToInsert = _mapper.Map<ClientModel>(client);

                await _clientRepository.InsertClient(clientToInsert);

                var clientToReturn = _mapper.Map<ClientToReturnDto>(clientToInsert);

                response.StatusCode = HttpStatusCode.Created;
                response.Data = clientToReturn;

                return response;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Error = e.Message;
                return response;
            }
        }

        public Task<bool> ClientExists(int clientId)
        {
            return _clientRepository.ClientExists(clientId);
        }

        //public async Task<Client> UpdateClient(int clientId, Client client)
        //{
        //    var clientExists = await _clientRepository.ClientExists(clientId);

        //    if (!clientExists)
        //    {
        //        return null;
        //    }

        //    var clientToUpdate = new Data.Models.Client
        //    {
        //        Id = clientId,
        //        Title = client.Title,
        //        FirstName = client.FirstName,
        //        LastName = client.LastName,
        //        Address = client.Address,
        //        Postcode = client.Postcode,
        //        City = client.City,
        //        HomePhone = client.HomePhone,
        //        MobilePhone = client.MobilePhone,
        //        Email = client.Email
        //    };

        //    var updatedClient = new Client
        //    {
        //        Id = clientToUpdate.Id,
        //        Title = clientToUpdate.Title,
        //        FirstName = clientToUpdate.FirstName,
        //        LastName = clientToUpdate.LastName,
        //        Address = clientToUpdate.Address,
        //        Postcode = clientToUpdate.Postcode,
        //        City = clientToUpdate.City,
        //        HomePhone = clientToUpdate.HomePhone,
        //        MobilePhone = clientToUpdate.MobilePhone,
        //        Email = clientToUpdate.Email
        //    };

        //    return updatedClient;
        //}
    }
}
