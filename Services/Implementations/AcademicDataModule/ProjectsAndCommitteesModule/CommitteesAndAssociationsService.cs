using Domain.Entities.ProjectsAndCommitteesModule;
using Services.Abstraction.Contracts.AcademicDataModule.ProjectsAndCommitteesModule;
using Services.Global;
using Services.Specifications.ProjectsAndCommitteesModule;
using Shared.Dtos.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.ProjectsAndCommitteesModule;

namespace Services.Implementations.AcademicDataModule.ProjectsAndCommitteesModule
{
    public class CommitteesAndAssociationsService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IAuthenticationService authenticationService)
                : BaseService<CommitteesAndAssociations, int>(unitOfWork, authenticationService, mapper), ICommitteesAndAssociationsService
    {
        protected override string EntityName => "Committees And Associations";
        public async Task<PaginatedResult<CommitteesAndAssociationsResponseDto>> GetAllCommitteesAndAssociationsAsync(CommitteesAndAssociationsSpecificationsParameters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            var committeesAndAssociations = await Repo.GetAllAsync(new CommitteesAndAssociationsSpecifications(parameters, currentUser.Email))
                ?? throw NotFound();

            var committeesAndAssociationsResult = Mapper.Map<IEnumerable<CommitteesAndAssociationsResponseDto>>(committeesAndAssociations);

            var currentPageCount = committeesAndAssociations.Count();

            var totalCount = await Repo.CountAsync(new CommitteesAndAssociationsCountSpecifications(parameters, currentUser.Email));

            return new PaginatedResult<CommitteesAndAssociationsResponseDto>(parameters.PageIndex, currentPageCount, totalCount, committeesAndAssociationsResult);
        }

        public async Task<CommitteesAndAssociationsResponseDto> GetCommitteeOrAssociationByIdAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var committeeOrAssociation = await Repo.GetAsync(new CommitteesAndAssociationsSpecifications(id))
                ?? throw NotFound();

            EnsureOwnership(committeeOrAssociation.FacultyMemberId, currentUser.UserId, EntityName);

            return Mapper.Map<CommitteesAndAssociationsResponseDto>(committeeOrAssociation);
        }

        public async Task<CommitteesAndAssociationsResponseDto> CreateCommitteeOrAssociationAsync(CommitteeOrAssociationCreateDto committeeOrAssociationCreateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var committeeOrAssociation = Mapper.Map<CommitteesAndAssociations>(committeeOrAssociationCreateDto);
            committeeOrAssociation.FacultyMemberId = currentUser.UserId;

            await Repo.AddAsync(committeeOrAssociation);
            await SaveChangesAsync();

            return Mapper.Map<CommitteesAndAssociationsResponseDto>(committeeOrAssociation);
        }
        public async Task<CommitteesAndAssociationsResponseDto> UpdateCommitteeOrAssociationAsync(int committeeOrAssociationId, CommitteeOrAssociationUpdateDto committeeOrAssociationUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var committeeOrAssociation = await Repo.GetAsync(new CommitteesAndAssociationsSpecifications(committeeOrAssociationId))
                ?? throw NotFound();

            EnsureOwnership(committeeOrAssociation.FacultyMemberId, currentUser.UserId, EntityName);

            Mapper.Map(committeeOrAssociationUpdateDto, committeeOrAssociation);

            Repo.Update(committeeOrAssociation);
            await SaveChangesAsync();

            return Mapper.Map<CommitteesAndAssociationsResponseDto>(committeeOrAssociation);
        }

        public async Task DeleteCommitteeOrAssociationAsync(int committeeOrAssociationId)
        {
            var currentUser = await GetCurrentUserAsync();

            var committeeOrAssociation = await Repo.GetAsync(new CommitteesAndAssociationsSpecifications(committeeOrAssociationId))
                ?? throw NotFound();

            EnsureOwnership(committeeOrAssociation.FacultyMemberId, currentUser.UserId, EntityName);

            committeeOrAssociation.IsDeleted = true;

            Repo.Update(committeeOrAssociation);
            await SaveChangesAsync();
        }
    }
}
