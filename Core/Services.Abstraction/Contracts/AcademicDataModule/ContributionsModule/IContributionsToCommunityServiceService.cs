using Shared.Dtos.AcademicDataModule.ContributionsModule;
using Shared.SpecificationParameters.AcademicDataModule.ContributionsModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.ContributionsModule
{
    public interface IContributionsToCommunityServiceService
    {
        Task<PaginatedResult<ContributionsToCommunityServiceResponseDTO>> GetAllContributionsToCommunityServiceAsync(
       ContributionsToCommunityServiceSpecificationParameters parameters,
       string? facultyMemberEmail = null);

        Task<ContributionsToCommunityServiceResponseDTO> GetContributionToCommunityServiceByIdAsync(
            int id,
            string? facultyMemberEmail = null);

        Task<ContributionsToCommunityServiceResponseDTO> CreateContributionToCommunityServiceAsync(
            ContributionsToCommunityServiceCreateDTO contributionsToCommunityServiceCreateDto,
            string? facultyMemberEmail = null);

        Task<ContributionsToCommunityServiceResponseDTO> UpdateContributionToCommunityServiceAsync(
            int contributionToCommunityServiceId,
            ContributionsToCommunityServiceUpdateDTO contributionsToCommunityServiceUpdateDto,
            string? facultyMemberEmail = null);

        Task DeleteContributionToCommunityServiceAsync(
            int contributionToCommunityServiceId,
            string? facultyMemberEmail = null);
    }
}
