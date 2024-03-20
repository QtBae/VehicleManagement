using APICleanArchi.Data;
using APICleanArchi.Entities;
using Microsoft.EntityFrameworkCore;

namespace APICleanArchi.Repositories
{
    public class GraduationRepository: IGraduationRepository
    {
        private readonly AppDbContext dbContext;

        public GraduationRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<GraduationEntity>> GetAllGraduationsAsync()
        {
            return await dbContext.Graduations.AsQueryable().Include(g=>g.User).ToListAsync();
        }

        public async Task CreateGraduationAsync(GraduationEntity graduation)
        {
            dbContext.Graduations.Add(graduation);
            await dbContext.SaveChangesAsync();
        }
    }

    public interface IGraduationRepository
    {
        Task<IEnumerable<GraduationEntity>> GetAllGraduationsAsync();
        Task CreateGraduationAsync(GraduationEntity graduation);
    }
}
