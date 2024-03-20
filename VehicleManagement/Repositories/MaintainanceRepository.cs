using Microsoft.EntityFrameworkCore;
using VehicleManagement.Data;
using VehicleManagement.Entities;

namespace VehicleManagement.Repositories
{
    public class MaintainanceRepository : IMaintainanceRepository
    {
        private readonly AppDbContext _context;

        public MaintainanceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MaintainanceEntity>> GetAllMaintainancesAsync()
        {
            return await _context.Maintainances.ToListAsync();
        }

        public async Task<MaintainanceEntity> GetMaintainanceByIdAsync(Guid id)
        {
            return await _context.Maintainances.FindAsync(id);
        }

        public async Task<MaintainanceEntity> CreateMaintainanceAsync(MaintainanceEntity maintainance)
        {
            _context.Maintainances.Add(maintainance);
            await _context.SaveChangesAsync();
            return maintainance;
        }

        public async Task<MaintainanceEntity> UpdateMaintainanceAsync(MaintainanceEntity maintainance)
        {
            //_context.Entry(Maintainance).State = EntityState.Modified; // this is another way to update
            _context.Maintainances.Update(maintainance); // this is another way to update
            await _context.SaveChangesAsync();
            return maintainance;
        }

        public async Task<bool> DeleteMaintainanceAsync(Guid id)
        {
            var maintainance = await _context.Maintainances.FindAsync(id);
            if (maintainance == null)
            {
                return false;
            }

            var result = _context.Maintainances.Remove(maintainance);
            if (result.State == EntityState.Deleted)
            {
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }


    public interface IMaintainanceRepository
    {
        // get all maintainances
        Task<IEnumerable<MaintainanceEntity>> GetAllMaintainancesAsync();

        // get maintainances by id
        Task<MaintainanceEntity> GetMaintainanceByIdAsync(Guid id);

        // create maintainance
        Task<MaintainanceEntity> CreateMaintainanceAsync(MaintainanceEntity maintainance);

        // update maintainance
        Task<MaintainanceEntity> UpdateMaintainanceAsync(MaintainanceEntity maintainance);

        // delete maintainance
        Task<bool> DeleteMaintainanceAsync(Guid id);


    }
}
