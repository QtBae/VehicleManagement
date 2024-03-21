using Microsoft.EntityFrameworkCore;
using VehicleManagement.Entities;

namespace VehicleManagement.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Entities.BrandEntity> Brands { get; set; }
        public DbSet<Entities.CarEntity> Cars { get; set; }

        public DbSet<Entities.MaintainanceEntity> Maintainances { get; set; }

        public DbSet<Entities.VehicleEntity> Vehicles { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=app.db");
        }


        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleEntity>()
                .HasMany(v => v.Maintainances)
                .WithOne()
                .HasForeignKey(m => m.VehicleId)
                .OnDelete(DeleteBehavior.Cascade)
                
                .IsRequired();

            modelBuilder.Entity<BrandEntity>()
                .HasData(Shared.SeedData.BrandData.GetBrands().ToArray());
        }
    }
}
