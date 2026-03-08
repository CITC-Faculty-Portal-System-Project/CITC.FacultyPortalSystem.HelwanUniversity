using Domain.Entities.AcademicDataModule.ScientificProgressionModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ScientificProgressionModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.ScientificProgressionModule;
using Shared.Dtos.AcademicDataModule.ScientificProgressionModule;
using Shared.SpecificationParameters.AcademicDataModule.ScientificProgressionModule;

namespace Services.Implementations.SharedLogicBetweenAdminAndFacultyMember.ScientificProgressionModule
{
    public class JobRanksHelper(
       IUnitOfWork unitOfWork,
       IAuthenticationService authenticationService,
       IMapper mapper)
       : BaseService<JobRanks, int>(unitOfWork, authenticationService, mapper),
         IJobRanksHelper
    {
        protected override string EntityName => "Job Ranks";

        public async Task<PaginatedResult<JobRankResponseDto>> GetAllJobRanksAsync(
            JobRanksSpecificationsParameters parameters,
            string facultyMemberEmail)
        {
            var jobRanks = await Repo.GetAllAsync(
                new JobRanksSpecifications(parameters, facultyMemberEmail))
                ?? throw NotFound();

            var jobRanksResult = Mapper.Map<IEnumerable<JobRankResponseDto>>(jobRanks);

            var currentPageCount = jobRanksResult.Count();

            var totalCount = await Repo.CountAsync(
                new JobRanksCountSpecifications(parameters, facultyMemberEmail));

            return new PaginatedResult<JobRankResponseDto>(
                parameters.PageIndex,
                currentPageCount,
                totalCount,
                jobRanksResult);
        }

        public async Task<JobRankResponseDto> GetJobRankByIdAsync(int id)
        {
            var jobRank = await Repo.GetAsync(new JobRanksSpecifications(id))
                ?? throw new NotFoundException("Job Rank is Not Found.");

            return Mapper.Map<JobRankResponseDto>(jobRank);
        }

        public async Task<JobRankResponseDto> CreateJobRankAsync(
            JobRankCreateDto jobRanksCreateDto,
            string facultyMemberEmail)
        {
            var facultyMember = await GetFacultyMemberByEmailAsync(facultyMemberEmail);

            var jobRank = Mapper.Map<JobRanks>(jobRanksCreateDto);
            jobRank.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(jobRank);
            await SaveChangesAsync();

            return Mapper.Map<JobRankResponseDto>(jobRank);
        }

        public async Task<JobRankResponseDto> UpdateJobRankAsync(
            int jobRankId,
            JobRankUpdateDto jobRanksUpdateDto)
        {
            var jobRank = await Repo.GetAsync(new JobRanksSpecifications(jobRankId))
                ?? throw NotFound();

            Mapper.Map(jobRanksUpdateDto, jobRank);

            Repo.Update(jobRank);
            await SaveChangesAsync();

            return Mapper.Map<JobRankResponseDto>(jobRank);
        }

        public async Task DeleteJobRankAsync(int jobRankId)
        {
            var jobRank = await Repo.GetAsync(new JobRanksSpecifications(jobRankId))
                ?? throw new NotFoundException("Job Rank is Not Found.");

            jobRank.IsDeleted = true;

            Repo.Update(jobRank);
            await SaveChangesAsync();
        }
    }
}
