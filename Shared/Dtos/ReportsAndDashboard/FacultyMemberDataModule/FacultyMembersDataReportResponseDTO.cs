namespace Shared.Dtos.ReportsAndDashboard.FacultyMemberDataModule
{
    public record FacultyMembersDataReportResponseDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Faculty { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int NoOfInternationalResearches { get; set; }
        public int NoOfLocalResearches { get; set; }
        public int NoOfPatents { get; set; }
        public int NoOfAwards { get; set; }

    }
}
