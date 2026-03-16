using Services.Abstraction.Contracts.CVGenerationModule;
using Shared.Dtos.CVGenerationModule;
using Shared.Models.CVGenerationModule;
using static Services.Implementations.CVGenerationModule.VisibilityFilterHelper;

namespace Services.Implementations.CVGenerationModule.SectionFilters.WritingsAndPatents
{
    public class PatentsVisibilityFilter : ICVSectionVisibilityFilter
    {
        public void Apply(CVResponseDTO response, CVVisibilityConfig config)
        {
            var settings = config.Patents;

            if (!settings.ShowPatents)
            {
                response.Patents.Clear();
                return;
            }

            if(!settings.ShowNameOfPatent && !settings.ShowAccreditationDate && !settings.ShowAccreditingAuthorityOrCountry)
            {
                response.Patents.Clear();
                return;
            }

            foreach(var p in response.Patents ?? [])
            {
                HideIfFalse(settings.ShowNameOfPatent, () => p.NameOfPatent = null!);
                HideIfFalse(settings.ShowAccreditationDate, () => p.AccreditationDate = null!);
                HideIfFalse(settings.ShowAccreditingAuthorityOrCountry, () => p.AccreditingAuthorityOrCountry = null!);
            }
        }
    }
}
