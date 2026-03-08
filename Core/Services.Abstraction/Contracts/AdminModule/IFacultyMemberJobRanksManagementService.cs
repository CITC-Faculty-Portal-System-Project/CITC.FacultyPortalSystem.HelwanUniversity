using Shared.Dtos.AcademicDataModule.ScientificProgressionModule;
using Shared.SpecificationParameters.AcademicDataModule.ScientificProgressionModule;

namespace Services.Abstraction.Contracts.AdminModule
{
    public interface IFacultyMemberJobRanksManagementService
    {
        Task<PaginatedResult<JobRankResponseDto>> GetFacultyMemberJobRanksAsync(
       JobRanksSpecificationsParameters parameters,
       string facultyMemberEmail);

        Task<JobRankResponseDto> GetFacultyMemberJobRankByIdAsync(int id);

        Task<JobRankResponseDto> CreateFacultyMemberJobRankAsync(
            JobRankCreateDto jobRanksCreateDto,
            string facultyMemberEmail);

        Task<JobRankResponseDto> UpdateFacultyMemberJobRankAsync(
            int jobRankId,
            JobRankUpdateDto jobRanksUpdateDto);

        Task DeleteFacultyMemberJobRankAsync(int jobRankId);
    }
}
