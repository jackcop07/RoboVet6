using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RoboVet6.Data.DbContext;
using RoboVet6.Data.Models.RoboVet6;
using RoboVet6.DataAccess.Common;
using RoboVet6.DataAccess.Common.Interfaces;

namespace RoboVet6.DataAccess.Repositories
{
    public class SpeciesRepository : ISpeciesRepository
    {
        private readonly ApplicationDbContext _context;

        public SpeciesRepository(ApplicationDbContext context)
        {
            _context = context
                       ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<SpeciesModel>> GetAllSpecies(string name)
        {
            var collection = _context.Species as IQueryable<SpeciesModel>;

            if (!string.IsNullOrWhiteSpace(name))
            {
                collection = collection.Where(x => x.Name.Contains(name));
            }

            return await collection.ToListAsync();
        }

        public async Task<SpeciesModel> GetSpeciesBySpeciesId(int speciesId)
        {
            return await _context.Species.FirstOrDefaultAsync(x => x.Id == speciesId);
        }

        public async Task InsertSpecies(SpeciesModel species)
        {
            await _context.Species.AddAsync(species);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSpecies(SpeciesModel species)
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> SpeciesExists(int speciesId)
        {
            var existingSpecies = await _context.Species.FindAsync(speciesId);

            return existingSpecies != null;
        }
    }
}
