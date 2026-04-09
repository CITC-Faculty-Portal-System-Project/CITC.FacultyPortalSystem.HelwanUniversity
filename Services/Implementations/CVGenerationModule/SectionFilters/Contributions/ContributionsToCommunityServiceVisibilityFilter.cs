using Services.Abstraction.Contracts.CVGenerationModule;
using Shared.Dtos.CVGenerationModule;
using Shared.Models.CVGenerationModule;
using static Services.Implementations.CVGenerationModule.VisibilityFilterHelper;

namespace Services.Implementations.CVGenerationModule.SectionFilters.Contributions
{
    public class ContributionsToCommunityServiceVisibilityFilter : ICVSectionVisibilityFilter
    {
        public void Apply(CVResponseDTO response, CVVisibilityConfig config , bool isPublic = false)
        {
            var settings = config.ContributionsToCommunityService;

            if (!settings.ShowContributionsToCommunityService && isPublic == false)
            {
                response.ContributionsToCommunityService.Clear();
                return;
            }

            if (!settings.ShowContributionTitle && !settings.ShowDateOfContribution && isPublic == false)
            {
                response.ContributionsToCommunityService.Clear();
                return;
            }

            foreach (var cics in response.ContributionsToCommunityService ?? [])
            {
                HideIfFalse(settings.ShowContributionTitle, () => cics.ContributionTitle = null!);
                
                if(isPublic == true)
                {
                    HideIfFalse(settings.ShowContributionTitleForPublic, () => cics.ContributionTitle = null!);
                    HideIfFalse(settings.ShowDateOfContributionForPublic, () => cics.DateOfContribution = null!);
                 }

                HideIfFalse(settings.ShowDateOfContribution, () => cics.DateOfContribution = null!);
            }
        }
    }
}
