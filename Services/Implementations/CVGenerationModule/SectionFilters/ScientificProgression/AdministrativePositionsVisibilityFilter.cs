using Services.Abstraction.Contracts.CVGenerationModule;
using Shared.Dtos.CVGenerationModule;
using Shared.Models.CVGenerationModule;
using static Services.Implementations.CVGenerationModule.VisibilityFilterHelper;

namespace Services.Implementations.CVGenerationModule.SectionFilters.ScientificProgression
{
    public class AdministrativePositionsVisibilityFilter : ICVSectionVisibilityFilter
    {
        public void Apply(CVResponseDTO response, CVVisibilityConfig config)
        {
            var settings = config.AdministrativePositions;

            if (!settings.ShowAdministrativePositions)
            {
                response.AdministrativePositions.Clear();
                return;
            }

            if (!settings.ShowPosition && !settings.ShowPositionStartDate && !settings.ShowPositionEndDate)
            {
                response.AdministrativePositions.Clear();
                return;
            }

            foreach(var ap in response.AdministrativePositions ?? [])
            {
                HideIfFalse(settings.ShowPosition, () => ap.Position = null!);
                HideIfFalse(settings.ShowPositionStartDate, () => ap.StartDate = null!);
                HideIfFalse(settings.ShowPositionEndDate, () => ap.EndDate = null!);
            }
        }
    }
}
