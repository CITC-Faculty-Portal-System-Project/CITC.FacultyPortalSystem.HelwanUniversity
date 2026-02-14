using Domain.Entities.AcademicDataModule.ContributionsModule;
using Services.Abstraction.Contracts.AcademicDataModule.ContributionsModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.ContributionsModule;
using Shared.Dtos.AcademicDataModule.ContributionsModule;
using Shared.SpecificationParameters.AcademicDataModule.ContributionsModule;

namespace Services.Implementations.AcademicDataModule.ContributionsModule
{
    public class ContributionsToUniversityService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IAuthenticationService authenticationService)
                : BaseService<ContributionsToUniversity, int>(unitOfWork, authenticationService, mapper), IContributionsToUniversityService
    {
        protected override string EntityName => "Contributions To University";
        public async Task<PaginatedResult<ContributionsToUniversityResponseDTO>> GetAllContributionsToUniversityAsync(ContributionsToUniversitySpecificationParameters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            var contributionsToUniversity = await Repo.GetAllAsync(new ContributionsToUniversitySpecifications(parameters, currentUser.Email))
                ?? throw NotFound();

            var contributionsResult = Mapper.Map<IEnumerable<ContributionsToUniversityResponseDTO>>(contributionsToUniversity);

            var currentPageCount = contributionsResult.Count();

            var totalCount = await Repo.CountAsync(new ContributionsToUniversityCountSpecifications(parameters, currentUser.Email));

            return new PaginatedResult<ContributionsToUniversityResponseDTO>(parameters.PageIndex, currentPageCount, totalCount, contributionsResult);
        }

        public async Task<ContributionsToUniversityResponseDTO> GetContributionToUniversityByIdAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var contributionToUniversity = await Repo.GetAsync(new ContributionsToUniversitySpecifications(id))
                ?? throw NotFound();

            EnsureOwnership(contributionToUniversity.FacultyMemberId, currentUser.UserId, EntityName);

            return Mapper.Map<ContributionsToUniversityResponseDTO>(contributionToUniversity);
        }

        public async Task<ContributionsToUniversityResponseDTO> CreateContributionToUniversityAsync(ContributionsToUniversityCreateDTO contributionsToUniversityCreateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var contributionToUniversity = Mapper.Map<ContributionsToUniversity>(contributionsToUniversityCreateDto);
            contributionToUniversity.FacultyMemberId = currentUser.UserId;

            await Repo.AddAsync(contributionToUniversity);
            await SaveChangesAsync();

            return Mapper.Map<ContributionsToUniversityResponseDTO>(contributionToUniversity);
        }

        public async Task<ContributionsToUniversityResponseDTO> UpdateContributionToUniversityAsync(int contributionToUniversityId, ContributionsToUniversityUpdateDTO contributionsToUniversityUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var contributionToUniversity = await Repo.GetAsync(new ContributionsToUniversitySpecifications(contributionToUniversityId))
                ?? throw NotFound();

            EnsureOwnership(contributionToUniversity.FacultyMemberId, currentUser.UserId, EntityName);

            Mapper.Map(contributionsToUniversityUpdateDto, contributionToUniversity);

            Repo.Update(contributionToUniversity);
            await SaveChangesAsync();

            return Mapper.Map<ContributionsToUniversityResponseDTO>(contributionToUniversity);
        }

        public async Task DeleteContributionToUniversityAsync(int contributionToUniversityId)
        {
            var currentUser = await GetCurrentUserAsync();

            var contributionToUniversity = await Repo.GetAsync(new ContributionsToUniversitySpecifications(contributionToUniversityId))
                ?? throw NotFound();

            EnsureOwnership(contributionToUniversity.FacultyMemberId, currentUser.UserId, EntityName);

            contributionToUniversity.IsDeleted = true;

            Repo.Update(contributionToUniversity);
            await SaveChangesAsync();
        }
    }
}