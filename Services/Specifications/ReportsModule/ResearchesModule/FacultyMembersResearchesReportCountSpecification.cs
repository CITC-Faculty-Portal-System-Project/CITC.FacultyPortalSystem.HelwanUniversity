using Shared.SpecificationParameters.ReportsAndDashboard.ResearchesModule;
using System.Linq.Expressions;

namespace Services.Specifications.ReportsModule.ResearchesModule
{
    public class FacultyMembersResearchesReportCountSpecification : BaseSpecifications<FacultyMember, Guid>
    {
        public FacultyMembersResearchesReportCountSpecification(FacultyMembersResearchesSpecificationParameters parameters)
            : base(fd =>
                !fd.IsDeleted
                && (
                    parameters.FacultyIds != null && parameters.FacultyIds.Any()
                        && parameters.FacultyIds.Contains(fd.PersonalData!.FacultyId!.Value)
                    ||
                    parameters.DepartmentIds != null && parameters.DepartmentIds.Any()
                        && parameters.DepartmentIds.Contains(fd.PersonalData!.DeptId)
                )
                && (parameters.PubYear == null || !parameters.PubYear.Any()
                    || fd.ResearchContributions!.Any(rc => parameters.PubYear.Contains(rc.Research!.PubYear!.Value)))
                 
            && (string.IsNullOrWhiteSpace(parameters.Search) ||
                    fd.PersonalData!.NameAr.Contains(parameters.Search) ||
                    fd.PersonalData!.NameEn.Contains(parameters.Search)))


        {
        }
    }
}
