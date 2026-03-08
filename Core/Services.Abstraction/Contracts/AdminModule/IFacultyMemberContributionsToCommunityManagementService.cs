using Shared.Dtos.AcademicDataModule.ContributionsModule;
using Shared.SpecificationParameters.AcademicDataModule.ContributionsModule;

namespace Services.Abstraction.Contracts.AdminModule
{
    public interface IContributionsToCommunityServiceManagementService
    {
        Task<PaginatedResult<ContributionsToCommunityServiceResponseDTO>> GetFacultyMemberContributionsToCommunityServiceAsync(
            ContributionsToCommunityServiceSpecificationParameters parameters,
            string facultyMemberEmail);

        Task<ContributionsToCommunityServiceResponseDTO> GetFacultyMemberContributionToCommunityServiceByIdAsync(int id);

        Task<ContributionsToCommunityServiceResponseDTO> CreateFacultyMemberContributionToCommunityServiceAsync(
            ContributionsToCommunityServiceCreateDTO contributionsToCommunityServiceCreateDto,
            string facultyMemberEmail);

        Task<ContributionsToCommunityServiceResponseDTO> UpdateFacultyMemberContributionToCommunityServiceAsync(
            int contributionToCommunityServiceId,
            ContributionsToCommunityServiceUpdateDTO contributionsToCommunityServiceUpdateDto);

        Task DeleteFacultyMemberContributionToCommunityServiceAsync(int contributionToCommunityServiceId);
    }
}
