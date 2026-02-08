using Shared.Dtos.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.ProjectsAndCommitteesModule
{
    public interface IProjectsService
    {
        public Task<PaginatedResult<ProjectsResponseDto>> GetAllProjectsAsync(ProjectsSpecifcationsParameters parameters);
        public Task<ProjectsResponseDto> GetProjectByIdAsync(int id);
        public Task<ProjectsResponseDto> CreateProjectAsync(ProjectCreateDto projectCreateDto);
        public Task<ProjectsResponseDto> UpdateProjectAsync(int projectId, ProjectUpdateDto projectUpdateDto);
        public Task DeleteProjectAsync(int projectId);
    }
}
