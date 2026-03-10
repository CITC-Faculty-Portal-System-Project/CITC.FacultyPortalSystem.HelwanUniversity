using Domain.Entities.AcademicDataModule.ProjectsAndCommitteesModule;
using Services.Abstraction.Contracts.AcademicDataModule.ProjectsAndCommitteesModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.Dtos.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Services.Implementations.AcademicDataModule.ProjectsAndCommitteesModule
{
    public class ReviewingArticlesService(
        IUnitOfWork unitOfWork,
        IAuthenticationService authenticationService,
        IMapper mapper)
        : BaseService<ReviewingArticles, int>(unitOfWork, authenticationService, mapper),
          IReviewingArticlesService
    {
        protected override string EntityName => "Reviewing Articles";

        public async Task<PaginatedResult<ReviewingArticlesDto>> GetAllReviewingArticlesAsync(
            ReviewingArticlesSpecificationsParameters parameters,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            var reviewingArticles = await Repo.GetAllAsync(
                new ReviewingArticlesSpecifications(parameters, email))
                ?? throw NotFound();

            var mapped = Mapper.Map<IEnumerable<ReviewingArticlesDto>>(reviewingArticles);

            var totalCount = await Repo.CountAsync(
                new ReviewingArticlesCountSpecifications(parameters, email));

            return new PaginatedResult<ReviewingArticlesDto>(
                parameters.PageIndex,
                mapped.Count(),
                totalCount,
                mapped);
        }

        public async Task<ReviewingArticlesDto> GetReviewingArticleByIdAsync(
            int id,
            string? facultyMemberEmail = null)
        {
            var reviewingArticle = await Repo.GetAsync(
                new ReviewingArticlesSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                reviewingArticle.FacultyMemberId,
                facultyMemberEmail);

            return Mapper.Map<ReviewingArticlesDto>(reviewingArticle);
        }

        public async Task<ReviewingArticlesDto> CreateReviewingArticleAsync(
            ReviewingArticleCreateDto dto,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            var facultyMember = await GetFacultyMemberByEmailAsync(email);

            var reviewingArticle = Mapper.Map<ReviewingArticles>(dto);
            reviewingArticle.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(reviewingArticle);
            await SaveChangesAsync();

            return Mapper.Map<ReviewingArticlesDto>(reviewingArticle);
        }

        public async Task<ReviewingArticlesDto> UpdateReviewingArticleAsync(
            int id,
            ReviewArticleUpdateDto dto,
            string? facultyMemberEmail = null)
        {
            var reviewingArticle = await Repo.GetAsync(
                new ReviewingArticlesSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                reviewingArticle.FacultyMemberId,
                facultyMemberEmail);

            Mapper.Map(dto, reviewingArticle);

            Repo.Update(reviewingArticle);
            await SaveChangesAsync();

            return Mapper.Map<ReviewingArticlesDto>(reviewingArticle);
        }

        public async Task DeleteReviewingArticleAsync(
            int id,
            string? facultyMemberEmail = null)
        {
            var reviewingArticle = await Repo.GetAsync(
                new ReviewingArticlesSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                reviewingArticle.FacultyMemberId,
                facultyMemberEmail);

            reviewingArticle.IsDeleted = true;

            Repo.Update(reviewingArticle);
            await SaveChangesAsync();
        }
    }
}
