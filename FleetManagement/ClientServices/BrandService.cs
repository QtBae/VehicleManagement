using Shared.ApiModels;
using Shared.SeedData;
using System.Net.Http.Json;

namespace FleetManagement.ClientServices
{
    public class BrandService(HttpClient httpClient, ILogger<BrandService> logger):ServiceBase(httpClient,logger), IBrandService
    {
        public async Task<IEnumerable<BrandModel?>> GetAllBrandsAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<IEnumerable<BrandModel>>("api/Brand");

                if (response != null)
                {
                    return response;
                }
                throw new Exception("No brands found");
            }
            catch (Exception ex)
            {
                logger.LogError($"Error getting brands from API {ex.Message}");
                throw;
            }
        }
    }

    public interface IBrandService
    {
        Task<IEnumerable<BrandModel?>> GetAllBrandsAsync();
    }
}
