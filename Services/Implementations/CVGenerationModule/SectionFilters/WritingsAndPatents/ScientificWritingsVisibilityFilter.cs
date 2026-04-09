using Services.Abstraction.Contracts.CVGenerationModule;
using Shared.Dtos.CVGenerationModule;
using Shared.Models.CVGenerationModule;
using static Services.Implementations.CVGenerationModule.VisibilityFilterHelper;

namespace Services.Implementations.CVGenerationModule.SectionFilters.WritingsAndPatents
{
    public class ScientificWritingsVisibilityFilter : ICVSectionVisibilityFilter
    {
        public void Apply(CVResponseDTO response, CVVisibilityConfig config, bool isPublic = false)
        {
            var settings = config.ScientificWritings;

            if (!settings.ShowScientificWritings && isPublic == false)
            {
                response.ScientificWritings.Clear();
                return;
            }

            if(!settings.ShowTitle && !settings.ShowPublishingHouse && !settings.ShowAuthorRole && !settings.ShowISBN && !settings.ShowPublishingDate && isPublic == false)
            {
                response.ScientificWritings.Clear();
                return;
            }

            foreach(var sw in response.ScientificWritings ?? [])
            {
                
                if(isPublic == true)
                {
                    HideIfFalse(settings.ShowTitleForPublic, () => sw.Title = null!);
                    HideIfFalse(settings.ShowAuthorRoleForPublic, () => sw.AuthorRole = null!);
                    HideIfFalse(settings.ShowISBNForPublic, () => sw.ISBN = null!);
                    HideIfFalse(settings.ShowPublishingHouseForPublic, () => sw.PublishingHouse = null!);
                    HideIfFalse(settings.ShowPublishingDateForPublic, () => sw.PublishingDate = null!);
                }


                HideIfFalse(settings.ShowTitle, () => sw.Title = null!);
                HideIfFalse(settings.ShowAuthorRole, () => sw.AuthorRole = null!);
                HideIfFalse(settings.ShowISBN, () => sw.ISBN = null!);
                HideIfFalse(settings.ShowPublishingHouse, () => sw.PublishingHouse = null!);
                HideIfFalse(settings.ShowPublishingDate, () => sw.PublishingDate = null!);
            }
        }
    }
}
