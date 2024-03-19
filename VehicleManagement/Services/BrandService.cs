using AutoMapper;
using Shared.ApiModels;
using VehicleManagement.Entities;
using VehicleManagement.Repositories;

namespace VehicleManagement.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BrandService> _logger;

        public BrandService(IBrandRepository brandRepository, IMapper mapper, ILogger<BrandService> logger)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<BrandModel>> GetAllBrandsAsync()
        {
            var brands = await _brandRepository.GetAllBrandsAsync();
            if (brands == null)
            {
                _logger.LogError("No brands found");
                return null;
            }

            _logger.LogInformation("Brands found");
            return _mapper.Map<IEnumerable<BrandModel>>(brands);
        }

        public async Task<BrandModel> GetBrandByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogError("Invalid brand id");
                return null;
            }

            var brand = await _brandRepository.GetBrandByIdAsync(id);
            if (brand == null)
            {
                _logger.LogError("Brand not found");
                return null;
            }

            _logger.LogInformation("Brand found");
            return _mapper.Map<BrandModel>(brand);
        }

        public async Task<BrandModel> CreateBrandAsync(BrandModel brand)
        {
            if (brand == null)
            {
                _logger.LogError("Invalid brand");
                return null;
            }

            var brandEntity = _mapper.Map<BrandEntity>(brand);
            var createdBrand = await _brandRepository.CreateBrandAsync(brandEntity);
            if (createdBrand == null)
            {
                _logger.LogError("Brand not created");
                return null;
            }

            _logger.LogInformation("Brand created");
            return _mapper.Map<BrandModel>(createdBrand);
        }

        public async Task<BrandModel?> UpdateBrandAsync(BrandModel brand)
        {
            if (brand == null)
            {
                _logger.LogError("Invalid brand");
                return null;
            }

            var brandEntity = _mapper.Map<BrandEntity>(brand);
            var updatedBrand = await _brandRepository.UpdateBrandAsync(brandEntity);
            if (updatedBrand == null)
            {
                _logger.LogError("Brand not updated");
                return null;
            }

            _logger.LogInformation("Brand updated");
            return _mapper.Map<BrandModel>(updatedBrand);
        }

        public async Task<bool> DeleteBrandAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogError("Invalid brand id");
                return false;
            }

            var deleted = await _brandRepository.DeleteBrandAsync(id);
            if (!deleted)
            {
                _logger.LogError("Brand not deleted");
                return false;
            }

            _logger.LogInformation("Brand deleted");
            return true;
        }
    }

    public interface IBrandService
    {
        Task<IEnumerable<BrandModel>> GetAllBrandsAsync();

        // get brand by id
        Task<BrandModel> GetBrandByIdAsync(int id);

        // create brand
        Task<BrandModel> CreateBrandAsync(BrandModel brand);

        // update brand
        Task<BrandModel> UpdateBrandAsync(BrandModel brand);

        // delete brand
        Task<bool> DeleteBrandAsync(int id);


    }
}
