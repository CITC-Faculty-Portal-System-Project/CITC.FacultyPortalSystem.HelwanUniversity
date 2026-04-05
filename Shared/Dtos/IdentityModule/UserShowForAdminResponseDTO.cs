namespace Shared.Dtos.IdentityModule
{
    public record UserShowForAdminResponseDTO
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? NormalizedUserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string NationalNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public List<PermissionResponseDTO>? Permissions { get; set; }
        public List<PermissionResponseDTO>? RolePermissions { get; set; }
        public List<string>? Roles { get; set; }
    }
}
