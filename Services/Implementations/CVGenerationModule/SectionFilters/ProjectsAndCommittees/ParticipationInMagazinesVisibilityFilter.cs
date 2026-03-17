using Services.Abstraction.Contracts.CVGenerationModule;
using Shared.Dtos.CVGenerationModule;
using Shared.Models.CVGenerationModule;
using static Services.Implementations.CVGenerationModule.VisibilityFilterHelper;

namespace Services.Implementations.CVGenerationModule.SectionFilters.ProjectsAndCommittees
{
    public class ParticipationInMagazinesVisibilityFilter : ICVSectionVisibilityFilter
    {
        public void Apply(CVResponseDTO response, CVVisibilityConfig config)
        {
            var settings = config.ParticipationInMagazines;

            if (!settings.ShowParticipationInMagazines)
            {
                response.ParticipationInMagazines.Clear();
                return;
            }

            if (!settings.ShowNameOfMagazine && !settings.ShowWebsiteOfMagazine && !settings.ShowTypeOfParticipation)
            {
                response.ParticipationInMagazines.Clear();
                return;
            }

            foreach(var pm in response.ParticipationInMagazines ?? [])
            {
                HideIfFalse(settings.ShowNameOfMagazine, () => pm.NameOfMagazine = null!);
                HideIfFalse(settings.ShowWebsiteOfMagazine, () => pm.WebsiteOfMagazine = null!);
                HideIfFalse(settings.ShowTypeOfParticipation, () => pm.TypeOfParticipation = null!);
            }
        }
    }
}
