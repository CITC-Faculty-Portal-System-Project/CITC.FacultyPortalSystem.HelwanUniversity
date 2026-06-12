using Domain.Entities.AcademicDataModule.ResearchesModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.ProjectsAndComiteesModule;
using System.Linq.Expressions;

namespace Services.Specifications.ReportsModule.ProjectsAndComiteesModule
{
    public class ParticipationInMagazinesReportCountSpecification : BaseSpecifications<FacultyMember, Guid>
    {
        public ParticipationInMagazinesReportCountSpecification
            (ParticipationInMagazinesReportTableSpecificationParameters parameters) 
            : base(fd =>
                !fd.IsDeleted

                && (
                    (parameters.FacultyIds != null && parameters.FacultyIds.Any()
                        && parameters.DepartmentIds != null && parameters.DepartmentIds.Any()
                        && (parameters.FacultyIds.Contains(fd.PersonalData!.FacultyId!.Value)
                            || parameters.DepartmentIds.Contains(fd.PersonalData!.DeptId!)))

                    || (parameters.FacultyIds != null && parameters.FacultyIds.Any()
                        && (parameters.DepartmentIds == null || !parameters.DepartmentIds.Any())
                        && parameters.FacultyIds.Contains(fd.PersonalData!.FacultyId!.Value))

                    || ((parameters.FacultyIds == null || !parameters.FacultyIds.Any())
                        && parameters.DepartmentIds != null && parameters.DepartmentIds.Any()
                        && parameters.DepartmentIds.Contains(fd.PersonalData!.DeptId!))

                    || ((parameters.FacultyIds == null || !parameters.FacultyIds.Any())
                        && (parameters.DepartmentIds == null || !parameters.DepartmentIds.Any()))
                )

                && (
                    parameters.TypesOfParticipation == null
                    || !parameters.TypesOfParticipation.Any()
                    || fd.ParticipationInMagazines.Any(p =>
                        parameters.TypesOfParticipation.Contains(p.TypeOfParticipationId))
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
