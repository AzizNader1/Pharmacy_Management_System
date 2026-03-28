using PharmacyManagementSystem.Application.DTOs.UserDTOs;
using PharmacyManagementSystem.WebAppMVC.Helpers;
using PharmacyManagementSystem.WebAppMVC.Services.Interfaces;
using System.Net.Http.Headers;

namespace PharmacyManagementSystem.WebAppMVC.Services.Implementations
{
    public class ApiUserServices : IApiUserServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private SessionHelper _sessionHelper;

        public ApiUserServices(IHttpClientFactory httpClientFactory, SessionHelper sessionHelper)
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
        public Task<IEnumerable<GetUserDto?>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GetUserDto?> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<GetUserDto?> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GetUserDto?> GetUserByNameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GetUserDto?>> GetUsersByRoleAsync(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
