using Domain.Entities.MissionsModule;
using Shared.Enums;
using Shared.SpceificationParameters.MissionsModule;

namespace Services.Specifications.MissionsModule
{
    internal class FacultyMemberGetsMissionSpecification : BaseSpecifications<ScientificMissions, int>
    {
        public FacultyMemberGetsMissionSpecification(MissionSpecificationParamaters specificationParamaters)
                : base(m => m.FacultyMember.Email == specificationParamaters.FacultyMemberEmail &&
                    string.IsNullOrEmpty(specificationParamaters.SearchCriteria)
                    || m.MissionName.Contains(specificationParamaters.SearchCriteria, StringComparison.CurrentCultureIgnoreCase)
                )

        {
            switch (specificationParamaters.OrderCriteria)
            {
                case MissionsSortingOptions.ByStartDateASC:
                    AddOrderBy(aq => aq.StartDate);
                    break;
                case MissionsSortingOptions.ByStartDateDESC:
                    AddOrderByDescending(aq => aq.StartDate);
                    break;
                default:
                    break;
            }
            applyPagination(specificationParamaters.PageSize, specificationParamaters.pageIndex);


        }


        public FacultyMemberGetsMissionSpecification(int id) : base(a => a.Id == id)
        {
            {

            }
        }
    }
}
