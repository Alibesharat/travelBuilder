using TravelBuilder.DAL.Models;
using Microsoft.EntityFrameworkCore;


namespace TravelBuilder.DAL.DbContexts
{
    public class TravelDbContext : DbContext
    {
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Itinerary> Itineraries { get; set; }
        public DbSet<TravelPackage> TravelPackages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Define any relationships or constraints here if necessary
    }

   
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
          options.UseSqlite($"Data Source=TravelDb.db");
        }

        public void Initialize()
        {
            // Apply any pending migrations and ensure the database is up to date
            Database.EnsureCreated();
        }
    }
}