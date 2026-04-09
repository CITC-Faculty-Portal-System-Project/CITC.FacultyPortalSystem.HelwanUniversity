using Services.Abstraction.Contracts.CVGenerationModule;
using Shared.Dtos.CVGenerationModule;
using Shared.Models.CVGenerationModule;
using static Services.Implementations.CVGenerationModule.VisibilityFilterHelper;

namespace Services.Implementations.CVGenerationModule.SectionFilters.ProjectsAndCommittees
{
    public class ReviewingArticlesVisibilityFilter : ICVSectionVisibilityFilter
    {
        public void Apply(CVResponseDTO response, CVVisibilityConfig config , bool isPublic)
        {
            var settings = config.ReviewingArticles;

            if (!settings.ShowReviewingArticles && isPublic == false)
            {
                response.ReviewingArticles.Clear();
                return;
            }

            if(!settings.ShowTitleOfArticle && !settings.ShowAuthority && !settings.ShowReviewingDate && isPublic == false)
            {
                response.ReviewingArticles.Clear();
                return;
            }

            foreach(var ra in response.ReviewingArticles ?? [])
            {
                
                if(isPublic == true)
                {
                    HideIfFalse(settings.ShowTitleOfArticleForPublic, () => ra.TitleOfArticle = null!);
                    HideIfFalse(settings.ShowAuthorityForPublic, () => ra.Authority = null!);
                    HideIfFalse(settings.ShowReviewingDateForPublic, () => ra.ReviewingDate = null!);
                }

                HideIfFalse(settings.ShowTitleOfArticle, () => ra.TitleOfArticle = null!);
                HideIfFalse(settings.ShowAuthority, () => ra.Authority = null!);
                HideIfFalse(settings.ShowReviewingDate, () => ra.ReviewingDate = null!);
            }
        }
    }
}
