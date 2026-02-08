using Domain.Entities.AcademicDataModule.ContributionsModule;
using Services.Abstraction.Contracts.AcademicDataModule.ContributionsModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.ContributionsModule;
using Shared.Dtos.AcademicDataModule.ContributionsModule;
using Shared.SpecificationParameters.AcademicDataModule.ContributionsModule;

namespace Services.Implementations.AcademicDataModule.ContributionsModule
{
    public class ContributionsToCommunityServiceService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IAuthenticationService authenticationService)
                : BaseService<ContributionsToCommunityService, int>(unitOfWork, authenticationService, mapper), IContributionsToCommunityServiceService
    {
        protected override string EntityName => "Contributions To Community Service";
        public async Task<PaginatedResult<ContributionsToCommunityServiceResponseDTO>> GetAllContributionsToCommunityServiceAsync(ContributionsToCommunityServiceSpecificationParameters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            var contributionsToCommunityService = await Repo.GetAllAsync(new ContributionsToCommunityServiceSpecifications(parameters, currentUser.Email))
                ?? throw NotFound();

            var contributionsResult = Mapper.Map<IEnumerable<ContributionsToCommunityServiceResponseDTO>>(contributionsToCommunityService);

            var currentPageCount = contributionsResult.Count();

            var totalCount = await Repo.CountAsync(new ContributionsToCommunityServiceCountSpecifications(parameters, currentUser.Email));

            return new PaginatedResult<ContributionsToCommunityServiceResponseDTO>(parameters.PageIndex, currentPageCount, totalCount, contributionsResult);
        }

        public async Task<ContributionsToCommunityServiceResponseDTO> GetContributionToCommunityServiceByIdAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var contributionToCommunityService = await Repo.GetAsync(new ContributionsToCommunityServiceSpecifications(id))
                ?? throw NotFound();

            EnsureOwnership(contributionToCommunityService.FacultyMemberId, currentUser.UserId, EntityName);

            return Mapper.Map<ContributionsToCommunityServiceResponseDTO>(contributionToCommunityService);
        }

        public async Task<ContributionsToCommunityServiceResponseDTO> CreateContributionToCommunityServiceAsync(ContributionsToCommunityServiceCreateDTO contributionsToCommunityServiceCreateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var contributionToCommunityService = Mapper.Map<ContributionsToCommunityService>(contributionsToCommunityServiceCreateDto);
            contributionToCommunityService.FacultyMemberId = currentUser.UserId;

            await Repo.AddAsync(contributionToCommunityService);
            await SaveChangesAsync();

            return Mapper.Map<ContributionsToCommunityServiceResponseDTO>(contributionToCommunityService);
        }

        public async Task<ContributionsToCommunityServiceResponseDTO> UpdateContributionToCommunityServiceAsync(int contributionToCommunityServiceId, ContributionsToCommunityServiceUpdateDTO contributionsToCommunityServiceUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var contributionToCommunityService = await Repo.GetAsync(new ContributionsToCommunityServiceSpecifications(contributionToCommunityServiceId))
                ?? throw NotFound();

            EnsureOwnership(contributionToCommunityService.FacultyMemberId, currentUser.UserId, EntityName);

            Mapper.Map(contributionsToCommunityServiceUpdateDto, contributionToCommunityService);

            Repo.Update(contributionToCommunityService);
            await SaveChangesAsync();

            return Mapper.Map<ContributionsToCommunityServiceResponseDTO>(contributionToCommunityService);
        }

        public async Task DeleteContributionToCommunityServiceAsync(int contributionToCommunityServiceId)
        {
            var currentUser = await GetCurrentUserAsync();

            var contributionToCommunityService = await Repo.GetAsync(new ContributionsToCommunityServiceSpecifications(contributionToCommunityServiceId))
                ?? throw NotFound();

            EnsureOwnership(contributionToCommunityService.FacultyMemberId, currentUser.UserId, EntityName);

            contributionToCommunityService.IsDeleted = true;

            Repo.Update(contributionToCommunityService);
            await SaveChangesAsync();
        }
    }
}