using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;
using RoboVet6.Data.Models;


namespace RoboVet6.Data.DbContext
{
    public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Animal> Animals { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = RoboVet6");
        }
    }
}
