namespace Services.Abstraction.Contracts.IdentityModule
{
    public interface IRolePermissionReadService
    {
        Task<bool> AnyRoleHasPermissionAsync(IReadOnlyCollection<Guid> roleIds, string permissionCode, CancellationToken ct);
        Task<IReadOnlyList<string>> GetPermissionCodesForRolesAsync(IReadOnlyCollection<Guid> roleIds, CancellationToken ct);

    }
}
