namespace Shared.Models.CVGenerationModule.ProjectsAndCommittees
{
    public class ReviewingArticlesVisibility
    {
        public bool ShowReviewingArticles { get; set; } = true;
        public bool ShowReviewingArticlesForPublic { get; set; } = true;
        public bool ShowTitleOfArticle { get; set; } = true;
        public bool ShowTitleOfArticleForPublic { get; set; } = true;
        public bool ShowAuthority { get; set; } = true;
        public bool ShowAuthorityForPublic { get; set; } = true;
        public bool ShowReviewingDate { get; set; } = true;
        public bool ShowReviewingDateForPublic { get; set; } = true;

    }
}
