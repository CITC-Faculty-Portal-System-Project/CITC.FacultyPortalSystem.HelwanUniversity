using Shared.Dtos.ResearchesModule;
using Shared.SpecificationParameters.ResearchesModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.ResearchesModule
{
    public interface IResearchesService
    {
        Task<PaginatedResult<ResearchResponseDTO>> GetAllRecommendedResearches(
       ResearchSpecificationParameters parameters,
       Guid? facultyMemberId = null);

        Task<ResearchResponseDTO> ConfirmRecommendedResearch(
            int researchId,
            Guid? facultyMemberId = null);

        Task DeleteResearch(
            int researchId,
            Guid? facultyMemberId = null);

        Task<ResearchResponseDTO> GetResearchByTitle(
            string title,
            Guid? facultyMemberId = null);

        Task<PaginatedResult<ResearchResponseDTO>> GetAllResearches(
            ResearchSpecificationParameters parameters,
            Guid? facultyMemberId = null);

        Task<ResearchResponseDTO> GetResarchById(
            int researchId,
            Guid? facultyMemberId = null);

        Task<ResearchResponseDTO> AddResearch(
            ResearchDTO research,
            Guid? facultyMemberId = null);

        Task RejectResearch(
            int researchId,
            Guid? facultyMemberId = null);

        Task<ResearchResponseDTO> UpdateResearch(
            int researchId,
            ResearchUpdateDTO researchUpdate,
            Guid? facultyMemberId = null);

    }
}
