using Domain.Entities.AcademicDataModule.ProjectsAndCommitteesModule;
using Services.Abstraction.Contracts.AcademicDataModule.ProjectsAndCommitteesModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ProjectsAndComiteesModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.Dtos.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Services.Implementations.AcademicDataModule.ProjectsAndCommitteesModule
{
    public class CommitteesAndAssociationsService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IAuthenticationService authenticationService,
        ICommitteesAndAssociationsHelper committeesAndAssociationsHelper)
        : BaseService<CommitteesAndAssociations, int>(unitOfWork, authenticationService, mapper),
          ICommitteesAndAssociationsService
    {
        private readonly ICommitteesAndAssociationsHelper _helper = committeesAndAssociationsHelper;

        protected override string EntityName => "Committees And Associations";

        public async Task<PaginatedResult<CommitteesAndAssociationsResponseDto>> GetAllCommitteesAndAssociationsAsync(
            CommitteesAndAssociationsSpecificationsParameters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            return await _helper.GetAllCommitteesAndAssociationsAsync(parameters, currentUser.Email);
        }

        public async Task<CommitteesAndAssociationsResponseDto> GetCommitteeOrAssociationByIdAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var committeeOrAssociation = await Repo.GetAsync(new CommitteesAndAssociationsSpecifications(id))
                ?? throw NotFound();

            EnsureOwnership(committeeOrAssociation.FacultyMemberId, currentUser.UserId, EntityName);

            return await _helper.GetCommitteeOrAssociationByIdAsync(id);
        }

        public async Task<CommitteesAndAssociationsResponseDto> CreateCommitteeOrAssociationAsync(
            CommitteeOrAssociationCreateDto committeeOrAssociationCreateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            return await _helper.CreateCommitteeOrAssociationAsync(committeeOrAssociationCreateDto, currentUser.Email);
        }

        public async Task<CommitteesAndAssociationsResponseDto> UpdateCommitteeOrAssociationAsync(
            int committeeOrAssociationId,
            CommitteeOrAssociationUpdateDto committeeOrAssociationUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var committeeOrAssociation = await Repo.GetAsync(
                new CommitteesAndAssociationsSpecifications(committeeOrAssociationId))
                ?? throw NotFound();

            EnsureOwnership(committeeOrAssociation.FacultyMemberId, currentUser.UserId, EntityName);

            return await _helper.UpdateCommitteeOrAssociationAsync(
                committeeOrAssociationId,
                committeeOrAssociationUpdateDto);
        }

        public async Task DeleteCommitteeOrAssociationAsync(int committeeOrAssociationId)
        {
            var currentUser = await GetCurrentUserAsync();

            var committeeOrAssociation = await Repo.GetAsync(
                new CommitteesAndAssociationsSpecifications(committeeOrAssociationId))
                ?? throw NotFound();

            EnsureOwnership(committeeOrAssociation.FacultyMemberId, currentUser.UserId, EntityName);

            await _helper.DeleteCommitteeOrAssociationAsync(committeeOrAssociationId);
        }
    }
}
