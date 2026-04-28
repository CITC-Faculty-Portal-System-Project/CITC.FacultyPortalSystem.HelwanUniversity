namespace Shared.Dtos.ReportsAndDashboard
{
    public record AdminDashboardResponseDTO
    {
        public string CurrentUserName { get; set; } = string.Empty;
        public List<string> CurrentUserRoles { get; set; } = new List<string>();
        public int TotalUsersNumber { get; set; }
        public int TotalFacultyMembersNumber { get; set; }
        public int TotalSystemManagersNumber { get; set; }
        public IReadOnlyList<FacultyUsersStatisticsDTO> UsersPerFaculty { get; set; } = new List<FacultyUsersStatisticsDTO>();
        public IReadOnlyList<ResearchesPerFacultyDTO> ResearchesPerFaculty { get; set; } = new List<ResearchesPerFacultyDTO>();
        public IReadOnlyList<ResearchesStatsDTO> ResearchesStats { get; set; } = new List<ResearchesStatsDTO>();
        public IReadOnlyList<ResearchesMonthlyRateDTO> ResearchesMonthlyRate { get; set; } = new List<ResearchesMonthlyRateDTO>();
        public IReadOnlyList<TicketsStatsDTO> TicketsStats { get; set; } = new List<TicketsStatsDTO>();
    }
}
