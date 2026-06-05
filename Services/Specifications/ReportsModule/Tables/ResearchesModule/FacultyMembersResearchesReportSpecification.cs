using Domain.Enums;
using Shared.Dtos.ReportsAndDashboard.ResearchesModule;
using Shared.Enums.ReportsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Base.ResearchesModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.ResearchesModule;

namespace Services.Specifications.ReportsModule.Tables.ResearchesModule
{
    public class FacultyMembersResearchesReportSpecification
        : AggregationSpecification<FacultyMember, FacultyMembersResearchesReportResponseDTO>
    {


        private readonly bool _isPaginated;
        private readonly int _pageIndex;
        private readonly int _pageSize;
        private readonly List<int> pubYears = new();
        public FacultyMembersResearchesReportSpecification(
            BaseFacultyMembersResearchesSpecificationParameters parameters
            , ReportMode mode, int pageIndex = 1, int pageSize = 9
                  , string? search = null)
        {
            _isPaginated = mode == ReportMode.Table;
            _pageIndex = pageIndex;
            _pageSize = pageSize;

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
                   && (parameters.PubYear == null || !parameters.PubYear.Any()
                       || fd.ResearchContributions!.Any(rc => !rc.IsDeleted && !rc.Research!.IsDeleted && parameters.PubYear.Contains(rc.Research!.PubYear!.Value)))
                   && (string.IsNullOrWhiteSpace(search)
                       || fd.PersonalData!.NameAr.Contains(search)
                       || fd.PersonalData!.NameEn.Contains(search)));


            if (parameters.PubYear != null)
            {
                pubYears.AddRange(parameters.PubYear);
            }

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

            if (mode == ReportMode.Table)
                applyPagination(pageSize, pageIndex);

            AddIncludes(fd => fd.PersonalData!);
        }

        public override IQueryable<FacultyMembersResearchesReportResponseDTO>
            Apply(IQueryable<FacultyMember> query)
        {
            var result = query
                .Where(Criteria!)
                .Select(fd => new FacultyMembersResearchesReportResponseDTO
                {
                    FacultyMemberName = (fd.PersonalData!.Title!.ValueAr ?? "") + ". " + (fd.PersonalData.NameAr ?? ""),
                    NoOfInternationalResearches = fd.ResearchContributions!
                .Count(r =>
                    !r.IsDeleted
                    && !r.Research!.IsDeleted
                    && r.Research.PublicationType == PublicationType.International
                    && (
                        pubYears == null
                        || !pubYears.Any()
                        || pubYears.Contains(r.Research.PubYear!.Value)
                    )),

                                NoOfLocalResearches = fd.ResearchContributions!
                .Count(r =>
                    !r.IsDeleted
                    && !r.Research!.IsDeleted
                    && r.Research.PublicationType == PublicationType.Local
                    && (
                        pubYears == null
                        || !pubYears.Any()
                        || pubYears.Contains(r.Research.PubYear!.Value)
                    )),
                            });

            if (_isPaginated)
            {
                result = result
                    .Skip((_pageIndex - 1) * _pageSize)
                    .Take(_pageSize);
            }

            return result;
        }
    }
}
