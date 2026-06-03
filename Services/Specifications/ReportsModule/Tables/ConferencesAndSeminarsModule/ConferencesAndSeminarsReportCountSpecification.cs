using Shared.SpecificationParameters.ReportsAndDashboard.Tables.ConferencesAndSeminarsModule;
using System.Linq.Expressions;

namespace Services.Specifications.ReportsModule.Tables.ConferencesAndSeminarsModule
{
    public class ConferencesAndSeminarsReportCountSpecification : BaseSpecifications<FacultyMember, Guid>

    {
        public ConferencesAndSeminarsReportCountSpecification
            (ConferencesAndSeminarsReportSpecificationParameters parameters) : base(fd =>
                 !fd.IsDeleted
                 && (
                     (parameters.FacultyIds != null && parameters.FacultyIds.Any() && parameters.DepartmentIds != null && parameters.DepartmentIds.Any()
                         && (parameters.FacultyIds.Contains(fd.PersonalData!.FacultyId!.Value) || parameters.DepartmentIds.Contains(fd.PersonalData!.DeptId!)))
                     || (parameters.FacultyIds != null && parameters.FacultyIds.Any() && (parameters.DepartmentIds == null || !parameters.DepartmentIds.Any())
                         && parameters.FacultyIds.Contains(fd.PersonalData!.FacultyId!.Value))
                     || ((parameters.FacultyIds == null || !parameters.FacultyIds.Any()) && parameters.DepartmentIds != null && parameters.DepartmentIds.Any()
                         && parameters.DepartmentIds.Contains(fd.PersonalData!.DeptId!))
                     || ((parameters.FacultyIds == null || !parameters.FacultyIds.Any()) && (parameters.DepartmentIds == null || !parameters.DepartmentIds.Any()))
                 )
                 && fd.ConferencesAndSeminars!.Any(c =>
                     !c.IsDeleted
                     && (parameters.Type == null || c.Type == (Domain.Enums.ConferenceOrSeminar)parameters.Type)
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
