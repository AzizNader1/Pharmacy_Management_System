using PharmacyManagementSystem.WebAppMVC.Services.Interfaces;

namespace PharmacyManagementSystem.WebAppMVC.Services.Implementations
{
    public class ApiAccountServices : IApiAccountServices
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiAccountServices(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}
