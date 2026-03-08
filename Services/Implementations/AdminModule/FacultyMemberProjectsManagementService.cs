using Services.Abstraction.Contracts.AdminModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ProjectsAndComiteesModule;
using Shared.Dtos.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Services.Implementations.AdminModule
{
    public class FacultyMemberProjectsManagementService(IProjectsHelper _helper)
        :IFacultyMemberProjectsManagementService
    {
        public Task<PaginatedResult<ProjectsResponseDto>> GetFacultyMemberProjectsAsync(
           ProjectsSpecifcationsParameters parameters,
           string facultyMemberEmail)
           => _helper.GetAllProjectsAsync(parameters, facultyMemberEmail);

        public Task<ProjectsResponseDto> GetFacultyMemberProjectByIdAsync(int id)
            => _helper.GetProjectByIdAsync(id);

        public Task<ProjectsResponseDto> CreateFacultyMemberProjectAsync(
            ProjectCreateDto projectCreateDto,
            string facultyMemberEmail)
            => _helper.CreateProjectAsync(projectCreateDto, facultyMemberEmail);

        public Task<ProjectsResponseDto> UpdateFacultyMemberProjectAsync(
            int projectId,
            ProjectUpdateDto projectUpdateDto)
            => _helper.UpdateProjectAsync(projectId, projectUpdateDto);

        public Task DeleteFacultyMemberProjectAsync(int projectId)
            => _helper.DeleteProjectAsync(projectId);
    }
}
