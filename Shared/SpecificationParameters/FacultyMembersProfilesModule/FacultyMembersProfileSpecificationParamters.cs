using Shared.Enums.ResearchesModule;

namespace Shared.SpecificationParameters.FacultyMembersProfilesModule
{
    public class FacultyMembersProfileSpecificationParamters : BaseFacultyMemberProfileSpecificationParamters
    {
        private const int defaultPageSize = 9;
        private const int maxPageSize = 9;
        public int PageIndex { get; set; } = 1;
        private int _pageSize = defaultPageSize;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > maxPageSize ? maxPageSize : value; }
        }


    }
}
