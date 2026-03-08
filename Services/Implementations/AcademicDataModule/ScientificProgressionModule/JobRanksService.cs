using Domain.Entities.AcademicDataModule.ScientificProgressionModule;
using Services.Abstraction.Contracts.AcademicDataModule.ScientificProgressionModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ScientificProgressionModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.ScientificProgressionModule;
using Shared.Dtos.AcademicDataModule.ScientificProgressionModule;
using Shared.SpecificationParameters.AcademicDataModule.ScientificProgressionModule;

namespace Services.Implementations.AcademicDataModule.ScientificProgressionModule
{
    public class JobRanksService(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IAuthenticationService authenticationService,
    IJobRanksHelper jobRanksHelper)
    : BaseService<JobRanks, int>(unitOfWork, authenticationService, mapper),
      IJobRanksService
    {
        private readonly IJobRanksHelper _helper = jobRanksHelper;

        protected override string EntityName => "Job Ranks";

        public async Task<PaginatedResult<JobRankResponseDto>> GetAllJobRanksAsync(
            JobRanksSpecificationsParameters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            return await _helper.GetAllJobRanksAsync(parameters, currentUser.Email);
        }

        public async Task<JobRankResponseDto> GetJobRankByIdAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var jobRank = await Repo.GetAsync(new JobRanksSpecifications(id))
                ?? throw new NotFoundException("Job Rank is Not Found.");

            EnsureOwnership(jobRank.FacultyMemberId, currentUser.UserId, EntityName);

            return await _helper.GetJobRankByIdAsync(id);
        }

        public async Task<JobRankResponseDto> CreateJobRankAsync(JobRankCreateDto jobRanksCreateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            return await _helper.CreateJobRankAsync(jobRanksCreateDto, currentUser.Email);
        }

        public async Task<JobRankResponseDto> UpdateJobRankAsync(int jobRankId, JobRankUpdateDto jobRanksUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var jobRank = await Repo.GetAsync(new JobRanksSpecifications(jobRankId))
                ?? throw NotFound();

            EnsureOwnership(jobRank.FacultyMemberId, currentUser.UserId, EntityName);

            return await _helper.UpdateJobRankAsync(jobRankId, jobRanksUpdateDto);
        }

        public async Task DeleteJobRankAsync(int jobRankId)
        {
            var currentUser = await GetCurrentUserAsync();

            var jobRank = await Repo.GetAsync(new JobRanksSpecifications(jobRankId))
                ?? throw new NotFoundException("Job Rank is Not Found.");

            EnsureOwnership(jobRank.FacultyMemberId, currentUser.UserId, EntityName);

            await _helper.DeleteJobRankAsync(jobRankId);
        }
    }
}