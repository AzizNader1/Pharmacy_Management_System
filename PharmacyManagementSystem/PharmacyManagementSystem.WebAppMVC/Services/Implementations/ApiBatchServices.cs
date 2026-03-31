using PharmacyManagementSystem.Application.DTOs.BatchDTOs;
using PharmacyManagementSystem.WebAppMVC.Helpers;
using PharmacyManagementSystem.WebAppMVC.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PharmacyManagementSystem.WebAppMVC.Services.Implementations
{
    public class ApiBatchServices : IApiBatchServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private SessionHelper _sessionHelper;
        private readonly JsonSerializerOptions _jsonOptions;

        public ApiBatchServices(IHttpClientFactory httpClientFactory, SessionHelper sessionHelper)
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

        public async Task<GetBatchDto>? CreateBatchAsync(CreateBatchDto batch)
        {
            var client = CreateAuthenticatedClient();
            var json = JsonSerializer.Serialize(batch, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("Batchs/Add", content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseData = JsonSerializer.Deserialize<int>(responseContent, _jsonOptions);
                return new GetBatchDto
                {
                    BatchId = responseData
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

            return new GetBatchDto
            {
                BatchId = 0,
                Message = errorMessage
            };
        }

        public async Task<bool>? DeleteBatchAsync(int id)
        {
            var client = CreateAuthenticatedClient();
            var response = await client.DeleteAsync($"Batchs/Delete/{id}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<List<GetBatchDto?>> GetAllBatchesAsync()
        {
            var client = CreateAuthenticatedClient();
            var response = await client.GetAsync("Batchs/GetAll");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<List<GetBatchDto>>(content, _jsonOptions);
                return result ?? new List<GetBatchDto>();
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

            return new List<GetBatchDto>
            {
                new GetBatchDto{BatchId = 0, Message = errorMessage}
            };
        }

        public async Task<GetBatchDto?> GetBatchByIdAsync(int id)
        {
            var client = CreateAuthenticatedClient();
            var response = await client.GetAsync($"Batchs/GetById/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<GetBatchDto>(content, _jsonOptions);
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

            return new GetBatchDto
            {
                BatchId = 0,
                Message = errorMessage
            };
        }

        public async Task<GetBatchDto>? UpdateBatchAsync(int id, UpdateBatchDto batch)
        {
            var client = CreateAuthenticatedClient();
            var json = JsonSerializer.Serialize(batch, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"Batchs/Update?id={id}", content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseData = JsonSerializer.Deserialize<GetBatchDto>(responseContent, _jsonOptions);
                return responseData ?? new GetBatchDto();
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

            return new GetBatchDto
            {
                BatchId = 0,
                Message = errorMessage
            };
        }
    }
}
