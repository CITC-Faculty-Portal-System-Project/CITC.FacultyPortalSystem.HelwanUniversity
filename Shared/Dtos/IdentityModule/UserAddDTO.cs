namespace Shared.Dtos.IdentityModule
{
    public record UserAddDTO
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NationalNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public List<PermissionResponseDTO>? Permissions { get; set; }
        public List<RoleResponseDTO>? Roles { get; set; }

    }
}
