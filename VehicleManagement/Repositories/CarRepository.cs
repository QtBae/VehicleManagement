using Microsoft.EntityFrameworkCore;
using VehicleManagement.Data;
using VehicleManagement.Entities;

namespace VehicleManagement.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly AppDbContext _context;

        public CarRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CarEntity>> GetAllCarsAsync()
        {
            return await _context.Cars.Include(b => b.Brand).ToListAsync();
        }

        public async Task<CarEntity> GetCarByIdAsync(Guid id)
        {
            return await _context.Cars.Include(b => b.Brand).FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<CarEntity> CreateCarAsync(CarEntity car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
            return car;
        }

        public async Task<CarEntity> UpdateCarAsync(CarEntity car)
        {
            //_context.Entry(Car).State = EntityState.Modified; // this is another way to update
            _context.Cars.Update(car); // this is another way to update
            await _context.SaveChangesAsync();
            return car;
        }

        public async Task<bool> DeleteCarAsync(Guid id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return false;
            }

            var result = _context.Cars.Remove(car);
            if (result.State == EntityState.Deleted)
            {
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }

    public interface ICarRepository
    {
        // get all cars
        Task<IEnumerable<CarEntity>> GetAllCarsAsync();

        // get cars by id
        Task<CarEntity> GetCarByIdAsync(Guid id);

        // create car
        Task<CarEntity> CreateCarAsync(CarEntity car);

        // update car
        Task<CarEntity> UpdateCarAsync(CarEntity car);

        // delete car
        Task<bool> DeleteCarAsync(Guid id);


    }
}
