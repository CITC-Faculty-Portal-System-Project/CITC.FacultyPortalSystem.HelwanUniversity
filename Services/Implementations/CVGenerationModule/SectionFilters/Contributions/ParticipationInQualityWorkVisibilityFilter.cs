using Services.Abstraction.Contracts.CVGenerationModule;
using Shared.Dtos.CVGenerationModule;
using Shared.Models.CVGenerationModule;
using static Services.Implementations.CVGenerationModule.VisibilityFilterHelper;

namespace Services.Implementations.CVGenerationModule.SectionFilters.Contributions
{
    public class ParticipationInQualityWorkVisibilityFilter : ICVSectionVisibilityFilter
    {
        public void Apply(CVResponseDTO response, CVVisibilityConfig config, bool isPublic = false)
        {
            var settings = config.ParticipationInQualityWork;

            if (!settings.ShowparticipationsInQualityWork && isPublic == false)
            {
                response.ParticipationInQualityWork.Clear();
                return;
            }

            if (!settings.ShowparticipationTitle && !settings.ShowParticipationStartDate && !settings.ShowParticipationEndDate && isPublic == false)
            {
                response.ParticipationInQualityWork.Clear();
                return;
            }

            foreach (var piqw in response.ParticipationInQualityWork ?? [])
            {
                
                if(isPublic == true)
                {
                    HideIfFalse(settings.ShowparticipationTitleForPublic, () => piqw.ParticipationTitle = null!);
                    HideIfFalse(settings.ShowParticipationStartDateForPublic, () => piqw.StartDate = null!);
                    HideIfFalse(settings.ShowParticipationEndDateForPublic, () => piqw.EndDate = null!);
                }

                HideIfFalse(settings.ShowparticipationTitle, () => piqw.ParticipationTitle = null!);
                HideIfFalse(settings.ShowParticipationStartDate, () => piqw.StartDate = null!);
                HideIfFalse(settings.ShowParticipationEndDate, () => piqw.EndDate = null!);
            }
        }
    }
}
