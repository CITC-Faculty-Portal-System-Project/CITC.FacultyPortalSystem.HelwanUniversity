using Shared.SpecificationParameters.ReportsAndDashboard.Tables.WritingsModule;
using System.Linq.Expressions;

namespace Services.Specifications.ReportsModule.Tables.WritingsModule
{
    public class WrtiningsReportCountSpecification : BaseSpecifications<FacultyMember, Guid>
    {
        public WrtiningsReportCountSpecification
            (WritingsReportSpecificationParameters parameters) 
            : base(fd =>
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
                ))
        {
        }
    }
}
