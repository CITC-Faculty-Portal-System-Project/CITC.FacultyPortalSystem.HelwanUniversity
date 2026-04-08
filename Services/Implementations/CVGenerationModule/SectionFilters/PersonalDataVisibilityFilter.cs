using Services.Abstraction.Contracts.CVGenerationModule;
using Shared.Dtos.CVGenerationModule;
using Shared.Models.CVGenerationModule;
using static Services.Implementations.CVGenerationModule.VisibilityFilterHelper;

namespace Services.Implementations.CVGenerationModule.DataFilters
{
    public class PersonalDataVisibilityFilter : ICVSectionVisibilityFilter
    {
        public void Apply(CVResponseDTO response, CVVisibilityConfig config , bool isPublic = false)
        {
            var settings = config.PersonalData;

            if (!settings.ShowPersonalData && isPublic == false)
            {
                response.University = null;
                response.Authority = null;
                response.Department = null;
                response.BirthDate = null;
                //response.ProfilePictureId = null;
                return;
            }

            if (!settings.ShowProfilePictureForPublic)
            {
                response.University = null;
                response.Authority = null;
                response.Department = null;
                response.BirthDate = null;
                //response.ProfilePictureId = null;
                return;
            }

            if(isPublic == true)
            {
                HideIfFalse(settings.ShowUniversityForPublic, () => response.University = null);
                HideIfFalse(settings.ShowAuthorityForPublic, () => response.Authority = null);
                HideIfFalse(settings.ShowDepartmentForPublic, () => response.Department = null);
                HideIfFalse(settings.ShowBirthDateForPublic, () => response.BirthDate = null);

            }

            HideIfFalse(settings.ShowUniversity, () => response.University = null);
            HideIfFalse(settings.ShowAuthority, () => response.Authority = null);
            HideIfFalse(settings.ShowDepartment, () => response.Department = null);
            HideIfFalse(settings.ShowBirthDate, () => response.BirthDate = null);
            //HideIfFalse(settings.ShowProfilePicture, () => response.ProfilePictureId = null);
        }
    }
}
