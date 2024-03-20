using AutoMapper;
using Shared.ApiModels;
using VehicleManagement.Entities;
using VehicleManagement.Repositories;

namespace VehicleManagement.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CarService> _logger;

        public CarService(ICarRepository carRepository, IMapper mapper, ILogger<CarService> logger)
        {
            _carRepository = carRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<CarModel>> GetAllCarsAsync()
        {
            var cars = await _carRepository.GetAllCarsAsync();
            if (cars == null)
            {
                _logger.LogError("No cars found");
                return null;
            }

            _logger.LogInformation("Cars found");
            return _mapper.Map<IEnumerable<CarModel>>(cars);
        }

        public async Task<CarModel> GetCarByIdAsync(Guid id)
        {
            if (id <= null)
            {
                _logger.LogError("Invalid car id");
                return null;
            }

            var car = await _carRepository.GetCarByIdAsync(id);
            if (car == null)
            {
                _logger.LogError("Car not found");
                return null;
            }

            _logger.LogInformation("Car found");
            return _mapper.Map<CarModel>(car);
        }

        public async Task<CarModel> CreateCarAsync(CarModel car)
        {
            if (car == null)
            {
                _logger.LogError("Invalid car");
                return null;
            }

            var carEntity = _mapper.Map<CarEntity>(car);
            var createdCar = await _carRepository.CreateCarAsync(carEntity);
            if (createdCar == null)
            {
                _logger.LogError("Car not created");
                return null;
            }

            _logger.LogInformation("Car created");
            return _mapper.Map<CarModel>(createdCar);
        }

        public async Task<CarModel?> UpdateCarAsync(CarModel car)
        {
            if (car == null)
            {
                _logger.LogError("Invalid car");
                return null;
            }

            var carEntity = _mapper.Map<CarEntity>(car);
            var updatedCar = await _carRepository.UpdateCarAsync(carEntity);
            if (updatedCar == null)
            {
                _logger.LogError("Car not updated");
                return null;
            }

            _logger.LogInformation("Car updated");
            return _mapper.Map<CarModel>(updatedCar);
        }

        public async Task<bool> DeleteCarAsync(Guid id)
        {
            if (id <= null)
            {
                _logger.LogError("Invalid car id");
                return false;
            }

            var deleted = await _carRepository.DeleteCarAsync(id);
            if (!deleted)
            {
                _logger.LogError("Car not deleted");
                return false;
            }

            _logger.LogInformation("Car deleted");
            return true;
        }
    }

    public interface ICarService
    {
        Task<IEnumerable<CarModel>> GetAllCarsAsync();

        // get car by id
        Task<CarModel> GetCarByIdAsync(Guid id);

        // create car
        Task<CarModel> CreateCarAsync(CarModel car);

        // update car
        Task<CarModel> UpdateCarAsync(CarModel car);

        // delete car
        Task<bool> DeleteCarAsync(Guid id);


    }
}
