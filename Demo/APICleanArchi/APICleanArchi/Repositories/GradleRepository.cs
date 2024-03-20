using APICleanArchi.Data;
using APICleanArchi.Entities;
using Microsoft.EntityFrameworkCore;

namespace APICleanArchi.Repositories
{
    public class GradleRepository: IGradleRepository
    {
        private readonly AppDbContext dbContext;

        public GradleRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<GradleEntity>> GetAllGradlesAsync()
        {
            return await dbContext.Gradles.ToListAsync();
        }

        public async Task CreateGradleAsync(GradleEntity gradle)
        {
            dbContext.Gradles.Add(gradle);
            await dbContext.SaveChangesAsync();
        }
    }

    public interface IGradleRepository
    {
        Task<IEnumerable<GradleEntity>> GetAllGradlesAsync();
        Task CreateGradleAsync(GradleEntity gradle);
    }
}
