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
      IAuthenticationService authenticationService,
      IMapper mapper)
      : BaseService<ContributionsToCommunityService, int>(unitOfWork, authenticationService, mapper),
        IContributionsToCommunityServiceService
    {
        protected override string EntityName => "Contributions To Community Service";

        public async Task<PaginatedResult<ContributionsToCommunityServiceResponseDTO>> GetAllContributionsToCommunityServiceAsync(
            ContributionsToCommunityServiceSpecificationParameters parameters,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            var contributions = await Repo.GetAllAsync(
                new ContributionsToCommunityServiceSpecifications(parameters, email))
                ?? throw NotFound();

            var mapped = Mapper.Map<IEnumerable<ContributionsToCommunityServiceResponseDTO>>(contributions);

            var totalCount = await Repo.CountAsync(
                new ContributionsToCommunityServiceCountSpecifications(parameters, email));

            return new PaginatedResult<ContributionsToCommunityServiceResponseDTO>(
                parameters.PageIndex,
                mapped.Count(),
                totalCount,
                mapped);
        }

        public async Task<ContributionsToCommunityServiceResponseDTO> GetContributionToCommunityServiceByIdAsync(
            int id,
            string? facultyMemberEmail = null)
        {
            var contribution = await Repo.GetAsync(
                new ContributionsToCommunityServiceSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                contribution.FacultyMemberId,
                facultyMemberEmail);

            return Mapper.Map<ContributionsToCommunityServiceResponseDTO>(contribution);
        }

        public async Task<ContributionsToCommunityServiceResponseDTO> CreateContributionToCommunityServiceAsync(
            ContributionsToCommunityServiceCreateDTO contributionsToCommunityServiceCreateDto,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            var facultyMember = await GetFacultyMemberByEmailAsync(email);

            var contribution = Mapper.Map<ContributionsToCommunityService>(contributionsToCommunityServiceCreateDto);
            contribution.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(contribution);
            await SaveChangesAsync();

            return Mapper.Map<ContributionsToCommunityServiceResponseDTO>(contribution);
        }

        public async Task<ContributionsToCommunityServiceResponseDTO> UpdateContributionToCommunityServiceAsync(
            int contributionToCommunityServiceId,
            ContributionsToCommunityServiceUpdateDTO contributionsToCommunityServiceUpdateDto,
            string? facultyMemberEmail = null)
        {
            var contribution = await Repo.GetAsync(
                new ContributionsToCommunityServiceSpecifications(contributionToCommunityServiceId))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                contribution.FacultyMemberId,
                facultyMemberEmail);

            Mapper.Map(contributionsToCommunityServiceUpdateDto, contribution);

            Repo.Update(contribution);
            await SaveChangesAsync();

            return Mapper.Map<ContributionsToCommunityServiceResponseDTO>(contribution);
        }

        public async Task DeleteContributionToCommunityServiceAsync(
            int contributionToCommunityServiceId,
            string? facultyMemberEmail = null)
        {
            var contribution = await Repo.GetAsync(
                new ContributionsToCommunityServiceSpecifications(contributionToCommunityServiceId))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                contribution.FacultyMemberId,
                facultyMemberEmail);

            contribution.IsDeleted = true;

            Repo.Update(contribution);
            await SaveChangesAsync();
        }
    }
}