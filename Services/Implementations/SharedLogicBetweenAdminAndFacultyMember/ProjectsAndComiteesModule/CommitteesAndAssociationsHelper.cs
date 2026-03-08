using Domain.Entities.AcademicDataModule.ProjectsAndCommitteesModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ProjectsAndComiteesModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.Dtos.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Services.Implementations.SharedLogicBetweenAdminAndFacultyMember.ProjectsAndComiteesModule
{
    public class CommitteesAndAssociationsHelper(
        IUnitOfWork unitOfWork,
        IAuthenticationService authenticationService,
        IMapper mapper)
        : BaseService<CommitteesAndAssociations, int>(unitOfWork, authenticationService, mapper),
          ICommitteesAndAssociationsHelper
    {
        protected override string EntityName => "Committees And Associations";

        public async Task<PaginatedResult<CommitteesAndAssociationsResponseDto>> GetAllCommitteesAndAssociationsAsync(
            CommitteesAndAssociationsSpecificationsParameters parameters,
            string facultyMemberEmail)
        {
            var committeesAndAssociations = await Repo.GetAllAsync(
                new CommitteesAndAssociationsSpecifications(parameters, facultyMemberEmail));

            var committeesAndAssociationsResult =
                Mapper.Map<IEnumerable<CommitteesAndAssociationsResponseDto>>(committeesAndAssociations);

            var currentPageCount = committeesAndAssociationsResult.Count();

            var totalCount = await Repo.CountAsync(
                new CommitteesAndAssociationsCountSpecifications(parameters, facultyMemberEmail));

            return new PaginatedResult<CommitteesAndAssociationsResponseDto>(
                parameters.PageIndex,
                currentPageCount,
                totalCount,
                committeesAndAssociationsResult);
        }

        public async Task<CommitteesAndAssociationsResponseDto> GetCommitteeOrAssociationByIdAsync(int id)
        {
            var committeeOrAssociation = await Repo.GetAsync(new CommitteesAndAssociationsSpecifications(id))
                ?? throw NotFound();

            return Mapper.Map<CommitteesAndAssociationsResponseDto>(committeeOrAssociation);
        }

        public async Task<CommitteesAndAssociationsResponseDto> CreateCommitteeOrAssociationAsync(
            CommitteeOrAssociationCreateDto committeeOrAssociationCreateDto,
            string facultyMemberEmail)
        {
            var facultyMember = await GetFacultyMemberByEmailAsync(facultyMemberEmail);

            var committeeOrAssociation = Mapper.Map<CommitteesAndAssociations>(committeeOrAssociationCreateDto);
            committeeOrAssociation.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(committeeOrAssociation);
            await SaveChangesAsync();

            return Mapper.Map<CommitteesAndAssociationsResponseDto>(committeeOrAssociation);
        }

        public async Task<CommitteesAndAssociationsResponseDto> UpdateCommitteeOrAssociationAsync(
            int committeeOrAssociationId,
            CommitteeOrAssociationUpdateDto committeeOrAssociationUpdateDto)
        {
            var committeeOrAssociation = await Repo.GetAsync(
                new CommitteesAndAssociationsSpecifications(committeeOrAssociationId))
                ?? throw NotFound();

            Mapper.Map(committeeOrAssociationUpdateDto, committeeOrAssociation);

            Repo.Update(committeeOrAssociation);
            await SaveChangesAsync();

            return Mapper.Map<CommitteesAndAssociationsResponseDto>(committeeOrAssociation);
        }

        public async Task DeleteCommitteeOrAssociationAsync(int committeeOrAssociationId)
        {
            var committeeOrAssociation = await Repo.GetAsync(
                new CommitteesAndAssociationsSpecifications(committeeOrAssociationId))
                ?? throw NotFound();

            committeeOrAssociation.IsDeleted = true;

            Repo.Update(committeeOrAssociation);
            await SaveChangesAsync();
        }
    }
}
