using Shared.Enums.AcademicDataModule.WritingsAndPatentsModule;
using Shared.Enums.IdentityModule.SpecificationEnums;

namespace Shared.SpecificationParameters.IdentityModule
{ 
    public class UserSpecificationParameters 
    {
        private const int defaultPageSize = 9;
        private const int maxPageSize = 9;
        public UsersSortingOptions Sort { get; set; }
        public string? Search { get; set; }
        public List<UserRolesFilteration>? Role { get; set; }
        public int PageIndex { get; set; } = 1;
        private int _pageSize = defaultPageSize;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > maxPageSize ? maxPageSize : value; }
        }
    }
}
