using Microsoft.EntityFrameworkCore;

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
    }
}
