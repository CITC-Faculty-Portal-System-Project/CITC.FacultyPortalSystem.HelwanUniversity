using Shared.Dtos.ResearchesModule;
using Shared.SpecificationParameters.ResearchesModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.ResearchesModule
{
    public interface IResearchesService
    {
        public Task<PaginatedResult<ResearchResponseDTO>> GetAllRecommendedResearches(RecommendedResearchesSpecificationParameters parameters);
        public Task<ResearchResponseDTO> ConfirmRecommendedResearch(int researchId);
        public Task DeleteResearch(int researchId);
        public Task<ResearchResponseDTO> GetResearchByTitle(string title);
        public Task<PaginatedResult<ResearchResponseDTO>> GetAllResearches(ResearchSpecificationParameters parameters);
        public Task<ResearchResponseDTO> GetResarchById(int researchId);
        public Task<ResearchResponseDTO> AddResearch(ResearchDTO research);
        public Task RejectResearch(int researchId);
        public Task<ResearchResponseDTO> UpdateResearch(int researchId , ResearchUpdateDTO researchUpdate);


    }
}
