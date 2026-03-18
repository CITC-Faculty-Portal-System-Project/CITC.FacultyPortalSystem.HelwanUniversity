using Domain.Entities.IdentityModule.Users;
using Shared.SpecificationParameters.IdentityModule;
using System.Linq.Expressions;

namespace Services.Specifications.IdnetityModuleSpecifications
{
    internal class UserCountSpecifications : BaseSpecifications<User, Guid>
    {
        public UserCountSpecifications
            (UserSpecificationParameters parameters , Guid userId)
                : base(u => u.Id != userId &&
                (
                    string.IsNullOrEmpty(parameters.Search) ||
                    u.UserName!.Contains(parameters.Search) ||
                    u.Email!.Contains(parameters.Search) ||
                    u.NationalNumber.Contains(parameters.Search)
                ) &&
                (
                    parameters.Role == null ||
                    parameters.Role.Count == 0 ||
                    u.Roles.Any(ur =>
                        parameters.Role
                            .Select(x => x.ToString().ToUpper())
                            .Contains(ur.Role.NormalizedName!.Replace(" " , "")))
                ))

        {
        }
    }
}
