using Shared.Dtos.AcademicDataModule.ScientificProgressionModule;
using Shared.SpecificationParameters.AcademicDataModule.ScientificProgressionModule;

namespace Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ScientificProgressionModule
{
    public interface IJobRanksHelper
    {
        Task<PaginatedResult<JobRankResponseDto>> GetAllJobRanksAsync(
      JobRanksSpecificationsParameters parameters,
      string facultyMemberEmail);

        Task<JobRankResponseDto> GetJobRankByIdAsync(int id);

        Task<JobRankResponseDto> CreateJobRankAsync(
            JobRankCreateDto jobRanksCreateDto,
            string facultyMemberEmail);

        Task<JobRankResponseDto> UpdateJobRankAsync(
            int jobRankId,
            JobRankUpdateDto jobRanksUpdateDto);

        Task DeleteJobRankAsync(int jobRankId);
    }
}
