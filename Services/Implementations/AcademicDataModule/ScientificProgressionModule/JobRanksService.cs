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
     IAuthenticationService authenticationService,
     IMapper mapper)
     : BaseService<JobRanks, int>(unitOfWork, authenticationService, mapper),
       IJobRanksService
    {
        protected override string EntityName => "Job Ranks";

        public async Task<PaginatedResult<JobRankResponseDto>> GetAllAsync(
            JobRanksSpecificationsParameters parameters,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            var jobRanks = await Repo.GetAllAsync(
                new JobRanksSpecifications(parameters, email))
                ?? throw NotFound();

            var mapped = Mapper.Map<IEnumerable<JobRankResponseDto>>(jobRanks);

            var totalCount = await Repo.CountAsync(
                new JobRanksCountSpecifications(parameters, email));

            return new PaginatedResult<JobRankResponseDto>(
                parameters.PageIndex,
                mapped.Count(),
                totalCount,
                mapped);
        }

        public async Task<JobRankResponseDto> GetByIdAsync(
            int id,
            string? facultyMemberEmail = null)
        {
            var jobRank = await Repo.GetAsync(new JobRanksSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                jobRank.FacultyMemberId,
                facultyMemberEmail);

            return Mapper.Map<JobRankResponseDto>(jobRank);
        }

        public async Task<JobRankResponseDto> CreateAsync(
            JobRankCreateDto dto,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            var facultyMember = await GetFacultyMemberByEmailAsync(email);

            var jobRank = Mapper.Map<JobRanks>(dto);
            jobRank.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(jobRank);
            await SaveChangesAsync();

            return Mapper.Map<JobRankResponseDto>(jobRank);
        }

        public async Task<JobRankResponseDto> UpdateAsync(
            int id,
            JobRankUpdateDto dto,
            string? facultyMemberEmail = null)
        {
            var jobRank = await Repo.GetAsync(new JobRanksSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                jobRank.FacultyMemberId,
                facultyMemberEmail);

            Mapper.Map(dto, jobRank);

            Repo.Update(jobRank);
            await SaveChangesAsync();

            return Mapper.Map<JobRankResponseDto>(jobRank);
        }

        public async Task DeleteAsync(
            int id,
            string? facultyMemberEmail = null)
        {
            var jobRank = await Repo.GetAsync(new JobRanksSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                jobRank.FacultyMemberId,
                facultyMemberEmail);

            jobRank.IsDeleted = true;

            Repo.Update(jobRank);
            await SaveChangesAsync();
        }
    }
}