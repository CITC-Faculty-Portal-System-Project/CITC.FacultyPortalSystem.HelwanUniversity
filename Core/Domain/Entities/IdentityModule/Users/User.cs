using Domain.Entities.IdentityModule.Authorization;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.IdentityModule.Users
{
    public class User : IdentityUser<Guid>
    {
        public string NationalNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        #region NavigationsAndRelations
        public ICollection<UserPermission>? Permissions { get; set; } = new List<UserPermission>();

        #endregion
    }
}
