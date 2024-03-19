using AutoMapper;
using Shared.ApiModels;
using VehicleManagement.Entities;
using VehicleManagement.Repositories;

namespace VehicleManagement.Services
{
    public class MaintainanceService : IMaintainanceService
    {
        private readonly IMaintainanceRepository _maintainanceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MaintainanceService> _logger;

        public MaintainanceService(IMaintainanceRepository maintainanceRepository, IMapper mapper, ILogger<MaintainanceService> logger)
        {
            _maintainanceRepository = maintainanceRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<MaintainanceModel>> GetAllMaintainancesAsync()
        {
            var maintainances = await _maintainanceRepository.GetAllMaintainancesAsync();
            if (maintainances == null)
            {
                _logger.LogError("No maintainances found");
                return null;
            }

            _logger.LogInformation("Maintainances found");
            return _mapper.Map<IEnumerable<MaintainanceModel>>(maintainances);
        }

        public async Task<MaintainanceModel> GetMaintainanceByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogError("Invalid maintainance id");
                return null;
            }

            var maintainance = await _maintainanceRepository.GetMaintainanceByIdAsync(id);
            if (maintainance == null)
            {
                _logger.LogError("Maintainance not found");
                return null;
            }

            _logger.LogInformation("Maintainance found");
            return _mapper.Map<MaintainanceModel>(maintainance);
        }

        public async Task<MaintainanceModel> CreateMaintainanceAsync(MaintainanceModel maintainance)
        {
            if (maintainance == null)
            {
                _logger.LogError("Invalid maintainance");
                return null;
            }

            var maintainanceEntity = _mapper.Map<MaintainanceEntity>(maintainance);
            var createdMaintainance = await _maintainanceRepository.CreateMaintainanceAsync(maintainanceEntity);
            if (createdMaintainance == null)
            {
                _logger.LogError("Maintainance not created");
                return null;
            }

            _logger.LogInformation("Maintainance created");
            return _mapper.Map<MaintainanceModel>(createdMaintainance);
        }

        public async Task<MaintainanceModel> UpdateMaintainanceAsync(MaintainanceModel maintainance)
        {
            if (maintainance == null)
            {
                _logger.LogError("Invalid maintainance");
                return null;
            }

            var maintainanceEntity = _mapper.Map<MaintainanceEntity>(maintainance);
            var updatedMaintainance = await _maintainanceRepository.UpdateMaintainanceAsync(maintainanceEntity);
            if (updatedMaintainance == null)
            {
                _logger.LogError("Maintainance not updated");
                return null;
            }

            _logger.LogInformation("Maintainance updated");
            return _mapper.Map<MaintainanceModel>(updatedMaintainance);
        }

        public async Task<bool> DeleteMaintainanceAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogError("Invalid maintainance id");
                return false;
            }

            var deleted = await _maintainanceRepository.DeleteMaintainanceAsync(id);
            if (!deleted)
            {
                _logger.LogError("Maintainance not deleted");
                return false;
            }

            _logger.LogInformation("Maintainance deleted");
            return true;
        }
    }

    public interface IMaintainanceService
    {
        Task<IEnumerable<MaintainanceModel>> GetAllMaintainancesAsync();

        // get maintainance by id
        Task<MaintainanceModel> GetMaintainanceByIdAsync(int id);

        // create maintainance
        Task<MaintainanceModel> CreateMaintainanceAsync(MaintainanceModel maintainance);

        // update maintainance
        Task<MaintainanceModel> UpdateMaintainanceAsync(MaintainanceModel maintainance);

        // delete maintainance
        Task<bool> DeleteMaintainanceAsync(int id);


    }
}
