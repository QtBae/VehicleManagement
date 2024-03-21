namespace FleetManagement.ClientServices
{
    public abstract class ServiceBase
    {
        protected readonly HttpClient _httpClient ;
        protected readonly ILogger<ServiceBase> _logger;

        public ServiceBase(HttpClient httpClient,ILogger<ServiceBase> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
    }
}
