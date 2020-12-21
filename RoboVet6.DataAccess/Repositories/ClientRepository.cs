using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RoboVet6.Data.DbContext;
using RoboVet6.Data.Models;
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

        public async Task<List<Client>> GetAllClients()
        {
            return await _context.Clients
                .Include(x=>x.Animals)
                .ToListAsync();


        }

        public async Task<Client> GetClientById(int clientId)
        {
            return await _context.Clients.Where(x => x.Id == clientId)
                .Include(x => x.Animals)
                .SingleOrDefaultAsync();
        }

        public async Task InsertClient(Client client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();

        }

        public async Task UpdateClient(Client client)
        {
            //TODO
            //None of this works
            var existingClient = _context.Clients.FirstOrDefaultAsync(x => x.Id == client.Id);

            existingClient.Result.FirstName = client.FirstName;

            await _context.SaveChangesAsync();
        }

        public async Task<bool> ClientExists(int clientId)
        {
            var existingClient = await _context.Clients.FindAsync(clientId);

            return existingClient != null;
        }
    }
}
