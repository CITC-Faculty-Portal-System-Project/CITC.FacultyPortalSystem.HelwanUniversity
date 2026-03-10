using Shared.Dtos.AcademicDataModule.ScientificProgressionModule;
using Shared.SpecificationParameters.AcademicDataModule.ScientificProgressionModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.ScientificProgressionModule
{
    public interface IJobRanksService
    {
        Task<PaginatedResult<JobRankResponseDto>> GetAllAsync(
       JobRanksSpecificationsParameters parameters,
       string? facultyMemberEmail = null);

        Task<JobRankResponseDto> GetByIdAsync(
            int id,
            string? facultyMemberEmail = null);

        Task<JobRankResponseDto> CreateAsync(
            JobRankCreateDto dto,
            string? facultyMemberEmail = null);

        Task<JobRankResponseDto> UpdateAsync(
            int id,
            JobRankUpdateDto dto,
            string? facultyMemberEmail = null);

        Task DeleteAsync(
            int id,
            string? facultyMemberEmail = null);
    }
}
