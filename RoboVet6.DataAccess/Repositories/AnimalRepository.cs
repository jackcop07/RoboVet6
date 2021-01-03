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
    public class AnimalRepository : IAnimalRepository
    {
        private readonly ApplicationDbContext _context;

        public AnimalRepository(ApplicationDbContext context)
        {
            _context = context
                    ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<List<AnimalModel>> GetAllAnimals(string name)
        {
            var collection = _context.Animals as IQueryable<AnimalModel>;

            if (!string.IsNullOrWhiteSpace(name))
            {
                collection = collection.Where(x => x.Name.Contains(name));
            }

            return await collection.ToListAsync();
        }

        public async Task<AnimalModel> GetAnimalByAnimalId(int animalId)
        {
            return await _context.Animals.FirstOrDefaultAsync(x => x.Id == animalId);
        }

        public async Task<List<AnimalModel>> GetAnimalsByClientId(int clientId)
        {
            return await _context.Animals.Where(x => x.ClientId == clientId).ToListAsync();
        }

        public async Task InsertAnimal(AnimalModel animal)
        {
            await _context.Animals.AddAsync(animal);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAnimal(AnimalModel animal)
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AnimalExists(int animalId)
        {
            var existingAnimal = await _context.Animals.FindAsync(animalId);

            return existingAnimal != null;
        }
    }
}
