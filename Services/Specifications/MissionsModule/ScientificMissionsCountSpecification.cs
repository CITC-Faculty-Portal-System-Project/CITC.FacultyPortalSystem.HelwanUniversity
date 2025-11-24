using Domain.Entities.MissionsModule;
using Shared.SpceificationParameters.MissionsModule;
using Shared.SpecificationParameters.MissionsModule;

namespace Services.Specifications.MissionsModule
{
    internal class ScientificMissionsCountSpecification : BaseSpecifications<ScientificMissions , int>
    {
        public ScientificMissionsCountSpecification(ScientificMissionSpecificationParamaters parameters)
            : base(sm =>
                  (!sm.IsDeleted &&
                    sm.FacultyMember!.Email == parameters.FacultyMemberEmail) &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   sm.MissionName.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase) ||
                   sm.CountryOrCity.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase))
            )
        {

        }
    }
}
