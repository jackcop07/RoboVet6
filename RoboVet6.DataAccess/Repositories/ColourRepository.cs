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
    public class ColourRepository : IColourRepository
    {
        private readonly ApplicationDbContext _context;

        public ColourRepository(ApplicationDbContext context)
        {
            _context = context
                       ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task<List<ColourModel>> GetAllColours(string searchTerm)
        {
            var collectionToReturn = new List<ColourModel>();

            await using (var context = _context)
            {
                var collection = context.Colours as IQueryable<ColourModel>;

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    collection = collection.Where(x => x.Name.Contains(searchTerm));
                }

                collectionToReturn = await collection.ToListAsync();

            }

            return collectionToReturn;

        }

        public async Task<ColourModel> GetColourById(int colourId)
        {
            return await _context.Colours.FirstOrDefaultAsync(x => x.Id == colourId);
        }

        public async Task UpdateColour(ColourModel colour)
        {
            await _context.SaveChangesAsync();
        }

        public async Task InsertColour(ColourModel colour)
        {
            await _context.Colours.AddAsync(colour);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ColourExists(int colourId)
        {
            var existingColour = await _context.Colours.FindAsync(colourId);

            return existingColour != null;
        }
    }
}
