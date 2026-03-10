using Shared.Dtos.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.ProjectsAndCommitteesModule
{
    public interface IProjectsService
    {
        Task<PaginatedResult<ProjectsResponseDto>> GetAllProjectsAsync(
      ProjectsSpecifcationsParameters parameters,
      string? facultyMemberEmail = null);

        Task<ProjectsResponseDto> GetProjectByIdAsync(
            int id,
            string? facultyMemberEmail = null);

        Task<ProjectsResponseDto> CreateProjectAsync(
            ProjectCreateDto dto,
            string? facultyMemberEmail = null);

        Task<ProjectsResponseDto> UpdateProjectAsync(
            int id,
            ProjectUpdateDto dto,
            string? facultyMemberEmail = null);

        Task DeleteProjectAsync(
            int id,
            string? facultyMemberEmail = null);
    }
}
