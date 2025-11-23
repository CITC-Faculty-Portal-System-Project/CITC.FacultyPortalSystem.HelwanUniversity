namespace Shared.Dtos.ProjectsAndCommitteesModule
{
    public record ReviewingArticlesDto
    {
        public string TitleOfArticle { get; set; } = string.Empty;
        public string Authority { get; set; } = string.Empty;
        public DateOnly ReviewingDate { get; set; }
        public string? Description { get; set; }
    }
}
