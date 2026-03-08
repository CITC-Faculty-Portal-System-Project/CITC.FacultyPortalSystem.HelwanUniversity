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
        IAuthenticationService authenticationService,
        IContributionsToCommunityServiceHelper contributionsToCommunityServiceHelper)
        : BaseService<ContributionsToCommunityService, int>(unitOfWork, authenticationService, mapper),
          IContributionsToCommunityServiceService
    {
        private readonly IContributionsToCommunityServiceHelper _helper = contributionsToCommunityServiceHelper;

        protected override string EntityName => "Contributions To Community Service";

        public async Task<PaginatedResult<ContributionsToCommunityServiceResponseDTO>> GetAllContributionsToCommunityServiceAsync(
            ContributionsToCommunityServiceSpecificationParameters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            return await _helper.GetAllContributionsToCommunityServiceAsync(
                parameters,
                currentUser.Email);
        }

        public async Task<ContributionsToCommunityServiceResponseDTO> GetContributionToCommunityServiceByIdAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var contribution = await Repo.GetAsync(new ContributionsToCommunityServiceSpecifications(id))
                ?? throw NotFound();

            EnsureOwnership(contribution.FacultyMemberId, currentUser.UserId, EntityName);

            return await _helper.GetContributionToCommunityServiceByIdAsync(id);
        }

        public async Task<ContributionsToCommunityServiceResponseDTO> CreateContributionToCommunityServiceAsync(
            ContributionsToCommunityServiceCreateDTO contributionsToCommunityServiceCreateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            return await _helper.CreateContributionToCommunityServiceAsync(
                contributionsToCommunityServiceCreateDto,
                currentUser.Email);
        }

        public async Task<ContributionsToCommunityServiceResponseDTO> UpdateContributionToCommunityServiceAsync(
            int contributionToCommunityServiceId,
            ContributionsToCommunityServiceUpdateDTO contributionsToCommunityServiceUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var contribution = await Repo.GetAsync(new ContributionsToCommunityServiceSpecifications(contributionToCommunityServiceId))
                ?? throw NotFound();

            EnsureOwnership(contribution.FacultyMemberId, currentUser.UserId, EntityName);

            return await _helper.UpdateContributionToCommunityServiceAsync(
                contributionToCommunityServiceId,
                contributionsToCommunityServiceUpdateDto);
        }

        public async Task DeleteContributionToCommunityServiceAsync(int contributionToCommunityServiceId)
        {
            var currentUser = await GetCurrentUserAsync();

            var contribution = await Repo.GetAsync(new ContributionsToCommunityServiceSpecifications(contributionToCommunityServiceId))
                ?? throw NotFound();

            EnsureOwnership(contribution.FacultyMemberId, currentUser.UserId, EntityName);

            await _helper.DeleteContributionToCommunityServiceAsync(contributionToCommunityServiceId);
        }
    }
}