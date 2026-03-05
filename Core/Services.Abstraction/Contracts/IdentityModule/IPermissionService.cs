using Shared.Dtos.IdentityModule;

namespace Services.Abstraction.Contracts.IdentityModule
{
    public interface IPermissionService
    {
        Task<bool> HasPermissionAsync(Guid userId, string permissionCode, CancellationToken ct = default);
        Task<IReadOnlyList<string>> GetEffectivePermissionsAsync(Guid userId, CancellationToken ct = default);

    }
}

