using Shared.ApiModels;
using System.Net.Http.Json;

namespace FleetManagement.ClientServices
{
    public class MaintenanceService (HttpClient httpClient,ILogger<IMaintenanceService> logger):IMaintenanceService
    {
        private readonly HttpClient _httpClient= httpClient;
        public async Task<MaintainanceModel?> GetMaintainanceModelByIdAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<MaintainanceModel>($"api/maintenances/{id}");

                if (response is not null)
                {
                    logger.LogInformation("MaintainanceModel found");
                    return response;
                }
                logger.LogInformation("MaintainanceModel not found");
                return null;
            }
            catch (Exception)
            {
                logger.LogError("Error getting MaintainanceModel");
                return null;
            }
        }

        //public async Task<MaintainanceModel?> GetMaintainanceModelByVehicleIdAsync(Guid vehicleId)
        //{
        //    try
        //    {
        //        var response = await _httpClient.GetFromJsonAsync<MaintainanceModel>($"api/maintenances/vehicle/{vehicleId}");

        //        if (response is not null)
        //        {
        //            logger.LogInformation("MaintainanceModel found");
        //            return response;
        //        }
        //        logger.LogInformation("MaintainanceModel not found");
        //        return null;
        //    }
        //    catch (Exception)
        //    {
        //        logger.LogError("Error getting MaintainanceModel");
        //        return null;
        //    }
        //}

        public async Task<IEnumerable<MaintainanceModel?>> GetMaintainanceModelsAsync(Guid Id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<IEnumerable<MaintainanceModel>>($"api/maintenances/{Id}");

                if (response is not null)
                {
                    logger.LogInformation("MaintainanceModels found");
                    return response;
                }
                logger.LogInformation("MaintainanceModels not found");
                return null;
            }
            catch (Exception)
            {
                logger.LogError("Error getting MaintainanceModels");
                return null;
            }
        }

        async Task<MaintainanceModel> IMaintenanceService.CreateMaintainanceModelAsync(MaintainanceModel maintainanceModel)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/maintenances", maintainanceModel);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<MaintainanceModel>();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        Task<MaintainanceModel> IMaintenanceService.GetMaintainanceModelByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<MaintainanceModel>> IMaintenanceService.GetMaintainanceModelsAsync(Guid Id)
        {
            throw new NotImplementedException();
        }
    }

    public interface IMaintenanceService
    {
        Task<MaintainanceModel> GetMaintainanceModelByIdAsync(Guid id);
        //Task<MaintainanceModel> GetMaintainanceModelByVehicleIdAsync(Guid vehicleId);
        Task<IEnumerable<MaintainanceModel>> GetMaintainanceModelsAsync(Guid Id);
        Task<MaintainanceModel> CreateMaintainanceModelAsync(MaintainanceModel maintainanceModel);

    }
}
