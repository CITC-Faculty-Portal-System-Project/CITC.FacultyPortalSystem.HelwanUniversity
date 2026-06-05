using Domain.Entities.AcademicDataModule.ResearchesModule;
using Domain.Entities.UniversityFacultiesAndDepartments;
using QuestPDF.Fluent;
using Services.Abstraction.Contracts.ReportsAndDashboard;
using Services.Implementations.ReportsAndDashboards.Documents;
using Services.Implementations.ReportsAndDashboards.Helpers;
using Services.ReportsAndDashboard.Helpers;
using Services.Specifications.ReportsModule.Tables.ConferencesAndSeminarsModule;
using Services.Specifications.ReportsModule.Tables.FacultyMembersDataModule;
using Services.Specifications.ReportsModule.Tables.ResearchesModule;
using Services.Specifications.ReportsModule.Tables.WritingsModule;
using Shared.Enums.ReportsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.PDF.ConferencesAndSeminarsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.PDF.FacultyMembersDataModule;
using Shared.SpecificationParameters.ReportsAndDashboard.PDF.ResearchesModule;
using Shared.SpecificationParameters.ReportsAndDashboard.PDF.WritingsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.WritingsModule;

namespace Services.Implementations.ReportsAndDashboards
{
    public class ReportsPDFGenerationService(IUnitOfWork _unitOfWork  , IDashboardService _dashboardService ,FacultyDepartmentResolver _facultyDepartmentResolver) : IReportsPDFGenerationService
    {
        public async Task<byte[]> GenerateAdminDashboardReportAsync(string? notes)
        {
            var data = await GetRequiredDataForDashboardReportsHelpers.PrepareGeneralSystemReportDataAsync(_dashboardService);

            var pdfBytes = new GeneralSystemReportPdfDocument(data , notes).GeneratePdf();

            return pdfBytes;
        }

        public async Task<byte[]> GenerateConferencesAndSeminarsReportAsync(ConferencesAndSeminarsReportPdfSpecificationParameters parameters, string? notes)
        {
            var repo = _unitOfWork.GetRepository<FacultyMember, int>();

            var data = await repo.ExecuteAggregationAsync(new ConferencesAndSeminarsReportSpecification(parameters, ReportMode.PDF));

            var (faculties, departments) =
         await _facultyDepartmentResolver
             .ResolveFacultiesAndDepartmentsAsync(
                 parameters.FacultyIds,
                     parameters.DepartmentIds);

            var pdfBytes = new SeminarsAndConferencesReportPdfDocument(faculties, departments, data.ToList(), notes).GeneratePdf();
            return pdfBytes;
        }

        public async Task<byte[]> GenerateFacultyMembersReportAsync(FacultyMembersDataReportPdfSpecificationParameters parameters, string? notes)
        {
            var repo = _unitOfWork.GetRepository<FacultyMember, int>();

            var data = await repo.ExecuteAggregationAsync(new FacultyMembersDataReportSpecification(parameters, ReportMode.PDF));

            var (faculties, departments) =
            await _facultyDepartmentResolver
                .ResolveFacultiesAndDepartmentsAsync(
                    parameters.FacultyIds,
                        parameters.DepartmentIds);

            var pdfBytes = new FacultyMembersDataReportPdfDocument(faculties, departments, data.ToList(), notes).GeneratePdf();
            return pdfBytes;
        }

        public async Task<byte[]> GenerateFacultyMembersResearchesReportAsync(FacultyMembersResearchesPdfSpecificationParameters parameters, string? notes)
        {
            var repo = _unitOfWork.GetRepository<FacultyMember, int>();

            var data = await repo.ExecuteAggregationAsync(new FacultyMembersResearchesReportSpecification(parameters, ReportMode.PDF));

            var (faculties, departments) =
            await _facultyDepartmentResolver
                .ResolveFacultiesAndDepartmentsAsync(
                    parameters.FacultyIds,
                        parameters.DepartmentIds);

            var pdfBytes = new ResearchesPerFacultyMemberReportPdfDocument(faculties, departments, data.ToList(), parameters.PubYear?.ToList() ?? new List<int>(), notes).GeneratePdf();
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

        public async Task<byte[]> GenerateResearchesPerYearReportAsync(ResearchesPerYearPdfReportSpecificationParameters parameters, string? notes)
        {
            var repo = _unitOfWork.GetRepository<Research, int>();

            var data = await repo.ExecuteAggregationAsync(new ResearchesPerYearReportSpecification(parameters, ReportMode.PDF));

            var (faculties, departments) =
            await _facultyDepartmentResolver
                .ResolveFacultiesAndDepartmentsAsync(
                    parameters.FacultyIds,
                        parameters.DepartmentIds);

            var pdfBytes = new ResearchesPerYearReportPdfDocument(faculties, departments , data.ToList(), parameters.PubYears?.ToList() ?? new List<int>(), notes).GeneratePdf();
            return pdfBytes;
        }

        public async Task<byte[]> GenerateWritingsReportAsync(WritingsReportPdfSpecificationParameters parameters, string? notes)
        {
            var repo = _unitOfWork.GetRepository<FacultyMember, int>();

            var data = await repo.ExecuteAggregationAsync(new WritingsReportSpecifications(parameters, ReportMode.PDF));

            var (faculties, departments) =
            await _facultyDepartmentResolver
                .ResolveFacultiesAndDepartmentsAsync(
                    parameters.FacultyIds,
                        parameters.DepartmentIds);

            var pdfBytes = new WritingsReportPdfDocument(faculties, departments, data.ToList(), notes).GeneratePdf();
            return pdfBytes;
        }
    }
}
