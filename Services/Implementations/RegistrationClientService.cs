using Microsoft.Extensions.Configuration;
using Shared.Dtos.IdentityModule;
using System.Text.Json;

namespace Services.Implementations
{
    public class RegistrationClientService(HttpClient _httpClient, IConfiguration _configuration) : IRegistrationClientService
    {
        public async Task<UserRegistrationClientDto?> CheckNationalNumber(string nationalNumber)
        {
            var response = await _httpClient.GetAsync($"{_configuration["FarooqExternalSystem"]}/{nationalNumber}");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<UserRegistrationClientDto?>(json);

            return data;
        }
    }
}
