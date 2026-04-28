using Domain.Entities.AcademicDataModule.ResearchesModule;
using Domain.Entities.AdminModule;
using Domain.Entities.IdentityModule.Users;
using Domain.Entities.UniversityFacultiesAndDepartments;
using Microsoft.AspNetCore.Identity;
using Services.Abstraction.Contracts.ReportsAndDashboard;
using Services.Specifications.AggregationSpecifications;
using Shared.Dtos.ReportsAndDashboard;

namespace Services.Implementations.ReportsAndDashboards
{
    public class DashboardService(UserManager<User> _userManager 
        , IAuthenticationService _authenticationService , IUnitOfWork _unitOfWork) : IDashboardService
    {
        public async Task<AdminDashboardResponseDTO> GetAdminDashboardDataAsync()
        {
            var personalDataRepo = _unitOfWork.GetRepository<PersonalData , int>();
            var researchesRepo = _unitOfWork.GetRepository<Research , int>();
            var facultiesRepo = _unitOfWork.GetRepository<Faculty , int>();
            var ticketsRepo = _unitOfWork.GetRepository<Ticket , int>();

            var currentUser = await _authenticationService.GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());
            var currentUserEntity = await _userManager.FindByEmailAsync(currentUser.Email);
            var currentUserRoles = await _userManager.GetRolesAsync(currentUserEntity!);

            var facultyMemberRoleUsers = await _userManager.GetUsersInRoleAsync("Faculty Member");
            var managementAdminRoleUsers = await _userManager.GetUsersInRoleAsync("ManagementAdmin");
            var supportAdminRoleUsers = await _userManager.GetUsersInRoleAsync("SupportAdmin");

            var allUsersCount = facultyMemberRoleUsers.Count + managementAdminRoleUsers.Count + supportAdminRoleUsers.Count;
            var facultyMembersCount = facultyMemberRoleUsers.Count;
            var managementUsersCount = managementAdminRoleUsers.Count + supportAdminRoleUsers.Count;

            var faculties = await facultiesRepo.GetAllAsync(new FacultySpecification());
            var usersPerFaculty = await personalDataRepo.ExecuteAggregationAsync(new PersonalDataAggregationSpecification(faculties.AsQueryable()));
            var researchesPerFaculty = await researchesRepo.ExecuteAggregationAsync(new ResearchAggregationSpecification(faculties.AsQueryable()));
            var totalResearches = await researchesRepo.ExecuteAggregationAsync(new ResearchesStatsAggregationSpecification());

            var researchesMonthlyRate = await researchesRepo.ExecuteAggregationAsync(new ResearchesMonthlyRateSpecification());
            var ticketsStats = await ticketsRepo.ExecuteAggregationAsync(new TicketingAggregationSpecification());
            return new AdminDashboardResponseDTO
            {
                TotalUsersNumber = allUsersCount,
                TotalFacultyMembersNumber = facultyMembersCount,
                TotalSystemManagersNumber = managementUsersCount,
                UsersPerFaculty = usersPerFaculty,
                ResearchesPerFaculty = researchesPerFaculty,
                ResearchesStats = totalResearches.FirstOrDefault() ?? new ResearchesStatsDTO(),
                ResearchesMonthlyRate = researchesMonthlyRate,
                CurrentUserName = currentUser.UserName,
                CurrentUserRoles = currentUserRoles.ToList(),
                TicketsStats = ticketsStats.FirstOrDefault()?? new TicketsStatsDTO() 
            };

        }

        public async Task<ResearchesDashboardDTO> GetResearchDashboardDataAsync()
        {
            var researchesRepo = _unitOfWork.GetRepository<Research , int>();
            
            var researchesStats = await researchesRepo.ExecuteAggregationAsync(new ResearchDashboardAggregationSpecification());

            return new ResearchesDashboardDTO
            {
                DepartmentStats = researchesStats.FirstOrDefault()?.DepartmentStats!,
                FacultyStats = researchesStats.FirstOrDefault()?.FacultyStats!,
                InternationalResearchesNo = researchesStats.FirstOrDefault()?.InternationalResearchesNo ?? 0,
                LocalResearchesNo = researchesStats.FirstOrDefault()?.LocalResearchesNo ?? 0,
                ResearchersStats = researchesStats.FirstOrDefault()?.ResearchersStats!,
                InterestsStats = researchesStats.FirstOrDefault()?.InterestsStats!,
                CitationsStats = researchesStats.FirstOrDefault()?.CitationsStats!
            };
        }
    }
}
