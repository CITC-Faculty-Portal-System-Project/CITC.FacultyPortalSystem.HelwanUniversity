using Shared.Dtos.AcademicDataModule.ScientificProgressionModule;
using Shared.SpecificationParameters.AcademicDataModule.ScientificProgressionModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.ScientificProgressionModule
{
    public interface IJobRanksService
    {
        public Task<PaginatedResult<JobRankResponseDto>> GetAllJobRanksAsync(JobRanksSpecificationsParameters parameters);
        public Task<JobRankResponseDto> GetJobRankByIdAsync(int id);
        public Task<JobRankResponseDto> CreateJobRankAsync(JobRankCreateDto jobRanksCreateDto);
        public Task<JobRankResponseDto> UpdateJobRankAsync(int jobRankId, JobRankUpdateDto jobRanksUpdateDto);
        public Task DeleteJobRankAsync(int jobRankId);
    }
}
