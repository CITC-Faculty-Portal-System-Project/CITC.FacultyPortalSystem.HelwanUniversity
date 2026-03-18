using Shared.Dtos.IdentityModule;

namespace Services.Abstraction.Contracts.AdminModule
{
    public interface IRoleManagementService
    {
        public Task<UserShowForAdminResponseDTO> GetUsersInRole(string roleName);
    }
}
