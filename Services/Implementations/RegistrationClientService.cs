using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Shared.Dtos.IdentityModule;
using Shared.Enums.Logging;
using System.Net;
using System.Text.Json;

namespace Services.Implementations
{
    public class RegistrationClientService(HttpClient _httpClient, IConfiguration _configuration, ILogger<RegistrationClientService> _logger) : IRegistrationClientService
    {
        public async Task<UserRegistrationClientDto?> CheckNationalNumber(string nationalNumber)
        {
            var clientLog = new LogEntry
            {
                Category = Category.Authentication.ToString(),
                CategoryAction = CategoryAction.CheckNationalNumber.ToString(),
            };

            var response = await _httpClient.GetAsync($"{_configuration["FarooqExternalSystem"]}/{nationalNumber}");
            if (response.IsSuccessStatusCode)
            {
				try
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var data = JsonSerializer.Deserialize<UserRegistrationClientDto?>(json);
					#region Log
					clientLog.Timestamp = DateTime.Now;
					clientLog.Level = "Information";
					clientLog.RenderedMessage = $"Successfully retrieved data for national number: {nationalNumber}";
					clientLog.AdditionalData = $"Retrieved user national number and email after registeration from the endpoint {_configuration["FarooqExternalSystem"]}/{nationalNumber}";
					clientLog.UserIP = "REQUEST_IP";
					_logger.LogInformation("{@LogDetails}", clientLog);
					#endregion
					return data;
                }
                catch (Exception ex)
                {
                    #region Log
                    clientLog.Timestamp = DateTime.Now;
					clientLog.Level = "Error";
					clientLog.RenderedMessage = $"Error in retriving registered user data {nationalNumber}";
					clientLog.AdditionalData = $"An error occurred while processing the response from the external system for national number: {nationalNumber}.";
					clientLog.Exception = ex.ToString();
					clientLog.ExceptionMessage = ex.Message;
					clientLog.ExceptionDetail = ex.StackTrace;
					_logger.LogError("{@LogDetails}", clientLog);
					#endregion
					throw new Exception("An error occurred while processing the response from the external system.");
                }
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                #region Log
                clientLog.Timestamp = DateTime.Now;
				clientLog.Level = "Warning";
				clientLog.RenderedMessage = $"No data found for national number: {nationalNumber}";
				clientLog.AdditionalData = $"The external system returned a 404 Not Found for national number: {nationalNumber}. This may indicate that the national number is invalid or not registered.";
				clientLog.UserIP = "REQUEST_IP";
				_logger.LogWarning("{@LogDetails}", clientLog);
				#endregion
				throw new NotFoundException($"User with National Number {nationalNumber} not Found.");
			}
			#region Log
			clientLog.Timestamp = DateTime.Now;
			clientLog.Level = "Error";
			clientLog.RenderedMessage = $"Failed to retrieve data for national number: {nationalNumber}";
			clientLog.AdditionalData = $"The external endpoint {_configuration["FarooqExternalSystem"]}/{nationalNumber} returned an unsuccessful status code ({response.StatusCode}) for national number: {nationalNumber}.";
			clientLog.UserIP = "REQUEST_IP";
			_logger.LogError("{@LogDetails}", clientLog);
			#endregion
			throw new Exception("An error occurred while communicating with the external system.");
		}
    }
}
