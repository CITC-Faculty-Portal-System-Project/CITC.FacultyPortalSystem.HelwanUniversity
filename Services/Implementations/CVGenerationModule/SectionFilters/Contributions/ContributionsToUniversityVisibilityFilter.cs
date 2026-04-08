using Services.Abstraction.Contracts.CVGenerationModule;
using Shared.Dtos.CVGenerationModule;
using Shared.Models.CVGenerationModule;
using static Services.Implementations.CVGenerationModule.VisibilityFilterHelper;

namespace Services.Implementations.CVGenerationModule.SectionFilters.Contributions
{
    public class ContributionsToUniversityVisibilityFilter : ICVSectionVisibilityFilter
    {
        public void Apply(CVResponseDTO response, CVVisibilityConfig config, bool isPublic = false)
        {
            var settings = config.ContributionsToUniversity;

            if (!settings.ShowContributionsToUniversity && isPublic == false)
            {
                response.ContributionsToUniversity.Clear();
                return;
            }

            if (!settings.ShowContributionTitle && !settings.ShowTypeOfContribution && !settings.ShowDateOfContribution && isPublic == false)
            {
                response.ContributionsToUniversity.Clear();
                return;
            }

            foreach (var cics in response.ContributionsToUniversity ?? [])
            {
                
                if(isPublic == true)
                {
                    HideIfFalse(settings.ShowContributionTitleForPublic, () => cics.ContributionTitle = null!);
                    HideIfFalse(settings.ShowDateOfContributionForPublic, () => cics.DateOfContribution = null!);
                    HideIfFalse(settings.ShowTypeOfContributionForPublic, () => cics.TypeOfContribution = null!);

                }

                HideIfFalse(settings.ShowContributionTitle, () => cics.ContributionTitle = null!);
                HideIfFalse(settings.ShowDateOfContribution, () => cics.DateOfContribution = null!);
                HideIfFalse(settings.ShowTypeOfContribution, () => cics.TypeOfContribution = null!);
            }
        }
    }
}
