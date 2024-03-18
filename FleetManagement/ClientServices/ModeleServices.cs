using Shared.ApiModels;
using System.Net.Http.Json;

namespace FleetManagement.ClientServices
{
    public class ModeleServices: IModeleServices
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ModeleServices> _logger;

        public ModeleServices(HttpClient httpClient, ILogger<ModeleServices> listLogger)
        {
            _httpClient = httpClient;
            _logger = listLogger;
        }

        public async Task<List<CarModel>> GetModelesAsync()
        {
            try
            {
                var modeles = await _httpClient.GetFromJsonAsync<List<CarModel>>("api/modele");

                if (modeles == null)
                {
                    _logger.LogError("Failed to retrieve modeles");
                    return new List<CarModel>();
                }
                return modeles;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving modeles", ex);
            }
        }

        public async Task<CarModel> GetModeleAsync(Guid id)
        {
            try
            {
                var modele = await _httpClient.GetFromJsonAsync<CarModel>($"api/modele/{id}");

                if (modele == null)
                {
                    _logger.LogError($"Failed to retrieve modele with id {id}");
                    return new CarModel();
                }
                return modele;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving modele with id {id}", ex);
            }
        }

        public async Task<CarModel> AddMedele(CarModel modele)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/modele", modele);
                

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Failed to add modele");
                    return null;
                }
                var result = await response.Content.ReadFromJsonAsync<CarModel>();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding modele", ex);
            }
        }

        public async Task<CarModel> UpdateModele(CarModel modele)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("api/modele", modele);
                

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Failed to update modele");
                    return null;
                }
                var result = await response.Content.ReadFromJsonAsync<CarModel>();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating modele", ex);
            }
        }

        public async Task DeleteModele(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/modele/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"Failed to delete modele with id {id}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting modele with id {id}", ex);
            }
        }
    }

    public interface IModeleServices
    {
        Task<List<CarModel>> GetModelesAsync();

        Task<CarModel> GetModeleAsync(Guid id);
        Task<CarModel> AddMedele(CarModel modele);
        Task<CarModel> UpdateModele(CarModel modele);
        Task DeleteModele(Guid id);

    }
}
