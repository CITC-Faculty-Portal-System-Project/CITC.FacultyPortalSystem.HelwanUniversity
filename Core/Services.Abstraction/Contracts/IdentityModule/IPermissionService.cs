using Shared.Dtos.IdentityModule;

namespace Services.Abstraction.Contracts.IdentityModule
{
    public interface IPermissionService
    {
        Task<bool> HasPermissionAsync(string permissionCode);
        Task<IReadOnlyList<string>> GetEffectivePermissionsAsync();

    }
}

