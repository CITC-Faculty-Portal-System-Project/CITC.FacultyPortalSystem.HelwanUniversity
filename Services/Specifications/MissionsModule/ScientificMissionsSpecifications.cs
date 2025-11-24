using Domain.Entities.MissionsModule;
using Shared.Enums.MissionsModule;
using Shared.SpceificationParameters.MissionsModule;

namespace Services.Specifications.MissionsModule
{
    internal class ScientificMissionsSpecifications : BaseSpecifications<ScientificMissions, int>
    {
        public ScientificMissionsSpecifications(ScientificMissionSpecificationParamaters parameters)
            : base(sm =>
                  (!sm.IsDeleted &&
                    sm.FacultyMember!.Email == parameters.FacultyMemberEmail) &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   sm.MissionName.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase) ||
                   sm.CountryOrCity.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase))
            )
        {

            switch (parameters.Sort)
            {
                case ScientificMissionsSortingOptions.NameAsc:
                    AddOrderBy(sm => sm.MissionName);
                    break;
                case ScientificMissionsSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.MissionName);
                    break;
                case ScientificMissionsSortingOptions.DateAsc:
                    AddOrderBy(p => p.StartDate);
                    break;
                case ScientificMissionsSortingOptions.DateDesc:
                    AddOrderByDescending(p => p.StartDate);
                    break;
                default:
                    break;
            }
            applyPagination(parameters.PageSize, parameters.PageIndex);

        }

        public ScientificMissionsSpecifications(int id) : base(sm => !sm.IsDeleted && sm.Id == id)
        {

        }
    }
}
