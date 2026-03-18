using Domain.Entities.IdentityModule.Users;
using Microsoft.EntityFrameworkCore;
using Shared.Enums.AcademicDataModule.MissionsModule;
using Shared.Enums.IdentityModule.SpecificationEnums;
using Shared.SpecificationParameters.IdentityModule;
using System.Linq.Expressions;
using System.Xml.Linq;

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
                            .Contains(ur.Role.NormalizedName!.ToUpper().Replace(" " , "")))
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


            switch (parameters.Sort)
            {
                case UsersSortingOptions.UsernameASC:
                    AddOrderBy(u => u.UserName!);
                    break;
                case UsersSortingOptions.UsernameDESC:
                    AddOrderByDescending(u => u.UserName!);
                    break;
                case UsersSortingOptions.NumberOfAcessedPermissionsASC:
                    AddOrderBy(u =>
                        u.Roles.SelectMany(r => r.Role.Permissions!).Count()
                        + u.Permissions!.Count()); 
                    break;
                case UsersSortingOptions.NumberOfAcessedPermissionsDESC:
                    AddOrderByDescending(u =>
                        u.Roles.SelectMany(r => r.Role.Permissions!).Count()
                        + u.Permissions!.Count()); 
                    break;
                default:
                    AddOrderBy(u => u.Id);
                    break;

            }
            applyPagination(parameters.PageSize, parameters.PageIndex);
            EnableSplitQuery();

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

            EnableSplitQuery();



        }


        public UserSpecifications
            (string nationalNumber)
                : base(u => u.NationalNumber == nationalNumber)

        {
       
        }


        public UserSpecifications(List<string> permissionCodes , Guid currentUserId)
      : base(u => u.Id != currentUserId && u.Roles.Any(r => r.Role!.Name!.Equals("SupportAdmin")) &&
          u.Permissions!.Any(up =>
              up.Permission != null &&
              permissionCodes.Contains(up.Permission.Code))
          ||
          u.Roles!.Any(ur =>
              ur.Role.Permissions!.Any(rp =>
                  rp.Permission != null &&
                  permissionCodes.Contains(rp.Permission.Code))
          )
      )
        {
         AddIncludeWithChain(u =>
         u.Include(u => u.Permissions!)
         .ThenInclude(u => u.Permission));

            AddIncludeWithChain(u =>
                u.Include(u => u.Roles!)
                .ThenInclude(u => u.Role!)
                .ThenInclude(u => u.Permissions!)
                .ThenInclude(u => u.Permission));

            EnableSplitQuery();

        }


    }
}
