using Shared.Dtos.IdentityModule;

namespace Services.Abstraction.Contracts.IdentityModule
{
    public interface IUserPermissionRead
    {

        Task<PermissionResponseDTO?> GetOverrideAsync(Guid userId, string permissionCode, CancellationToken ct);
        Task<IReadOnlyList<PermissionResponseDTO>> GetOverridesAsync(Guid userId, CancellationToken ct);

    }
}
