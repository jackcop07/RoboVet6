using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RoboVet6.Data.DbContext;
using RoboVet6.Data.Models.RoboVet6;
using RoboVet6.DataAccess.Common.Interfaces;


namespace RoboVet6.DataAccess.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _context;

        public ClientRepository(ApplicationDbContext context)
        {
            _context = context
                    ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task<List<ClientModel>> GetAllClients(string lastName, string address, string phone, string email)
        {
            //Written n this manner so if we add any extra query strings, the can easily be added
            var collection = _context.Clients as IQueryable<ClientModel>;

            if (!string.IsNullOrWhiteSpace(address))
            {
                address = address.Trim();
                collection = collection.Where(
                    c => c.Address.Contains(address));
            }

            if (!string.IsNullOrWhiteSpace(lastName))
            {
                lastName = lastName.Trim();
                collection = collection.Where(
                    c => c.LastName.Contains(lastName));
            }

            if (!string.IsNullOrWhiteSpace(phone))
            {
                phone = phone.Trim();
                collection = collection.Where(
                    c => c.MobilePhone.Contains(phone) || c.HomePhone.Contains(phone));
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                email = email.Trim();
                collection = collection.Where(
                    c => c.Email.Contains(email));
            }

            collection = collection.Include(x => x.Animals);

            return await collection.ToListAsync();


        }

        public async Task<ClientModel> GetClientById(int clientId)
        {
            return await _context.Clients.Where(x => x.Id == clientId)
                .Include(x => x.Animals)
                .SingleOrDefaultAsync();
        }

        public async Task InsertClient(ClientModel client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();

        }

        public async Task UpdateClient(ClientModel client)
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ClientExists(int clientId)
        {
            var existingClient = await _context.Clients.FindAsync(clientId);

            return existingClient != null;
        }
    }
}
