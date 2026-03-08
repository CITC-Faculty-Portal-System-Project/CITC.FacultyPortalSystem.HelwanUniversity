using Services.Abstraction.Contracts.AdminModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ProjectsAndComiteesModule;
using Shared.Dtos.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Services.Implementations.AdminModule
{
    public class FacultyMemberCommitteesAndAssociationsManagementService(ICommitteesAndAssociationsHelper _helper)
        :IFacultyMemberCommitteesAndAssociationsManagementService
    {
        public Task<PaginatedResult<CommitteesAndAssociationsResponseDto>> GetFacultyMemberCommitteesAndAssociationsAsync(
      CommitteesAndAssociationsSpecificationsParameters parameters,
      string facultyMemberEmail)
      => _helper.GetAllCommitteesAndAssociationsAsync(parameters, facultyMemberEmail);

        public Task<CommitteesAndAssociationsResponseDto> GetFacultyMemberCommitteeOrAssociationByIdAsync(int id)
            => _helper.GetCommitteeOrAssociationByIdAsync(id);

        public Task<CommitteesAndAssociationsResponseDto> CreateFacultyMemberCommitteeOrAssociationAsync(
            CommitteeOrAssociationCreateDto committeeOrAssociationCreateDto,
            string facultyMemberEmail)
            => _helper.CreateCommitteeOrAssociationAsync(committeeOrAssociationCreateDto, facultyMemberEmail);

        public Task<CommitteesAndAssociationsResponseDto> UpdateFacultyMemberCommitteeOrAssociationAsync(
            int committeeOrAssociationId,
            CommitteeOrAssociationUpdateDto committeeOrAssociationUpdateDto)
            => _helper.UpdateCommitteeOrAssociationAsync(committeeOrAssociationId, committeeOrAssociationUpdateDto);

        public Task DeleteFacultyMemberCommitteeOrAssociationAsync(int committeeOrAssociationId)
            => _helper.DeleteCommitteeOrAssociationAsync(committeeOrAssociationId);
    }
}
