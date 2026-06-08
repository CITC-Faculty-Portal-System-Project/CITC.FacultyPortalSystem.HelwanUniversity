using Domain.Entities.AcademicDataModule.ResearchesModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.CVModule;
using System.Linq.Expressions;

namespace Services.Specifications.ReportsModule.CVModule
{
    public class CVReportCountSpecification : BaseSpecifications<FacultyMember, Guid>
    {
        public CVReportCountSpecification
            (CVTableReportSpecificationParameters parameters) 
            : base(fd =>
                !fd.IsDeleted
                && fd.PersonalData != null
                && fd.CVPreferences != null
                && fd.CVPreferences.Any()
                && (
                    parameters.FacultyIds == null || !parameters.FacultyIds.Any()
                    ||
                    
                        fd.PersonalData.FacultyId.HasValue
                        && parameters.FacultyIds.Contains(fd.PersonalData.FacultyId.Value)
                    
                )
                && (
                    string.IsNullOrWhiteSpace(parameters.Search)
                    || fd.PersonalData.Faculty.NameAR.Contains(parameters.Search)
                    || fd.PersonalData.Faculty.NameEN.Contains(parameters.Search)
                )
            )
        {
        }
    }
}
