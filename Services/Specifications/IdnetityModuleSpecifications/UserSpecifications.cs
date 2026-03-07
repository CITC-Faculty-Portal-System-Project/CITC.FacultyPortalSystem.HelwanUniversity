using Domain.Entities.IdentityModule.Users;
using Microsoft.EntityFrameworkCore;
using Shared.Enums.AcademicDataModule.MissionsModule;
using Shared.Enums.IdentityModule.SpecificationEnums;
using Shared.SpecificationParameters.IdentityModule;
using System.Linq.Expressions;

namespace Services.Specifications.IdnetityModuleSpecifications
{
    internal class UserSpecifications : BaseSpecifications<User, Guid>
    {
        public UserSpecifications
            (UserSpecificationParameters parameters, Guid userId)
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
                            .Contains(ur.Role.NormalizedName!))
                ))
        {
            AddIncludeWithChain(u =>
                    u.Include(u => u.Permissions!)
                    .ThenInclude(u => u.Permission));

            AddIncludeWithChain(u =>
                u.Include(u => u.Roles!)
                .ThenInclude(u => u.Role!)
                .ThenInclude(u => u.Permissions!)
                .ThenInclude(u => u.Permission));

            AddIncludes(u => u.Roles);

            switch (parameters.Sort)
            {
                case UsersSortingOptions.UsernameASC:
                    AddOrderBy(u => u.UserName!);
                    break;
                case UsersSortingOptions.UsernameDESC:
                    AddOrderByDescending(u => u.UserName!);
                    break;
                case UsersSortingOptions.NationalNumberASC:
                    AddOrderBy(u => u.NationalNumber);
                    break;
                case UsersSortingOptions.NationalNumberDESC:
                    AddOrderByDescending(u => u.NationalNumber);
                    break;
                case UsersSortingOptions.NumberOfAcessedPermissionsASC:
                    AddOrderBy(u => u.Roles.Sum(ur => ur.Role.Permissions!.Count) + u.Permissions!.Count);
                    break;
                case UsersSortingOptions.NumberOfAcessedPermissionsDESC:
                    AddOrderByDescending(u => u.Roles.Sum(ur => ur.Role.Permissions!.Count) + u.Permissions!.Count);
                    break;
                default:
                    break;

            }

            applyPagination(parameters.PageSize, parameters.PageIndex);

        }


        public UserSpecifications
            (Guid userId)
                : base(u => u.Id == userId)

        {
            AddIncludeWithChain(u =>
                    u.Include(u => u.Permissions!)
                    .ThenInclude(u => u.Permission));

            AddIncludeWithChain(u =>
                u.Include(u => u.Roles!)
                .ThenInclude(u => u.Role!)
                .ThenInclude(u => u.Permissions!)
                .ThenInclude(u => u.Permission));

            AddIncludes(u => u.Roles);


        }


        public UserSpecifications
            (string nationalNumber)
                : base(u => u.NationalNumber == nationalNumber)

        {
       
        }
    }
}
