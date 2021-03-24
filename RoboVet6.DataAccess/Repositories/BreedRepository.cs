using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RoboVet6.Data.DbContext;
using RoboVet6.Data.Models.RoboVet6;
using RoboVet6.DataAccess.Common.Interfaces;

namespace RoboVet6.DataAccess.Repositories
{
    public class BreedRepository : IBreedRepository
    {
        private readonly ApplicationDbContext _context;

        public BreedRepository(ApplicationDbContext context)
        {
            _context = context
                       ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<BreedModel>> GetAllBreeds(string name)
        {
            var collection = _context.Breeds as IQueryable<BreedModel>;

            if (Equals(!string.IsNullOrWhiteSpace(name)))
            {
                collection = collection.Where(x => x.Name.Contains(name));
            }

            return await collection.ToListAsync();
        }

        public async Task<BreedModel> GetBreedByBreedId(int breedId)
        {
            return await _context.Breeds.FirstOrDefaultAsync(x => x.Id == breedId);
        }

        public async Task<List<BreedModel>> GetBreedsBySpeciesId(int speciesId)
        {
            return await _context.Breeds.Where(x => x.SpeciesId == speciesId).ToListAsync();
        }

        public async Task InsertBreed(BreedModel breed)
        {
            await _context.Breeds.AddAsync(breed);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBreed(BreedModel breed)
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> BreedExists(int breedId)
        {
            var existingBreed = await _context.Breeds.FindAsync(breedId);

            return existingBreed != null;
        }
    }
}
