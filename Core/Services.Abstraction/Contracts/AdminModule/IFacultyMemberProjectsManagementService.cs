using Shared.Dtos.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Services.Abstraction.Contracts.AdminModule
{
    public interface IFacultyMemberProjectsManagementService
    {
        Task<PaginatedResult<ProjectsResponseDto>> GetFacultyMemberProjectsAsync(
            ProjectsSpecifcationsParameters parameters,
            string facultyMemberEmail);

        Task<ProjectsResponseDto> GetFacultyMemberProjectByIdAsync(int id);

        Task<ProjectsResponseDto> CreateFacultyMemberProjectAsync(
            ProjectCreateDto projectCreateDto,
            string facultyMemberEmail);

        Task<ProjectsResponseDto> UpdateFacultyMemberProjectAsync(
            int projectId,
            ProjectUpdateDto projectUpdateDto);

        Task DeleteFacultyMemberProjectAsync(int projectId);
    }
}
