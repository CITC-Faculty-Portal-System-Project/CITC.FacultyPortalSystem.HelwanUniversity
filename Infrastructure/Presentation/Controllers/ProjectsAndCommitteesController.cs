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
        public async Task<ActionResult<PaginatedResult<CommitteesAndAssociationsResponseDto>>> GetAllCommitteesAndAssociationsAsync([FromQuery] CommitteesAndAssociationsSpecificationsParameters parameters)
            => Ok(await _serviceManager.ProjectsAndCommitteesService.GetAllCommitteesAndAssociationsAsync(parameters));

        [ProducesResponseType(typeof(CommitteesAndAssociationsResponseDto), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("CommitteeOrAssociation/{id:int}")]
        public async Task<ActionResult<CommitteesAndAssociationsResponseDto>> GetCommitteeOrAssociationByIdAsync(int id)
            => Ok(await _serviceManager.ProjectsAndCommitteesService.GetCommitteeOrAssociationByIdAsync(id));

        [ProducesResponseType(typeof(CommitteesAndAssociationsResponseDto), StatusCodes.Status200OK)]
        [HttpPost("CreateCommitteeOrAssociation")]
        public async Task<ActionResult<CommitteesAndAssociationsResponseDto>> CreateCommitteeOrAssociationAsync([FromQuery] string facultyMemberEmail, CommitteeOrAssociationCreateDto committeesAndAssociationsCreateDto)
            => Ok(await _serviceManager.ProjectsAndCommitteesService.CreateCommitteeOrAssociationAsync(facultyMemberEmail, committeesAndAssociationsCreateDto));

        [ProducesResponseType(typeof(CommitteesAndAssociationsResponseDto), StatusCodes.Status200OK)]
        [HttpPut("UpdateCommitteeOrAssociation")]
        public async Task<ActionResult<CommitteesAndAssociationsResponseDto>> UpdateCommitteeOrAssociationAsync([FromQuery] int committeeOrAssociationId, [FromQuery] string facultyMemberEmail, CommitteeOrAssociationUpdateDto committeesAndAssociationsUpdateDto)
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
        public async Task<ActionResult<PaginatedResult<ReviewingArticlesDto>>> GetAllReviewingArticlesAsync([FromQuery] ReviewingArticlesSpecificationsParameters parameters)
            => Ok(await _serviceManager.ProjectsAndCommitteesService.GetAllReviewingArticlesAsync(parameters));

        [ProducesResponseType(typeof(ReviewingArticlesDto), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("ReviewingArticle/{id:int}")]
        public async Task<ActionResult<ReviewingArticlesDto>> GetReviewingArticleByIdAsync(int id)
            => Ok(await _serviceManager.ProjectsAndCommitteesService.GetReviewingArticleByIdAsync(id));

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

        #region Participation In Magazines
        [ProducesResponseType(typeof(PaginatedResult<ParticipationInMagazinesResponseDto>), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("ParticipationInMagazines")]
        public async Task<ActionResult<PaginatedResult<ParticipationInMagazinesResponseDto>>> GetAllParticipationInMagazinesAsync([FromQuery] ParticipationInMagazinesSpecificationsParameters parameters)
            => Ok(await _serviceManager.ProjectsAndCommitteesService.GetAllParticipationInMagazinesAsync(parameters));

        [ProducesResponseType(typeof(ParticipationInMagazinesResponseDto), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("ParticipationInMagazine/{id:int}")]
        public async Task<ActionResult<ParticipationInMagazinesResponseDto>> GetParticipationInMagazineByIdAsync(int id)
            => Ok(await _serviceManager.ProjectsAndCommitteesService.GetParticipationInMagazineByIdAsync(id));

        [ProducesResponseType(typeof(ParticipationInMagazinesResponseDto), StatusCodes.Status200OK)]
        [HttpPost("CreateParticipationInMagazine")]
        public async Task<ActionResult<ParticipationInMagazinesResponseDto>> CreateParticipationInMagazineAsync([FromQuery] string facultyMemberEmail, ParticipationInMagazineCreateDto participationInMagazineCreateDto)
            => Ok(await _serviceManager.ProjectsAndCommitteesService.CreateParticipationInMagazineAsync(facultyMemberEmail, participationInMagazineCreateDto));

        [ProducesResponseType(typeof(ParticipationInMagazinesResponseDto), StatusCodes.Status200OK)]
        [HttpPut("UpdateParticipationInMagazine")]
        public async Task<ActionResult<ParticipationInMagazinesResponseDto>> UpdateParticipationInMagazineAsync([FromQuery] int reviewingArticleId, [FromQuery] string facultyMemberEmail, ParticipationInMagazineUpdateDto participationInMagazineUpdateDto)
            => Ok(await _serviceManager.ProjectsAndCommitteesService.UpdateParticipationInMagazineAsync(reviewingArticleId, facultyMemberEmail, participationInMagazineUpdateDto));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("DeleteParticipationInMagazine/{id:int}")]
        public async Task<ActionResult> DeleteParticipationInMagazineAsync(int id, [FromQuery] string facultyMemberEmail)
        {
            await _serviceManager.ProjectsAndCommitteesService.DeleteParticipationInMagazineAsync(id, facultyMemberEmail);
            return NoContent();
        }
        #endregion

        #region Projects
        [ProducesResponseType(typeof(PaginatedResult<ProjectsResponseDto>), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("Projects")]
        public async Task<ActionResult<PaginatedResult<ProjectsResponseDto>>> GetAllProjectsAsync([FromQuery] ProjectsSpecifcationsParameters parameters)
            => Ok(await _serviceManager.ProjectsAndCommitteesService.GetAllProjectsAsync(parameters));

        [ProducesResponseType(typeof(ParticipationInMagazinesResponseDto), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("Project/{id:int}")]
        public async Task<ActionResult<ProjectsResponseDto>> GetProjectByIdAsync(int id)
            => Ok(await _serviceManager.ProjectsAndCommitteesService.GetProjectByIdAsync(id));

        [ProducesResponseType(typeof(ProjectsResponseDto), StatusCodes.Status200OK)]
        [HttpPost("CreateProject")]
        public async Task<ActionResult<ProjectsResponseDto>> CreateProjectAsync([FromQuery] string facultyMemberEmail, ProjectCreateDto projectCreateDto)
            => Ok(await _serviceManager.ProjectsAndCommitteesService.CreateProjectAsync(facultyMemberEmail, projectCreateDto));

        [ProducesResponseType(typeof(ProjectsResponseDto), StatusCodes.Status200OK)]
        [HttpPut("UpdateProject")]
        public async Task<ActionResult<ProjectsResponseDto>> UpdateProjectAsync([FromQuery] int reviewingArticleId, [FromQuery] string facultyMemberEmail, ProjectUpdateDto projectUpdateDto)
            => Ok(await _serviceManager.ProjectsAndCommitteesService.UpdateProjectAsync(reviewingArticleId, facultyMemberEmail, projectUpdateDto));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("DeleteProject/{id:int}")]
        public async Task<ActionResult> DeleteProjectAsync(int id, [FromQuery] string facultyMemberEmail)
        {
            await _serviceManager.ProjectsAndCommitteesService.DeleteProjectAsync(id, facultyMemberEmail);
            return NoContent();
        }
        #endregion
    }
}
