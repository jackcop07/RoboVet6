using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using RoboVet6.DataAccess.Common.Interfaces;
using RoboVet6.Service.Common.Interfaces;
using RoboVet6.Service.Common.Models.API;
using RoboVet6.Service.Common.Models.API.Client;


namespace RoboVet6.Service.Services
{
    public class ClientsService : IClientsService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientsService(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }
        public async Task<ClientToReturnDto> GetClientByClientId(int clientId)
        {
            try
            {
                var clientFromRepo = await _clientRepository.GetClientById(clientId);

                if (clientFromRepo == null)
                {
                    return null;
                }

                var clientToReturn = _mapper.Map<ClientToReturnDto>(clientFromRepo);

                return clientToReturn;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public async Task<List<ClientToReturnDto>> GetAllClients()
        {
            var clientsFromRepo = await _clientRepository.GetAllClients();

            var clientsToReturn = _mapper.Map<List<ClientToReturnDto>>(clientsFromRepo);
           

            return clientsToReturn;
        }

        public async Task<ClientToReturnDto> InsertClient(ClientToInsertDto client)
        {


            var clientToInsert = _mapper.Map<Data.Models.Client>(client);

            await _clientRepository.InsertClient(clientToInsert);


            var clientToReturn = _mapper.Map<ClientToReturnDto>(clientToInsert);

            return clientToReturn;
        }

        public Task<bool> ClientExists(int clientId)
        {
            return _clientRepository.ClientExists(clientId);
        }

        public async Task<Client> UpdateClient(int clientId, Client client)
        {
            var clientExists = await _clientRepository.ClientExists(clientId);

            if (!clientExists)
            {
                return null;
            }

            var clientToUpdate = new Data.Models.Client
            {
                Id = clientId,
                Title = client.Title,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Address = client.Address,
                Postcode = client.Postcode,
                City = client.City,
                HomePhone = client.HomePhone,
                MobilePhone = client.MobilePhone,
                Email = client.Email
            };

            var updatedClient = new Client
            {
                Id = clientToUpdate.Id,
                Title = clientToUpdate.Title,
                FirstName = clientToUpdate.FirstName,
                LastName = clientToUpdate.LastName,
                Address = clientToUpdate.Address,
                Postcode = clientToUpdate.Postcode,
                City = clientToUpdate.City,
                HomePhone = clientToUpdate.HomePhone,
                MobilePhone = clientToUpdate.MobilePhone,
                Email = clientToUpdate.Email
            };

            return updatedClient;
        }
    }
}
