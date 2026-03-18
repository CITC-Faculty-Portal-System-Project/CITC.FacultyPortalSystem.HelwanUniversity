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
       IAuthenticationService authenticationService,
       IMapper mapper)
       : BaseService<ContributionsToUniversity, int>(unitOfWork, authenticationService, mapper),
         IContributionsToUniversityService
    {
        protected override string EntityName => "Contributions To University";

        public async Task<PaginatedResult<ContributionsToUniversityResponseDTO>> GetAllContributionsToUniversityAsync(
            ContributionsToUniversitySpecificationParameters parameters,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            var contributions = await Repo.GetAllAsync(
                new ContributionsToUniversitySpecifications(parameters, email))
                ?? throw NotFound();

            var mapped = Mapper.Map<IEnumerable<ContributionsToUniversityResponseDTO>>(contributions);

            var totalCount = await Repo.CountAsync(
                new ContributionsToUniversityCountSpecifications(parameters, email));

            return new PaginatedResult<ContributionsToUniversityResponseDTO>(
                parameters.PageIndex,
                mapped.Count(),
                totalCount,
                mapped);
        }

        public async Task<ContributionsToUniversityResponseDTO> GetContributionToUniversityByIdAsync(
            int id,
            string? facultyMemberEmail = null)
        {
            var contribution = await Repo.GetAsync(
                new ContributionsToUniversitySpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                contribution.FacultyMemberId,
                facultyMemberEmail);

            return Mapper.Map<ContributionsToUniversityResponseDTO>(contribution);
        }

        public async Task<ContributionsToUniversityResponseDTO> CreateContributionToUniversityAsync(
            ContributionsToUniversityCreateDTO contributionsToUniversityCreateDto,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            var facultyMember = await GetFacultyMemberByEmailAsync(email);

            var contribution = Mapper.Map<ContributionsToUniversity>(contributionsToUniversityCreateDto);
            contribution.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(contribution);
            await SaveChangesAsync();

            return Mapper.Map<ContributionsToUniversityResponseDTO>(contribution);
        }

        public async Task<ContributionsToUniversityResponseDTO> UpdateContributionToUniversityAsync(
            int contributionToUniversityId,
            ContributionsToUniversityUpdateDTO contributionsToUniversityUpdateDto,
            string? facultyMemberEmail = null)
        {
            var contribution = await Repo.GetAsync(
                new ContributionsToUniversitySpecifications(contributionToUniversityId))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                contribution.FacultyMemberId,
                facultyMemberEmail);

            Mapper.Map(contributionsToUniversityUpdateDto, contribution);

            Repo.Update(contribution);
            await SaveChangesAsync();

            return Mapper.Map<ContributionsToUniversityResponseDTO>(contribution);
        }

        public async Task DeleteContributionToUniversityAsync(
            int contributionToUniversityId,
            string? facultyMemberEmail = null)
        {
            var contribution = await Repo.GetAsync(
                new ContributionsToUniversitySpecifications(contributionToUniversityId))
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