﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RoboVet6.Data.DbContext;
using RoboVet6.Data.Migrations;
using RoboVet6.Data.Models;
using RoboVet6.DataAccess.Common.Interfaces;


namespace RoboVet6.DataAccess.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly ApplicationDbContext _context;

        public AnimalRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Animal>> GetAllAnimals()
        {
            return await _context.Animals.ToListAsync();
        }

        public async Task<Animal> GetAnimalByAnimalId(int animalId)
        {
            return await _context.Animals.FirstOrDefaultAsync(x => x.Id == animalId);
        }

        public async Task<List<Animal>> GetAnimalsByClientId(int clientId)
        {
            return await _context.Animals.Where(x => x.ClientId == clientId).ToListAsync();
        }

        public async Task InsertAnimal(Animal animal)
        {
            await _context.Animals.AddAsync(animal);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAnimal(Animal animal)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AnimalExists(int animalId)
        {
            var existingAnimal = await _context.Animals.FindAsync(animalId);

            return existingAnimal != null;
        }
    }
}
