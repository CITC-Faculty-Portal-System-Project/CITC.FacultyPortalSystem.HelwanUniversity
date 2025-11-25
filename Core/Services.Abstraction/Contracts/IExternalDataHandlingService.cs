using Shared.Dtos.ScientificProgressionModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction.Contracts
{
    public interface IExternalDataHandlingService
    {
        public Task<AcademicQualificationCreateDto> AcademicDataHandle(string? json);
        public Task ManagerialDataHandle(string? json);
        public Task EmploymentDataHandle(string? json);


    }
}
