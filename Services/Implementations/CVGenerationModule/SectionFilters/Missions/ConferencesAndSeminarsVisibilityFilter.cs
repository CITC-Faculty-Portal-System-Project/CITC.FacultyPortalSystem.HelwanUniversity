using Services.Abstraction.Contracts.CVGenerationModule;
using Shared.Dtos.CVGenerationModule;
using Shared.Models.CVGenerationModule;
using static Services.Implementations.CVGenerationModule.VisibilityFilterHelper;

namespace Services.Implementations.CVGenerationModule.SectionFilters.Missions
{
    public class ConferencesAndSeminarsVisibilityFilter : ICVSectionVisibilityFilter
    {
        public void Apply(CVResponseDTO response, CVVisibilityConfig config , bool isPublic)
        {
            var settings = config.ConferencesAndSeminars;

            if (!settings.ShowConferencesAndSeminars && isPublic == false)
            {
                response.ConferencesAndSeminars.Clear();
                return;
            }

            if (!settings.ShowConferenceOrSeminarVenue && !settings.ShowConferenceOrSeminarName && !settings.ShowConferenceOrSeminarStartDate && !settings.ShowConferenceOrSeminarEndDate && !settings.ShowConferenceOrSeminarOrganizingAuthority && !settings.ShowConferenceOrSeminarWebsite && !settings.ShowConferenceOrSeminarRoleOfParticipation && isPublic == false)
            {
                response.ConferencesAndSeminars.Clear();
                return;
            }

            foreach(var cas in response.ConferencesAndSeminars ?? [])
            {
                
                if(isPublic == true)
                {
                    HideIfFalse(settings.ShowConferenceOrSeminarNameForPublic, () => cas.Name = null!);
                    HideIfFalse(settings.ShowConferenceOrSeminarVenueForPublic, () => cas.Venue = null!);
                    HideIfFalse(settings.ShowConferenceOrSeminarStartDateForPublic, () => cas.StartDate = null);
                    HideIfFalse(settings.ShowConferenceOrSeminarEndDateForPublic, () => cas.EndDate = null);
                    HideIfFalse(settings.ShowConferenceOrSeminarOrganizingAuthorityForPublic, () => cas.OrganizingAuthority = null!);
                    HideIfFalse(settings.ShowConferenceOrSeminarWebsiteForPublic, () => cas.Website = null!);
                    HideIfFalse(settings.ShowConferenceOrSeminarRoleOfParticipationForPublic, () => cas.RoleOfParticipation = null!);

                }


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
