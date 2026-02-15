using Domain.Entities.AcademicDataModule.ProjectsAndCommitteesModule;
using Services.Abstraction.Contracts.AcademicDataModule.ProjectsAndCommitteesModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.Dtos.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Services.Implementations.AcademicDataModule.ProjectsAndCommitteesModule
{
    public class ProjectsService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IAuthenticationService authenticationService)
                : BaseService<Projects, int>(unitOfWork, authenticationService, mapper), IProjectsService
    {
        protected override string EntityName => "Projects";
        public async Task<PaginatedResult<ProjectsResponseDto>> GetAllProjectsAsync(ProjectsSpecifcationsParameters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            var projects = await Repo.GetAllAsync(new ProjectsSpecifications(parameters, currentUser.Email))
                ?? throw NotFound();

            var projectsResult = Mapper.Map<IEnumerable<ProjectsResponseDto>>(projects);

            var currentPageCount = projects.Count();

            var totalCount = await Repo.CountAsync(new ProjectsCountSpecifications(parameters, currentUser.Email));

            return new PaginatedResult<ProjectsResponseDto>(parameters.PageIndex, currentPageCount, totalCount, projectsResult);
        }

        public async Task<ProjectsResponseDto> GetProjectByIdAsync(int id)
        { 
            var currentUser = await GetCurrentUserAsync();

            var project = await Repo.GetAsync(new ProjectsSpecifications(id)) ?? throw new NotFoundException("errors.Project.notFound" , id);

            EnsureOwnership(project.FacultyMemberId, currentUser.UserId, EntityName);

            return Mapper.Map<ProjectsResponseDto>(project);
        }

        public async Task<ProjectsResponseDto> CreateProjectAsync(ProjectCreateDto projectCreateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var project = Mapper.Map<Projects>(projectCreateDto);
            project.FacultyMemberId = currentUser.UserId;

            await Repo.AddAsync(project);
            await SaveChangesAsync();

            return Mapper.Map<ProjectsResponseDto>(project);
        }

        public async Task<ProjectsResponseDto> UpdateProjectAsync(int projectId, ProjectUpdateDto projectUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var project = await Repo.GetAsync(new ProjectsSpecifications(projectId))
                ?? throw NotFound();

            EnsureOwnership(project.FacultyMemberId, currentUser.UserId, EntityName);

            Mapper.Map(projectUpdateDto, project);

            Repo.Update(project);
            await SaveChangesAsync();

            return Mapper.Map<ProjectsResponseDto>(project);
        }

        public async Task DeleteProjectAsync(int projectId)
        {
            var currentUser = await GetCurrentUserAsync();

            var project = await Repo.GetAsync(new ProjectsSpecifications(projectId))
                ?? throw NotFound();

            EnsureOwnership(project.FacultyMemberId, currentUser.UserId, EntityName);

            project.IsDeleted = true;

            Repo.Update(project);
            await SaveChangesAsync();
        }
    }
}