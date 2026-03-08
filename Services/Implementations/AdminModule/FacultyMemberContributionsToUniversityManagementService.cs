using Services.Abstraction.Contracts.AdminModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ContributionsModule;
using Shared.Dtos.AcademicDataModule.ContributionsModule;
using Shared.SpecificationParameters.AcademicDataModule.ContributionsModule;

namespace Services.Implementations.AdminModule
{
    public class FacultyMemberContributionsToUniversityManagementService(
         IContributionsToUniversityHelper contributionsToUniversityHelper)
         : IFacultyMemberContributionsToUniversityManagementService
    {
        private readonly IContributionsToUniversityHelper _helper = contributionsToUniversityHelper;

        public Task<PaginatedResult<ContributionsToUniversityResponseDTO>> GetFacultyMemberContributionsToUniversityAsync(
            ContributionsToUniversitySpecificationParameters parameters,
            string facultyMemberEmail)
            => _helper.GetAllContributionsToUniversityAsync(parameters, facultyMemberEmail);

        public Task<ContributionsToUniversityResponseDTO> GetFacultyMemberContributionToUniversityByIdAsync(int id)
            => _helper.GetContributionToUniversityByIdAsync(id);

        public Task<ContributionsToUniversityResponseDTO> CreateFacultyMemberContributionToUniversityAsync(
            ContributionsToUniversityCreateDTO contributionsToUniversityCreateDto,
            string facultyMemberEmail)
            => _helper.CreateContributionToUniversityAsync(contributionsToUniversityCreateDto, facultyMemberEmail);

        public Task<ContributionsToUniversityResponseDTO> UpdateFacultyMemberContributionToUniversityAsync(
            int contributionToUniversityId,
            ContributionsToUniversityUpdateDTO contributionsToUniversityUpdateDto)
            => _helper.UpdateContributionToUniversityAsync(contributionToUniversityId, contributionsToUniversityUpdateDto);

        public Task DeleteFacultyMemberContributionToUniversityAsync(int contributionToUniversityId)
            => _helper.DeleteContributionToUniversityAsync(contributionToUniversityId);
    }
}
