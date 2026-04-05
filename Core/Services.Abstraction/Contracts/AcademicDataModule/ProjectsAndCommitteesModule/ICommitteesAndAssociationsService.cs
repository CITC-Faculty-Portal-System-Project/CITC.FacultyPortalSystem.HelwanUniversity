using Shared.Dtos.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.ProjectsAndCommitteesModule
{
    public interface ICommitteesAndAssociationsService
    {
        Task<PaginatedResult<CommitteesAndAssociationsResponseDto>> GetAllCommitteesAndAssociationsAsync(
       CommitteesAndAssociationsSpecificationsParameters parameters,
       string? facultyMemberEmail = null);

        Task<CommitteesAndAssociationsResponseDto> GetCommitteeOrAssociationByIdAsync(
            int id,
            string? facultyMemberEmail = null);

        Task<CommitteesAndAssociationsResponseDto> CreateCommitteeOrAssociationAsync(
            CommitteeOrAssociationCreateDto dto,
            string? facultyMemberEmail = null);

        Task<CommitteesAndAssociationsResponseDto> UpdateCommitteeOrAssociationAsync(
            int id,
            CommitteeOrAssociationUpdateDto dto,
            string? facultyMemberEmail = null);

        Task DeleteCommitteeOrAssociationAsync(
            int id,
            string? facultyMemberEmail = null);
    }
}
