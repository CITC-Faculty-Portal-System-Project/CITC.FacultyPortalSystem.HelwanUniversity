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
        public Task<bool> AcademicDataHandle(string? json);
        public Task<bool> ManagerialDataHandle(string? json);
        public Task<bool> EmploymentDataHandle(string? json);
        public Task<bool> ContactDataHandle(string? json);
        public Task<bool> PersonalDataHandle(string? json);
        public Task<bool> SpecializationDataHandle(string? json);
        public Task<bool> ScientificDutyDataHandle(string? json);
        public Task<bool> TrainingProgramDataHandle(string? json);
        public Task<bool> ThesisSupervisingDataHandle(string? json);
        public Task<bool> ThesisDataHandle(string? json);







    }
}
