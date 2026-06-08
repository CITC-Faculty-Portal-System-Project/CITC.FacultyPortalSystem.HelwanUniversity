using Domain.Entities.AcademicDataModule.ProjectsAndCommitteesModule;
using Services.Abstraction.Contracts.AcademicDataModule.ProjectsAndCommitteesModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.Dtos.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Services.Implementations.AcademicDataModule.ProjectsAndCommitteesModule
{
    public class CommitteesAndAssociationsService(
      IUnitOfWork unitOfWork,
      IAuthenticationService authenticationService,
      IMapper mapper)
      : BaseService<CommitteesAndAssociations, int>(unitOfWork, authenticationService, mapper),
        ICommitteesAndAssociationsService
    {
        protected override string EntityName => "Committees And Associations";

        public async Task<PaginatedResult<CommitteesAndAssociationsResponseDto>> GetAllCommitteesAndAssociationsAsync(
            CommitteesAndAssociationsSpecificationsParameters parameters,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            var committees = await Repo.GetAllAsync(
                new CommitteesAndAssociationsSpecifications(parameters, email))
                ?? throw NotFound();

            var mapped = Mapper.Map<IEnumerable<CommitteesAndAssociationsResponseDto>>(committees);

            var totalCount = await Repo.CountAsync(
                new CommitteesAndAssociationsCountSpecifications(parameters, email));

            return new PaginatedResult<CommitteesAndAssociationsResponseDto>(
                parameters.PageIndex,
                mapped.Count(),
                totalCount,
                mapped);
        }

        public async Task<CommitteesAndAssociationsResponseDto> GetCommitteeOrAssociationByIdAsync(
            int id,
            string? facultyMemberEmail = null)
        {
            var committee = await Repo.GetAsync(
                new CommitteesAndAssociationsSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                committee.FacultyMemberId,
                facultyMemberEmail);

            return Mapper.Map<CommitteesAndAssociationsResponseDto>(committee);
        }

        public async Task<CommitteesAndAssociationsResponseDto> CreateCommitteeOrAssociationAsync(
            CommitteeOrAssociationCreateDto dto,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            var facultyMember = await GetFacultyMemberByEmailAsync(email);

            var committee = Mapper.Map<CommitteesAndAssociations>(dto);
            committee.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(committee);
            await SaveChangesAsync();

            return Mapper.Map<CommitteesAndAssociationsResponseDto>(committee);
        }

        public async Task<CommitteesAndAssociationsResponseDto> UpdateCommitteeOrAssociationAsync(
            int id,
            CommitteeOrAssociationUpdateDto dto,
            string? facultyMemberEmail = null)
        {
            var committee = await Repo.GetAsync(
                new CommitteesAndAssociationsSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                committee.FacultyMemberId,
                facultyMemberEmail);

            Mapper.Map(dto, committee);

            Repo.Update(committee);
            await SaveChangesAsync();

            return Mapper.Map<CommitteesAndAssociationsResponseDto>(committee);
        }

        public async Task DeleteCommitteeOrAssociationAsync(
            int id,
            string? facultyMemberEmail = null)
        {
            var committee = await Repo.GetAsync(
                new CommitteesAndAssociationsSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                committee.FacultyMemberId,
                facultyMemberEmail);

            committee.IsDeleted = true;

            Repo.Update(committee);
            await SaveChangesAsync();
        }
    }
}
