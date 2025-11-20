using Domain.Entities.FacultyMemberDataModule;

namespace Domain.Entities.ProjectsAndCommitteesModule
{
    public class ReviewingArticles : BaseEntity<int>
    {
        public string TitleOfArticle { get; set; } = string.Empty;
        public string Authority { get; set; } = string.Empty;
        public DateOnly ReviewingDate {  get; set; }
        public string? Description { get; set; }

        #region Relation With FacultyMember
        public Guid FacultyMemberId { get; set; }
        #endregion

        #region Navigation Properties
        public FacultyMember? FacultyMember { get; set; }
        #endregion
    }
}
