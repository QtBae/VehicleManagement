using FleetManagement.Data;
using Shared.ApiModels;
using System.Net.Http.Json;

namespace FleetManagement.ClientServices
{
    public class VehicleServices(HttpClient httpClient) : IVehicleServices
    {
        private readonly HttpClient _httpClient = httpClient;
        public async Task<IEnumerable<VehicleModel?>> GetAllVehiclesAsync()
        {

            try
            {
                var response = await _httpClient.GetFromJsonAsync<IEnumerable<VehicleModel>>("api/vehicles");

                if (response != null)
                {
                    List<VehicleModel?> allVehiclesAsync = response.ToList();
                    return !allVehiclesAsync!.Any() ? allVehiclesAsync : VehicleData.Instance.GetVehicles();
                }
                return VehicleData.Instance.VehicleModels.ToList();
            }
            catch (Exception)
            {
                return VehicleData.Instance.VehicleModels.ToList();
            }
        }

        public async Task<VehicleModel?> GetVehicleByIdAsync(Guid id)
        {
            //var response = await _httpClient.GetFromJsonAsync<VehicleModel>($"api/vehicles/{id}");

            //return response ?? VehicleData.Instance.VehicleModels.FirstOrDefault(v => v != null && v.Id == id);

            return VehicleData.Instance.VehicleModels.FirstOrDefault(v => v != null && v.Id == id);
        }

        public async Task<VehicleModel?> AddVehicleAsync(VehicleModel vehicle)
        {
            var response = await _httpClient.PostAsJsonAsync("api/vehicles", vehicle);

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
            var response = await _httpClient.PutAsJsonAsync($"api/vehicles/{vehicle.Id}", vehicle);

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
                var response = await _httpClient.DeleteAsync($"api/vehicles/{vehicle.Id}");

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
