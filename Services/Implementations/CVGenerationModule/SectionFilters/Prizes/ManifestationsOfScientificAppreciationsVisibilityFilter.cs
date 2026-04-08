using Services.Abstraction.Contracts.CVGenerationModule;
using Shared.Dtos.CVGenerationModule;
using Shared.Models.CVGenerationModule;
using static Services.Implementations.CVGenerationModule.VisibilityFilterHelper;

namespace Services.Implementations.CVGenerationModule.SectionFilters.Prizes
{
    public class ManifestationsOfScientificAppreciationsVisibilityFilter : ICVSectionVisibilityFilter
    {
        public void Apply(CVResponseDTO response, CVVisibilityConfig config, bool isPublic = false)
        {
            var settings = config.ManifestationsOfScientificAppreciation;

            if (!settings.ShowManifestationsOfScientificAppreciation && isPublic == false)
            {
                response.ManifestationsOfScientificAppreciation.Clear();
                return;
            }

            if (!settings.ShowTitleOfAppreciation && !settings.ShowIssuingAuthority && !settings.ShowDateOfAppreciation && isPublic == false)
            {
                response.ManifestationsOfScientificAppreciation.Clear();
                return;
            }

            foreach (var m in response.ManifestationsOfScientificAppreciation ?? [])
            {
                
                if(isPublic == true)
                {
                    HideIfFalse(settings.ShowTitleOfAppreciationForPublic, () => m.TitleOfAppreciation = null!);
                    HideIfFalse(settings.ShowIssuingAuthorityForPublic, () => m.IssuingAuthority = null!);
                    HideIfFalse(settings.ShowDateOfAppreciationForPublic, () => m.DateOfAppreciation = null!);

                }

                HideIfFalse(settings.ShowTitleOfAppreciation, () => m.TitleOfAppreciation = null!);
                HideIfFalse(settings.ShowIssuingAuthority, () => m.IssuingAuthority = null!);
                HideIfFalse(settings.ShowDateOfAppreciation, () => m.DateOfAppreciation = null!);

            }
        }
    }
}
