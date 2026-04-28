namespace Shared.Dtos.ReportsAndDashboard
{
    public record FacultyUsersStatisticsDTO
    {
        public string FacultyNameAR { get; set; } = string.Empty;
        public string FacultyNameEN { get; set; } = string.Empty;
        public int TotalNumberOfUsers { get; set; }
    }
}
