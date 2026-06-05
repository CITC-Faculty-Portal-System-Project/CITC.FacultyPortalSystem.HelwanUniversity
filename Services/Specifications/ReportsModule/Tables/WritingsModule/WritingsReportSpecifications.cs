using Shared.Dtos.ReportsAndDashboard.WrtingsModule;
using Shared.Enums.ReportsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Base.WritingsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.WritingsModule;

namespace Services.Specifications.ReportsModule.Tables.WritingsModule
{
    public class WritingsReportSpecifications
        : AggregationSpecification<FacultyMember, WritingsReportResponseDTO>
    {

        private readonly bool _isPaginated;
        private readonly int _pageIndex;
        private readonly int _pageSize;
        public WritingsReportSpecifications(
            BaseWritingsReportSpecificationParameters parameters
            , ReportMode mode, int pageIndex = 1, int pageSize = 9
                  , string? search = null)
        {

            _isPaginated = mode == ReportMode.Table;
            _pageIndex = pageIndex;
            _pageSize = pageSize;


            SetCriteria(fd =>
                !fd.IsDeleted

                && (
                    parameters.FacultyIds != null
                     && parameters.FacultyIds.Any()
                     && parameters.FacultyIds.Contains(fd.PersonalData!.FacultyId!.Value)

                    ||

                    parameters.DepartmentIds != null
                     && parameters.DepartmentIds.Any()
                     && parameters.DepartmentIds.Contains(fd.PersonalData!.DeptId)
                )

                && (
                    parameters.Roles == null
                    || !parameters.Roles.Any()
                    || fd.ScientificWritings!.Any(w =>
                        !w.IsDeleted
                        && parameters.Roles.Contains(w.AuthorRoleId))
                )

                && (
                    string.IsNullOrWhiteSpace(search)
                    || fd.PersonalData!.NameAr.Contains(search)
                    || fd.PersonalData!.NameEn.Contains(search)
                )
            );

            AddIncludes(fd => fd.PersonalData!);
            AddIncludes(fd => fd.PersonalData!.Title);

            AddIncludes(fd => fd.ScientificWritings!);
            AddIncludes(fd => fd.ScientificWritings!.Select(sw =>sw.AuthorRole));
        }

        public override IQueryable<WritingsReportResponseDTO>
            Apply(IQueryable<FacultyMember> query)
        {
            var result = query
                .Where(Criteria!)
                .SelectMany(fd => fd.ScientificWritings!
                    .Select(w => new WritingsReportResponseDTO
                    {
                        FacultyMemberName =
                            (fd.PersonalData!.Title!.ValueAr ?? "") + ". "
                            + (fd.PersonalData.NameAr ?? ""),

                        AuthorRole = w.AuthorRole.ValueAr,

                        NoOfWritings = fd.ScientificWritings!
                            .Count(sw =>
                                !sw.IsDeleted
                                && sw.AuthorRoleId == w.AuthorRoleId)
                    }));

            if (_isPaginated)
            {
                result = result
                    .Skip((_pageIndex - 1) * _pageSize)
                    .Take(_pageSize);
            }

            return result;
        }
    }
}