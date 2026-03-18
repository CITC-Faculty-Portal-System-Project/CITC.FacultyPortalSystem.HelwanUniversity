using Microsoft.AspNetCore.Authorization;
using Services.Abstraction.Contracts.IdentityModule;

namespace Presentation.Authorization
{
    public sealed class PermissionAuthorizationHandler
        : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IPermissionService _permissionService;

        public PermissionAuthorizationHandler(IPermissionService permissionService)
            => _permissionService = permissionService;

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            if (context.User?.Identity?.IsAuthenticated != true)
                return;

            var ok = await _permissionService.HasPermissionAsync(requirement.PermissionCode);
            if (ok)
                context.Succeed(requirement);
        }
    }
}
