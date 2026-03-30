using PharmacyManagementSystem.Application.DTOs.SalesItemsDTOs;
using PharmacyManagementSystem.WebAppMVC.Helpers;
using PharmacyManagementSystem.WebAppMVC.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PharmacyManagementSystem.WebAppMVC.Services.Implementations
{
    public class ApiSaleItemsServices : IApiSaleItemsServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private SessionHelper _sessionHelper;
        private readonly JsonSerializerOptions _jsonOptions;

        public ApiSaleItemsServices(IHttpClientFactory httpClientFactory, SessionHelper sessionHelper)
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

        public async Task<GetSaleItemDto>? CreateSaleItemAsync(CreateSaleItemDto saleItem)
        {
            var client = CreateAuthenticatedClient();
            var json = JsonSerializer.Serialize(saleItem, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/SaleItems/Add", content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseData = JsonSerializer.Deserialize<int>(responseContent, _jsonOptions);
                return new GetSaleItemDto
                {
                    SaleItemId = responseData
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

            return new GetSaleItemDto
            {
                SaleItemId = 0,
                Message = errorMessage,
            };
        }

        public async Task<bool>? DeleteSaleItemAsync(int id)
        {
            var client = CreateAuthenticatedClient();
            var response = await client.DeleteAsync($"api/SaleItems/Delete/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<GetSaleItemDto?>> GetAllSaleItemesAsync()
        {
            var client = CreateAuthenticatedClient();
            var response = await client.GetAsync("api/SaleItems/GetAll");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<IEnumerable<GetSaleItemDto>>(content, _jsonOptions);
                return result ?? new List<GetSaleItemDto>();
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

            return new List<GetSaleItemDto>
            { new GetSaleItemDto
            {
                SaleItemId = 0,
                Message = errorMessage
            }
            };
        }

        public async Task<IEnumerable<GetSaleItemDto?>> GetAllSaleItemesBySaleIdAsync(int saleId)
        {
            var client = CreateAuthenticatedClient();
            var response = await client.GetAsync($"SaleItems/GetAllSaleItemesBySaleId/{saleId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<IEnumerable<GetSaleItemDto>>(content, _jsonOptions);
                return result ?? new List<GetSaleItemDto>();
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

            return new List<GetSaleItemDto>
            { new GetSaleItemDto
            {
                SaleItemId = 0,
                Message = errorMessage
            }
            };
        }

        public async Task<GetSaleItemDto?> GetSaleItemByIdAsync(int id)
        {
            var client = CreateAuthenticatedClient();
            var response = await client.GetAsync($"SaleItems/GetById/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<GetSaleItemDto>(content, _jsonOptions);
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

            return new GetSaleItemDto
            {
                SaleItemId = 0,
                Message = errorMessage,
            };
        }

        public async Task<GetSaleItemDto>? UpdateSaleItemAsync(int id, UpdateSaleItemDto saleItem)
        {
            var client = CreateAuthenticatedClient();
            var json = JsonSerializer.Serialize(saleItem, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"SaleItems/Update?id={id}", content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseData = JsonSerializer.Deserialize<GetSaleItemDto>(responseContent, _jsonOptions);
                return responseData ?? new GetSaleItemDto();
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

            return new GetSaleItemDto
            {
                SaleItemId = 0,
                Message = errorMessage,
            };
        }
    }
}
