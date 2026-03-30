using PharmacyManagementSystem.Application.DTOs.UserDTOs;
using PharmacyManagementSystem.WebAppMVC.Helpers;
using PharmacyManagementSystem.WebAppMVC.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PharmacyManagementSystem.WebAppMVC.Services.Implementations
{
    public class ApiUserServices : IApiUserServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private SessionHelper _sessionHelper;
        private readonly JsonSerializerOptions _jsonOptions;

        public ApiUserServices(IHttpClientFactory httpClientFactory, SessionHelper sessionHelper)
        {
            _httpClientFactory = httpClientFactory;
            _sessionHelper = sessionHelper;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };
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

        public async Task<IEnumerable<GetUserDto?>> GetAllUsersAsync()
        {
            var client = CreateAuthenticatedClient();
            var response = await client.GetAsync("Users/GetAll");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<IEnumerable<GetUserDto>>(content, _jsonOptions);
                return result ?? new List<GetUserDto>();
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            string errorMessage = "Invalid retrive attempt.";

            try
            {
                var errorObj = JsonSerializer.Deserialize<Dictionary<string, string>>(errorContent, _jsonOptions);
                if (errorObj != null && errorObj.ContainsKey("message"))
                {
                    errorMessage = errorObj["message"];
                }
            }
            catch { }

            return new List<GetUserDto>
            {
                new GetUserDto
                {
                    UserId = 0,
                    Message = errorMessage
                }
            };
        }

        public async Task<GetUserDto?> GetUserByEmailAsync(string email)
        {
            var client = CreateAuthenticatedClient();
            var response = await client.GetAsync($"Users/GetUserByEmail/{email}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<GetUserDto>(content, _jsonOptions);
                return result;
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            string errorMessage = "Invalid retrive attempt.";

            try
            {
                var errorObj = JsonSerializer.Deserialize<Dictionary<string, string>>(errorContent, _jsonOptions);
                if (errorObj != null && errorObj.ContainsKey("message"))
                {
                    errorMessage = errorObj["message"];
                }
            }
            catch { }

            return new GetUserDto
            {
                UserId = 0,
                Message = errorMessage,
            };
        }

        public async Task<GetUserDto?> GetUserByIdAsync(int id)
        {
            var client = CreateAuthenticatedClient();
            var response = await client.GetAsync($"Users/GetById/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<GetUserDto>(content, _jsonOptions);
                return result;
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            string errorMessage = "Invalid retrive attempt.";

            try
            {
                var errorObj = JsonSerializer.Deserialize<Dictionary<string, string>>(errorContent, _jsonOptions);
                if (errorObj != null && errorObj.ContainsKey("message"))
                {
                    errorMessage = errorObj["message"];
                }
            }
            catch { }

            return new GetUserDto
            {
                UserId = 0,
                Message = errorMessage,
            };
        }

        public async Task<GetUserDto?> GetUserByNameAsync(string username)
        {
            var client = CreateAuthenticatedClient();
            var response = await client.GetAsync($"Users/GetUserByName/{username}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<GetUserDto>(content, _jsonOptions);
                return result;
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            string errorMessage = "Invalid retrive attempt.";

            try
            {
                var errorObj = JsonSerializer.Deserialize<Dictionary<string, string>>(errorContent, _jsonOptions);
                if (errorObj != null && errorObj.ContainsKey("message"))
                {
                    errorMessage = errorObj["message"];
                }
            }
            catch { }

            return new GetUserDto
            {
                UserId = 0,
                Message = errorMessage,
            };
        }

        public async Task<IEnumerable<GetUserDto?>> GetUsersByRoleAsync(string roleName)
        {
            var client = CreateAuthenticatedClient();
            var response = await client.GetAsync($"Users/GetUsersByRole/{roleName}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<IEnumerable<GetUserDto>>(content, _jsonOptions);
                return result ?? new List<GetUserDto>();
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            string errorMessage = "Invalid retrive attempt.";

            try
            {
                var errorObj = JsonSerializer.Deserialize<Dictionary<string, string>>(errorContent, _jsonOptions);
                if (errorObj != null && errorObj.ContainsKey("message"))
                {
                    errorMessage = errorObj["message"];
                }
            }
            catch { }

            return new List<GetUserDto>
            {
                new GetUserDto
                {
                    UserId = 0,
                    Message = errorMessage
                }
            };
        }
    }
}
