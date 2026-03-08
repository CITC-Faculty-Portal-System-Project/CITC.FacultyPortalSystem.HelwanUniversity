using Shared.Dtos.IdentityModule;
using Shared.SpecificationParameters.IdentityModule;

namespace Services.Abstraction.Contracts.AdminModule
{
    public interface IUserManagementService
    {
        public Task<IEnumerable<PermissionResponseDTO>> GetAllSystemPermissionsAsync(PermissionSpecificationParameters parameters);
        public Task<PaginatedResult<UserShowForAdminResponseDTO>> GetAllUsersAsync(UserSpecificationParameters parameters);
        public Task<UserShowForAdminResponseDTO> GetUserByIdAsync(Guid userId);
        public Task<UserShowForAdminResponseDTO> AddUserAsync(UserAddDTO user);
        public Task<UserShowForAdminResponseDTO> EditUserCredeintalsAsync(UserEditDTO user , Guid userId);
        public Task<UserShowForAdminResponseDTO> AssignPermissionsToUserAsync(IList<PermissionResponseDTO> permissions , Guid userId);
        public Task<UserShowForAdminResponseDTO> RevokePermissionsFromUserAsync(IList<PermissionResponseDTO> permissions , Guid userId);
        public Task<IEnumerable<PermissionResponseDTO>> GetCurrentLoggedInUserPermissionsAsync();
    }
}
