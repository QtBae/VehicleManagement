namespace FleetManagement.ClientServices
{
    public abstract class ServiceBase(HttpClient httpClient,ILogger<ServiceBase> logger)
    {
        protected readonly HttpClient _httpClient = httpClient;
        protected readonly ILogger<ServiceBase> _logger = logger;
    }
}
