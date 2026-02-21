using Domain.Entities.AcademicDataModule.ScientificProgressionModule;
using Services.Abstraction.Contracts.AcademicDataModule.ScientificProgressionModule;
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
        IValidationService validationService)
                : BaseService<JobRanks, int>(unitOfWork, authenticationService, mapper, validationService), IJobRanksService
    {
        protected override string EntityName => "Job Ranks";
        public async Task<PaginatedResult<JobRankResponseDto>> GetAllJobRanksAsync(JobRanksSpecificationsParameters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            var jobRanks = await Repo.GetAllAsync(new JobRanksSpecifications(parameters, currentUser.Email))
                ?? throw NotFound();

            var jobRanksResult = Mapper.Map<IEnumerable<JobRankResponseDto>>(jobRanks);

            var currentPageCount = jobRanksResult.Count();

            var totalCount = await Repo.CountAsync(new JobRanksCountSpecifications(parameters, currentUser.Email));

            return new PaginatedResult<JobRankResponseDto>(parameters.PageIndex, currentPageCount, totalCount, jobRanksResult);
        }

        public async Task<JobRankResponseDto> GetJobRankByIdAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var jobRank = await Repo.GetAsync(new JobRanksSpecifications(id)) ?? throw new NotFoundException("errors.JobRank.notFound"  , id);

            EnsureOwnership(jobRank.FacultyMemberId, currentUser.UserId, EntityName);

            return Mapper.Map<JobRankResponseDto>(jobRank);
        }

        public async Task<JobRankResponseDto> CreateJobRankAsync(JobRankCreateDto jobRanksCreateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var jobRank = Mapper.Map<JobRanks>(jobRanksCreateDto);
            jobRank.FacultyMemberId = currentUser.UserId;

            await Repo.AddAsync(jobRank);
            await SaveChangesAsync();

            return Mapper.Map<JobRankResponseDto>(jobRank);

        }

        public async Task<JobRankResponseDto> UpdateJobRankAsync(int jobRankId, JobRankUpdateDto jobRanksUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var jobRank = await Repo.GetAsync(new JobRanksSpecifications(jobRankId))
                ?? throw NotFound();

            EnsureOwnership(jobRank.FacultyMemberId, currentUser.UserId, EntityName);

            Mapper.Map(jobRanksUpdateDto, jobRank);

            Repo.Update(jobRank);
            await SaveChangesAsync();

            return Mapper.Map<JobRankResponseDto>(jobRank);
        }

        public async Task DeleteJobRankAsync(int jobRankId)
        {
            var currentUser = await GetCurrentUserAsync();

            var jobRank = await Repo.GetAsync(new JobRanksSpecifications(jobRankId)) ?? throw new NotFoundException("errors.JobRank.notFound" , jobRankId);

            EnsureOwnership(jobRank.FacultyMemberId, currentUser.UserId, EntityName);

            jobRank.IsDeleted = true;

            Repo.Update(jobRank);
            await SaveChangesAsync();
        }
    }
}