namespace Shared.Dtos.CVGenerationModule.ProjectsAndCommittees
{
    public record CVReviewingArticlesDTO
    {
        public int Id { get; set; }
        public string? TitleOfArticle { get; set; } 
        public string? Authority { get; set; }
        public DateOnly? ReviewingDate { get; set; }
    }
}
