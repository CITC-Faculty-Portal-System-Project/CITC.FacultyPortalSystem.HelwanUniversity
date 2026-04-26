using Domain.Entities.AcademicDataModule.ResearchesModule;
using Domain.Entities.IdentityModule.Users;
using Microsoft.AspNetCore.Identity;
using Services.Abstraction.Contracts.ReportsAndDashboard;
using Services.Specifications.AggregationSpecifications;
using Services.Specifications.ResearchesModule;
using Shared.ReportsAndDashboard;

namespace Services.Implementations.ReportsAndDashboards
{
    public class DashboardService(UserManager<User> _userManager 
        , IAuthenticationService _authenticationService , IUnitOfWork _unitOfWork) : IDashboardService
    {
        public async Task<AdminDashboardResponseDTO> GetAdminDashboardDataAsync()
        {
            var personalDataRepo = _unitOfWork.GetRepository<PersonalData , int>();
            var researchesRepo = _unitOfWork.GetRepository<Research , int>();

            var currentUser = await _authenticationService.GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());
            var currentUserEntity = await _userManager.FindByEmailAsync(currentUser.Email);
            var currentUserRoles = await _userManager.GetRolesAsync(currentUserEntity!);

            var facultyMemberRoleUsers = await _userManager.GetUsersInRoleAsync("Faculty Member");
            var managementAdminRoleUsers = await _userManager.GetUsersInRoleAsync("ManagementAdmin");
            var supportAdminRoleUsers = await _userManager.GetUsersInRoleAsync("SupportAdmin");

            var allUsersCount = facultyMemberRoleUsers.Count + managementAdminRoleUsers.Count + supportAdminRoleUsers.Count;
            var facultyMembersCount = facultyMemberRoleUsers.Count;
            var managementUsersCount = managementAdminRoleUsers.Count + supportAdminRoleUsers.Count;

            var usersPerFaculty = await personalDataRepo.ExecuteAggregationAsync(new PersonalDataAggregationSpecification());
            var researchesPerFaculty = await researchesRepo.ExecuteAggregationAsync(new ResearchAggregationSpecification());
            var totalResearches = await researchesRepo.GetAllAsync(new TotalResearchesSpecification());
            var totalResearchesCount = totalResearches.Count();

            var researchesMonthlyRate = await researchesRepo.ExecuteAggregationAsync(new ResearchesMonthlyRateSpecification());
            return new AdminDashboardResponseDTO
            {
                TotalUsersNumber = allUsersCount,
                TotalFacultyMembersNumber = facultyMembersCount,
                TotalSystemManagersNumber = managementUsersCount,
                UsersPerFaculty = usersPerFaculty,
                ResearchesPerFaculty = researchesPerFaculty,
                TotalResearchesNumber = totalResearchesCount,
                ResearchesMonthlyRate = researchesMonthlyRate,
                CurrentUserName = currentUser.UserName,
                CurrentUserRoles = currentUserRoles.ToList()
            };

        }
    }
}
