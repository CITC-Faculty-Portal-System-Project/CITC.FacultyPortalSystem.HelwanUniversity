using Microsoft.EntityFrameworkCore;
using Shared.Dtos.ReportsAndDashboard.WrtingsAndPatentsModule;
using Shared.Dtos.ReportsAndDashboard.WrtingsModule;
using Shared.Enums.ReportsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Base.WritingsModule;

namespace Services.Specifications.ReportsModule.WritingsModule
{
    public class WritingsReportSpecifications
        : AggregationSpecification<FacultyMember, WritingsReportResponseDTO>
    {
        private readonly bool _isPaginated;
        private readonly int _pageIndex;
        private readonly int _pageSize;

        public WritingsReportSpecifications(
            BaseWritingsReportSpecificationParameters parameters,
            ReportMode mode,
            int pageIndex = 1,
            int pageSize = 9,
            string? search = null)
        {
            _isPaginated = mode == ReportMode.Table;
            _pageIndex = pageIndex;
            _pageSize = pageSize;

            SetCriteria(fd =>
                !fd.IsDeleted

                && (
                    parameters.FacultyIds != null && parameters.FacultyIds.Any()
                    && parameters.DepartmentIds != null && parameters.DepartmentIds.Any()
                        && (parameters.FacultyIds.Contains(fd.PersonalData!.FacultyId!.Value)
                            || parameters.DepartmentIds.Contains(fd.PersonalData!.DeptId!))

                    || parameters.FacultyIds != null && parameters.FacultyIds.Any()
                        && (parameters.DepartmentIds == null || !parameters.DepartmentIds.Any())
                        && parameters.FacultyIds.Contains(fd.PersonalData!.FacultyId!.Value)

                    || (parameters.FacultyIds == null || !parameters.FacultyIds.Any())
                        && parameters.DepartmentIds != null && parameters.DepartmentIds.Any()
                        && parameters.DepartmentIds.Contains(fd.PersonalData!.DeptId!)

                    || (parameters.FacultyIds == null || !parameters.FacultyIds.Any())
                        && (parameters.DepartmentIds == null || !parameters.DepartmentIds.Any())
                )

                && (
                    parameters.Roles == null
                    || !parameters.Roles.Any()
                    || fd.ScientificWritings!.Any(w =>
                        !w.IsDeleted &&
                        parameters.Roles.Contains(w.AuthorRoleId))
                )

                && (
                    string.IsNullOrWhiteSpace(search)
                    || fd.PersonalData!.NameAr.Contains(search)
                    || fd.PersonalData!.NameEn.Contains(search)
                )
            );

            switch (parameters.Sort)
            {
                case WritingsReportSortingOptions.FacultyMemberNameAsc:
                    AddOrderBy(fd => fd.PersonalData!.NameAr);
                    break;

                case WritingsReportSortingOptions.FacultyMemberNameDesc:
                    AddOrderByDescending(fd => fd.PersonalData!.NameAr);
                    break;

                case WritingsReportSortingOptions.NoOfWritingsASC:
                    AddOrderBy(fd => fd.ScientificWritings!
                        .Count(w => !w.IsDeleted));
                    break;

                case WritingsReportSortingOptions.NoOfWritingsDESC:
                    AddOrderByDescending(fd => fd.ScientificWritings!
                        .Count(w => !w.IsDeleted));
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
                        .Include(w => w.ScientificWritings)
                        .ThenInclude(w => w.AuthorRole));
        }

        public override IQueryable<WritingsReportResponseDTO> Apply(IQueryable<FacultyMember> query)
        {
            var baseQuery = query.Where(Criteria!);

            var projected = baseQuery.SelectMany(fd =>
                fd.ScientificWritings!
                    .Where(w => !w.IsDeleted)
                    .Select(w => new WritingsReportResponseDTO
                    {
                        FacultyMemberName =
                            (fd.PersonalData!.Title!.ValueAr ?? "") + 
                            (fd.PersonalData.NameAr ?? ""),

                        Writings = fd.ScientificWritings!
                        .Where(w => !w.IsDeleted)
                        .GroupBy(w => w.AuthorRole)
                        .Select(g => new FacultyMemberWritingsAnalysisDTO
                        {
                            AuthorRole = g.Key.ValueAr,
                            NoOfWritings = g.Count()
                        })
                        .ToList()
                    }));

            return projected;
        }
    }
}