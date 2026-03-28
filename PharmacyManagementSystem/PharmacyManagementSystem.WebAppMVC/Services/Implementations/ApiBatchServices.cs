using PharmacyManagementSystem.WebAppMVC.Services.Interfaces;

namespace PharmacyManagementSystem.WebAppMVC.Services.Implementations
{
    public class ApiBatchServices : IApiBatchServices
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiBatchServices(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}
