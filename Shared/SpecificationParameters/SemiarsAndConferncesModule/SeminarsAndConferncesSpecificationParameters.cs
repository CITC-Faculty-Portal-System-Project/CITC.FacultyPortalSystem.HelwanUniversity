using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.SpecificationParameters.SemiarsAndConferncesModule
{
    public class SeminarsAndConferncesSpecificationParameters
    {
        private const int defaultPageSize = 9;
        private const int maxPageSize = 9;
        public int pageIndex { get; set; } = 1;
        private int _pageSize { get; set; } = defaultPageSize;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > maxPageSize ? maxPageSize : value; }
        }

        public MissionsSortingOptions OrderCriteria { get; set; }
        public string FacultyMemberEmail { get; set; } = string.Empty;
        public string? SearchCriteria { get; set; } = string.Empty;
    }
}
