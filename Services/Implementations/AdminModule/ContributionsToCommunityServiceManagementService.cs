using Services.Abstraction.Contracts.AdminModule;
using Shared.Dtos.AcademicDataModule.ContributionsModule;
using Shared.SpecificationParameters.AcademicDataModule.ContributionsModule;

namespace Services.Implementations.AdminModule
{
    public class ContributionsToCommunityServiceManagementService(
        IContributionsToCommunityServiceHelper contributionsToCommunityServiceHelper)
        : IContributionsToCommunityServiceManagementService
    {
        private readonly IContributionsToCommunityServiceHelper _helper = contributionsToCommunityServiceHelper;

        public Task<PaginatedResult<ContributionsToCommunityServiceResponseDTO>> GetFacultyMemberContributionsToCommunityServiceAsync(
            ContributionsToCommunityServiceSpecificationParameters parameters,
            string facultyMemberEmail)
            => _helper.GetAllContributionsToCommunityServiceAsync(parameters, facultyMemberEmail);

        public Task<ContributionsToCommunityServiceResponseDTO> GetFacultyMemberContributionToCommunityServiceByIdAsync(int id)
            => _helper.GetContributionToCommunityServiceByIdAsync(id);

        public Task<ContributionsToCommunityServiceResponseDTO> CreateFacultyMemberContributionToCommunityServiceAsync(
            ContributionsToCommunityServiceCreateDTO contributionsToCommunityServiceCreateDto,
            string facultyMemberEmail)
            => _helper.CreateContributionToCommunityServiceAsync(
                contributionsToCommunityServiceCreateDto,
                facultyMemberEmail);

        public Task<ContributionsToCommunityServiceResponseDTO> UpdateFacultyMemberContributionToCommunityServiceAsync(
            int contributionToCommunityServiceId,
            ContributionsToCommunityServiceUpdateDTO contributionsToCommunityServiceUpdateDto)
            => _helper.UpdateContributionToCommunityServiceAsync(
                contributionToCommunityServiceId,
                contributionsToCommunityServiceUpdateDto);

        public Task DeleteFacultyMemberContributionToCommunityServiceAsync(int contributionToCommunityServiceId)
            => _helper.DeleteContributionToCommunityServiceAsync(contributionToCommunityServiceId);
    }
}
