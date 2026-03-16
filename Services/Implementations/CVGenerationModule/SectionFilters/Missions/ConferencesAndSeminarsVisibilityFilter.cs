using Services.Abstraction.Contracts.CVGenerationModule;
using Shared.Dtos.CVGenerationModule;
using Shared.Models.CVGenerationModule;
using static Services.Implementations.CVGenerationModule.VisibilityFilterHelper;

namespace Services.Implementations.CVGenerationModule.SectionFilters.Missions
{
    public class ConferencesAndSeminarsVisibilityFilter : ICVSectionVisibilityFilter
    {
        public void Apply(CVResponseDTO response, CVVisibilityConfig config)
        {
            var settings = config.ConferencesAndSeminars;

            if (!settings.ShowConferencesAndSeminars)
            {
                response.ConferencesAndSeminars.Clear();
                return;
            }

            if (!settings.ShowConferenceOrSeminarVenue && !settings.ShowConferenceOrSeminarName && !settings.ShowConferenceOrSeminarStartDate && !settings.ShowConferenceOrSeminarEndDate && !settings.ShowConferenceOrSeminarOrganizingAuthority && !settings.ShowConferenceOrSeminarWebsite && !settings.ShowConferenceOrSeminarRoleOfParticipation)
            {
                response.ConferencesAndSeminars.Clear();
                return;
            }

            foreach(var cas in response.ConferencesAndSeminars ?? [])
            {
                HideIfFalse(settings.ShowConferenceOrSeminarName, () => cas.Name = null!);
                HideIfFalse(settings.ShowConferenceOrSeminarVenue, () => cas.Venue = null!);
                HideIfFalse(settings.ShowConferenceOrSeminarStartDate, () => cas.StartDate = null);
                HideIfFalse(settings.ShowConferenceOrSeminarEndDate, () => cas.EndDate = null);
                HideIfFalse(settings.ShowConferenceOrSeminarOrganizingAuthority, () => cas.OrganizingAuthority = null!);
                HideIfFalse(settings.ShowConferenceOrSeminarWebsite, () => cas.Website = null!);
                HideIfFalse(settings.ShowConferenceOrSeminarRoleOfParticipation, () => cas.RoleOfParticipation = null!);
            }
        }
    }
}
