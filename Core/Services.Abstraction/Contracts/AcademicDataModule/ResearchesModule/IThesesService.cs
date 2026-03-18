using Microsoft.AspNetCore.Http;
using Shared.Dtos.AcademicDataModule.HigherStudiesModule;
using Shared.Dtos.ResearchesModule;
using Shared.SpecificationParameters.ResearchesModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.ResearchesModule
{
    public interface IThesesService
    {
        Task<ThesesResponseDTO> AddTheses(
                ThesesDTO theses,
                Guid? facultyMemberId = null);

        Task<ThesesResponseDTO> GetThesesById(
            int id,
            Guid? facultyMemberId = null);

        Task<PaginatedResult<ThesesResponseDTO>> GetAllTheses(
            ThesesSpecificationParameters parameters,
            Guid? facultyMemberId = null);

        Task DeleteTheses(
            int id,
            Guid? facultyMemberId = null);

        Task<ThesesResponseDTO> UpdateTheses(
            int id,
            ThesesUpdateDTO theses,
            Guid? facultyMemberId = null);
    }
}
