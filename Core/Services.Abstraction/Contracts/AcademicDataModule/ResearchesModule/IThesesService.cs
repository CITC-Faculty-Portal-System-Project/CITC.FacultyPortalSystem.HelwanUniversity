using Microsoft.AspNetCore.Http;
using Shared.Dtos.HigherStudiesModule;
using Shared.Dtos.ResearchesModule;
using Shared.SpecificationParameters.ResearchesModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.ResearchesModule
{
    public interface IThesesService
    {
        public Task<ThesesResponseDTO> AddTheses(ThesesDTO theses);
        public Task<ThesesResponseDTO> GetThesesById(int Id);
        public Task<PaginatedResult<ThesesResponseDTO>> GetAllTheses(ThesesSpecificationParameters parameters);
    }
}
