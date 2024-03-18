using Shared.ApiModels;
using System.Net.Http.Json;

namespace FleetManagement.ClientServices
{
    public class ModelServices(HttpClient httpClient, ILogger<ModelServices> listLogger) : IModelServices
    {
        public async Task<List<CarModel>> GetModelsAsync()
        {
            try
            {
                var models = await httpClient.GetFromJsonAsync<List<CarModel>>("api/model");

                if (models != null) return models;
                listLogger.LogError("Failed to retrieve models");
                return [];
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving modeles", ex);
            }
        }

        public async Task<CarModel> GetModelAsync(Guid id)
        {
            try
            {
                var model = await httpClient.GetFromJsonAsync<CarModel>($"api/modele/{id}");

                if (model != null) return model;
                listLogger.LogError($"Failed to retrieve model with id {id}");
                return new CarModel();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving modele with id {id}", ex);
            }
        }

        public async Task<CarModel?> AddModel(CarModel model)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/modele", model);
                

                if (!response.IsSuccessStatusCode)
                {
                    listLogger.LogError("Failed to add model");
                    return null;
                }
                var result = await response.Content.ReadFromJsonAsync<CarModel>();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding model", ex);
            }
        }

        public async Task<CarModel?> UpdateModel(CarModel model)
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync("api/model", model);
                

                if (!response.IsSuccessStatusCode)
                {
                    listLogger.LogError("Failed to update model");
                    return null;
                }
                var result = await response.Content.ReadFromJsonAsync<CarModel>();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating model", ex);
            }
        }

        public async Task DeleteModel(Guid id)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"api/model/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    listLogger.LogError($"Failed to delete model with id {id}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting model with id {id}", ex);
            }
        }
    }

    public interface IModelServices
    {
        Task<List<CarModel>> GetModelsAsync();

        Task<CarModel> GetModelAsync(Guid id);
        Task<CarModel?> AddModel(CarModel model);
        Task<CarModel?> UpdateModel(CarModel model);
        Task DeleteModel(Guid id);

    }
}
