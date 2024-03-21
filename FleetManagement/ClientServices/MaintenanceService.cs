using Shared.ApiModels;
using System.Net.Http.Json;

namespace FleetManagement.ClientServices
{
    public class MaintenanceService (HttpClient httpClient,ILogger<MaintenanceService> logger):ServiceBase(httpClient,logger), IMaintenanceService
    {
        public async Task<MaintainanceModel?> GetMaintainanceModelByIdAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<MaintainanceModel>($"api/maintenance/{id}");

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
        //public async Task<IEnumerable<MaintainanceModel?>> GetMaintainanceModelsAsync(Guid Id)
        //{
        //    try
        //    {
        //        var response = await _httpClient.GetFromJsonAsync<IEnumerable<MaintainanceModel>>($"api/maintenance/{Id}");

        //        if (response is not null)
        //        {
        //            logger.LogInformation("MaintainanceModels found");
        //            return response;
        //        }
        //        logger.LogInformation("MaintainanceModels not found");
        //        return null;
        //    }
        //    catch (Exception)
        //    {
        //        logger.LogError("Error getting MaintainanceModels");
        //        return null;
        //    }
        //}

        async Task<MaintainanceModel> IMaintenanceService.CreateMaintainanceModelAsync(MaintainanceModel maintainanceModel)
        {
            try
            {
                if (maintainanceModel is not null)
                {
                    var response = await _httpClient.PostAsJsonAsync("api/maintainance",maintainanceModel);

                    if (response.IsSuccessStatusCode)
                    {
                        logger.LogInformation("MaintainanceModel created");
                        return await response.Content.ReadFromJsonAsync<MaintainanceModel>();
                    }
                    else
                    {
                        logger.LogInformation("MaintainanceModel not created");
                        return null;
                    }
                }
                logger.LogInformation("MaintainanceModel not created");
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    public interface IMaintenanceService
    {
        Task<MaintainanceModel> GetMaintainanceModelByIdAsync(Guid id);
        //Task<MaintainanceModel> GetMaintainanceModelByVehicleIdAsync(Guid vehicleId);
        //Task<IEnumerable<MaintainanceModel>> GetMaintainanceModelsAsync(Guid Id);
        Task<MaintainanceModel> CreateMaintainanceModelAsync(MaintainanceModel maintainanceModel);

    }
}
