using Domain.Entities.IdentityModule.Authorization;
using Shared.Enums.IdentityModule.SpecificationEnums;
using Shared.SpecificationParameters.IdentityModule;
using System.Linq;
using System.Linq.Expressions;

namespace Services.Specifications.IdnetityModuleSpecifications
{
    internal class PermissionsSpecifications : BaseSpecifications<Permission, int>
    {
        public PermissionsSpecifications
            (string code) 
            : base(p => p.Code == code && !p.IsDeleted)
        {
        }

        public PermissionsSpecifications
           (PermissionSpecificationParameters parameters)
           : base(p => !p.IsDeleted &&
                   ( 
                        string.IsNullOrEmpty(parameters.Search) ||
                        p.Code!.Contains(parameters.Search!) ||
                        p.Description!.Contains(parameters.Search!) ||
                        p.DisplayName.Contains(parameters.Search!)
                   ) 
                    && (
                        parameters.Type == null ||
                        parameters.Type.Value == (Shared.Enums.IdentityModule.PermissionType)p.Type)
                
           )
        {
            switch (parameters.Sort)
            {
                case PermissionSortingOptions.TypeASC:
                    AddOrderBy(p => p.Type); 
                    break;

                case PermissionSortingOptions.TypeDESC:
                    AddOrderByDescending(p => p.Type);
                    break;
            }

        }
    }
}
