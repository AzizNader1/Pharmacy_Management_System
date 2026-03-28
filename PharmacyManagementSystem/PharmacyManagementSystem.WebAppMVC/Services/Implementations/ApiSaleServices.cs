using PharmacyManagementSystem.WebAppMVC.Services.Interfaces;

namespace PharmacyManagementSystem.WebAppMVC.Services.Implementations
{
    public class ApiSaleServices : IApiSaleServices
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiSaleServices(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}
