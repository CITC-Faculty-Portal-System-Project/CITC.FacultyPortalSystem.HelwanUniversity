using Shared.Dtos.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.ProjectsAndCommitteesModule
{
    public interface IReviewingArticlesService
    {
        Task<PaginatedResult<ReviewingArticlesDto>> GetAllReviewingArticlesAsync(
          ReviewingArticlesSpecificationsParameters parameters,
          string? facultyMemberEmail = null);

        Task<ReviewingArticlesDto> GetReviewingArticleByIdAsync(
            int id,
            string? facultyMemberEmail = null);

        Task<ReviewingArticlesDto> CreateReviewingArticleAsync(
            ReviewingArticleCreateDto dto,
            string? facultyMemberEmail = null);

        Task<ReviewingArticlesDto> UpdateReviewingArticleAsync(
            int id,
            ReviewArticleUpdateDto dto,
            string? facultyMemberEmail = null);

        Task DeleteReviewingArticleAsync(
            int id,
            string? facultyMemberEmail = null);
    }
}
