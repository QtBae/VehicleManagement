using FleetManagement.Data;
using Shared.ApiModels;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;

namespace FleetManagement.ClientServices
{
    public class VehicleServices(HttpClient httpClient,ILogger<VehicleServices> logger):ServiceBase(httpClient,logger), IVehicleServices
    {
        public async Task<IEnumerable<VehicleModel?>> GetAllVehiclesAsync()
        {

            try
            {
                var response = await _httpClient.GetFromJsonAsync<IEnumerable<VehicleModel>>("api/Vehicle");

                if (response != null)
                {
                    return response;
                }
                return VehicleData.Instance.VehicleModels.ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting vehicles from API {ex.Message}");
                return VehicleData.Instance.VehicleModels.ToList();
            }
        }

        public async Task<VehicleModel?> GetVehicleByIdAsync(Guid id)
        {
            var response = await _httpClient.GetFromJsonAsync<VehicleModel>($"api/vehicle/{id}");

            return response ?? VehicleData.Instance.VehicleModels.FirstOrDefault(v => v != null && v.Id == id);

            //return VehicleData.Instance.VehicleModels.FirstOrDefault(v => v != null && v.Id == id);
        }

        public async Task<VehicleModel?> AddVehicleAsync(VehicleModel vehicle)
        {
            var response = await _httpClient.PostAsJsonAsync("api/vehicle", vehicle);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<VehicleModel>();
            }
            else
            {
                return null;
            }
        }

        public async Task<VehicleModel?> UpdateVehicleAsync(VehicleModel vehicle)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/vehicle", vehicle);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<VehicleModel>();
            }
            else
            {
                return null;
            }
        }

        public async Task<VehicleModel?> DeleteVehicleAsync(VehicleModel? vehicle)
        {
            if (vehicle != null)
            {
                var response = await _httpClient.DeleteAsync($"api/vehicle/{vehicle.Id}");

                return response.IsSuccessStatusCode ? vehicle : null;
            }
            else
            {
                return null;
            }
        }

    }

    public interface IVehicleServices
    {
        Task<IEnumerable<VehicleModel?>> GetAllVehiclesAsync();
        Task<VehicleModel?> GetVehicleByIdAsync(Guid id);
        Task<VehicleModel?> AddVehicleAsync(VehicleModel vehicle);

        Task<VehicleModel?> UpdateVehicleAsync(VehicleModel vehicle);
        Task<VehicleModel?> DeleteVehicleAsync(VehicleModel? vehicle);
    }
}
