using Services.Abstraction.Contracts.CVGenerationModule;
using Shared.Dtos.CVGenerationModule;
using Shared.Models.CVGenerationModule;
using static Services.Implementations.CVGenerationModule.VisibilityFilterHelper;

namespace Services.Implementations.CVGenerationModule.SectionFilters.WritingsAndPatents
{
    public class PatentsVisibilityFilter : ICVSectionVisibilityFilter
    {
        public void Apply(CVResponseDTO response, CVVisibilityConfig config, bool isPublic = false)
        {
            var settings = config.Patents;

            if (!settings.ShowPatents && isPublic == false)
            {
                response.Patents.Clear();
                return;
            }

            if(!settings.ShowNameOfPatent && !settings.ShowAccreditationDate && !settings.ShowAccreditingAuthorityOrCountry && isPublic == false)
            {
                response.Patents.Clear();
                return;
            }

            foreach(var p in response.Patents ?? [])
            {
                
                if(isPublic == true)
                {
                    HideIfFalse(settings.ShowNameOfPatentForPublic, () => p.NameOfPatent = null!);
                    HideIfFalse(settings.ShowAccreditationDateForPublic, () => p.AccreditationDate = null!);
                    HideIfFalse(settings.ShowAccreditingAuthorityOrCountryForPublic, () => p.AccreditingAuthorityOrCountry = null!);

                }

                HideIfFalse(settings.ShowNameOfPatent, () => p.NameOfPatent = null!);
                HideIfFalse(settings.ShowAccreditationDate, () => p.AccreditationDate = null!);
                HideIfFalse(settings.ShowAccreditingAuthorityOrCountry, () => p.AccreditingAuthorityOrCountry = null!);
            }
        }
    }
}
