using PharmacyManagementSystem.WebAppMVC.Services.Interfaces;

namespace PharmacyManagementSystem.WebAppMVC.Services.Implementations
{
    public class ApiMedicineServices : IApiMedicineServices
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiMedicineServices(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}
