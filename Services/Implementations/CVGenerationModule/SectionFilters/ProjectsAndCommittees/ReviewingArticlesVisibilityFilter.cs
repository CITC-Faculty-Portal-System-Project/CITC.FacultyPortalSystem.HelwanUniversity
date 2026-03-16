using Services.Abstraction.Contracts.CVGenerationModule;
using Shared.Dtos.CVGenerationModule;
using Shared.Models.CVGenerationModule;
using static Services.Implementations.CVGenerationModule.VisibilityFilterHelper;

namespace Services.Implementations.CVGenerationModule.SectionFilters.ProjectsAndCommittees
{
    public class ReviewingArticlesVisibilityFilter : ICVSectionVisibilityFilter
    {
        public void Apply(CVResponseDTO response, CVVisibilityConfig config)
        {
            var settings = config.ReviewingArticles;

            if (!settings.ShowReviewingArticles)
            {
                response.ReviewingArticles.Clear();
                return;
            }

            if(!settings.ShowTitleOfArticle && !settings.ShowAuthority && !settings.ShowReviewingDate)
            {
                response.ReviewingArticles.Clear();
                return;
            }

            foreach(var ra in response.ReviewingArticles ?? [])
            {
                HideIfFalse(settings.ShowTitleOfArticle, () => ra.TitleOfArticle = null!);
                HideIfFalse(settings.ShowAuthority, () => ra.Authority = null!);
                HideIfFalse(settings.ShowReviewingDate, () => ra.ReviewingDate = null!);
            }
        }
    }
}
