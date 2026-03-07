using Shared.Enums.IdentityModule;
using Shared.Enums.IdentityModule.SpecificationEnums;

namespace Shared.SpecificationParameters.IdentityModule
{
    public class PermissionSpecificationParameters 
    {
        private const int defaultPageSize = 9;
        private const int maxPageSize = 9;
        public PermissionSortingOptions Sort { get; set; }
        public string? Search { get; set; }
        public PermissionType? Type { get; set; }
        public int PageIndex { get; set; } = 1;
        private int _pageSize = defaultPageSize;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > maxPageSize ? maxPageSize : value; }
        }

    }
}
