using Shared.Dtos.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ProjectsAndComiteesModule
{
    public interface ICommitteesAndAssociationsHelper
    {
        Task<PaginatedResult<CommitteesAndAssociationsResponseDto>> GetAllCommitteesAndAssociationsAsync(
            CommitteesAndAssociationsSpecificationsParameters parameters,
            string facultyMemberEmail);

        Task<CommitteesAndAssociationsResponseDto> GetCommitteeOrAssociationByIdAsync(int id);

        Task<CommitteesAndAssociationsResponseDto> CreateCommitteeOrAssociationAsync(
            CommitteeOrAssociationCreateDto committeeOrAssociationCreateDto,
            string facultyMemberEmail);

        Task<CommitteesAndAssociationsResponseDto> UpdateCommitteeOrAssociationAsync(
            int committeeOrAssociationId,
            CommitteeOrAssociationUpdateDto committeeOrAssociationUpdateDto);

        Task DeleteCommitteeOrAssociationAsync(int committeeOrAssociationId);
    }

}
