using Services.Abstraction.Contracts.CVGenerationModule;
using Shared.Dtos.CVGenerationModule;
using Shared.Models.CVGenerationModule;
using static Services.Implementations.CVGenerationModule.VisibilityFilterHelper;

namespace Services.Implementations.CVGenerationModule.SectionFilters.ProjectsAndCommittees
{
    public class ParticipationInMagazinesVisibilityFilter : ICVSectionVisibilityFilter
    {
        public void Apply(CVResponseDTO response, CVVisibilityConfig config , bool isPublic)
        {
            var settings = config.ParticipationInMagazines;

            if (!settings.ShowParticipationInMagazines && isPublic == false)
            {
                response.ParticipationInMagazines.Clear();
                return;
            }

            if (!settings.ShowNameOfMagazine && !settings.ShowWebsiteOfMagazine && !settings.ShowTypeOfParticipation && isPublic == false)
            {
                response.ParticipationInMagazines.Clear();
                return;
            }

            foreach(var pm in response.ParticipationInMagazines ?? [])
            {
                
                if(isPublic == true)
                {
                    HideIfFalse(settings.ShowNameOfMagazineForPublic, () => pm.NameOfMagazine = null!);
                    HideIfFalse(settings.ShowWebsiteOfMagazineForPublic, () => pm.WebsiteOfMagazine = null!);
                    HideIfFalse(settings.ShowTypeOfParticipationForPublic, () => pm.TypeOfParticipation = null!);
                }

                HideIfFalse(settings.ShowNameOfMagazine, () => pm.NameOfMagazine = null!);
                HideIfFalse(settings.ShowWebsiteOfMagazine, () => pm.WebsiteOfMagazine = null!);
                HideIfFalse(settings.ShowTypeOfParticipation, () => pm.TypeOfParticipation = null!);
            }
        }
    }
}
