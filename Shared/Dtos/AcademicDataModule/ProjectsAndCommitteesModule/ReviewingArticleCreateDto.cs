namespace Shared.Dtos.AcademicDataModule.ProjectsAndCommitteesModule
{
    public record ReviewingArticleCreateDto
    {
        public string TitleOfArticle { get; set; } = string.Empty;
        public string Authority { get; set; } = string.Empty;
        public DateOnly ReviewingDate { get; set; }
        public string? Description { get; set; }

        public Guid FacultyMemberId { get; set; }
    }
}
