using Domain.Entities.AcademicDataModule.ProjectsAndCommitteesModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ProjectsAndComiteesModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.Dtos.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Services.Implementations.SharedLogicBetweenAdminAndFacultyMember.ProjectsAndComiteesModule
{
    public class ReviewingArticlesHelper(
      IUnitOfWork unitOfWork,
      IAuthenticationService authenticationService,
      IMapper mapper)
      : BaseService<ReviewingArticles, int>(unitOfWork, authenticationService, mapper),
        IReviewingArticlesHelper
    {
        protected override string EntityName => "Reviewing Articles";

        public async Task<PaginatedResult<ReviewingArticlesDto>> GetAllReviewingArticlesAsync(
            ReviewingArticlesSpecificationsParameters parameters,
            string facultyMemberEmail)
        {
            var reviewingArticles = await Repo.GetAllAsync(
                new ReviewingArticlesSpecifications(parameters, facultyMemberEmail));

            var reviewingArticlesResult =
                Mapper.Map<IEnumerable<ReviewingArticlesDto>>(reviewingArticles);

            var currentPageCount = reviewingArticlesResult.Count();

            var totalCount = await Repo.CountAsync(
                new ReviewingArticlesCountSpecifications(parameters, facultyMemberEmail));

            return new PaginatedResult<ReviewingArticlesDto>(
                parameters.PageIndex,
                currentPageCount,
                totalCount,
                reviewingArticlesResult);
        }

        public async Task<ReviewingArticlesDto> GetReviewingArticleByIdAsync(int id)
        {
            var reviewingArticle = await Repo.GetAsync(new ReviewingArticlesSpecifications(id))
                ?? throw NotFound();

            return Mapper.Map<ReviewingArticlesDto>(reviewingArticle);
        }

        public async Task<ReviewingArticlesDto> CreateReviewingArticleAsync(
            ReviewingArticleCreateDto reviewingArticleCreateDto,
            string facultyMemberEmail)
        {
            var facultyMember = await GetFacultyMemberByEmailAsync(facultyMemberEmail);

            var reviewingArticle = Mapper.Map<ReviewingArticles>(reviewingArticleCreateDto);
            reviewingArticle.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(reviewingArticle);
            await SaveChangesAsync();

            return Mapper.Map<ReviewingArticlesDto>(reviewingArticle);
        }

        public async Task<ReviewingArticlesDto> UpdateReviewingArticleAsync(
            int reviewingArticleId,
            ReviewArticleUpdateDto reviewingArticleUpdateDto)
        {
            var reviewingArticle = await Repo.GetAsync(
                new ReviewingArticlesSpecifications(reviewingArticleId))
                ?? throw NotFound();

            Mapper.Map(reviewingArticleUpdateDto, reviewingArticle);

            Repo.Update(reviewingArticle);
            await SaveChangesAsync();

            return Mapper.Map<ReviewingArticlesDto>(reviewingArticle);
        }

        public async Task DeleteReviewingArticleAsync(int reviewingArticleId)
        {
            var reviewingArticle = await Repo.GetAsync(
                new ReviewingArticlesSpecifications(reviewingArticleId))
                ?? throw NotFound();

            reviewingArticle.IsDeleted = true;

            Repo.Update(reviewingArticle);
            await SaveChangesAsync();
        }
    }
}
