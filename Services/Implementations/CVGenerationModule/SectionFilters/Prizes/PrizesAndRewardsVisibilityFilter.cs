using Services.Abstraction.Contracts.CVGenerationModule;
using Shared.Dtos.CVGenerationModule;
using Shared.Models.CVGenerationModule;
using static Services.Implementations.CVGenerationModule.VisibilityFilterHelper;

namespace Services.Implementations.CVGenerationModule.SectionFilters.Prizes
{
    public class PrizesAndRewardsVisibilityFilter : ICVSectionVisibilityFilter
    {
        public void Apply(CVResponseDTO response, CVVisibilityConfig config)
        {
            var settings = config.PrizesAndRewards;

            if (!settings.ShowPrizesAndRewards)
            {
                response.PrizesAndRewards.Clear();
                return;
            }

            if (!settings.ShowPrizeName && !settings.ShowawardingAuthority && !settings.ShowDateReceived )
            {
                response.PrizesAndRewards.Clear();
                return;
            }

            foreach (var par in response.PrizesAndRewards ?? [])
            {
                HideIfFalse(settings.ShowPrizeName, () => par.Prize = null!);
                HideIfFalse(settings.ShowawardingAuthority, () => par.AwardingAuthority = null!);
                HideIfFalse(settings.ShowDateReceived, () => par.DateReceived = null!);
            }
        }
    }
}
