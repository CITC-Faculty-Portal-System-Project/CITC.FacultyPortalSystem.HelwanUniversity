using Domain.Entities.AcademicDataModule.ResearchesModule;
using Services.Abstraction.Contracts.ReportsAndDashboard;
using Services.Specifications.ReportsModule.Tables.ConferencesAndSeminarsModule;
using Services.Specifications.ReportsModule.Tables.FacultyMembersDataModule;
using Services.Specifications.ReportsModule.Tables.ResearchesModule;
using Services.Specifications.ReportsModule.Tables.WritingsModule;
using Shared.Dtos.ReportsAndDashboard.ConferencesAndSeminarsModule;
using Shared.Dtos.ReportsAndDashboard.FacultyMemberDataModule;
using Shared.Dtos.ReportsAndDashboard.ResearchesModule;
using Shared.Dtos.ReportsAndDashboard.WrtingsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.ConferencesAndSeminarsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.FacultyMembersDataModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.ResearchesModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.WritingsModule;

namespace Services.Implementations.ReportsAndDashboards
{
    public class ReportsDataService(IUnitOfWork _unitOfWork) : IReportsDataService
    {
        public async Task<PaginatedResult<ConferenceAndSeminarsReportResponseDTO>> GetConferencesAndSeminarsReportAsync(ConferencesAndSeminarsReportSpecificationParameters parameters)
        {
            var repo = _unitOfWork.GetRepository<FacultyMember, Guid>();

            var data = await repo.ExecuteAggregationAsync(new ConferencesAndSeminarsReportSpecification(parameters));
            var totalCount = await repo.CountAsync(
                new ConferencesAndSeminarsReportCountSpecification(parameters));

            return new PaginatedResult<ConferenceAndSeminarsReportResponseDTO>(
                parameters.PageIndex, data.Count, totalCount, data);
        }

        public async Task<PaginatedResult<FacultyMembersDataReportResponseDTO>> GetFacultyMembersDataReportAsync(
        FacultyMembersDataReportSpecificatonParameters parameters)
        {
            var repo = _unitOfWork.GetRepository<FacultyMember, Guid>();

            var data = await repo.ExecuteAggregationAsync(new FacultyMembersDataReportSpecification(parameters));        
            var totalCount = await repo.CountAsync(                      
                new FacultyMembersDataReportCountSpecifications(parameters));

            return new PaginatedResult<FacultyMembersDataReportResponseDTO>(
                parameters.PageIndex, data.Count, totalCount, data);
        }

        public async Task<PaginatedResult<FacultyMembersResearchesReportResponseDTO>> GetFacultyMembersResearchesReportAsync(FacultyMembersResearchesSpecificationParameters parameters)
        {
            var repo = _unitOfWork.GetRepository<FacultyMember, Guid>();

            var data = await repo.ExecuteAggregationAsync(new FacultyMembersResearchesReportSpecification(parameters));        
            var totalCount = await repo.CountAsync(                      
                new FacultyMembersResearchesReportCountSpecification(parameters));

            return new PaginatedResult<FacultyMembersResearchesReportResponseDTO>(
                parameters.PageIndex, data.Count, totalCount, data);
        }

        public async Task<PaginatedResult<ResearchesPerYearReportResponseDTO>> GetResearchesPeryearReportAsync(ResearchesPerYearReportSpecificationParameters parameters)
        {
            var repo = _unitOfWork.GetRepository<Research , int>();

            var data = await repo.ExecuteAggregationAsync(new ResearchesPerYearReportSpecification(parameters));
            
            var totalCount = await repo.CountAsync(
                new ResearchesPerYearReportCountSpecification(parameters));
            
            return new PaginatedResult<ResearchesPerYearReportResponseDTO>(
                parameters.PageIndex, data.Count, totalCount, data);
        }

        public async Task<PaginatedResult<WritingsReportResponseDTO>> GetWritingsReportAsync(WritingsReportSpecificationParameters parameters)
        {
            var repo = _unitOfWork.GetRepository<FacultyMember, Guid>();

            var data = await repo.ExecuteAggregationAsync(new WritingsReportSpecifications(parameters));
            var totalCount = await repo.CountAsync(
                new WrtiningsReportCountSpecification(parameters));

            return new PaginatedResult<WritingsReportResponseDTO>(
                parameters.PageIndex, data.Count, totalCount, data);
        }
    }
}
