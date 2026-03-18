namespace Shared.Dtos.IdentityModule
{
    public record UserIdentifiersResposnseDTO
    {
        public string Email { get; set; } = string.Empty;
        public Guid Id { get; set; }
    }
}
