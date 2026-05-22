using Domain.Entities.AcademicDataModule.ResearchesModule;
using Shared.SpecificationParameters.ReportsAndDashboard.ResearchesModule;
using Shared.SpecificationParameters.ResearchesModule;
using System.Linq.Expressions;

namespace Services.Specifications.ReportsModule.ResearchesModule
{
    public class ResearchesPerYearReportCountSpecification : BaseSpecifications<Research, int>
    {
        public ResearchesPerYearReportCountSpecification
            (ResearchesPerYearReportSpecificationParameters parameters) 
            : base(BuildCriteria(parameters))
        {

        }
        private static Expression<Func<Research, bool>> BuildCriteria(
       ResearchesPerYearReportSpecificationParameters parameters)
        {

            Domain.Enums.PublicationType? mappedPublicationType = null;
            if (parameters.PublicationType.HasValue)
            {
                mappedPublicationType = Enum.Parse<Domain.Enums.PublicationType>(
                    parameters.PublicationType.Value.ToString(),
                    ignoreCase: true);
            }

            return r =>
               !r.IsDeleted
    && (
        (parameters.FacultyIds != null && parameters.FacultyIds.Any()
            && r.Contributions!.Any(c => parameters.FacultyIds.Contains(c.Contributor!.PersonalData!.FacultyId!.Value)))
        ||
        (parameters.DepartmentIds != null && parameters.DepartmentIds.Any()
            && r.Contributions!.Any(c => parameters.DepartmentIds.Contains(c.Contributor!.PersonalData!.DeptId)))
    )
       && (parameters.PubYears == null || !parameters.PubYears.Any()
    || parameters.PubYears.Contains(r.PubYear!.Value))
    && (!mappedPublicationType.HasValue || r.PublicationType == mappedPublicationType.Value)
    
    && (string.IsNullOrWhiteSpace(parameters.Search) ||
                    r.Title.Contains(parameters.Search));
        }
    }
}
