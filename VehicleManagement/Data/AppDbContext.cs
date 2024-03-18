using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace VehicleManagement.Data
{
    public class DbContextDbContext
    {
        public DbSet<Entities.BrandEntity> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=app.db");

        }
    }
}
