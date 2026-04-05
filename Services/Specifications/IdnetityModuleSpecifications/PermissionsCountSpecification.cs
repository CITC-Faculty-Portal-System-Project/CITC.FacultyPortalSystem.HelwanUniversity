using Domain.Entities.IdentityModule.Authorization;
using Shared.SpecificationParameters.IdentityModule;
using System.Linq.Expressions;

namespace Services.Specifications.IdnetityModuleSpecifications
{
    internal class PermissionsCountSpecification : BaseSpecifications<Permission, int>
    {
        public PermissionsCountSpecification
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
        }
    }
}
