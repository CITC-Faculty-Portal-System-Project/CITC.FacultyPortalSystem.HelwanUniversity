using Shared.Dtos.ReportsAndDashboard.WrtingsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.WritingsModule;

namespace Services.Specifications.ReportsModule.Tables.WritingsModule
{
    public class WritingsReportSpecifications
        : AggregationSpecification<FacultyMember, WritingsReportResponseDTO>
    {
        public WritingsReportSpecifications(
            WritingsReportSpecificationParameters parameters)
        {
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
                    string.IsNullOrWhiteSpace(parameters.Search)
                    || fd.PersonalData!.NameAr.Contains(parameters.Search)
                    || fd.PersonalData!.NameEn.Contains(parameters.Search)
                )
            );

            applyPagination(parameters.PageSize, parameters.PageIndex);

            AddIncludes(fd => fd.PersonalData!);
            AddIncludes(fd => fd.PersonalData!.Title);

            AddIncludes(fd => fd.ScientificWritings!);
            AddIncludes(fd => fd.ScientificWritings!.Select(sw =>sw.AuthorRole));
        }

        public override IQueryable<WritingsReportResponseDTO>
            Apply(IQueryable<FacultyMember> query)
        {
            return query
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
        }
    }
}