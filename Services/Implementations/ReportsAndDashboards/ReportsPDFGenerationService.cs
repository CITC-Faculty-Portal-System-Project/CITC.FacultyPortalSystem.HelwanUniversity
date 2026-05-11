using Domain.Entities.UniversityFacultiesAndDepartments;
using QuestPDF.Fluent;
using Services.Abstraction.Contracts.ReportsAndDashboard;
using Services.Implementations.ReportsAndDashboards.Documents;
using Services.ReportsAndDashboard.Helpers;

namespace Services.Implementations.ReportsAndDashboards
{
    public class ReportsPDFGenerationService(IUnitOfWork _unitOfWork  , IDashboardService _dashboardService ,
      IReportsPreviewingService _reportsPreviewingService) : IReportsPDFGenerationService
    {
        public async Task<byte[]> GenerateAdminDashboardReportAsync(string? notes)
        {
            var data = await GetRequiredDataForDashboardReportsHelpers.PrepareGeneralSystemReportDataAsync(_dashboardService);

            var pdfBytes = new GeneralSystemReportPdfDocument(data , notes).GeneratePdf();

            return pdfBytes;
        }

        public async Task<byte[]> GenerateFacultyResearchesReportAsync(int facultyId, string? notes)
        {
            var facultyRepo = _unitOfWork.GetRepository<Faculty, int>();
            var faculty = await facultyRepo.GetByIdAsync(facultyId) ?? throw new NotFoundException("Faculty Not Found");

            var data = await GetRequiredDataForDashboardReportsHelpers.PrepareFacultyResearchReportDataAsync(facultyId, _dashboardService);

            var pdfBytes = new FacultyResearchReportPdfDocument(faculty, data, notes).GeneratePdf();

            return pdfBytes;
        }

        public async Task<byte[]> GenerateResearchDashboardReportAsync(string? notes)
        {
            var data = await GetRequiredDataForDashboardReportsHelpers.PrepareResearchDashboardReportDataAsync(_dashboardService);

            var pdfBytes = new ResearchesReportPdfDocument(data, notes).GeneratePdf();

            return pdfBytes;
        }
    }
}
