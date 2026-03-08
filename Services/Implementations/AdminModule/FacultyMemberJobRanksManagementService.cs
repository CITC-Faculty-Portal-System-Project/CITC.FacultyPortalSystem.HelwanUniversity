using Services.Abstraction.Contracts.AdminModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ScientificProgressionModule;
using Shared.Dtos.AcademicDataModule.ScientificProgressionModule;
using Shared.SpecificationParameters.AcademicDataModule.ScientificProgressionModule;

namespace Services.Implementations.AdminModule
{
    public class FacultyMemberJobRanksManagementService(IJobRanksHelper _helper)
        : IFacultyMemberJobRanksManagementService
    {
        public Task<PaginatedResult<JobRankResponseDto>> GetFacultyMemberJobRanksAsync(
       JobRanksSpecificationsParameters parameters,
       string facultyMemberEmail)
       => _helper.GetAllJobRanksAsync(parameters, facultyMemberEmail);

        public Task<JobRankResponseDto> GetFacultyMemberJobRankByIdAsync(int id)
            => _helper.GetJobRankByIdAsync(id);

        public Task<JobRankResponseDto> CreateFacultyMemberJobRankAsync(
            JobRankCreateDto jobRanksCreateDto,
            string facultyMemberEmail)
            => _helper.CreateJobRankAsync(jobRanksCreateDto, facultyMemberEmail);

        public Task<JobRankResponseDto> UpdateFacultyMemberJobRankAsync(
            int jobRankId,
            JobRankUpdateDto jobRanksUpdateDto)
            => _helper.UpdateJobRankAsync(jobRankId, jobRanksUpdateDto);

        public Task DeleteFacultyMemberJobRankAsync(int jobRankId)
            => _helper.DeleteJobRankAsync(jobRankId);
    }
}
