using Domain.Entities.AcademicDataModule.ResearchesModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.WritingsAndPatentsModule;
using System.Linq.Expressions;

namespace Services.Specifications.ReportsModule.WritingsAndPatentsModule
{
    public class PatentsReportCountSpecification : BaseSpecifications<FacultyMember, Guid>
    {
        public PatentsReportCountSpecification
            (PatentsReportTableSpecificationParameters parameters) 
            : base(fd =>
                !fd.IsDeleted

                && (
                    parameters.FacultyIds != null && parameters.FacultyIds.Any()
                    && parameters.DepartmentIds != null && parameters.DepartmentIds.Any()
                    && (
                        parameters.FacultyIds.Contains(fd.PersonalData!.FacultyId!.Value)
                        || parameters.DepartmentIds.Contains(fd.PersonalData!.DeptId)
                    )

                    || parameters.FacultyIds != null && parameters.FacultyIds.Any()
                    && (parameters.DepartmentIds == null || !parameters.DepartmentIds.Any())
                    && parameters.FacultyIds.Contains(fd.PersonalData!.FacultyId!.Value)

                    || (parameters.FacultyIds == null || !parameters.FacultyIds.Any())
                    && parameters.DepartmentIds != null && parameters.DepartmentIds.Any()
                    && parameters.DepartmentIds.Contains(fd.PersonalData!.DeptId)

                    || (parameters.FacultyIds == null || !parameters.FacultyIds.Any())
                    && (parameters.DepartmentIds == null || !parameters.DepartmentIds.Any())
                )

                && (
                    parameters.LocalOrInternational == null
                    || fd.Patents!.Any(p =>
                        !p.IsDeleted
                        && p.LocalOrInternational == (Domain.Enums.LocalOrInternational)parameters.LocalOrInternational
                    )
                )

                && (
                    string.IsNullOrWhiteSpace(parameters.Search)
                    || fd.PersonalData!.NameAr.Contains(parameters.Search)
                    || fd.PersonalData!.NameEn.Contains(parameters.Search)
                )
            )
        {
        }
    }
}
