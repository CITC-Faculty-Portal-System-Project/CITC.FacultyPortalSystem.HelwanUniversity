using Services.Abstraction.Contracts.CVGenerationModule;
using Shared.Dtos.CVGenerationModule;
using Shared.Models.CVGenerationModule;
using static Services.Implementations.CVGenerationModule.VisibilityFilterHelper;

namespace Services.Implementations.CVGenerationModule.SectionFilters.Prizes
{
    public class ManifestationsOfScientificAppreciationsVisibilityFilter : ICVSectionVisibilityFilter
    {
        public void Apply(CVResponseDTO response, CVVisibilityConfig config)
        {
            var settings = config.ManifestationsOfScientificAppreciation;

            if (!settings.ShowManifestationsOfScientificAppreciation)
            {
                response.ManifestationsOfScientificAppreciation.Clear();
                return;
            }

            if (!settings.ShowTitleOfAppreciation && !settings.ShowIssuingAuthority && !settings.ShowDateOfAppreciation)
            {
                response.ManifestationsOfScientificAppreciation.Clear();
                return;
            }

            foreach (var m in response.ManifestationsOfScientificAppreciation ?? [])
            {
                HideIfFalse(settings.ShowTitleOfAppreciation, () => m.TitleOfAppreciation = null!);
                HideIfFalse(settings.ShowIssuingAuthority, () => m.IssuingAuthority = null!);
                HideIfFalse(settings.ShowDateOfAppreciation, () => m.DateOfAppreciation = null!);
            }
        }
    }
}
