using Domain.Entities.AcademicDataModule.MissionsModule;
using Shared.SpecificationParameters.AcademicDataModule.MissionsModule;

namespace Services.Specifications.AcademicDataModule.MissionsModule
{
    internal class ScientificMissionsCountSpecification : BaseSpecifications<ScientificMissions , int>
    {
        public ScientificMissionsCountSpecification(ScientificMissionSpecificationParamaters parameters, string facultyMemberEmail)
            : base(sm =>
                  !sm.IsDeleted &&
                    sm.FacultyMember!.Email == facultyMemberEmail &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   sm.MissionName.Contains(parameters.Search) ||
                   sm.CountryOrCity.Contains(parameters.Search))
            )
        {

        }
    }
}
