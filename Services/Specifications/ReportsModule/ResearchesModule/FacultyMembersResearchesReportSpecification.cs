using Domain.Enums;
using Shared.Dtos.ReportsAndDashboard.ResearchesModule;
using Shared.Enums.ReportsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.ResearchesModule;

namespace Services.Specifications.ReportsModule.ResearchesModule
{
    public class FacultyMembersResearchesReportSpecification
        : AggregationSpecification<FacultyMember, FacultyMembersResearchesReportResponseDTO>
    {
        public FacultyMembersResearchesReportSpecification(
            FacultyMembersResearchesSpecificationParameters parameters)
        {
            SetCriteria(fd =>
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
                    fd.PersonalData!.NameEn.Contains(parameters.Search)));
    

            switch (parameters.Sort)
            {
                case FacultyMembersResearchesSortingOptions.NoOfInternationalResearchesASC:
                    AddOrderBy(fd => fd.ResearchContributions!
                        .Count(r => r.Research!.PublicationType == PublicationType.International));
                    break;
                case FacultyMembersResearchesSortingOptions.NoOfInternationalResearchesDESC:
                    AddOrderByDescending(fd => fd.ResearchContributions!
                        .Count(r => r.Research!.PublicationType == PublicationType.International));
                    break;
                case FacultyMembersResearchesSortingOptions.NoOfLocalResearchesASC:
                    AddOrderBy(fd => fd.ResearchContributions!
                        .Count(r => r.Research!.PublicationType == PublicationType.Local));
                    break;
                case FacultyMembersResearchesSortingOptions.NoOfLocalResearchesDESC:
                    AddOrderByDescending(fd => fd.ResearchContributions!
                        .Count(r => r.Research!.PublicationType == PublicationType.Local));
                    break;
            }

            applyPagination(parameters.PageSize, parameters.PageIndex);

            AddIncludes(fd => fd.PersonalData!);
        }

        public override IQueryable<FacultyMembersResearchesReportResponseDTO>
            Apply(IQueryable<FacultyMember> query)
        {
            return query
                .Where(Criteria!)
                .Skip(Skip)
                .Take(Take)
                .Select(fd => new FacultyMembersResearchesReportResponseDTO
                {
                    FacultyMemberName = (fd.PersonalData!.Title!.ValueAr ?? "") + ". " + (fd.PersonalData.NameAr ?? ""),
                    NoOfInternationalResearches = fd.ResearchContributions!
                        .Count(r => !r.IsDeleted && !r.Research!.IsDeleted
                                 && r.Research.PublicationType == PublicationType.International),
                    NoOfLocalResearches = fd.ResearchContributions!
                        .Count(r => !r.IsDeleted && !r.Research!.IsDeleted
                                 && r.Research.PublicationType == PublicationType.Local),
                   });
        }
    }
}
