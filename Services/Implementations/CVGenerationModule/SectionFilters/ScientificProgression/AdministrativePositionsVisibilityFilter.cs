using Services.Abstraction.Contracts.CVGenerationModule;
using Shared.Dtos.CVGenerationModule;
using Shared.Models.CVGenerationModule;
using static Services.Implementations.CVGenerationModule.VisibilityFilterHelper;

namespace Services.Implementations.CVGenerationModule.SectionFilters.ScientificProgression
{
    public class AdministrativePositionsVisibilityFilter : ICVSectionVisibilityFilter
    {
        public void Apply(CVResponseDTO response, CVVisibilityConfig config , bool isPublic = false)
        {
            var settings = config.AdministrativePositions;

            if (!settings.ShowAdministrativePositions && isPublic == false)
            {
                response.AdministrativePositions.Clear();
                return;
            }

            if (!settings.ShowPosition && !settings.ShowPositionStartDate && !settings.ShowPositionEndDate && isPublic == false)
            {
                response.AdministrativePositions.Clear();
                return;
            }

            foreach(var ap in response.AdministrativePositions ?? [])
            {
               
                if(isPublic == true)
                {
                    HideIfFalse(settings.ShowPositionForPublic, () => ap.Position = null!);
                    HideIfFalse(settings.ShowPositionStartDateForPublic, () => ap.StartDate = null!);
                    HideIfFalse(settings.ShowPositionEndDateForPublic, () => ap.EndDate = null!);

                }

                HideIfFalse(settings.ShowPosition, () => ap.Position = null!);
                HideIfFalse(settings.ShowPositionStartDate, () => ap.StartDate = null!);
                HideIfFalse(settings.ShowPositionEndDate, () => ap.EndDate = null!);
            }
        }
    }
}
