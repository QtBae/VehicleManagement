using APICleanArchi.Entities;
using Microsoft.EntityFrameworkCore;

namespace APICleanArchi.Data
{
    public class AppDbContext: DbContext
    {
        public DbSet<Entities.UserEntity> Users { get; set; }
        public DbSet<GraduationEntity> Graduations { get; set; }
        public DbSet<GradleEntity> Gradles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=app.db");

        }

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                .HasMany(u => u.Gradles)
                .WithOne()
                .HasForeignKey(g => g.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
