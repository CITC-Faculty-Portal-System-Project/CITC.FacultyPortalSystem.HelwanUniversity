using Services.Abstraction.Contracts.CVGenerationModule;
using Shared.Dtos.CVGenerationModule;
using Shared.Models.CVGenerationModule;
using static Services.Implementations.CVGenerationModule.VisibilityFilterHelper;

namespace Services.Implementations.CVGenerationModule.SectionFilters.Contributions
{
    public class ContributionsToUniversityVisibilityFilter : ICVSectionVisibilityFilter
    {
        public void Apply(CVResponseDTO response, CVVisibilityConfig config)
        {
            var settings = config.ContributionsToUniversity;

            if (!settings.ShowContributionsToUniversity)
            {
                response.ContributionsToUniversity.Clear();
                return;
            }

            if (!settings.ShowContributionTitle && !settings.ShowTypeOfContribution && !settings.ShowDateOfContribution)
            {
                response.ContributionsToUniversity.Clear();
                return;
            }

            foreach (var cics in response.ContributionsToUniversity ?? [])
            {
                HideIfFalse(settings.ShowContributionTitle, () => cics.ContributionTitle = null!);
                HideIfFalse(settings.ShowDateOfContribution, () => cics.DateOfContribution = null!);
                HideIfFalse(settings.ShowTypeOfContribution, () => cics.TypeOfContribution = null!);
            }
        }
    }
}
