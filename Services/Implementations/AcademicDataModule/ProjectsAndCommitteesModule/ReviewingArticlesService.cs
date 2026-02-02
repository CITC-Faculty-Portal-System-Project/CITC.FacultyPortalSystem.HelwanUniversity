using Domain.Entities.AcademicDataModule.ProjectsAndCommitteesModule;
using Services.Abstraction.Contracts.AcademicDataModule.ProjectsAndCommitteesModule;
using Services.Global;
using Services.Specifications.ProjectsAndCommitteesModule;
using Shared.Dtos.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.ProjectsAndCommitteesModule;

namespace Services.Implementations.AcademicDataModule.ProjectsAndCommitteesModule
{
    public class ReviewingArticlesService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IAuthenticationService authenticationService)
                : BaseService<ReviewingArticles, int>(unitOfWork, authenticationService, mapper), IReviewingArticlesService
    {
        protected override string EntityName => "Reviewing Articles";
        public async Task<PaginatedResult<ReviewingArticlesDto>> GetAllReviewingArticlesAsync(ReviewingArticlesSpecificationsParameters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            var reviewingArticles = await Repo.GetAllAsync(new ReviewingArticlesSpecifications(parameters, currentUser.Email))
                ?? throw NotFound();

            var reviewingArticlesResult = Mapper.Map<IEnumerable<ReviewingArticlesDto>>(reviewingArticles);

            var currentPageCount = reviewingArticles.Count();

            var totalCount = await Repo.CountAsync(new ReviewingArticlesCountSpecifications(parameters, currentUser.Email));

            return new PaginatedResult<ReviewingArticlesDto>(parameters.PageIndex, currentPageCount, totalCount, reviewingArticlesResult);
        }

        public async Task<ReviewingArticlesDto> GetReviewingArticleByIdAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var reviewingArticle = await Repo.GetAsync(new ReviewingArticlesSpecifications(id))
                ?? throw NotFound();

            EnsureOwnership(reviewingArticle.FacultyMemberId, currentUser.UserId, EntityName);

            return Mapper.Map<ReviewingArticlesDto>(reviewingArticle);
        }

        public async Task<ReviewingArticlesDto> CreateReviewingArticleAsync(ReviewingArticleCreateDto reviewingArticleCreateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var reviewingArticle = Mapper.Map<ReviewingArticles>(reviewingArticleCreateDto);
            reviewingArticle.FacultyMemberId = currentUser.UserId;

            await Repo.AddAsync(reviewingArticle);
            await SaveChangesAsync();

            return Mapper.Map<ReviewingArticlesDto>(reviewingArticle);
        }

        public async Task<ReviewingArticlesDto> UpdateReviewingArticleAsync(int reviewingArticleId, ReviewArticleUpdateDto reviewingArticleUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var reviewingArticle = await Repo.GetAsync(new ReviewingArticlesSpecifications(reviewingArticleId))
                ?? throw NotFound();

            EnsureOwnership(reviewingArticle.FacultyMemberId, currentUser.UserId, EntityName);

            Mapper.Map(reviewingArticleUpdateDto, reviewingArticle);

            Repo.Update(reviewingArticle);
            await SaveChangesAsync();

            return Mapper.Map<ReviewingArticlesDto>(reviewingArticle);
        }

        public async Task DeleteReviewingArticleAsync(int reviewingArticleId)
        {
            var currentUser = await GetCurrentUserAsync();

            var reviewingArticle = await Repo.GetAsync(new ReviewingArticlesSpecifications(reviewingArticleId))
                ?? throw NotFound();

            EnsureOwnership(reviewingArticle.FacultyMemberId, currentUser.UserId, EntityName);

            reviewingArticle.IsDeleted = true;

            Repo.Update(reviewingArticle);
            await SaveChangesAsync();
        }
    }
}
