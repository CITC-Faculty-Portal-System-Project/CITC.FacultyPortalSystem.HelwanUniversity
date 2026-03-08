using Shared.Dtos.AcademicDataModule.ContributionsModule;
using Shared.SpecificationParameters.AcademicDataModule.ContributionsModule;

namespace Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ContributionsModule
{
    public interface IContributionsToUniversityHelper
    {
        Task<PaginatedResult<ContributionsToUniversityResponseDTO>> GetAllContributionsToUniversityAsync(
            ContributionsToUniversitySpecificationParameters parameters,
            string facultyMemberEmail);

        Task<ContributionsToUniversityResponseDTO> GetContributionToUniversityByIdAsync(int id);

        Task<ContributionsToUniversityResponseDTO> CreateContributionToUniversityAsync(
            ContributionsToUniversityCreateDTO contributionsToUniversityCreateDto,
            string facultyMemberEmail);

        Task<ContributionsToUniversityResponseDTO> UpdateContributionToUniversityAsync(
            int contributionToUniversityId,
            ContributionsToUniversityUpdateDTO contributionsToUniversityUpdateDto);

        Task DeleteContributionToUniversityAsync(int contributionToUniversityId);
    }
}
