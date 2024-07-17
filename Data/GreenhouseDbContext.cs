using GreenhouseAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GreenhouseAPI.Data
{
    public class GreenhouseDbContext : IdentityDbContext<AppUser>
    {
        public GreenhouseDbContext(DbContextOptions<GreenhouseDbContext> options) : base(options)
        {
        }

        public DbSet<Greenhouse> Greenhouses { get; set; }
        public DbSet<Crop> Crops { get; set; }
        public DbSet<Plant> Plants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure entities
            modelBuilder.Entity<Greenhouse>()
                .Property(g => g.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Crop>()
                .HasOne(c => c.Plant)
                .WithMany()
                .HasForeignKey(c => c.PlantId);

            modelBuilder.Entity<Crop>()
                .HasOne(c => c.Greenhouse)
                .WithMany(g => g.Crops)
                .HasForeignKey(c => c.GreenhouseId);

            modelBuilder.Entity<Crop>()
                .Property(c => c.Status)
                .HasConversion<string>();
        }
    }
}