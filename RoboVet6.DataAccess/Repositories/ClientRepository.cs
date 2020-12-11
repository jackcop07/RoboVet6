using System;
using System.Collections.Generic;
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
            _context = context;
        }

        public async Task<List<Client>> GetAllClients()
        {
            return await _context.Clients.ToListAsync();


        }

        public async Task<Client> GetClientById(int clientId)
        {
            return await _context.Clients.FirstOrDefaultAsync(x => x.Id == clientId);
        }

        public async Task InsertClient(Client client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();

        }
    }
}
