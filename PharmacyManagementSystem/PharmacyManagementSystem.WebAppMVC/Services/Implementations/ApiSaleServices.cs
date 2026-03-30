using PharmacyManagementSystem.Application.DTOs.SalesDTOs;
using PharmacyManagementSystem.WebAppMVC.Helpers;
using PharmacyManagementSystem.WebAppMVC.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PharmacyManagementSystem.WebAppMVC.Services.Implementations
{
    public class ApiSaleServices : IApiSaleServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private SessionHelper _sessionHelper;
        private readonly JsonSerializerOptions _jsonOptions;

        public ApiSaleServices(IHttpClientFactory httpClientFactory, SessionHelper sessionHelper)
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

        public async Task<GetSaleDto>? CreateSaleAsync(CreateSaleDto sale)
        {
            var client = CreateAuthenticatedClient();
            var json = JsonSerializer.Serialize(sale, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("Sales/Add", content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseData = JsonSerializer.Deserialize<int>(responseContent, _jsonOptions);
                return new GetSaleDto
                {
                    SaleId = responseData
                };
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            string errorMessage = "Invalid creation attempt.";

            try
            {
                var errorObj = JsonSerializer.Deserialize<Dictionary<string, string>>(errorContent, _jsonOptions);
                if (errorObj != null && errorObj.ContainsKey("message"))
                {
                    errorMessage = errorObj["message"];
                }
            }
            catch { }

            return new GetSaleDto
            {
                SaleId = 0,
                Message = errorMessage
            };

        }

        public async Task<bool>? DeleteSaleAsync(int id)
        {
            var client = CreateAuthenticatedClient();
            var response = await client.DeleteAsync($"Sales/Delete/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<GetSaleDto?>> GetAllSalesAsync()
        {
            var client = CreateAuthenticatedClient();
            var response = await client.GetAsync("Sales/GetAll");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<IEnumerable<GetSaleDto>>(content, _jsonOptions);
                return result ?? new List<GetSaleDto>();
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            string errorMessage = "Invalid retive attempt.";

            try
            {
                var errorObj = JsonSerializer.Deserialize<Dictionary<string, string>>(errorContent, _jsonOptions);
                if (errorObj != null && errorObj.ContainsKey("message"))
                {
                    errorMessage = errorObj["message"];
                }
            }
            catch { }

            return new List<GetSaleDto>
            {
                new GetSaleDto
                {
                    SaleId = 0,
                    Message = errorMessage
                }
            };
        }

        public async Task<IEnumerable<GetSaleDto?>> GetAllSalesByUserIdAsync(int userId)
        {
            var client = CreateAuthenticatedClient();
            var response = await client.GetAsync($"Sales/GetAllSalesByUserId/{userId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<IEnumerable<GetSaleDto>>(content, _jsonOptions);
                return result ?? new List<GetSaleDto>();
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            string errorMessage = "Invalid retive attempt.";

            try
            {
                var errorObj = JsonSerializer.Deserialize<Dictionary<string, string>>(errorContent, _jsonOptions);
                if (errorObj != null && errorObj.ContainsKey("message"))
                {
                    errorMessage = errorObj["message"];
                }
            }
            catch { }

            return new List<GetSaleDto>
            {
                new GetSaleDto
                {
                    SaleId = 0,
                    Message = errorMessage
                }
            };
        }

        public async Task<IEnumerable<GetSaleDto?>> GetAllSalesByUserNameAsync(string userName)
        {
            var client = CreateAuthenticatedClient();
            var response = await client.GetAsync($"Sales/GetAllSalesByUserName/{userName}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<IEnumerable<GetSaleDto>>(content, _jsonOptions);
                return result ?? new List<GetSaleDto>();
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            string errorMessage = "Invalid retive attempt.";

            try
            {
                var errorObj = JsonSerializer.Deserialize<Dictionary<string, string>>(errorContent, _jsonOptions);
                if (errorObj != null && errorObj.ContainsKey("message"))
                {
                    errorMessage = errorObj["message"];
                }
            }
            catch { }

            return new List<GetSaleDto>
            {
                new GetSaleDto
                {
                    SaleId = 0,
                    Message = errorMessage
                }
            };
        }

        public async Task<GetSaleDto?> GetSaleByIdAsync(int id)
        {
            var client = CreateAuthenticatedClient();
            var response = await client.GetAsync($"Sales/GetById/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<GetSaleDto>(content, _jsonOptions);
                return result;
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            string errorMessage = "Invalid retive attempt.";

            try
            {
                var errorObj = JsonSerializer.Deserialize<Dictionary<string, string>>(errorContent, _jsonOptions);
                if (errorObj != null && errorObj.ContainsKey("message"))
                {
                    errorMessage = errorObj["message"];
                }
            }
            catch { }

            return new GetSaleDto
            {
                SaleId = 0,
                Message = errorMessage
            };
        }

        public async Task<GetSaleDto>? UpdateSaleAsync(int id, UpdateSaleDto sale)
        {
            var client = CreateAuthenticatedClient();
            var json = JsonSerializer.Serialize(sale, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"Sales/Update?id={id}", content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseData = JsonSerializer.Deserialize<GetSaleDto>(responseContent, _jsonOptions);
                return responseData ?? new GetSaleDto();
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            string errorMessage = "Invalid update attempt.";

            try
            {
                var errorObj = JsonSerializer.Deserialize<Dictionary<string, string>>(errorContent, _jsonOptions);
                if (errorObj != null && errorObj.ContainsKey("message"))
                {
                    errorMessage = errorObj["message"];
                }
            }
            catch { }

            return new GetSaleDto
            {
                SaleId = 0,
                Message = errorMessage
            };
        }
    }
}
