using Microsoft.EntityFrameworkCore;
using VehicleManagement.Data;
using VehicleManagement.Entities;

namespace VehicleManagement.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly AppDbContext _context;

        public BrandRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BrandEntity>> GetAllBrandsAsync()
        {
            return await _context.Brands.ToListAsync();
        }

        public async Task<BrandEntity> GetBrandByIdAsync(int id)
        {
            return await _context.Brands.FindAsync(id);
        }

        public async Task<BrandEntity> CreateBrandAsync(BrandEntity brand)
        {
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();
            return brand;
        }

        public async Task<BrandEntity> UpdateBrandAsync(BrandEntity brand)
        {
            //_context.Entry(brand).State = EntityState.Modified; // this is another way to update
            _context.Brands.Update(brand); // this is another way to update
            await _context.SaveChangesAsync();
            return brand;
        }

        public async Task<bool> DeleteBrandAsync(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return false;
            }

            var result = _context.Brands.Remove(brand);
            if (result.State == EntityState.Deleted)
            {
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    
}

    public interface IBrandRepository
    {
        // get all brands
        Task<IEnumerable<BrandEntity>> GetAllBrandsAsync();

        // get  brands by id
        Task<BrandEntity> GetBrandByIdAsync(int id);

        // create brand
        Task<BrandEntity> CreateBrandAsync(BrandEntity brand);

        // update brand
        Task<BrandEntity> UpdateBrandAsync(BrandEntity brand);

        // delete brand
        Task<bool> DeleteBrandAsync(int id);


    }
}
