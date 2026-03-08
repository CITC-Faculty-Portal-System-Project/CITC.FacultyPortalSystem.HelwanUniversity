using Domain.Entities.AcademicDataModule.ContributionsModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ContributionsModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.ContributionsModule;
using Shared.Dtos.AcademicDataModule.ContributionsModule;
using Shared.SpecificationParameters.AcademicDataModule.ContributionsModule;

namespace Services.Implementations.SharedLogicBetweenAdminAndFacultyMember.ContributionsModule
{
    public class ContributionsToUniversityHelper(
        IUnitOfWork unitOfWork,
        IAuthenticationService authenticationService,
        IMapper mapper)
        : BaseService<ContributionsToUniversity, int>(unitOfWork, authenticationService, mapper),
          IContributionsToUniversityHelper
    {
        protected override string EntityName => "Contributions To University";

        public async Task<PaginatedResult<ContributionsToUniversityResponseDTO>> GetAllContributionsToUniversityAsync(
            ContributionsToUniversitySpecificationParameters parameters,
            string facultyMemberEmail)
        {
            var contributions = await Repo.GetAllAsync(
                new ContributionsToUniversitySpecifications(parameters, facultyMemberEmail));

            var contributionsResult =
                Mapper.Map<IEnumerable<ContributionsToUniversityResponseDTO>>(contributions);

            var currentPageCount = contributionsResult.Count();

            var totalCount = await Repo.CountAsync(
                new ContributionsToUniversityCountSpecifications(parameters, facultyMemberEmail));

            return new PaginatedResult<ContributionsToUniversityResponseDTO>(
                parameters.PageIndex,
                currentPageCount,
                totalCount,
                contributionsResult);
        }

        public async Task<ContributionsToUniversityResponseDTO> GetContributionToUniversityByIdAsync(int id)
        {
            var contribution = await Repo.GetAsync(new ContributionsToUniversitySpecifications(id))
                ?? throw NotFound();

            return Mapper.Map<ContributionsToUniversityResponseDTO>(contribution);
        }

        public async Task<ContributionsToUniversityResponseDTO> CreateContributionToUniversityAsync(
            ContributionsToUniversityCreateDTO contributionsToUniversityCreateDto,
            string facultyMemberEmail)
        {
            var facultyMember = await GetFacultyMemberByEmailAsync(facultyMemberEmail);

            var contribution = Mapper.Map<ContributionsToUniversity>(contributionsToUniversityCreateDto);
            contribution.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(contribution);
            await SaveChangesAsync();

            return Mapper.Map<ContributionsToUniversityResponseDTO>(contribution);
        }

        public async Task<ContributionsToUniversityResponseDTO> UpdateContributionToUniversityAsync(
            int contributionToUniversityId,
            ContributionsToUniversityUpdateDTO contributionsToUniversityUpdateDto)
        {
            var contribution = await Repo.GetAsync(new ContributionsToUniversitySpecifications(contributionToUniversityId))
                ?? throw NotFound();

            Mapper.Map(contributionsToUniversityUpdateDto, contribution);

            Repo.Update(contribution);
            await SaveChangesAsync();

            return Mapper.Map<ContributionsToUniversityResponseDTO>(contribution);
        }

        public async Task DeleteContributionToUniversityAsync(int contributionToUniversityId)
        {
            var contribution = await Repo.GetAsync(new ContributionsToUniversitySpecifications(contributionToUniversityId))
                ?? throw NotFound();

            contribution.IsDeleted = true;

            Repo.Update(contribution);
            await SaveChangesAsync();
        }
    }
}
