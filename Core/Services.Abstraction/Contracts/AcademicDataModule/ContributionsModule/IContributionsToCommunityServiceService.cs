using Shared.Dtos.AcademicDataModule.ContributionsModule;
using Shared.SpecificationParameters.AcademicDataModule.ContributionsModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.ContributionsModule
{
    public interface IContributionsToCommunityServiceService
    {
        public Task<PaginatedResult<ContributionsToCommunityServiceResponseDTO>> GetAllContributionsToCommunityServiceAsync(ContributionsToCommunityServiceSpecificationParameters parameters);
        public Task<ContributionsToCommunityServiceResponseDTO> GetContributionToCommunityServiceByIdAsync(int id);
        public Task<ContributionsToCommunityServiceResponseDTO> CreateContributionToCommunityServiceAsync(ContributionsToCommunityServiceCreateDTO contributionsToCommunityServiceCreateDto);
        public Task<ContributionsToCommunityServiceResponseDTO> UpdateContributionToCommunityServiceAsync(int contributionToCommunityServiceId, ContributionsToCommunityServiceUpdateDTO contributionsToCommunityServiceUpdateDto);
        public Task DeleteContributionToCommunityServiceAsync(int contributionToCommunityServiceId);
    }
}
