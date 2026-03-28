using PharmacyManagementSystem.WebAppMVC.Helpers;
using PharmacyManagementSystem.WebAppMVC.Services.Interfaces;
using System.Net.Http.Headers;

namespace PharmacyManagementSystem.WebAppMVC.Services.Implementations
{
    public class ApiBatchServices : IApiBatchServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private SessionHelper _sessionHelper;

        public ApiBatchServices(IHttpClientFactory httpClientFactory, SessionHelper sessionHelper)
        {
            _httpClientFactory = httpClientFactory;
            _sessionHelper = sessionHelper;
        }

        private HttpClient CreateAuthenticatedClient()
        {
            var client = _httpClientFactory.CreateClient("PharmacyApi");

            var token = _sessionHelper.GetToken();
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }
    }
}
