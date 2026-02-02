using Domain.Entities.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.Enums.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.ProjectsAndCommitteesModule;

namespace Services.Specifications.ProjectsAndCommitteesModule
{
    internal class ReviewingArticlesSpecifications : BaseSpecifications<ReviewingArticles, int>
    {
        public ReviewingArticlesSpecifications(ReviewingArticlesSpecificationsParameters parameters, string facultyMemberId) 
            : base(ra =>
                  (!ra.IsDeleted &&
                    ra.FacultyMember!.Email == facultyMemberId) &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   ra.TitleOfArticle.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase) ||
                   ra.Authority.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase))
            )
        {

            switch (parameters.Sort)
            {
                case ReviewingArticlesSortingOptions.DateAsc:
                    AddOrderBy(caa => caa.ReviewingDate);
                    break;
                case ReviewingArticlesSortingOptions.DateDesc:
                    AddOrderByDescending(caa => caa.ReviewingDate);
                    break;
                case ReviewingArticlesSortingOptions.NameAsc:
                    AddOrderBy(caa => caa.TitleOfArticle);
                    break;
                case ReviewingArticlesSortingOptions.NameDesc:
                    AddOrderByDescending(caa => caa.TitleOfArticle);
                    break;
                default:
                    break;
            }
            applyPagination(parameters.PageSize, parameters.PageIndex);

        }

        public ReviewingArticlesSpecifications(int id) : base(ra => !ra.IsDeleted && ra.Id == id)
        {

        }
    }
}
