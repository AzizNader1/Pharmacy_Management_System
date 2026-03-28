using PharmacyManagementSystem.WebAppMVC.Services.Interfaces;

namespace PharmacyManagementSystem.WebAppMVC.Services.Implementations
{
    public class ApiSaleItemsServices : IApiSaleItemsServices
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiSaleItemsServices(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}
