using Domain.Entities.MissionsModule;
using Shared.Dtos.DataFetchingFromExternalService;
using Shared.Enums.MissionsModule;
using Shared.SpceificationParameters.MissionsModule;

namespace Services.Specifications.MissionsModule
{
    internal class ScientificMissionsSpecifications : BaseSpecifications<ScientificMissions, int>
    {
        public ScientificMissionsSpecifications(ScientificMissionSpecificationParamaters parameters, string facultyMemberEmail)
            : base(sm =>
                  (!sm.IsDeleted &&
                    sm.FacultyMember!.Email == facultyMemberEmail) &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   sm.MissionName.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase) ||
                   sm.CountryOrCity.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase))

            )

        {
            AddIncludes(sm => sm.FacultyMember);
            switch (parameters.Sort)
            {
                case ScientificMissionsSortingOptions.NameAsc:
                    AddOrderBy(sm => sm.MissionName);
                    break;
                case ScientificMissionsSortingOptions.NameDesc:
                    AddOrderByDescending(sm => sm.MissionName);
                    break;
                case ScientificMissionsSortingOptions.DateAsc:
                    AddOrderBy(sm => sm.StartDate);
                    break;
                case ScientificMissionsSortingOptions.DateDesc:
                    AddOrderByDescending(sm => sm.StartDate);
                    break;
                default:
                    break;
            }
            applyPagination(parameters.PageSize, parameters.PageIndex);

        }

        public ScientificMissionsSpecifications(int id) : base(sm => !sm.IsDeleted && sm.Id == id)
        {
            AddIncludes(sm => sm.FacultyMember);
        }

        public ScientificMissionsSpecifications(SceintificMissionsFetchingDTO sceintificMissionsFetchingDTO) 
            : base(sm => sm.StartDate == sceintificMissionsFetchingDTO.StartDate && sm.EndDate == sceintificMissionsFetchingDTO.EndDate &&
            sm.MissionName == sceintificMissionsFetchingDTO.Name && sm.UniversityOrFaculty == sceintificMissionsFetchingDTO.UniversityFaculty
            && sm.CountryOrCity == sceintificMissionsFetchingDTO.CountryCity && sm.Notes == sceintificMissionsFetchingDTO.Description 
            && sm.FacultyMember.NationalNumber == sceintificMissionsFetchingDTO.NationalNumber)
        {
            AddIncludes(sm => sm.FacultyMember);
        }
    }
}
