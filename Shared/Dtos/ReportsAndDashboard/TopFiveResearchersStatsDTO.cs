namespace Shared.Dtos.ReportsAndDashboard
{
    public record TopFiveResearchersStatsDTO
    {
        public string ResearcherJobTitleAR { get; set; } = string.Empty;
        public string ResearcherJobTitleEN { get; set; } = string.Empty;
        public string ResearcherNameAR { get; set; } = string.Empty;
        public string ResearcherNameEN { get; set; } = string.Empty;
        public string ResearcherFacultyAR { get; set; } = string.Empty;
        public string ResearcherFacultyEN { get; set; } = string.Empty;
        public int TotalResearchesNo { get; set; }
        public double Score { get; set; }
    }
}
