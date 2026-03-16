using Services.Abstraction.Contracts.CVGenerationModule;
using Shared.Dtos.CVGenerationModule;
using Shared.Models.CVGenerationModule;
using static Services.Implementations.CVGenerationModule.VisibilityFilterHelper;

namespace Services.Implementations.CVGenerationModule.SectionFilters.Experiences
{
    public class GeneralExperiencesVisibilityFilter : ICVSectionVisibilityFilter
    {
        public void Apply(CVResponseDTO response, CVVisibilityConfig config)
        {
            var settings = config.GeneralExperiences;

            if (!settings.ShowGeneralExperiences)
            {
                response.GeneralExperiences.Clear();
                return;
            }

            if (!settings.ShowExperienceTitle && !settings.ShowAuthority && !settings.ShowCountryOrCity && !settings.ShowStartDate && !settings.ShowEndDate)
            {
                response.GeneralExperiences.Clear();
                return;
            }

            foreach(var ge in response.GeneralExperiences ?? [])
            {
                HideIfFalse(settings.ShowExperienceTitle, () => ge.ExperienceTitle = null!);
                HideIfFalse(settings.ShowAuthority, () => ge.Authority = null!);
                HideIfFalse(settings.ShowCountryOrCity, () => ge.CountryOrCity = null!);
                HideIfFalse(settings.ShowStartDate, () => ge.StartDate = null);
                HideIfFalse(settings.ShowEndDate, () => ge.EndDate = null);
            }
        }
    }
}
