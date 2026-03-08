using Shared.Dtos.AcademicDataModule.ContributionsModule;
using Shared.SpecificationParameters.AcademicDataModule.ContributionsModule;

namespace Services.Abstraction.Contracts.AdminModule
{
    public interface IFacultyMemberContributionsToUniversityManagementService
    {
        Task<PaginatedResult<ContributionsToUniversityResponseDTO>> GetFacultyMemberContributionsToUniversityAsync(
            ContributionsToUniversitySpecificationParameters parameters,
            string facultyMemberEmail);

        Task<ContributionsToUniversityResponseDTO> GetFacultyMemberContributionToUniversityByIdAsync(int id);

        Task<ContributionsToUniversityResponseDTO> CreateFacultyMemberContributionToUniversityAsync(
            ContributionsToUniversityCreateDTO contributionsToUniversityCreateDto,
            string facultyMemberEmail);

        Task<ContributionsToUniversityResponseDTO> UpdateFacultyMemberContributionToUniversityAsync(
            int contributionToUniversityId,
            ContributionsToUniversityUpdateDTO contributionsToUniversityUpdateDto);

        Task DeleteFacultyMemberContributionToUniversityAsync(int contributionToUniversityId);
    }

}
