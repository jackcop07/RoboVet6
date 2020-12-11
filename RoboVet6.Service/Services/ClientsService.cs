using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using RoboVet6.DataAccess.Common.Interfaces;
using RoboVet6.Service.Common.Interfaces;
using RoboVet6.Service.Common.Models.API;


namespace RoboVet6.Service.Services
{
    public class ClientsService : IClientsService
    {
        private readonly IClientRepository _clientRepository;

        public ClientsService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        public async Task<Client> GetClientByClientId(int clientId)
        {
            var clientRepo = await _clientRepository.GetClientById(clientId);
            
            var niceClient = new Client
            {
                Id = clientRepo.Id,
                Title = clientRepo.Title,
                FirstName = clientRepo.FirstName,
                LastName = clientRepo.LastName,
                Address = clientRepo.Address,
                Postcode = clientRepo.Postcode,
                City = clientRepo.City,
                HomePhone = clientRepo.HomePhone,
                MobilePhone = clientRepo.MobilePhone,
                Email = clientRepo.Email
            };

            return niceClient;
        }

        public async Task<List<Client>> GetAllClients()
        {
            var clientsFromRepo = await _clientRepository.GetAllClients();

            var clientsToReturn = new List<Client>();

            foreach (var client in clientsToReturn)
            {
                clientsToReturn.Add(new Client
                {
                    Id = client.Id,
                    Title = client.Title,
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                    Address = client.Address,
                    Postcode = client.Postcode,
                    City = client.City,
                    HomePhone = client.HomePhone,
                    MobilePhone = client.MobilePhone,
                    Email = client.Email
                });
            }

            return clientsToReturn;
        }

        public async Task<Client> InsertClient(Client client)
        {
            var clientToInsert = new Data.Models.Client
            {
                Id = client.Id,
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
            await _clientRepository.InsertClient(clientToInsert);


            var clientToReturn = new Client
            {
                Id = clientToInsert.Id,
                Title = clientToInsert.Title,
                FirstName = clientToInsert.FirstName,
                LastName = clientToInsert.LastName,
                Address = clientToInsert.Address,
                Postcode = clientToInsert.Postcode,
                City = clientToInsert.City,
                HomePhone = clientToInsert.HomePhone,
                MobilePhone = clientToInsert.MobilePhone,
                Email = clientToInsert.Email
            };

            return clientToReturn;
        }
    }
}
