using Domain.Entities.AcademicDataModule.ProjectsAndCommitteesModule;
using Services.Abstraction.Contracts.AcademicDataModule.ProjectsAndCommitteesModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ProjectsAndComiteesModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.Dtos.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Services.Implementations.AcademicDataModule.ProjectsAndCommitteesModule
{
    public class ReviewingArticlesService(
     IUnitOfWork unitOfWork,
     IMapper mapper,
     IAuthenticationService authenticationService,
     IReviewingArticlesHelper reviewingArticlesHelper)
     : BaseService<ReviewingArticles, int>(unitOfWork, authenticationService, mapper),
       IReviewingArticlesService
    {
        private readonly IReviewingArticlesHelper _helper = reviewingArticlesHelper;

        protected override string EntityName => "Reviewing Articles";

        public async Task<PaginatedResult<ReviewingArticlesDto>> GetAllReviewingArticlesAsync(
            ReviewingArticlesSpecificationsParameters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            return await _helper.GetAllReviewingArticlesAsync(parameters, currentUser.Email);
        }

        public async Task<ReviewingArticlesDto> GetReviewingArticleByIdAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var reviewingArticle = await Repo.GetAsync(new ReviewingArticlesSpecifications(id))
                ?? throw NotFound();

            EnsureOwnership(reviewingArticle.FacultyMemberId, currentUser.UserId, EntityName);

            return await _helper.GetReviewingArticleByIdAsync(id);
        }

        public async Task<ReviewingArticlesDto> CreateReviewingArticleAsync(
            ReviewingArticleCreateDto reviewingArticleCreateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            return await _helper.CreateReviewingArticleAsync(reviewingArticleCreateDto, currentUser.Email);
        }

        public async Task<ReviewingArticlesDto> UpdateReviewingArticleAsync(
            int reviewingArticleId,
            ReviewArticleUpdateDto reviewingArticleUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var reviewingArticle = await Repo.GetAsync(new ReviewingArticlesSpecifications(reviewingArticleId))
                ?? throw NotFound();

            EnsureOwnership(reviewingArticle.FacultyMemberId, currentUser.UserId, EntityName);

            return await _helper.UpdateReviewingArticleAsync(reviewingArticleId, reviewingArticleUpdateDto);
        }

        public async Task DeleteReviewingArticleAsync(int reviewingArticleId)
        {
            var currentUser = await GetCurrentUserAsync();

            var reviewingArticle = await Repo.GetAsync(new ReviewingArticlesSpecifications(reviewingArticleId))
                ?? throw NotFound();

            EnsureOwnership(reviewingArticle.FacultyMemberId, currentUser.UserId, EntityName);

            await _helper.DeleteReviewingArticleAsync(reviewingArticleId);
        }
    }
}
