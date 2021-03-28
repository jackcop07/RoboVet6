using Microsoft.EntityFrameworkCore;
using RoboVet6.Data.Models.RoboVet6;


namespace RoboVet6.Data.DbContext
{
    public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<ClientModel> Clients { get; set; }
        public DbSet<AnimalModel> Animals { get; set; }
        public DbSet<SpeciesModel> Species { get; set; }
        public DbSet<BreedModel> Breeds { get; set; }
        public DbSet<ColourModel> Colours { get; set; }
        public DbSet<ConsultationModel> Consultations { get; set; }
        public DbSet<ConsultationDetailModel> ConsultationDetails { get; set; }
        public DbSet<ProductModel> Products { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-OADBINK\\SQLEXPRESS;Initial Catalog=RoboVet6;User ID=supertech;Password=7CleverDogs;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
