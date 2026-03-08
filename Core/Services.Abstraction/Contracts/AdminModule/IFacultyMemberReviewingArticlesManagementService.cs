using Shared.Dtos.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Services.Abstraction.Contracts.AdminModule
{
    public interface IFacultyMemberReviewingArticlesManagementService
    {
        Task<PaginatedResult<ReviewingArticlesDto>> GetFacultyMemberReviewingArticlesAsync(
           ReviewingArticlesSpecificationsParameters parameters,
           string facultyMemberEmail);

        Task<ReviewingArticlesDto> GetFacultyMemberReviewingArticleByIdAsync(int id);

        Task<ReviewingArticlesDto> CreateFacultyMemberReviewingArticleAsync(
            ReviewingArticleCreateDto reviewingArticleCreateDto,
            string facultyMemberEmail);

        Task<ReviewingArticlesDto> UpdateFacultyMemberReviewingArticleAsync(
            int reviewingArticleId,
            ReviewArticleUpdateDto reviewingArticleUpdateDto);

        Task DeleteFacultyMemberReviewingArticleAsync(int reviewingArticleId);
    }
}
