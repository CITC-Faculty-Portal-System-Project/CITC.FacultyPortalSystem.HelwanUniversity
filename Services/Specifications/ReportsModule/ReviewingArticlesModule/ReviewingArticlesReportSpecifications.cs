using Domain.Entities.FacultyMemberDataModule;
using Shared.Dtos.ReportsAndDashboard.ReviewingArticlesModule;
using Shared.Enums.ReportsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Base.ReviewingArticlesModule;

namespace Services.Specifications.ReportsModule.ReviewingArticlesModule
{
    public class ReviewingArticlesReportSpecifications
        : AggregationSpecification<FacultyMember, ReviewingArticlesReportResponseDTO>
    {
        public ReviewingArticlesReportSpecifications(
            BaseReviewingArticlesReportSpecificationParameters parameters,
            ReportMode mode,
            int pageIndex = 1,
            int pageSize = 9,
            string? search = null)
        {
            SetCriteria(fd =>
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
                    string.IsNullOrWhiteSpace(search)
                    || fd.PersonalData!.NameAr.Contains(search)
                    || fd.PersonalData!.NameEn.Contains(search)
                )
            );

            switch (parameters.Sort)
            {
                case ReviewingArticlesReportSortingOptions.FacultyMemberNameAsc:
                    AddOrderBy(fd => fd.PersonalData!.NameAr);
                    break;

                case ReviewingArticlesReportSortingOptions.FacultyMemberNameDesc:
                    AddOrderByDescending(fd => fd.PersonalData!.NameAr);
                    break;

                case ReviewingArticlesReportSortingOptions.NoOfArticlesAsc:
                    AddOrderBy(fd => fd.ReviewingArticles!.Count(ra => !ra.IsDeleted));
                    break;

                case ReviewingArticlesReportSortingOptions.NoOfArticlesDesc:
                    AddOrderByDescending(fd => fd.ReviewingArticles!.Count(ra => !ra.IsDeleted));
                    break;

                default:
                    AddOrderBy(fd => fd.PersonalData!.NameAr);
                    break;
            }

            if (mode == ReportMode.Table)
                applyPagination(pageSize, pageIndex);

            AddIncludes(fd => fd.PersonalData!);
            AddIncludes(fd => fd.PersonalData!.Title);
            AddIncludes(fd => fd.ReviewingArticles!);
        }

        public override IQueryable<ReviewingArticlesReportResponseDTO> Apply(IQueryable<FacultyMember> query)
        {
            return query
                .Where(Criteria!)
                .Select(fd => new ReviewingArticlesReportResponseDTO
                {
                    FacultyMemberName =
                        (fd.PersonalData!.Title!.ValueAr ?? "")
                        + (fd.PersonalData.NameAr ?? ""),

                    NoOfArticles = fd.ReviewingArticles!
                        .Count(ra => !ra.IsDeleted)
                });
        }
    }
}