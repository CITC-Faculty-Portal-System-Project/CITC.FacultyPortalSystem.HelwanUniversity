using Domain.Entities.AcademicDataModule.ResearchesModule;
using Shared.Dtos.ReportsAndDashboard.ResearchesModule;
using Shared.Enums.ReportsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.ResearchesModule;

namespace Services.Specifications.ReportsModule.Tables.ResearchesModule
{
    public class ResearchesPerYearReportSpecification : AggregationSpecification<Research, ResearchesPerYearReportResponseDTO>
    {
       public ResearchesPerYearReportSpecification(ResearchesPerYearReportSpecificationParameters parameters)
        {

            Domain.Enums.PublicationType? mappedPublicationType = null;
            if (parameters.PublicationType.HasValue)
            {
                mappedPublicationType = Enum.Parse<Domain.Enums.PublicationType>(
                    parameters.PublicationType.Value.ToString(),
                    ignoreCase: true);
            }

            SetCriteria(r => !r.IsDeleted
    && (
        (
            (parameters.FacultyIds == null || !parameters.FacultyIds.Any()) &&
            (parameters.DepartmentIds == null || !parameters.DepartmentIds.Any())
        )

        ||

        (
            parameters.FacultyIds != null &&
            parameters.FacultyIds.Any() &&
            r.Contributions != null &&
            r.Contributions.Any(c =>
                c.Contributor != null &&
                c.Contributor.PersonalData != null &&
                c.Contributor.PersonalData.FacultyId.HasValue &&
                parameters.FacultyIds.Contains(
                    c.Contributor.PersonalData.FacultyId.Value
                )
            )
        )

        ||

        (
            parameters.DepartmentIds != null &&
            parameters.DepartmentIds.Any() &&
            r.Contributions != null &&
            r.Contributions.Any(c =>
                c.Contributor != null &&
                c.Contributor.PersonalData != null &&
                parameters.DepartmentIds.Contains(
                    c.Contributor.PersonalData.DeptId
                )
            )
        )
    )

    &&

    (
        parameters.PubYears == null ||
        !parameters.PubYears.Any() ||
        (
            r.PubYear.HasValue &&
            parameters.PubYears.Contains(r.PubYear.Value)
        )
    )

    &&

    (
        !mappedPublicationType.HasValue ||
        r.PublicationType == mappedPublicationType.Value
    )

    &&

    (
        string.IsNullOrWhiteSpace(parameters.Search) ||
        r.Title.Contains(parameters.Search)
    ));

            switch (parameters.Sort)
            {
                case ResearchesPerYearReportSortingOptions.PubYearASC:
                    AddOrderBy(r => r.PubYear!);
                    break;
                case ResearchesPerYearReportSortingOptions.PubYearDESC:
                    AddOrderByDescending(r => r.PubYear!);
                    break;
            }

            applyPagination(parameters.PageSize, parameters.PageIndex);

        }

        public override IQueryable<ResearchesPerYearReportResponseDTO>
            Apply(IQueryable<Research> query)
        {
            return query
                .Where(Criteria!)
                .Select(r => new ResearchesPerYearReportResponseDTO
                {
                   ResearchTitle = r.Title,
                    PublicationType = Enum.Parse<Shared.Enums.ResearchesModule.PublicationType>(
                            r.PublicationType.ToString(), true),
                   
                    PubYear = r.PubYear
                });
        }
    }
}
