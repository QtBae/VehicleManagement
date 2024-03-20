using AutoMapper;
using Shared.ApiModels;
using VehicleManagement.Entities;
using VehicleManagement.Repositories;

namespace VehicleManagement.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<VehicleService> _logger;

        public VehicleService(IVehicleRepository vehicleRepository, IMapper mapper, ILogger<VehicleService> logger)
        {
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<VehicleModel>> GetAllVehiclesAsync()
        {
            var vehicles = await _vehicleRepository.GetAllVehiclesAsync();
            if (vehicles == null)
            {
                _logger.LogError("No vehicles found");
                return null;
            }

            _logger.LogInformation("Vehicles found");
            return _mapper.Map<IEnumerable<VehicleModel>>(vehicles);
        }

        public async Task<VehicleModel> GetVehicleByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogError("Invalid vehicle id");
                return null;
            }

            var vehicle = await _vehicleRepository.GetVehicleByIdAsync(id);
            if (vehicle == null)
            {
                _logger.LogError("Vehicle not found");
                return null;
            }

            _logger.LogInformation("Vehicle found");
            return _mapper.Map<VehicleModel>(vehicle);
        }

        public async Task<VehicleModel?> CreateVehicleAsync(VehicleModel vehicle)
        {
            if (vehicle == null)
            {
                _logger.LogError("Invalid vehicle");
                return null;
            }

            var vehicleEntity = _mapper.Map<VehicleEntity>(vehicle);
            var createdVehicle = await _vehicleRepository.CreateVehicleAsync(vehicleEntity);
            if (createdVehicle == null)
            {
                _logger.LogError("Vehicle not created");
                return null;
            }

            _logger.LogInformation("Vehicle created");
            return _mapper.Map<VehicleModel>(createdVehicle);
        }

        public async Task<VehicleModel?> UpdateVehicleAsync(VehicleModel vehicle)
        {
            if (vehicle == null)
            {
                _logger.LogError("Invalid vehicle");
                return null;
            }

            var vehicleEntity = _mapper.Map<VehicleEntity>(vehicle);
            var updatedVehicle = await _vehicleRepository.UpdateVehicleAsync(vehicleEntity);
            if (updatedVehicle == null)
            {
                _logger.LogError("Vehicle not updated");
                return null;
            }

            _logger.LogInformation("Vehicle updated");
            return _mapper.Map<VehicleModel>(updatedVehicle);
        }

        public async Task<bool> DeleteVehicleAsync(Guid id)
        {
            if (id <= null)
            {
                _logger.LogError("Invalid vehicle id");
                return false;
            }

            var deleted = await _vehicleRepository.DeleteVehicleAsync(id);
            if (!deleted)
            {
                _logger.LogError("Vehicle not deleted");
                return false;
            }

            _logger.LogInformation("Vehicle deleted");
            return true;
        }
    }

    public interface IVehicleService
    {
        Task<IEnumerable<VehicleModel>> GetAllVehiclesAsync();

        // get vehicle by id
        Task<VehicleModel> GetVehicleByIdAsync(Guid id);

        // create vehicle
        Task<VehicleModel> CreateVehicleAsync(VehicleModel vehicle);

        // update vehicle
        Task<VehicleModel> UpdateVehicleAsync(VehicleModel vehicle);

        // delete vehicle
        Task<bool> DeleteVehicleAsync(Guid id);


    }
}
