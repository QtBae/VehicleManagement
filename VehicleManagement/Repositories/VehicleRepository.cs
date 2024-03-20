using Microsoft.EntityFrameworkCore;
using VehicleManagement.Data;
using VehicleManagement.Entities;

namespace VehicleManagement.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly AppDbContext _context;

        public VehicleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VehicleEntity>> GetAllVehiclesAsync()
        {
            return await _context.Vehicles.AsQueryable().Include(m=>m.Model).Include(m=>m.Brand).Include(m=>m.Maintainances).ToListAsync();
        }

        public async Task<VehicleEntity> GetVehicleByIdAsync(Guid id)
        {
            return await _context.Vehicles.AsQueryable().Include(m=>m.Model).Include(m=>m.Brand).Include(m=>m.Maintainances).FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<VehicleEntity> CreateVehicleAsync(VehicleEntity vehicle)
        {
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();
            return vehicle;
        }

        public async Task<VehicleEntity> UpdateVehicleAsync(VehicleEntity vehicle)
        {
            //_context.Entry(Vehicle).State = EntityState.Modified; // this is another way to update
            _context.Vehicles.Update(vehicle); // this is another way to update
            await _context.SaveChangesAsync();
            return vehicle;
        }

        public async Task<bool> DeleteVehicleAsync(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return false;
            }

            var result = _context.Vehicles.Remove(vehicle);
            if (result.State == EntityState.Deleted)
            {
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
    public interface IVehicleRepository
    {
        // get all vehicles
        Task<IEnumerable<VehicleEntity>> GetAllVehiclesAsync();

        // get vehicles by id
        Task<VehicleEntity> GetVehicleByIdAsync(Guid id);

        // create vehicle
        Task<VehicleEntity> CreateVehicleAsync(VehicleEntity vehicle);

        // update vehicle
        Task<VehicleEntity> UpdateVehicleAsync(VehicleEntity vehicle);

        // delete vehicle
        Task<bool> DeleteVehicleAsync(int id);


    }
}
