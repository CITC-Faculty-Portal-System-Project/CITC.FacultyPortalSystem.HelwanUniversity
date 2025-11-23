namespace Shared.Dtos.IdentityModule
{
    public record UserResultDto(
        string UserName,
        string Email,
        Guid UserId = default,
        string? Token = default
    );
}
