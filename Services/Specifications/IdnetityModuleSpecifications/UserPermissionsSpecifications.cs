using Domain.Entities.IdentityModule.Authorization;

namespace Services.Specifications.IdnetityModuleSpecifications
{
    internal class UserPermissionsSpecifications : BaseSpecifications<Permission , int>
    {
        public UserPermissionsSpecifications(Guid userId)
        : base(p => p.Users!.Any(up => !up.IsDeleted && up.UserId == userId))
        {
        }

    }
}
