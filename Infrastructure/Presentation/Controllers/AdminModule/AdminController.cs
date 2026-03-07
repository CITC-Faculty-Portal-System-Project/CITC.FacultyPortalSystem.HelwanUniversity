using Microsoft.AspNetCore.Authorization;
using Shared;
using Shared.Dtos.AttachmentsModule;
using Shared.Dtos.IdentityModule;
using Shared.Dtos.ResearchesModule;
using Shared.SpecificationParameters.IdentityModule;
using Shared.SpecificationParameters.ResearchesModule;
namespace Presentation.Controllers.AdminModule
{
    
    public class AdminController(IServiceManager _serviceManager) : ApiController
    {
        [Authorize(Policy = "Permission:UserAccount.Read")]
        [ProducesResponseType(typeof(PaginatedResult<PermissionResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("Permissions")]
        public async Task<ActionResult<PaginatedResult<PermissionResponseDTO>>> GetAllPermissions
                   ([FromQuery] PermissionSpecificationParameters parameters)
             => Ok(await _serviceManager.UserManagementService.GetAllSystemPermissionsAsync(parameters));


        [Authorize(Policy = "Permission:UserAccount.Read")]
        [ProducesResponseType(typeof(PaginatedResult<UserShowForAdminResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("Users")]
        public async Task<ActionResult<PaginatedResult<UserShowForAdminResponseDTO>>> GetAllUsers
                     ([FromQuery] UserSpecificationParameters parameters)
               => Ok(await _serviceManager.UserManagementService.GetAllUsersAsync(parameters));

        [Authorize(Policy = "Permission:UserAccount.Read")]
        [ProducesResponseType(typeof(UserShowForAdminResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("User/{id}")]
        public async Task<ActionResult<PaginatedResult<UserShowForAdminResponseDTO>>> GetUserById
                 (Guid id)
           => Ok(await _serviceManager.UserManagementService.GetUserByIdAsync(id));


        [Authorize(Policy = "Permission:UserAccount.Create")]
        [ProducesResponseType(typeof(UserShowForAdminResponseDTO), StatusCodes.Status200OK)]
        [HttpPost("User")]
        public async Task<ActionResult<UserShowForAdminResponseDTO>> AddUser
              (UserAddDTO user)
                => Ok(await _serviceManager.UserManagementService.AddUserAsync(user));


        [Authorize(Policy = "Permission:UserAccount.Update")]
        [ProducesResponseType(typeof(UserShowForAdminResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("UserCredeintals/{id}")]
        public async Task<ActionResult<UserShowForAdminResponseDTO>> EditUser
        (UserEditDTO user , Guid id)
        => Ok(await _serviceManager.UserManagementService.EditUserCredeintalsAsync(user , id));


        [Authorize(Policy = "Permission:UserAccount.Update")]
        [ProducesResponseType(typeof(UserShowForAdminResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("UserGrantPermissions/{id}")]
        public async Task<ActionResult<UserShowForAdminResponseDTO>> AssignPermissionToUser
        (IList<PermissionResponseDTO> permissions, Guid id)
        => Ok(await _serviceManager.UserManagementService.AssignPermissionsToUserAsync(permissions, id));

        [Authorize(Policy = "Permission:UserAccount.Update")]
        [ProducesResponseType(typeof(UserShowForAdminResponseDTO), StatusCodes.Status200OK)]
        [HttpDelete("UserRevokePermissions/{id}")]
        public async Task<ActionResult<UserShowForAdminResponseDTO>> RevokePermissionFromUser
           (IList<PermissionResponseDTO> permissions, Guid id)
           => Ok(await _serviceManager.UserManagementService.RevokePermissionsFromUserAsync(permissions, id));


    }
}
