using Shared.Dtos.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.ProjectsAndCommitteesModule
{
    public interface ICommitteesAndAssociationsService
    {
        public Task<PaginatedResult<CommitteesAndAssociationsResponseDto>> GetAllCommitteesAndAssociationsAsync(CommitteesAndAssociationsSpecificationsParameters parameters);
        public Task<CommitteesAndAssociationsResponseDto> GetCommitteeOrAssociationByIdAsync(int id);
        public Task<CommitteesAndAssociationsResponseDto> CreateCommitteeOrAssociationAsync(CommitteeOrAssociationCreateDto committeeOrAssociationCreateDto);
        public Task<CommitteesAndAssociationsResponseDto> UpdateCommitteeOrAssociationAsync(int committeeOrAssociationId, CommitteeOrAssociationUpdateDto committeesAndAssociationsUpdateDto);
        public Task DeleteCommitteeOrAssociationAsync(int committeeOrAssociationId);
    }
}
