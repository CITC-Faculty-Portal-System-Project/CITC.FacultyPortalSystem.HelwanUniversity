using Shared.Dtos.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.ProjectsAndCommitteesModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.ProjectsAndCommitteesModule
{
    public interface IReviewingArticlesService
    {
        public Task<PaginatedResult<ReviewingArticlesDto>> GetAllReviewingArticlesAsync(ReviewingArticlesSpecificationsParameters parameters);
        public Task<ReviewingArticlesDto> GetReviewingArticleByIdAsync(int id);
        public Task<ReviewingArticlesDto> CreateReviewingArticleAsync(ReviewingArticleCreateDto reviewingArticleCreateDto);
        public Task<ReviewingArticlesDto> UpdateReviewingArticleAsync(int reviewingArticleId, ReviewArticleUpdateDto reviewingArticleUpdateDto);
        public Task DeleteReviewingArticleAsync(int reviewingArticleId);
    }
}
