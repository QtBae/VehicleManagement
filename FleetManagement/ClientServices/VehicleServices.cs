using FleetManagement.Data;
using Shared.ApiModels;
using System.Net.Http.Json;

namespace FleetManagement.ClientServices
{
    public class VehicleServices:IVehicleServices
    {
        private readonly HttpClient _httpClient;
        public VehicleServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<VehicleModel>> GetAllVehiclesAsync()
        {

            try
            {
                var response = await _httpClient.GetFromJsonAsync<IEnumerable<VehicleModel>>("api/vehicles");

                if (!response!.Any())
                {
                    return response;
                }
                else
                {
                    return VehicleData.GetVehicles();
                }
            }
            catch (Exception)
            {

                return VehicleData.GetVehicles();
            }
        }

        public async Task<VehicleModel> GetVehicleByIdAsync(Guid id)
        {
            var response = await _httpClient.GetFromJsonAsync<VehicleModel>($"api/vehicles/{id}");

            if (response != null)
            {
                return response;
            }
            else
            {
                return VehicleData.GetVehicles().FirstOrDefault(v => v.Id == id);
            }
        }

        public async Task<VehicleModel> AddVehicleAsync(VehicleModel vehicle)
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

        public async Task<VehicleModel> UpdateVehicleAsync(VehicleModel vehicle)
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

        public async Task<VehicleModel> DeleteVehicleAsync(VehicleModel vehicle)
        {
            var response = await _httpClient.DeleteAsync($"api/vehicles/{vehicle.Id}");

            if (response.IsSuccessStatusCode)
            {
                return vehicle;
            }
            else
            {
                return null;
            }
        }

    }

    public interface IVehicleServices
    {
        Task<IEnumerable<VehicleModel>> GetAllVehiclesAsync();
        Task<VehicleModel> GetVehicleByIdAsync(Guid id);
        Task<VehicleModel> AddVehicleAsync(VehicleModel vehicle);

        Task<VehicleModel> UpdateVehicleAsync(VehicleModel vehicle);
        Task<VehicleModel> DeleteVehicleAsync(VehicleModel vehicle);
    }
}
