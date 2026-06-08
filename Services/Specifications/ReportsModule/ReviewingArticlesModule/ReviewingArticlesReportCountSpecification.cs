using Domain.Entities.AcademicDataModule.ResearchesModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.ReviewingArticlesModule;
using System.Linq.Expressions;

namespace Services.Specifications.ReportsModule.ReviewingArticlesModule
{
    public class ReviewingArticlesReportCountSpecification : BaseSpecifications<FacultyMember, Guid>
    {
        public ReviewingArticlesReportCountSpecification
            (ReviewingArticlesReportTableSpecificationParameters parameters)
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
                    string.IsNullOrWhiteSpace(parameters.Search)
                    || fd.PersonalData!.NameAr.Contains(parameters.Search)
                    || fd.PersonalData!.NameEn.Contains(parameters.Search)
                ))
        {
        }
    }
}
