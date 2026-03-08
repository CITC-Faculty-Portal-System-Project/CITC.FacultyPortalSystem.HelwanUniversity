using Shared.Dtos.AcademicDataModule.ContributionsModule;
using Shared.SpecificationParameters.AcademicDataModule.ContributionsModule;

public interface IContributionsToCommunityServiceHelper
{
    Task<PaginatedResult<ContributionsToCommunityServiceResponseDTO>> GetAllContributionsToCommunityServiceAsync(
        ContributionsToCommunityServiceSpecificationParameters parameters,
        string facultyMemberEmail);

    Task<ContributionsToCommunityServiceResponseDTO> GetContributionToCommunityServiceByIdAsync(
        int id
       );

    Task<ContributionsToCommunityServiceResponseDTO> CreateContributionToCommunityServiceAsync(
        ContributionsToCommunityServiceCreateDTO contributionsToCommunityServiceCreateDto,
        string facultyMemberEmail);

    Task<ContributionsToCommunityServiceResponseDTO> UpdateContributionToCommunityServiceAsync(
        int contributionToCommunityServiceId,
        ContributionsToCommunityServiceUpdateDTO contributionsToCommunityServiceUpdateDto
        );

    Task DeleteContributionToCommunityServiceAsync(
        int contributionToCommunityServiceId);
}