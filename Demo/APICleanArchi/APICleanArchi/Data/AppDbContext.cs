using Microsoft.EntityFrameworkCore;

namespace APICleanArchi.Data
{
    public class AppDbContext: DbContext
    {
        public DbSet<Entities.UserEntity> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=app.db");

        }
    }
}
