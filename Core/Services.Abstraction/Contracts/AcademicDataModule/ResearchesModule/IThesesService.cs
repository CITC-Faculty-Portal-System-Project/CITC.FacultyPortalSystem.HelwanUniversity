using Microsoft.AspNetCore.Http;
using Shared.Dtos.AcademicDataModule.HigherStudiesModule;
using Shared.Dtos.ResearchesModule;
using Shared.SpecificationParameters.ResearchesModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.ResearchesModule
{
    public interface IThesesService
    {
        public Task<ThesesResponseDTO> AddTheses(ThesesDTO theses);
        public Task<ThesesResponseDTO> GetThesesById(int Id);
        public Task<PaginatedResult<ThesesResponseDTO>> GetAllTheses(ThesesSpecificationParameters parameters);
        public Task DeleteTheses(int Id);
        public Task<ThesesResponseDTO> UpdateTheses(int id, ThesesUpdateDTO theses);
     
    }
}
