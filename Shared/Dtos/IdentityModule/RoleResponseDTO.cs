namespace Shared.Dtos.IdentityModule
{
    public record RoleResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        
    }
}
