using Domain.Entities.ProjectsAndCommitteesModule;
using Shared.Enums.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.ProjectsAndCommitteesModule;

namespace Services.Specifications.ProjectsAndCommitteesModule
{
    internal class ReviewingArticlesSpecifications : BaseSpecifications<ReviewingArticles, int>
    {
        public ReviewingArticlesSpecifications(ReviewingArticlesSpecificationsParameters parameters) 
            : base(ra =>
                  (!ra.IsDeleted &&
                    ra.FacultyMember!.Email == parameters.FacultyMemberEmail) &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   ra.TitleOfArticle.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase) ||
                   ra.Authority.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase))
            )
        {

            switch (parameters.Sort)
            {
                case ReviewingArticlesSortOptions.DateAsc:
                    AddOrderBy(caa => caa.ReviewingDate);
                    break;
                case ReviewingArticlesSortOptions.DateDesc:
                    AddOrderByDescending(caa => caa.ReviewingDate);
                    break;
                case ReviewingArticlesSortOptions.NameAsc:
                    AddOrderBy(caa => caa.TitleOfArticle);
                    break;
                case ReviewingArticlesSortOptions.NameDesc:
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
