using Services.Abstraction.Contracts.CVGenerationModule;
using Shared.Dtos.CVGenerationModule;
using Shared.Models.CVGenerationModule;
using static Services.Implementations.CVGenerationModule.VisibilityFilterHelper;

namespace Services.Implementations.CVGenerationModule.SectionFilters.Missions
{
    public class ScientificMissionsVisibilityFilter : ICVSectionVisibilityFilter
    {
        public void Apply(CVResponseDTO response, CVVisibilityConfig config)
        {
            var settings = config.ScientificMissions;

            if (!settings.ShowScientificMissions)
            {
                response.ScientificMissions.Clear();
                return;
            }

            if (!settings.ShowMissionName && !settings.ShowMissionStartDate && !settings.ShowMissionEndDate && !settings.ShowMissionUniversityOrFaculty && !settings.ShowMissionCountryOrCity)
            {
                response.ScientificMissions.Clear();
                return;
            }

            foreach(var sm in response.ScientificMissions ?? [])
            {
                HideIfFalse(settings.ShowMissionName, () => sm.MissionName = null!);
                HideIfFalse(settings.ShowMissionStartDate, () => sm.StartDate = null);
                HideIfFalse(settings.ShowMissionEndDate, () => sm.EndDate = null);
                HideIfFalse(settings.ShowMissionUniversityOrFaculty, () => sm.UniversityOrFaculty = null!);
                HideIfFalse(settings.ShowMissionCountryOrCity, () => sm.CountryOrCity = null!);

            }
        }
    }
}
