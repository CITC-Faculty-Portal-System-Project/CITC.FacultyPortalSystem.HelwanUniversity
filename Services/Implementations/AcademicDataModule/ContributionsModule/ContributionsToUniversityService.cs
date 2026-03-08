using Domain.Entities.AcademicDataModule.ContributionsModule;
using Services.Abstraction.Contracts.AcademicDataModule.ContributionsModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ContributionsModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.ContributionsModule;
using Shared.Dtos.AcademicDataModule.ContributionsModule;
using Shared.SpecificationParameters.AcademicDataModule.ContributionsModule;

namespace Services.Implementations.AcademicDataModule.ContributionsModule
{
    public class ContributionsToUniversityService(
          IUnitOfWork unitOfWork,
          IMapper mapper,
          IAuthenticationService authenticationService,
          IContributionsToUniversityHelper contributionsToUniversityHelper)
          : BaseService<ContributionsToUniversity, int>(unitOfWork, authenticationService, mapper),
            IContributionsToUniversityService
    {
        private readonly IContributionsToUniversityHelper _helper = contributionsToUniversityHelper;

        protected override string EntityName => "Contributions To University";

        public async Task<PaginatedResult<ContributionsToUniversityResponseDTO>> GetAllContributionsToUniversityAsync(
            ContributionsToUniversitySpecificationParameters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            return await _helper.GetAllContributionsToUniversityAsync(
                parameters,
                currentUser.Email);
        }

        public async Task<ContributionsToUniversityResponseDTO> GetContributionToUniversityByIdAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var contribution = await Repo.GetAsync(new ContributionsToUniversitySpecifications(id))
                ?? throw NotFound();

            EnsureOwnership(contribution.FacultyMemberId, currentUser.UserId, EntityName);

            return await _helper.GetContributionToUniversityByIdAsync(id);
        }

        public async Task<ContributionsToUniversityResponseDTO> CreateContributionToUniversityAsync(
            ContributionsToUniversityCreateDTO contributionsToUniversityCreateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            return await _helper.CreateContributionToUniversityAsync(
                contributionsToUniversityCreateDto,
                currentUser.Email);
        }

        public async Task<ContributionsToUniversityResponseDTO> UpdateContributionToUniversityAsync(
            int contributionToUniversityId,
            ContributionsToUniversityUpdateDTO contributionsToUniversityUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var contribution = await Repo.GetAsync(new ContributionsToUniversitySpecifications(contributionToUniversityId))
                ?? throw NotFound();

            EnsureOwnership(contribution.FacultyMemberId, currentUser.UserId, EntityName);

            return await _helper.UpdateContributionToUniversityAsync(
                contributionToUniversityId,
                contributionsToUniversityUpdateDto);
        }

        public async Task DeleteContributionToUniversityAsync(int contributionToUniversityId)
        {
            var currentUser = await GetCurrentUserAsync();

            var contribution = await Repo.GetAsync(new ContributionsToUniversitySpecifications(contributionToUniversityId))
                ?? throw NotFound();

            EnsureOwnership(contribution.FacultyMemberId, currentUser.UserId, EntityName);

            await _helper.DeleteContributionToUniversityAsync(contributionToUniversityId);
        }
    }
}