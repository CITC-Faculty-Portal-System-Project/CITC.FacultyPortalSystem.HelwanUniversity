using Shared.Dtos.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Services.Abstraction.Contracts.AdminModule
{
    public interface IFacultyMemberCommitteesAndAssociationsManagementService
    {
        Task<PaginatedResult<CommitteesAndAssociationsResponseDto>> GetFacultyMemberCommitteesAndAssociationsAsync(
         CommitteesAndAssociationsSpecificationsParameters parameters,
         string facultyMemberEmail);

        Task<CommitteesAndAssociationsResponseDto> GetFacultyMemberCommitteeOrAssociationByIdAsync(int id);

        Task<CommitteesAndAssociationsResponseDto> CreateFacultyMemberCommitteeOrAssociationAsync(
            CommitteeOrAssociationCreateDto committeeOrAssociationCreateDto,
            string facultyMemberEmail);

        Task<CommitteesAndAssociationsResponseDto> UpdateFacultyMemberCommitteeOrAssociationAsync(
            int committeeOrAssociationId,
            CommitteeOrAssociationUpdateDto committeeOrAssociationUpdateDto);

        Task DeleteFacultyMemberCommitteeOrAssociationAsync(int committeeOrAssociationId);
    }
}
