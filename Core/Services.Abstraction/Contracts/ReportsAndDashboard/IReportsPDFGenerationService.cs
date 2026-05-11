namespace Services.Abstraction.Contracts.ReportsAndDashboard
{
    public interface IReportsPDFGenerationService
    {
        public Task<byte[]> GenerateAdminDashboardReportAsync(string? notes);
        public Task<byte[]> GenerateResearchDashboardReportAsync(string? notes);
        public Task<byte[]> GenerateFacultyResearchesReportAsync(int facultyId, string? notes);
    }
}
