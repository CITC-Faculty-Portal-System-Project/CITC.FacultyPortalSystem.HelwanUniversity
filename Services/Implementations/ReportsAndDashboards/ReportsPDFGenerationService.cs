using Domain.Entities.UniversityFacultiesAndDepartments;
using QuestPDF.Fluent;
using Services.Abstraction.Contracts.ReportsAndDashboard;
using Services.Implementations.ReportsAndDashboards.Documents;
using Services.Implementations.ReportsAndDashboards.Helpers;
using Services.ReportsAndDashboard.Helpers;
using Services.Specifications.LookUpItems;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.ConferencesAndSeminarsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.FacultyMembersDataModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.ResearchesModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.WritingsModule;

namespace Services.Implementations.ReportsAndDashboards
{
    public class ReportsPDFGenerationService(IUnitOfWork _unitOfWork  , IDashboardService _dashboardService ,
     IReportsDataService _reportsDataService , FacultyDepartmentResolver _facultyDepartmentResolver) : IReportsPDFGenerationService
    {
        public async Task<byte[]> GenerateAdminDashboardReportAsync(string? notes)
        {
            var data = await GetRequiredDataForDashboardReportsHelpers.PrepareGeneralSystemReportDataAsync(_dashboardService);

            var pdfBytes = new GeneralSystemReportPdfDocument(data , notes).GeneratePdf();

            return pdfBytes;
        }

        public async Task<byte[]> GenerateConferencesAndSeminarsReportAsync(ConferencesAndSeminarsReportSpecificationParameters parameters, string? notes)
        {
            var data = await _reportsDataService.GetConferencesAndSeminarsReportAsync(parameters);

            var (faculties, departments) =
         await _facultyDepartmentResolver
             .ResolveFacultiesAndDepartmentsAsync(
                 parameters.FacultyIds,
                     parameters.DepartmentIds);

            var pdfBytes = new SeminarsAndConferencesReportPdfDocument(faculties, departments, data.Data.ToList(), notes).GeneratePdf();
            return pdfBytes;
        }

        public async Task<byte[]> GenerateFacultyMembersReportAsync(FacultyMembersDataReportSpecificatonParameters parameters, string? notes)
        {
            var data = await _reportsDataService.GetFacultyMembersDataReportAsync(parameters);

            var (faculties, departments) =
            await _facultyDepartmentResolver
                .ResolveFacultiesAndDepartmentsAsync(
                    parameters.FacultyIds,
                        parameters.DepartmentIds);

            var pdfBytes = new FacultyMembersDataReportPdfDocument(faculties, departments, data.Data.ToList(), notes).GeneratePdf();
            return pdfBytes;
        }

        public async Task<byte[]> GenerateFacultyMembersResearchesReportAsync(FacultyMembersResearchesSpecificationParameters parameters, string? notes)
        {
            var data = await _reportsDataService.GetFacultyMembersResearchesReportAsync(parameters);

            var (faculties, departments) =
            await _facultyDepartmentResolver
                .ResolveFacultiesAndDepartmentsAsync(
                    parameters.FacultyIds,
                        parameters.DepartmentIds);

            var pdfBytes = new ResearchesPerFacultyMemberReportPdfDocument(faculties, departments, data.Data.ToList(), parameters.PubYear?.ToList() ?? new List<int>(), notes).GeneratePdf();
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

        public async Task<byte[]> GenerateResearchesPerYearReportAsync(ResearchesPerYearReportSpecificationParameters parameters, string? notes)
        {
            var data = await _reportsDataService.GetResearchesPeryearReportAsync(parameters);

            var (faculties, departments) =
            await _facultyDepartmentResolver
                .ResolveFacultiesAndDepartmentsAsync(
                    parameters.FacultyIds,
                        parameters.DepartmentIds);

            var pdfBytes = new ResearchesPerYearReportPdfDocument(faculties, departments, data.Data.ToList(), parameters.PubYears?.ToList() ?? new List<int>(), notes).GeneratePdf();
            return pdfBytes;
        }

        public async Task<byte[]> GenerateWritingsReportAsync(WritingsReportSpecificationParameters parameters, string? notes)
        {
            var data = await _reportsDataService.GetWritingsReportAsync(parameters);

            var (faculties, departments) =
            await _facultyDepartmentResolver
                .ResolveFacultiesAndDepartmentsAsync(
                    parameters.FacultyIds,
                        parameters.DepartmentIds);

            var pdfBytes = new WritingsReportPdfDocument(faculties, departments, data.Data.ToList(), notes).GeneratePdf();
            return pdfBytes;
        }
    }
}
