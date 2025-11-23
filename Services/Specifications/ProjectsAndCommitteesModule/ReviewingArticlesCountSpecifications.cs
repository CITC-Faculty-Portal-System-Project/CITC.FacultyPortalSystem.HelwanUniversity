using Domain.Entities.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.ProjectsAndCommitteesModule;

namespace Services.Specifications.ProjectsAndCommitteesModule
{
    internal class ReviewingArticlesCountSpecifications : BaseSpecifications<ReviewingArticles, int>
    {
        public ReviewingArticlesCountSpecifications(ReviewingArticlesSpecificationsParameters parameters)
            : base(ra =>
                  (!ra.IsDeleted &&
                    ra.FacultyMember!.Email == parameters.FacultyMemberEmail) &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   ra.TitleOfArticle.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase) ||
                   ra.Authority.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase))
            )
        {

        }
    }
}
