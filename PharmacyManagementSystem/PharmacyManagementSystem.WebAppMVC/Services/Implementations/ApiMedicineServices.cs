using PharmacyManagementSystem.Application.DTOs.MedicineDTOs;
using PharmacyManagementSystem.WebAppMVC.Helpers;
using PharmacyManagementSystem.WebAppMVC.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PharmacyManagementSystem.WebAppMVC.Services.Implementations
{
    public class ApiMedicineServices : IApiMedicineServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private SessionHelper _sessionHelper;
        private readonly JsonSerializerOptions _jsonOptions;

        public ApiMedicineServices(IHttpClientFactory httpClientFactory, SessionHelper sessionHelper)
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

        public async Task<GetMedicineDto>? CreateMedicineAsync(CreateMedicineDto medicine)
        {
            var client = CreateAuthenticatedClient();
            var json = JsonSerializer.Serialize(medicine, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/Medicines/Add", content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseData = JsonSerializer.Deserialize<int>(responseContent, _jsonOptions);
                return new GetMedicineDto
                {
                    MedicineId = responseData
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

            return new GetMedicineDto
            {
                MedicineId = 0,
                Message = errorMessage
            };
        }

        public async Task<bool>? DeleteMedicineAsync(int id)
        {
            var client = CreateAuthenticatedClient();
            var response = await client.DeleteAsync($"api/Medicines/Delete/{id}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<GetMedicineDto?>> GetAllMedicinesAsync()
        {
            var client = CreateAuthenticatedClient();
            var response = await client.GetAsync("api/Medicines/GetAll");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<IEnumerable<GetMedicineDto>>(content, _jsonOptions);
                return result ?? new List<GetMedicineDto>();
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

            return new List<GetMedicineDto>
            {
                new GetMedicineDto{MedicineId = 0, Message = errorMessage}
            };
        }

        public async Task<IEnumerable<GetMedicineDto?>> GetAllMedicinesByCategoryAsync(string categoryName)
        {
            var client = CreateAuthenticatedClient();
            var response = await client.GetAsync($"api/Medicines/GetAllMedicinesByCategory/{categoryName}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<IEnumerable<GetMedicineDto>>(content, _jsonOptions);
                return result ?? new List<GetMedicineDto>();
            }

            return new List<GetMedicineDto>();
        }

        public async Task<GetMedicineDto?> GetMedicineByIdAsync(int id)
        {
            var client = CreateAuthenticatedClient();
            var response = await client.GetAsync($"api/Medicines/GetById/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<GetMedicineDto>(content, _jsonOptions);
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

            return new GetMedicineDto
            {
                MedicineId = 0,
                Message = errorMessage
            };
        }

        public async Task<GetMedicineDto?> GetMedicineByNameAsync(string medicineName)
        {
            var client = CreateAuthenticatedClient();
            var response = await client.GetAsync($"api/Medicines/GetMedicineByName/{medicineName}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<GetMedicineDto>(content, _jsonOptions);
                return result;
            }

            return null;
        }

        public async Task<GetMedicineDto>? UpdateMedicineAsync(int id, UpdateMedicineDto medicine)
        {
            var client = CreateAuthenticatedClient();
            var json = JsonSerializer.Serialize(medicine, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"api/Medicines/Update?id={id}", content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseData = JsonSerializer.Deserialize<GetMedicineDto>(responseContent, _jsonOptions);
                return responseData ?? new GetMedicineDto();
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

            return new GetMedicineDto
            {
                MedicineId = 0,
                Message = errorMessage
            };
        }
    }
}
