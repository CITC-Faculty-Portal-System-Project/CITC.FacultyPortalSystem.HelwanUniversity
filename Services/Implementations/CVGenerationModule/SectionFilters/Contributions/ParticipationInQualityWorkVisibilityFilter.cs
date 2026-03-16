using Services.Abstraction.Contracts.CVGenerationModule;
using Shared.Dtos.CVGenerationModule;
using Shared.Models.CVGenerationModule;
using static Services.Implementations.CVGenerationModule.VisibilityFilterHelper;

namespace Services.Implementations.CVGenerationModule.SectionFilters.Contributions
{
    public class ParticipationInQualityWorkVisibilityFilter : ICVSectionVisibilityFilter
    {
        public void Apply(CVResponseDTO response, CVVisibilityConfig config)
        {
            var settings = config.ParticipationInQualityWork;

            if (!settings.ShowparticipationsInQualityWork)
            {
                response.ParticipationInQualityWork.Clear();
                return;
            }

            if (!settings.ShowparticipationTitle && !settings.ShowParticipationStartDate && !settings.ShowParticipationEndDate)
            {
                response.ParticipationInQualityWork.Clear();
                return;
            }

            foreach (var piqw in response.ParticipationInQualityWork ?? [])
            {
                HideIfFalse(settings.ShowparticipationTitle, () => piqw.ParticipationTitle = null!);
                HideIfFalse(settings.ShowParticipationStartDate, () => piqw.StartDate = null!);
                HideIfFalse(settings.ShowParticipationEndDate, () => piqw.EndDate = null!);
            }
        }
    }
}
