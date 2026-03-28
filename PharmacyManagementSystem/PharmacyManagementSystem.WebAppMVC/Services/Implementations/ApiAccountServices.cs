using PharmacyManagementSystem.Application.DTOs.UserDTOs;
using PharmacyManagementSystem.WebAppMVC.Helpers;
using PharmacyManagementSystem.WebAppMVC.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace PharmacyManagementSystem.WebAppMVC.Services.Implementations
{
    public class ApiAccountServices : IApiAccountServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly SessionHelper _sessionHelper;

        public ApiAccountServices(IHttpClientFactory httpClientFactory, SessionHelper sessionHelper)
        {
            _httpClientFactory = httpClientFactory;
            _sessionHelper = sessionHelper;
        }

        private HttpClient CreateClient()
        {
            return _httpClientFactory.CreateClient("PharmacyApi");
        }

        public async Task<AuthResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var client = CreateClient();

            var json = JsonSerializer.Serialize(loginRequestDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("Auth/Login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var loginResponseDto = JsonSerializer.Deserialize<AuthResponseDto>(
                    responseContent,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

                if (loginResponseDto != null && loginResponseDto.IsAuthenticated)
                {
                    _sessionHelper.SetAuthSession(loginResponseDto);
                }

                return loginResponseDto ?? new AuthResponseDto { IsAuthenticated = false };
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            string errorMessage = "Invalid login attempt.";

            try
            {
                var errorObj = JsonSerializer.Deserialize<Dictionary<string, string>>(errorContent);
                if (errorObj != null && errorObj.ContainsKey("message"))
                {
                    errorMessage = errorObj["message"];
                }
            }
            catch { }

            return new AuthResponseDto { IsAuthenticated = false, Message = errorMessage };
        }

        public async Task<AuthResponseDto> Register(CreateUserDto createUserDto)
        {
            var client = CreateClient();

            var json = JsonSerializer.Serialize(createUserDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("Auth/Register", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var registerResponseDto = JsonSerializer.Deserialize<AuthResponseDto>(
                    responseContent,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

                if (registerResponseDto != null && registerResponseDto.IsAuthenticated)
                {
                    _sessionHelper.SetAuthSession(registerResponseDto);
                }

                return registerResponseDto ?? new AuthResponseDto { IsAuthenticated = false };
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            string errorMessage = "Registration failed.";

            try
            {
                var errorObj = JsonSerializer.Deserialize<Dictionary<string, string>>(errorContent);
                if (errorObj != null && errorObj.ContainsKey("message"))
                {
                    errorMessage = errorObj["message"];
                }
            }
            catch { }

            return new AuthResponseDto { IsAuthenticated = false, Message = errorMessage };
        }

        public void Logout()
        {
            _sessionHelper.ClearSession();
        }
    }
}