using System.Security.Principal;

namespace Shared.Dtos.ReportsAndDashboard.ReviewingArticlesModule
{
    public record ReviewingArticlesReportResponseDTO
    {
        public string FacultyMemberName { get; set; } = string.Empty;
        public int NoOfArticles { get; set; }

    }
}
