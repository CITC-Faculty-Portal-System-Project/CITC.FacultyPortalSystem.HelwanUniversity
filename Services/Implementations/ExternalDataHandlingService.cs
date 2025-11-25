using Shared.Dtos.ScientificProgressionModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class ExternalDataHandlingService : IExternalDataHandlingService
    {
        public Task<AcademicQualificationCreateDto> AcademicDataHandle(string? json)
        {

            if (string.IsNullOrWhiteSpace(json))
                throw new ArgumentException("JSON is null or empty", nameof(json));

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var dto = JsonSerializer.Deserialize<AcademicQualificationCreateDto>(json, options);

            if (dto == null)
                throw new InvalidOperationException("Failed to deserialize JSON");

            return Task.FromResult(dto);

        }

        public Task EmploymentDataHandle(string? json)
        {
            Console.WriteLine(json); return Task.CompletedTask;
        }

        public Task ManagerialDataHandle(string? json)
        {
            Console.WriteLine(json); return Task.CompletedTask;
        }
    }
}
