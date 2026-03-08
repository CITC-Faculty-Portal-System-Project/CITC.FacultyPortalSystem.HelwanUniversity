using Shared.Dtos.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ProjectsAndComiteesModule
{
    public interface IProjectsHelper
    {
        Task<PaginatedResult<ProjectsResponseDto>> GetAllProjectsAsync(
            ProjectsSpecifcationsParameters parameters,
            string facultyMemberEmail);

        Task<ProjectsResponseDto> GetProjectByIdAsync(int id);

        Task<ProjectsResponseDto> CreateProjectAsync(
            ProjectCreateDto projectCreateDto,
            string facultyMemberEmail);

        Task<ProjectsResponseDto> UpdateProjectAsync(
            int projectId,
            ProjectUpdateDto projectUpdateDto);

        Task DeleteProjectAsync(int projectId);
    }
}
