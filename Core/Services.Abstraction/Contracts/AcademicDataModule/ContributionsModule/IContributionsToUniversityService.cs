using Shared.Dtos.AcademicDataModule.ContributionsModule;
using Shared.SpecificationParameters.AcademicDataModule.ContributionsModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.ContributionsModule
{
    public interface IContributionsToUniversityService
    {
        Task<PaginatedResult<ContributionsToUniversityResponseDTO>> GetAllContributionsToUniversityAsync(
         ContributionsToUniversitySpecificationParameters parameters,
         string? facultyMemberEmail = null);

        Task<ContributionsToUniversityResponseDTO> GetContributionToUniversityByIdAsync(
            int id,
            string? facultyMemberEmail = null);

        Task<ContributionsToUniversityResponseDTO> CreateContributionToUniversityAsync(
            ContributionsToUniversityCreateDTO contributionsToUniversityCreateDto,
            string? facultyMemberEmail = null);

        Task<ContributionsToUniversityResponseDTO> UpdateContributionToUniversityAsync(
            int contributionToUniversityId,
            ContributionsToUniversityUpdateDTO contributionsToUniversityUpdateDto,
            string? facultyMemberEmail = null);

        Task DeleteContributionToUniversityAsync(
            int contributionToUniversityId,
            string? facultyMemberEmail = null);
    }
}
