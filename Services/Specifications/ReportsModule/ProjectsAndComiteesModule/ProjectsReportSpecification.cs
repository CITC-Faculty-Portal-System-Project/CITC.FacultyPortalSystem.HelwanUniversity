using Domain.Entities.FacultyMemberDataModule;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos.ReportsAndDashboard.ProjectsAndComiteesModule;
using Shared.Enums.ReportsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Base.ProjectsAndComiteesModule;
using System.Linq;

namespace Services.Specifications.ReportsModule.ProjectsAndComiteesModule
{
    public class ProjectsReportSpecification
        : AggregationSpecification<FacultyMember, ProjectsReportResponseDTO>
    {
        private readonly bool _isPaginated;
        private readonly int _pageIndex;
        private readonly int _pageSize;

        private readonly List<Guid>? _typesOfProject;

        public ProjectsReportSpecification(
            BaseProjectsReportSpecificationParameters parameters,
            ReportMode mode,
            int pageIndex = 1,
            int pageSize = 9,
            string? search = null)
        {
            _isPaginated = mode == ReportMode.Table;
            _pageIndex = pageIndex;
            _pageSize = pageSize;

            if (parameters.TypesOfProject is not null)
                _typesOfProject = parameters.TypesOfProject;

            SetCriteria(fd =>
                !fd.IsDeleted

                &&
                (
                    (parameters.FacultyIds != null && parameters.FacultyIds.Any()
                        && parameters.DepartmentIds != null && parameters.DepartmentIds.Any()
                        && (parameters.FacultyIds.Contains(fd.PersonalData!.FacultyId!.Value)
                            || parameters.DepartmentIds.Contains(fd.PersonalData!.DeptId!)))

                    || (parameters.FacultyIds != null && parameters.FacultyIds.Any()
                        && (parameters.DepartmentIds == null || !parameters.DepartmentIds.Any())
                        && parameters.FacultyIds.Contains(fd.PersonalData!.FacultyId!.Value))

                    || ((parameters.FacultyIds == null || !parameters.FacultyIds.Any())
                        && parameters.DepartmentIds != null && parameters.DepartmentIds.Any()
                        && parameters.DepartmentIds.Contains(fd.PersonalData!.DeptId!))

                    || ((parameters.FacultyIds == null || !parameters.FacultyIds.Any())
                        && (parameters.DepartmentIds == null || !parameters.DepartmentIds.Any()))
                )

                &&
                (
                    _typesOfProject == null
                    || !_typesOfProject.Any()
                    || fd.Projects.Any(p =>
                        _typesOfProject.Contains(p.TypeOfProjectId))
                )

                &&
                (
                    string.IsNullOrWhiteSpace(search)
                    || fd.PersonalData!.NameAr.Contains(search)
                    || fd.PersonalData!.NameEn.Contains(search)
                )
            );

            switch (parameters.Sort)
            {
                case ProjectsReportSortingOptions.FacultyMemberNameASC:
                    AddOrderBy(fd => fd.PersonalData!.NameAr);
                    break;

                case ProjectsReportSortingOptions.FacultyMemberNameDESC:
                    AddOrderByDescending(fd => fd.PersonalData!.NameAr);
                    break;

                case ProjectsReportSortingOptions.NoOfProjectsASC:
                    AddOrderBy(fd => fd.Projects.Count(p => !p.IsDeleted));
                    break;

                case ProjectsReportSortingOptions.NoOfProjectsDESC:
                    AddOrderByDescending(fd => fd.Projects.Count(p => !p.IsDeleted));
                    break;

                default:
                    AddOrderBy(fd => fd.PersonalData!.NameAr);
                    break;
            }

            if (_isPaginated)
                applyPagination(pageSize, pageIndex);

            AddIncludes(fd => fd.PersonalData!);
            AddIncludes(fd => fd.PersonalData!.Title);

            AddIncludeWithChain(fd => fd
                .Include(x => x.Projects)
                    .ThenInclude(p => p.TypeOfProject));

            AddIncludeWithChain(fd => fd
                .Include(x => x.Projects)
                    .ThenInclude(p => p.ParticipationRole));
        }

        public override IQueryable<ProjectsReportResponseDTO> Apply(IQueryable<FacultyMember> query)
        {
            var baseQuery = query.Where(Criteria!);

            var projected = baseQuery.Select(fd => new ProjectsReportResponseDTO
            {
                FacultyMemberName =
                    (fd.PersonalData!.Title!.ValueAr ?? "") +
                    (fd.PersonalData.NameAr ?? ""),

                Projects =
                    fd.Projects
                        .Where(p =>
                            !p.IsDeleted &&
                            (
                                _typesOfProject == null ||
                                !_typesOfProject.Any() ||
                                _typesOfProject.Contains(p.TypeOfProjectId)
                            )
                        )
                        .GroupBy(p => p.TypeOfProject.ValueAr)
                        .Select(g => new FacultyMemberProjectAnalysisDTO
                        {
                            ProjectType = g.Key,
                            NoOfProjects = g.Count()
                        })
                        .ToList()
            });

            return projected;
        }
    }
}