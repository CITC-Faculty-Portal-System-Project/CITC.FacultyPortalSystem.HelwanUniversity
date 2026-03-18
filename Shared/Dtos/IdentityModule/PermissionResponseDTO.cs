using Shared.Enums.IdentityModule;

namespace Shared.Dtos.IdentityModule
{
    public record PermissionResponseDTO
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public PermissionType Type { get; set; }

    }
}
