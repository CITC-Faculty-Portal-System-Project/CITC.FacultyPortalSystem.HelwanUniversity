using Services.Abstraction.Contracts.CVGenerationModule;
using Shared.Dtos.CVGenerationModule;
using Shared.Models.CVGenerationModule;
using static Services.Implementations.CVGenerationModule.VisibilityFilterHelper;

namespace Services.Implementations.CVGenerationModule.SectionFilters
{
    public class SocialMediaVisibilityFilter : ICVSectionVisibilityFilter
    {
        public void Apply(CVResponseDTO response, CVVisibilityConfig config, bool isPublic)
        {
            var settings = config.SocialMedia;

            if (!settings.ShowSocialMedia && isPublic == false)
            {
                response.LinkedIn = null;
                response.Facebook = null;
                response.GoogleScholar = null;
                response.Scopus = null;
                response.Instagram = null;
                response.X = null;
                response.YouTube = null;
                response.PersonalWebsite = null;
                return;
            }

            
            if(isPublic == true)
            {
                HideIfFalse(settings.ShowLinkedInForPublic, () => response.LinkedIn = null);
                HideIfFalse(settings.ShowFacebookForPublic, () => response.Facebook = null);
                HideIfFalse(settings.ShowGoogleScholarForPublic, () => response.GoogleScholar = null);
                HideIfFalse(settings.ShowInstagramForPublic, () => response.Instagram = null);
                HideIfFalse(settings.ShowScopusForPublic, () => response.Scopus = null);
                HideIfFalse(settings.ShowPersonalWebsiteForPublic, () => response.PersonalWebsite = null);
                HideIfFalse(settings.ShowYouTubeForPublic, () => response.YouTube = null);
                HideIfFalse(settings.ShowXForPublic, () => response.X = null);

            }

            HideIfFalse(settings.ShowLinkedIn, () => response.LinkedIn = null);
            HideIfFalse(settings.ShowFacebook, () => response.Facebook = null);
            HideIfFalse(settings.ShowGoogleScholar, () => response.GoogleScholar = null);
            HideIfFalse(settings.ShowInstagram, () => response.Instagram = null);
            HideIfFalse(settings.ShowScopus, () => response.Scopus = null);
            HideIfFalse(settings.ShowPersonalWebsite, () => response.PersonalWebsite = null);
            HideIfFalse(settings.ShowYouTube, () => response.YouTube = null);
            HideIfFalse(settings.ShowX, () => response.X = null);
        }
    }
}
