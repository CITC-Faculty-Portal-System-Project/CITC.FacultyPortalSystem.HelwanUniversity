using Services.Abstraction.Contracts.CVGenerationModule;
using Shared.Dtos.CVGenerationModule;
using Shared.Models.CVGenerationModule;
using static Services.Implementations.CVGenerationModule.VisibilityFilterHelper;

namespace Services.Implementations.CVGenerationModule.SectionFilters
{
    public class ContactVisibilityFilter : ICVSectionVisibilityFilter
    {
        public void Apply(CVResponseDTO response, CVVisibilityConfig config , bool isPublic)
        {
            var settings = config.ContactInfo;

            if (!settings.ShowContactInfo && isPublic == false)
            {
                response.OfficialEmail = null;
                response.MainPhoneNumber = null;
                response.WorkPhoneNumber = null;
                response.FaxNumber = null;
                return;
            }

            
            if(isPublic == true)
            {
                HideIfFalse(settings.ShowMainPhoneForPublic, () => response.MainPhoneNumber = null);
                HideIfFalse(settings.ShowWorkPhoneForPublic, () => response.WorkPhoneNumber = null);
                HideIfFalse(settings.ShowOfficialEmailForPublic, () => response.OfficialEmail = null);
                HideIfFalse(settings.ShowFaxForPublic, () => response.FaxNumber = null);
            }

            HideIfFalse(settings.ShowMainPhone, () => response.MainPhoneNumber = null);
            HideIfFalse(settings.ShowWorkPhone, () => response.WorkPhoneNumber = null);
            HideIfFalse(settings.ShowOfficialEmail, () => response.OfficialEmail = null);
            HideIfFalse(settings.ShowFax, () => response.FaxNumber = null);
        }
    }
}
