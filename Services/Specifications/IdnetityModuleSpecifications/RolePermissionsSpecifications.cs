using Domain.Entities.IdentityModule.Authorization;
using Domain.Entities.IdentityModule.Users;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Services.Specifications.IdnetityModuleSpecifications
{
    internal class RolePermissionsSpecifications : BaseSpecifications<Permission, int>
    {
        public RolePermissionsSpecifications (IReadOnlyCollection<Guid> roleIds)
            :base(p => p.Roles!.Any(rp => !rp.IsDeleted && roleIds.Contains(rp.RoleId)))
        {
        }

    }
}
