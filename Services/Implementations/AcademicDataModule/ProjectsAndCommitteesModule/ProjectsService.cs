using Domain.Entities.AcademicDataModule.ProjectsAndCommitteesModule;
using Services.Abstraction.Contracts.AcademicDataModule.ProjectsAndCommitteesModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ProjectsAndComiteesModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.Dtos.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Services.Implementations.AcademicDataModule.ProjectsAndCommitteesModule
{
    public class ProjectsService(
          IUnitOfWork unitOfWork,
          IMapper mapper,
          IAuthenticationService authenticationService,
          IProjectsHelper projectsHelper)
          : BaseService<Projects, int>(unitOfWork, authenticationService, mapper),
            IProjectsService
    {
        private readonly IProjectsHelper _helper = projectsHelper;

        protected override string EntityName => "Projects";

        public async Task<PaginatedResult<ProjectsResponseDto>> GetAllProjectsAsync(
            ProjectsSpecifcationsParameters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            return await _helper.GetAllProjectsAsync(parameters, currentUser.Email);
        }

        public async Task<ProjectsResponseDto> GetProjectByIdAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var project = await Repo.GetAsync(new ProjectsSpecifications(id))
                ?? throw new NotFoundException("Project is Not Found.");

            EnsureOwnership(project.FacultyMemberId, currentUser.UserId, EntityName);

            return await _helper.GetProjectByIdAsync(id);
        }

        public async Task<ProjectsResponseDto> CreateProjectAsync(ProjectCreateDto projectCreateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            return await _helper.CreateProjectAsync(projectCreateDto, currentUser.Email);
        }

        public async Task<ProjectsResponseDto> UpdateProjectAsync(int projectId, ProjectUpdateDto projectUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var project = await Repo.GetAsync(new ProjectsSpecifications(projectId))
                ?? throw NotFound();

            EnsureOwnership(project.FacultyMemberId, currentUser.UserId, EntityName);

            return await _helper.UpdateProjectAsync(projectId, projectUpdateDto);
        }

        public async Task DeleteProjectAsync(int projectId)
        {
            var currentUser = await GetCurrentUserAsync();

            var project = await Repo.GetAsync(new ProjectsSpecifications(projectId))
                ?? throw NotFound();

            EnsureOwnership(project.FacultyMemberId, currentUser.UserId, EntityName);

            await _helper.DeleteProjectAsync(projectId);
        }
    }
}