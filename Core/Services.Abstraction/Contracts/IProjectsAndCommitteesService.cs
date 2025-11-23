using Shared.Dtos.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.ProjectsAndCommitteesModule;

namespace Services.Abstraction.Contracts
{
    public interface IProjectsAndCommitteesService
    {
        #region Committees And Associations
        public Task<PaginatedResult<CommitteesAndAssociationsResponseDto>> GetAllCommitteesAndAssociationsAsync(CommitteesAndAssociationsSpecificationsParameters parameters);
        public Task<CommitteesAndAssociationsResponseDto> GetCommitteeOrAssociationById(int id);
        public Task<CommitteesAndAssociationsResponseDto> CreateCommitteeOrAssociationAsync(string facultyMemberEmail, CommitteesAndAssociationsCreateDto committeesAndAssociationsCreateDto);
        public Task<CommitteesAndAssociationsResponseDto> UpdateCommitteeOrAssociationAsync(int committeeOrAssociationId, string facultyMemberEmail, CommitteesAndAssociationsUpdateDto committeesAndAssociationsUpdateDto);
        public Task DeleteCommitteeOrAssociationAsync(int committeeOrAssociationId, string facultyMemberEmail);
        #endregion

        #region Reviewing Articles
        public Task<PaginatedResult<ReviewingArticlesDto>> GetAllReviewingArticlesAsync(ReviewingArticlesSpecificationsParameters parameters);
        public Task<ReviewingArticlesDto> GetReviewingArticleById(int id);
        public Task<ReviewingArticlesDto> CreateReviewingArticleAsync(string facultyMemberEmail, ReviewingArticleCreateDto reviewingArticleCreateDto);
        public Task<ReviewingArticlesDto> UpdateReviewingArticleAsync(int reviewingArticleId, string facultyMemberEmail, ReviewingArticlesDto reviewingArticleUpdateDto);
        public Task DeleteReviewingArticleAsync(int reviewingArticleId, string facultyMemberEmail);
        #endregion
    }
}
