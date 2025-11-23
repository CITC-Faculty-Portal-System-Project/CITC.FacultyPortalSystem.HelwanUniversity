using Shared.Dtos.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.ProjectsAndCommitteesModule;

namespace Services.Abstraction.Contracts
{
    public interface IProjectsAndCommitteesService
    {
        #region Committees And Associations
        public Task<PaginatedResult<CommitteesAndAssociationsResponseDto>> GetAllCommitteesAndAssociationsAsync(CommitteesAndAssociationsSpecificationsParameters parameters);
        public Task<CommitteesAndAssociationsResponseDto> GetCommitteeOrAssociationByIdAsync(int id);
        public Task<CommitteesAndAssociationsResponseDto> CreateCommitteeOrAssociationAsync(string facultyMemberEmail, CommitteeOrAssociationCreateDto committeeOrAssociationCreateDto);
        public Task<CommitteesAndAssociationsResponseDto> UpdateCommitteeOrAssociationAsync(int committeeOrAssociationId, string facultyMemberEmail, CommitteeOrAssociationUpdateDto committeesAndAssociationsUpdateDto);
        public Task DeleteCommitteeOrAssociationAsync(int committeeOrAssociationId, string facultyMemberEmail);
        #endregion

        #region Reviewing Articles
        public Task<PaginatedResult<ReviewingArticlesDto>> GetAllReviewingArticlesAsync(ReviewingArticlesSpecificationsParameters parameters);
        public Task<ReviewingArticlesDto> GetReviewingArticleByIdAsync(int id);
        public Task<ReviewingArticlesDto> CreateReviewingArticleAsync(string facultyMemberEmail, ReviewingArticleCreateDto reviewingArticleCreateDto);
        public Task<ReviewingArticlesDto> UpdateReviewingArticleAsync(int reviewingArticleId, string facultyMemberEmail, ReviewingArticlesDto reviewingArticleUpdateDto);
        public Task DeleteReviewingArticleAsync(int reviewingArticleId, string facultyMemberEmail);
        #endregion

        #region Participation In Magazines
        public Task<PaginatedResult<ParticipationInMagazinesResponseDto>> GetAllParticipationInMagazinesAsync(ParticipationInMagazinesSpecificationsParameters parameters);
        public Task<ParticipationInMagazinesResponseDto> GetParticipationInMagazineByIdAsync(int id);
        public Task<ParticipationInMagazinesResponseDto> CreateParticipationInMagazineAsync(string facultyMemberEmail, ParticipationInMagazineCreateDto participationInMagazinesCreateDto);
        public Task<ParticipationInMagazinesResponseDto> UpdateParticipationInMagazineAsync(int participationInMagazineId, string facultyMemberEmail, ParticipationInMagazineUpdateDto participationInMagazinesUpdateDto);
        public Task DeleteParticipationInMagazineAsync(int participationInMagazineId, string facultyMemberEmail);
        #endregion

        #region Projects
        public Task<PaginatedResult<ProjectsResponseDto>> GetAllProjectsAsync(ProjectsSpecifcationsParameters parameters);
        public Task<ProjectsResponseDto> GetProjectByIdAsync(int id);
        public Task<ProjectsResponseDto> CreateProjectAsync(string facultyMemberEmail, ProjectCreateDto projectCreateDto);
        public Task<ProjectsResponseDto> UpdateProjectAsync(int projectId, string facultyMemberEmail, ProjectUpdateDto projectUpdateDto);
        public Task DeleteProjectAsync(int projectId, string facultyMemberEmail);
        #endregion
    }
}