using Services.Abstraction.Contracts.CVGenerationModule;
using Shared.Dtos.CVGenerationModule;
using Shared.Models.CVGenerationModule;
using static Services.Implementations.CVGenerationModule.VisibilityFilterHelper;

namespace Services.Implementations.CVGenerationModule.SectionFilters.Prizes
{
    public class PrizesAndRewardsVisibilityFilter : ICVSectionVisibilityFilter
    {
        public void Apply(CVResponseDTO response, CVVisibilityConfig config , bool isPublic = false)
        {
            var settings = config.PrizesAndRewards;

            if (!settings.ShowPrizesAndRewards && isPublic == false)
            {
                response.PrizesAndRewards.Clear();
                return;
            }

            if (!settings.ShowPrizeName && !settings.ShowawardingAuthority && !settings.ShowDateReceived && isPublic == false )
            {
                response.PrizesAndRewards.Clear();
                return;
            }

            foreach (var par in response.PrizesAndRewards ?? [])
            {
                
                if(isPublic == true)
                {
                    HideIfFalse(settings.ShowPrizeNameForPublic, () => par.Prize = null!);
                    HideIfFalse(settings.ShowawardingAuthorityForPublic, () => par.AwardingAuthority = null!);
                    HideIfFalse(settings.ShowDateReceivedForPublic, () => par.DateReceived = null!);
                }

                HideIfFalse(settings.ShowPrizeName, () => par.Prize = null!);
                HideIfFalse(settings.ShowawardingAuthority, () => par.AwardingAuthority = null!);
                HideIfFalse(settings.ShowDateReceived, () => par.DateReceived = null!);
            }
        }
    }
}
