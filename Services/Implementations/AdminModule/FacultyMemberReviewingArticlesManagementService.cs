using Services.Abstraction.Contracts.AdminModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ProjectsAndComiteesModule;
using Shared.Dtos.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Services.Implementations.AdminModule
{
    public class FacultyMemberReviewingArticlesManagementService(IReviewingArticlesHelper _helper)
        :IFacultyMemberReviewingArticlesManagementService
    {
        public Task<PaginatedResult<ReviewingArticlesDto>> GetFacultyMemberReviewingArticlesAsync(
         ReviewingArticlesSpecificationsParameters parameters,
         string facultyMemberEmail)
         => _helper.GetAllReviewingArticlesAsync(parameters, facultyMemberEmail);

        public Task<ReviewingArticlesDto> GetFacultyMemberReviewingArticleByIdAsync(int id)
            => _helper.GetReviewingArticleByIdAsync(id);

        public Task<ReviewingArticlesDto> CreateFacultyMemberReviewingArticleAsync(
            ReviewingArticleCreateDto reviewingArticleCreateDto,
            string facultyMemberEmail)
            => _helper.CreateReviewingArticleAsync(reviewingArticleCreateDto, facultyMemberEmail);

        public Task<ReviewingArticlesDto> UpdateFacultyMemberReviewingArticleAsync(
            int reviewingArticleId,
            ReviewArticleUpdateDto reviewingArticleUpdateDto)
            => _helper.UpdateReviewingArticleAsync(reviewingArticleId, reviewingArticleUpdateDto);

        public Task DeleteFacultyMemberReviewingArticleAsync(int reviewingArticleId)
            => _helper.DeleteReviewingArticleAsync(reviewingArticleId);
    }
}
