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
       IAuthenticationService authenticationService,
       IMapper mapper)
       : BaseService<Projects, int>(unitOfWork, authenticationService, mapper),
         IProjectsService
    {
        protected override string EntityName => "Projects";

        public async Task<PaginatedResult<ProjectsResponseDto>> GetAllProjectsAsync(
            ProjectsSpecifcationsParameters parameters,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            var projects = await Repo.GetAllAsync(
                new ProjectsSpecifications(parameters, email))
                ?? throw NotFound();

            var mapped = Mapper.Map<IEnumerable<ProjectsResponseDto>>(projects);

            var totalCount = await Repo.CountAsync(
                new ProjectsCountSpecifications(parameters, email));

            return new PaginatedResult<ProjectsResponseDto>(
                parameters.PageIndex,
                mapped.Count(),
                totalCount,
                mapped);
        }

        public async Task<ProjectsResponseDto> GetProjectByIdAsync(
            int id,
            string? facultyMemberEmail = null)
        {
            var project = await Repo.GetAsync(
                new ProjectsSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                project.FacultyMemberId,
                facultyMemberEmail);

            return Mapper.Map<ProjectsResponseDto>(project);
        }

        public async Task<ProjectsResponseDto> CreateProjectAsync(
            ProjectCreateDto dto,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            var facultyMember = await GetFacultyMemberByEmailAsync(email);

            var project = Mapper.Map<Projects>(dto);
            project.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(project);
            await SaveChangesAsync();

            return Mapper.Map<ProjectsResponseDto>(project);
        }

        public async Task<ProjectsResponseDto> UpdateProjectAsync(
            int id,
            ProjectUpdateDto dto,
            string? facultyMemberEmail = null)
        {
            var project = await Repo.GetAsync(
                new ProjectsSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                project.FacultyMemberId,
                facultyMemberEmail);

            Mapper.Map(dto, project);

            Repo.Update(project);
            await SaveChangesAsync();

            return Mapper.Map<ProjectsResponseDto>(project);
        }

        public async Task DeleteProjectAsync(
            int id,
            string? facultyMemberEmail = null)
        {
            var project = await Repo.GetAsync(
                new ProjectsSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                project.FacultyMemberId,
                facultyMemberEmail);

            project.IsDeleted = true;

            Repo.Update(project);
            await SaveChangesAsync();
        }
    }
}