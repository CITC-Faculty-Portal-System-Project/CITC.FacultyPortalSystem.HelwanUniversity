//using Domain.Entities.IdentityModule.Users;
//using Microsoft.AspNetCore.Identity;
//using Services.Abstraction.Contracts.AdminModule;
//using Services.Global;

//namespace Services.Implementations.AdminModule
//{
//    public class RoleManagementService(IUnitOfWork unitOfWork,
//    IMapper mapper,
//    IAuthenticationService authenticationService
//            , UserManager<User> userManager
//            , RoleManager<Role> roleManager)
//            : BaseService<Role, Guid>(unitOfWork, authenticationService, mapper),
//            IRoleManagementService
//    {
//        protected override string EntityName => "Role";

//        public async Task<UserShowForAdminResponseDTO> GetUsersInRole(string roleName)
//        {
//            var users =  await userManager.GetUsersInRoleAsync(roleName);

//            users.
//        }
//    }
//}
