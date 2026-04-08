using Services.Abstraction.Contracts.CVGenerationModule;
using Shared.Dtos.CVGenerationModule;
using Shared.Models.CVGenerationModule;
using static Services.Implementations.CVGenerationModule.VisibilityFilterHelper;

namespace Services.Implementations.CVGenerationModule.SectionFilters.Experiences
{
    public class GeneralExperiencesVisibilityFilter : ICVSectionVisibilityFilter
    {
        public void Apply(CVResponseDTO response, CVVisibilityConfig config, bool isPublic = false)
        {
            var settings = config.GeneralExperiences;

            if (!settings.ShowGeneralExperiences)
            {
                response.GeneralExperiences.Clear();
                return;
            }

            if (!settings.ShowExperienceTitle && !settings.ShowAuthority && !settings.ShowCountryOrCity && !settings.ShowStartDate && !settings.ShowEndDate && isPublic == false)
            {
                response.GeneralExperiences.Clear();
                return;
            }

            foreach(var ge in response.GeneralExperiences ?? [])
            {
                
                
                if(isPublic == true)
                {
                    HideIfFalse(settings.ShowExperienceTitleForPublic, () => ge.ExperienceTitle = null!);
                    HideIfFalse(settings.ShowAuthorityForPublic, () => ge.Authority = null!);
                    HideIfFalse(settings.ShowCountryOrCityForPublic, () => ge.CountryOrCity = null!);
                    HideIfFalse(settings.ShowStartDateForPublic, () => ge.StartDate = null);
                    HideIfFalse(settings.ShowEndDateForPublic, () => ge.EndDate = null);
                }

                HideIfFalse(settings.ShowExperienceTitle, () => ge.ExperienceTitle = null!);
                HideIfFalse(settings.ShowAuthority, () => ge.Authority = null!);
                HideIfFalse(settings.ShowCountryOrCity, () => ge.CountryOrCity = null!);
                HideIfFalse(settings.ShowStartDate, () => ge.StartDate = null);
                HideIfFalse(settings.ShowEndDate, () => ge.EndDate = null);
            }
        }
    }
}
