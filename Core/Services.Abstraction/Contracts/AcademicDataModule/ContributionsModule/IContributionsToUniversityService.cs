using Shared.Dtos.AcademicDataModule.ContributionsModule;
using Shared.SpecificationParameters.AcademicDataModule.ContributionsModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.ContributionsModule
{
    public interface IContributionsToUniversityService
    {
        public Task<PaginatedResult<ContributionsToUniversityResponseDTO>> GetAllContributionsToUniversityAsync(ContributionsToUniversitySpecificationParameters parameters);
        public Task<ContributionsToUniversityResponseDTO> GetContributionToUniversityByIdAsync(int id);
        public Task<ContributionsToUniversityResponseDTO> CreateContributionToUniversityAsync(ContributionsToUniversityCreateDTO contributionsToUniversityCreateDto);
        public Task<ContributionsToUniversityResponseDTO> UpdateContributionToUniversityAsync(int contributionToUniversityId, ContributionsToUniversityUpdateDTO contributionsToUniversityUpdateDto);
        public Task DeleteContributionToUniversityAsync(int contributionToUniversityId);
    }
}
