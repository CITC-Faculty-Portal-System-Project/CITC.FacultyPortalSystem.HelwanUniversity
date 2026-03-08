using Domain.Entities.AcademicDataModule.ContributionsModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.ContributionsModule;
using Shared.Dtos.AcademicDataModule.ContributionsModule;
using Shared.SpecificationParameters.AcademicDataModule.ContributionsModule;

namespace Services.Implementations.SharedLogicBetweenAdminAndFacultyMember.AcademicDataModule.ContributionsModule
{
    public class ContributionsToCommunityServiceHelper(
        IUnitOfWork unitOfWork,
        IAuthenticationService authenticationService,
        IMapper mapper)
        : BaseService<ContributionsToCommunityService, int>(unitOfWork, authenticationService, mapper),
          IContributionsToCommunityServiceHelper
    {
        protected override string EntityName => "Contributions To Community Service";

        public async Task<PaginatedResult<ContributionsToCommunityServiceResponseDTO>> GetAllContributionsToCommunityServiceAsync(
            ContributionsToCommunityServiceSpecificationParameters parameters,
            string facultyMemberEmail)
        {
            var contributions = await Repo.GetAllAsync(
                new ContributionsToCommunityServiceSpecifications(parameters, facultyMemberEmail));

            var contributionsResult =
                Mapper.Map<IEnumerable<ContributionsToCommunityServiceResponseDTO>>(contributions);

            var currentPageCount = contributionsResult.Count();

            var totalCount = await Repo.CountAsync(
                new ContributionsToCommunityServiceCountSpecifications(parameters, facultyMemberEmail));

            return new PaginatedResult<ContributionsToCommunityServiceResponseDTO>(
                parameters.PageIndex,
                currentPageCount,
                totalCount,
                contributionsResult);
        }

        public async Task<ContributionsToCommunityServiceResponseDTO> GetContributionToCommunityServiceByIdAsync(
            int id)
        {
            var contribution = await Repo.GetAsync(
                new ContributionsToCommunityServiceSpecifications(id))
                ?? throw NotFound();

            return Mapper.Map<ContributionsToCommunityServiceResponseDTO>(contribution);
        }

        public async Task<ContributionsToCommunityServiceResponseDTO> CreateContributionToCommunityServiceAsync(
            ContributionsToCommunityServiceCreateDTO contributionsToCommunityServiceCreateDto,
            string facultyMemberEmail)
        {
            var facultyMember = await GetFacultyMemberByEmailAsync(facultyMemberEmail);

            var contribution =
                Mapper.Map<ContributionsToCommunityService>(contributionsToCommunityServiceCreateDto);

            contribution.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(contribution);
            await SaveChangesAsync();

            return Mapper.Map<ContributionsToCommunityServiceResponseDTO>(contribution);
        }

        public async Task<ContributionsToCommunityServiceResponseDTO> UpdateContributionToCommunityServiceAsync(
            int contributionToCommunityServiceId,
            ContributionsToCommunityServiceUpdateDTO contributionsToCommunityServiceUpdateDto)
        {
            var contribution = await Repo.GetAsync(
                new ContributionsToCommunityServiceSpecifications(
                    contributionToCommunityServiceId))
                ?? throw NotFound();

            Mapper.Map(contributionsToCommunityServiceUpdateDto, contribution);

            Repo.Update(contribution);
            await SaveChangesAsync();

            return Mapper.Map<ContributionsToCommunityServiceResponseDTO>(contribution);
        }

        public async Task DeleteContributionToCommunityServiceAsync(
            int contributionToCommunityServiceId)
        {
            var contribution = await Repo.GetAsync(
                new ContributionsToCommunityServiceSpecifications(
                    contributionToCommunityServiceId))
                ?? throw NotFound();

            contribution.IsDeleted = true;

            Repo.Update(contribution);
            await SaveChangesAsync();
        }
    }
}