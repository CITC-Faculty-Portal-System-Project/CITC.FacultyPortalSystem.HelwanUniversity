using Domain.Entities.IdentityModule.Users;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Services.Specifications.IdnetityModuleSpecifications
{
    internal class RoleSpecification : BaseSpecifications<Role, Guid>
    {
        public RoleSpecification
            (string roleName) 
            :base(r => r.Name == roleName)
        {
            AddIncludeWithChain(r => r.Include(r => r.Permissions!).ThenInclude(r => r.Permission));
        }
    }
}
