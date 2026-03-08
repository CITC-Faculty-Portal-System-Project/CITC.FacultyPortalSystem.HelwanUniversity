using Shared.Dtos.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ProjectsAndComiteesModule
{
    public interface IReviewingArticlesHelper
    {
        Task<PaginatedResult<ReviewingArticlesDto>> GetAllReviewingArticlesAsync(
           ReviewingArticlesSpecificationsParameters parameters,
           string facultyMemberEmail);

        Task<ReviewingArticlesDto> GetReviewingArticleByIdAsync(int id);

        Task<ReviewingArticlesDto> CreateReviewingArticleAsync(
            ReviewingArticleCreateDto reviewingArticleCreateDto,
            string facultyMemberEmail);

        Task<ReviewingArticlesDto> UpdateReviewingArticleAsync(
            int reviewingArticleId,
            ReviewArticleUpdateDto reviewingArticleUpdateDto);

        Task DeleteReviewingArticleAsync(int reviewingArticleId);
    }
}
