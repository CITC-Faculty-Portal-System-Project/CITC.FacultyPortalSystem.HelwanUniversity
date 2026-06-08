using Domain.Entities.AcademicDataModule.ResearchesModule;
using Services.Abstraction.Contracts.ReportsAndDashboard;
using Services.Specifications.ReportsModule.ConferencesAndSeminarsModule;
using Services.Specifications.ReportsModule.CVModule;
using Services.Specifications.ReportsModule.ExperiencesModule;
using Services.Specifications.ReportsModule.FacultyMembersDataModule;
using Services.Specifications.ReportsModule.ProjectsAndComiteesModule;
using Services.Specifications.ReportsModule.ResearchesModule;
using Services.Specifications.ReportsModule.ReviewingArticlesModule;
using Services.Specifications.ReportsModule.WritingsAndPatentsModule;
using Services.Specifications.ReportsModule.WritingsModule;
using Shared.Dtos.ReportsAndDashboard.ConferencesAndSeminarsModule;
using Shared.Dtos.ReportsAndDashboard.CVModule;
using Shared.Dtos.ReportsAndDashboard.ExpereincesModule;
using Shared.Dtos.ReportsAndDashboard.FacultyMemberDataModule;
using Shared.Dtos.ReportsAndDashboard.ProjectsAndComiteesModule;
using Shared.Dtos.ReportsAndDashboard.ResearchesModule;
using Shared.Dtos.ReportsAndDashboard.ReviewingArticlesModule;
using Shared.Dtos.ReportsAndDashboard.WrtingsAndPatentsModule;
using Shared.Dtos.ReportsAndDashboard.WrtingsModule;
using Shared.Enums.ReportsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.ConferencesAndSeminarsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.CVModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.ExperincesModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.FacultyMembersDataModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.ProjectsAndComiteesModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.ResearchesModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.ReviewingArticlesModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.WritingsAndPatentsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.WritingsModule;

namespace Services.Implementations.ReportsAndDashboards
{
    public class ReportsDataService(IUnitOfWork _unitOfWork) : IReportsDataService
    {
        public async Task<PaginatedResult<ConferenceAndSeminarsReportResponseDTO>> GetConferencesAndSeminarsReportAsync(ConferencesAndSeminarsReportSpecificationParameters parameters)
        {
            var repo = _unitOfWork.GetRepository<FacultyMember, Guid>();

            var data = await repo.ExecuteAggregationAsync(new ConferencesAndSeminarsReportSpecification(parameters, ReportMode.Table, parameters.PageIndex, parameters.PageSize, parameters.Search));
            var totalCount = await repo.CountAsync(
                new ConferencesAndSeminarsReportCountSpecification(parameters));

            return new PaginatedResult<ConferenceAndSeminarsReportResponseDTO>(
                parameters.PageIndex, data.Count, totalCount, data);
        }

        public async Task<PaginatedResult<CVReportResponseDTO>> GetCvsReportAsync(CVTableReportSpecificationParameters parameters)
        {
            var repo = _unitOfWork.GetRepository<FacultyMember, Guid>();

            var data = await repo.ExecuteAggregationAsync(new CVReportSpecification(parameters, ReportMode.Table, parameters.PageIndex, parameters.PageSize, parameters.Search));
            var totalCount = await repo.CountAsync(
                new CVReportCountSpecification(parameters));

            return new PaginatedResult<CVReportResponseDTO>(
                parameters.PageIndex, data.Count, totalCount, data);
        }

        public async Task<PaginatedResult<ExpereinceReportResponseDTO>> GetExperiencesReportAsync(ExperinceReportTableSpecificationParameters parameters)
        {
            var repo = _unitOfWork.GetRepository<FacultyMember, Guid>();

            var data = await repo.ExecuteAggregationAsync(new ExperienceReportSpecification(parameters, ReportMode.Table, parameters.PageIndex, parameters.PageSize, parameters.Search));
            var totalCount = await repo.CountAsync(
                new ExperienceReportCountSpecifications(parameters));

            return new PaginatedResult<ExpereinceReportResponseDTO>(
                parameters.PageIndex, data.Count, totalCount, data);
        }

        public async Task<PaginatedResult<FacultyMembersDataReportResponseDTO>> GetFacultyMembersDataReportAsync(
        FacultyMembersDataReportSpecificatonParameters parameters)
        {
            var repo = _unitOfWork.GetRepository<FacultyMember, Guid>();

            var data = await repo.ExecuteAggregationAsync(new FacultyMembersDataReportSpecification(parameters, ReportMode.Table, parameters.PageIndex, parameters.PageSize, parameters.Search));        
            var totalCount = await repo.CountAsync(                      
                new FacultyMembersDataReportCountSpecifications(parameters));

            return new PaginatedResult<FacultyMembersDataReportResponseDTO>(
                parameters.PageIndex, data.Count, totalCount, data);
        }

        public async Task<PaginatedResult<FacultyMembersResearchesReportResponseDTO>> GetFacultyMembersResearchesReportAsync(FacultyMembersResearchesSpecificationParameters parameters)
        {
            var repo = _unitOfWork.GetRepository<FacultyMember, Guid>();

            var data = await repo.ExecuteAggregationAsync(new FacultyMembersResearchesReportSpecification(parameters, ReportMode.Table, parameters.PageIndex, parameters.PageSize, parameters.Search));        
            var totalCount = await repo.CountAsync(                      
                new FacultyMembersResearchesReportCountSpecification(parameters));

            return new PaginatedResult<FacultyMembersResearchesReportResponseDTO>(
                parameters.PageIndex, data.Count, totalCount, data);
        }

        public async Task<PaginatedResult<ParticipationInMagazinesReportResponseDTO>> GetParticipationInMagazinesReportAsync(ParticipationInMagazinesReportTableSpecificationParameters parameters)
        {
            var repo = _unitOfWork.GetRepository<FacultyMember, Guid>();

            var data = await repo.ExecuteAggregationAsync(new ParticipationInMagazinesReportSpecification(parameters, ReportMode.Table, parameters.PageIndex, parameters.PageSize, parameters.Search));
            var totalCount = await repo.CountAsync(
                new ParticipationInMagazinesReportCountSpecification(parameters));

            return new PaginatedResult<ParticipationInMagazinesReportResponseDTO>(
                parameters.PageIndex, data.Count, totalCount, data);
        }

        public async Task<PaginatedResult<PatentsReportReponseDTO>> GetPatentsReportAsync(PatentsReportTableSpecificationParameters parameters)
        {
            var repo = _unitOfWork.GetRepository<FacultyMember, Guid>();

            var data = await repo.ExecuteAggregationAsync(new PatentsReportSpecification(parameters, ReportMode.Table, parameters.PageIndex, parameters.PageSize, parameters.Search));
            var totalCount = await repo.CountAsync(
                new PatentsReportCountSpecification(parameters));

            return new PaginatedResult<PatentsReportReponseDTO>(
                parameters.PageIndex, data.Count, totalCount, data);
        }

        public async Task<PaginatedResult<ProjectsReportResponseDTO>> GetProjectsReportAsync(ProjectsReportTableSpecificationParameters parameters)
        {
            var repo = _unitOfWork.GetRepository<FacultyMember, Guid>();

            var data = await repo.ExecuteAggregationAsync(new ProjectsReportSpecification(parameters, ReportMode.Table, parameters.PageIndex, parameters.PageSize, parameters.Search));
            var totalCount = await repo.CountAsync(
                new ProjectsReportCountSpecification(parameters));

            return new PaginatedResult<ProjectsReportResponseDTO>(
                parameters.PageIndex, data.Count, totalCount, data);
        }

        public async Task<PaginatedResult<ResearchesPerYearReportResponseDTO>> GetResearchesPeryearReportAsync(ResearchesPerYearReportSpecificationParameters parameters)
        {
            var repo = _unitOfWork.GetRepository<Research , int>();

            var data = await repo.ExecuteAggregationAsync(new ResearchesPerYearReportSpecification(parameters , ReportMode.Table , parameters.PageIndex , parameters.PageSize , parameters.Search));
            
            var totalCount = await repo.CountAsync(
                new ResearchesPerYearReportCountSpecification(parameters));
            
            return new PaginatedResult<ResearchesPerYearReportResponseDTO>(
                parameters.PageIndex, data.Count, totalCount, data);
        }

        public async Task<PaginatedResult<ReviewingArticlesReportResponseDTO>> GetReviewingArticlesReportAsync(ReviewingArticlesReportTableSpecificationParameters parameters)
        {
            var repo = _unitOfWork.GetRepository<FacultyMember, Guid>();

            var data = await repo.ExecuteAggregationAsync(new ReviewingArticlesReportSpecifications(parameters, ReportMode.Table, parameters.PageIndex, parameters.PageSize, parameters.Search));
            var totalCount = await repo.CountAsync(
                new ReviewingArticlesReportCountSpecification(parameters));

            return new PaginatedResult<ReviewingArticlesReportResponseDTO>(
                parameters.PageIndex, data.Count, totalCount, data);
        }

        public async Task<PaginatedResult<WritingsReportResponseDTO>> GetWritingsReportAsync(WritingsReportSpecificationParameters parameters)
        {
            var repo = _unitOfWork.GetRepository<FacultyMember, Guid>();

            var data = await repo.ExecuteAggregationAsync(new WritingsReportSpecifications(parameters, ReportMode.Table, parameters.PageIndex, parameters.PageSize, parameters.Search));
            var totalCount = await repo.CountAsync(
                new WrtiningsReportCountSpecification(parameters));

            return new PaginatedResult<WritingsReportResponseDTO>(
                parameters.PageIndex, data.Count, totalCount, data);
        }
    }
}
