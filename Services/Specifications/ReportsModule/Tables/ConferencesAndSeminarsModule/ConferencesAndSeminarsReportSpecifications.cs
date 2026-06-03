using Domain.Entities.FacultyMemberDataModule;
using Shared.Dtos.ReportsAndDashboard.ConferencesAndSeminarsModule;
using Shared.Enums.ReportsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.ConferencesAndSeminarsModule;

namespace Services.Specifications.ReportsModule.Tables.ConferencesAndSeminarsModule
{
    public class ConferencesAndSeminarsReportSpecification
        : AggregationSpecification<FacultyMember, ConferenceAndSeminarsReportResponseDTO>
    {
        public ConferencesAndSeminarsReportSpecification(
            ConferencesAndSeminarsReportSpecificationParameters parameters)
        {
            SetCriteria(fd =>
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
             );

            switch (parameters.Sort)
            {
                case ConferencesAndSeminarsSortingOptions.NoOfConferencesOrSeminarsAsc:
                    AddOrderBy(fd =>
                        fd.ConferencesAndSeminars!
                            .Count(c =>
                                !c.IsDeleted
                                && (parameters.Type == null || c.Type == (Domain.Enums.ConferenceOrSeminar)parameters.Type)));
                    break;

                case ConferencesAndSeminarsSortingOptions.NoOfConferencesOrSeminarsDesc:
                    AddOrderByDescending(fd =>
                        fd.ConferencesAndSeminars!
                            .Count(c =>
                                !c.IsDeleted
                                && (parameters.Type == null || c.Type == (Domain.Enums.ConferenceOrSeminar)parameters.Type)));
                    break;
            }

            applyPagination(parameters.PageSize, parameters.PageIndex);

            AddIncludes(fd => fd.PersonalData!);
            AddIncludes(fd => fd.PersonalData!.Title);
        }

        public override IQueryable<ConferenceAndSeminarsReportResponseDTO>
            Apply(IQueryable<FacultyMember> query)
        {
            return query
                .Where(Criteria!)
                .Select(fd => new ConferenceAndSeminarsReportResponseDTO
                {
                    FacultyMemberName =
                        (fd.PersonalData!.Title!.ValueAr ?? "") + ". "
                        + (fd.PersonalData.NameAr ?? ""),

                    NoOfConferencesOrSeminars =
                        fd.ConferencesAndSeminars!
                            .Count()
                });
        }
    }
}