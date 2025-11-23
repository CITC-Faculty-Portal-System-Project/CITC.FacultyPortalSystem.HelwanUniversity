using Shared;
using Shared.Dtos.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.ProjectsAndCommitteesModule;

namespace Presentation.Controllers
{
    public class ProjectsAndCommitteesController(IServiceManager _serviceManager) : ApiController
    {
        #region Committees And Associations
        [ProducesResponseType(typeof(PaginatedResult<CommitteesAndAssociationsResponseDto>), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("CommitteesAndAssociations")]
        public async Task<ActionResult<CommitteesAndAssociationsResponseDto>> GetAllCommitteesAndAssociationsAsync([FromQuery] CommitteesAndAssociationsSpecificationsParameters parameters)
            => Ok(await _serviceManager.ProjectsAndCommitteesService.GetAllCommitteesAndAssociationsAsync(parameters));

        [ProducesResponseType(typeof(CommitteesAndAssociationsResponseDto), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("CommitteeOrAssociation/{id:int}")]
        public async Task<ActionResult<CommitteesAndAssociationsResponseDto>> GetAdministrativePositionById(int id)
            => Ok(await _serviceManager.ProjectsAndCommitteesService.GetCommitteeOrAssociationById(id));

        [ProducesResponseType(typeof(CommitteesAndAssociationsResponseDto), StatusCodes.Status200OK)]
        [HttpPost("CreateCommitteeOrAssociation")]
        public async Task<ActionResult<CommitteesAndAssociationsResponseDto>> CreateAdministrativePositionAsync([FromQuery] string facultyMemberEmail, CommitteesAndAssociationsCreateDto committeesAndAssociationsCreateDto)
            => Ok(await _serviceManager.ProjectsAndCommitteesService.CreateCommitteeOrAssociationAsync(facultyMemberEmail, committeesAndAssociationsCreateDto));

        [ProducesResponseType(typeof(CommitteesAndAssociationsResponseDto), StatusCodes.Status200OK)]
        [HttpPut("UpdateCommitteeOrAssociation")]
        public async Task<ActionResult<CommitteesAndAssociationsResponseDto>> UpdateAdministrativePositionAsync([FromQuery] int committeeOrAssociationId, [FromQuery] string facultyMemberEmail, CommitteesAndAssociationsUpdateDto committeesAndAssociationsUpdateDto)
            => Ok(await _serviceManager.ProjectsAndCommitteesService.UpdateCommitteeOrAssociationAsync(committeeOrAssociationId, facultyMemberEmail, committeesAndAssociationsUpdateDto));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("DeleteCommitteeOrAssociation/{id:int}")]
        public async Task<ActionResult> DeleteCommitteeOrAssociationAsync(int id, [FromQuery] string facultyMemberEmail)
        {
            await _serviceManager.ProjectsAndCommitteesService.DeleteCommitteeOrAssociationAsync(id, facultyMemberEmail);
            return NoContent();
        }
        #endregion

        #region Reviewing Articles
        [ProducesResponseType(typeof(PaginatedResult<ReviewingArticlesDto>), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("ReviewingArticles")]
        public async Task<ActionResult<ReviewingArticlesDto>> GetAllReviewingArticlesAsync([FromQuery] ReviewingArticlesSpecificationsParameters parameters)
            => Ok(await _serviceManager.ProjectsAndCommitteesService.GetAllReviewingArticlesAsync(parameters));

        [ProducesResponseType(typeof(ReviewingArticlesDto), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("ReviewingArticle/{id:int}")]
        public async Task<ActionResult<ReviewingArticlesDto>> GetReviewingArticleById(int id)
            => Ok(await _serviceManager.ProjectsAndCommitteesService.GetReviewingArticleById(id));

        [ProducesResponseType(typeof(ReviewingArticlesDto), StatusCodes.Status200OK)]
        [HttpPost("CreateReviewingArticle")]
        public async Task<ActionResult<ReviewingArticlesDto>> CreateReviewingArticleAsync([FromQuery] string facultyMemberEmail, ReviewingArticleCreateDto reviewingArticleCreateDto)
            => Ok(await _serviceManager.ProjectsAndCommitteesService.CreateReviewingArticleAsync(facultyMemberEmail, reviewingArticleCreateDto));

        [ProducesResponseType(typeof(ReviewingArticlesDto), StatusCodes.Status200OK)]
        [HttpPut("UpdateReviewingArticle")]
        public async Task<ActionResult<ReviewingArticlesDto>> UpdateReviewingArticleAsync([FromQuery] int reviewingArticleId, [FromQuery] string facultyMemberEmail, ReviewingArticlesDto reviewingArticlesUpdateDto)
            => Ok(await _serviceManager.ProjectsAndCommitteesService.UpdateReviewingArticleAsync(reviewingArticleId, facultyMemberEmail, reviewingArticlesUpdateDto));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("DeleteReviewingArticle/{id:int}")]
        public async Task<ActionResult> DeleteReviewingArticleAsync(int id, [FromQuery] string facultyMemberEmail)
        {
            await _serviceManager.ProjectsAndCommitteesService.DeleteReviewingArticleAsync(id, facultyMemberEmail);
            return NoContent();
        }
        #endregion
    }
}
