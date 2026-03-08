using Domain.Entities.AcademicDataModule.ProjectsAndCommitteesModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ProjectsAndComiteesModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.Dtos.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Services.Implementations.SharedLogicBetweenAdminAndFacultyMember.ProjectsAndComiteesModule
{
    public class ProjectsHelper(
        IUnitOfWork unitOfWork,
        IAuthenticationService authenticationService,
        IMapper mapper)
        : BaseService<Projects, int>(unitOfWork, authenticationService, mapper),
          IProjectsHelper
    {
        protected override string EntityName => "Projects";

        public async Task<PaginatedResult<ProjectsResponseDto>> GetAllProjectsAsync(
            ProjectsSpecifcationsParameters parameters,
            string facultyMemberEmail)
        {
            var projects = await Repo.GetAllAsync(
                new ProjectsSpecifications(parameters, facultyMemberEmail));

            var projectsResult =
                Mapper.Map<IEnumerable<ProjectsResponseDto>>(projects);

            var currentPageCount = projectsResult.Count();

            var totalCount = await Repo.CountAsync(
                new ProjectsCountSpecifications(parameters, facultyMemberEmail));

            return new PaginatedResult<ProjectsResponseDto>(
                parameters.PageIndex,
                currentPageCount,
                totalCount,
                projectsResult);
        }

        public async Task<ProjectsResponseDto> GetProjectByIdAsync(int id)
        {
            var project = await Repo.GetAsync(new ProjectsSpecifications(id))
                ?? throw new NotFoundException("Project is Not Found.");

            return Mapper.Map<ProjectsResponseDto>(project);
        }

        public async Task<ProjectsResponseDto> CreateProjectAsync(
            ProjectCreateDto projectCreateDto,
            string facultyMemberEmail)
        {
            var facultyMember = await GetFacultyMemberByEmailAsync(facultyMemberEmail);

            var project = Mapper.Map<Projects>(projectCreateDto);
            project.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(project);
            await SaveChangesAsync();

            return Mapper.Map<ProjectsResponseDto>(project);
        }

        public async Task<ProjectsResponseDto> UpdateProjectAsync(
            int projectId,
            ProjectUpdateDto projectUpdateDto)
        {
            var project = await Repo.GetAsync(new ProjectsSpecifications(projectId))
                ?? throw NotFound();

            Mapper.Map(projectUpdateDto, project);

            Repo.Update(project);
            await SaveChangesAsync();

            return Mapper.Map<ProjectsResponseDto>(project);
        }

        public async Task DeleteProjectAsync(int projectId)
        {
            var project = await Repo.GetAsync(new ProjectsSpecifications(projectId))
                ?? throw NotFound();

            project.IsDeleted = true;

            Repo.Update(project);
            await SaveChangesAsync();
        }
    }
}
