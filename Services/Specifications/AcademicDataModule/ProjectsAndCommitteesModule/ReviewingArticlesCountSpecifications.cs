using Domain.Entities.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Services.Specifications.AcademicDataModule.ProjectsAndCommitteesModule
{
    internal class ReviewingArticlesCountSpecifications : BaseSpecifications<ReviewingArticles, int>
    {
        public ReviewingArticlesCountSpecifications(ReviewingArticlesSpecificationsParameters parameters, string facultyMemberId)
            : base(ra =>
                  !ra.IsDeleted &&
                    ra.FacultyMember!.Email == facultyMemberId &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   ra.TitleOfArticle.Contains(parameters.Search) ||
                   ra.Authority.Contains(parameters.Search))
            )
        {

        }
    }
}
