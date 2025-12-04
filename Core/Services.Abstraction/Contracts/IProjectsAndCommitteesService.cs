using Shared.Dtos.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.ProjectsAndCommitteesModule;

namespace Services.Abstraction.Contracts
{
    public interface IProjectsAndCommitteesService
    {
        #region Committees And Associations
        public Task<PaginatedResult<CommitteesAndAssociationsResponseDto>> GetAllCommitteesAndAssociationsAsync(CommitteesAndAssociationsSpecificationsParameters parameters);
        public Task<CommitteesAndAssociationsResponseDto> GetCommitteeOrAssociationByIdAsync(int id);
        public Task<CommitteesAndAssociationsResponseDto> CreateCommitteeOrAssociationAsync(CommitteeOrAssociationCreateDto committeeOrAssociationCreateDto);
        public Task<CommitteesAndAssociationsResponseDto> UpdateCommitteeOrAssociationAsync(int committeeOrAssociationId, CommitteeOrAssociationUpdateDto committeesAndAssociationsUpdateDto);
        public Task DeleteCommitteeOrAssociationAsync(int committeeOrAssociationId);
        #endregion

        #region Reviewing Articles
        public Task<PaginatedResult<ReviewingArticlesDto>> GetAllReviewingArticlesAsync(ReviewingArticlesSpecificationsParameters parameters);
        public Task<ReviewingArticlesDto> GetReviewingArticleByIdAsync(int id);
        public Task<ReviewingArticlesDto> CreateReviewingArticleAsync(ReviewingArticleCreateDto reviewingArticleCreateDto);
        public Task<ReviewingArticlesDto> UpdateReviewingArticleAsync(int reviewingArticleId, ReviewArticleUpdateDto reviewingArticleUpdateDto);
        public Task DeleteReviewingArticleAsync(int reviewingArticleId);
        #endregion

        #region Participation In Magazines
        public Task<PaginatedResult<ParticipationInMagazinesResponseDto>> GetAllParticipationInMagazinesAsync(ParticipationInMagazinesSpecificationsParameters parameters);
        public Task<ParticipationInMagazinesResponseDto> GetParticipationInMagazineByIdAsync(int id);
        public Task<ParticipationInMagazinesResponseDto> CreateParticipationInMagazineAsync(ParticipationInMagazineCreateDto participationInMagazinesCreateDto);
        public Task<ParticipationInMagazinesResponseDto> UpdateParticipationInMagazineAsync(int participationInMagazineId, ParticipationInMagazineUpdateDto participationInMagazinesUpdateDto);
        public Task DeleteParticipationInMagazineAsync(int participationInMagazineId);
        #endregion

        #region Projects
        public Task<PaginatedResult<ProjectsResponseDto>> GetAllProjectsAsync(ProjectsSpecifcationsParameters parameters);
        public Task<ProjectsResponseDto> GetProjectByIdAsync(int id);
        public Task<ProjectsResponseDto> CreateProjectAsync(ProjectCreateDto projectCreateDto);
        public Task<ProjectsResponseDto> UpdateProjectAsync(int projectId, ProjectUpdateDto projectUpdateDto);
        public Task DeleteProjectAsync(int projectId);
        #endregion
    }
}