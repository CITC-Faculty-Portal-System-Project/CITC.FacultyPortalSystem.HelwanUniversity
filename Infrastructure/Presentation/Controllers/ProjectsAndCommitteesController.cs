using Microsoft.AspNetCore.Authorization;
using Shared;
using Shared.Dtos.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.ProjectsAndCommitteesModule;

namespace Presentation.Controllers
{
    [Authorize]
    public class ProjectsAndCommitteesController(IServiceManager _serviceManager) : ApiController
    {
        #region Committees And Associations
        [ProducesResponseType(typeof(PaginatedResult<CommitteesAndAssociationsResponseDto>), StatusCodes.Status200OK)]
        [HttpGet("CommitteesAndAssociations")]
        public async Task<ActionResult<PaginatedResult<CommitteesAndAssociationsResponseDto>>> GetAllCommitteesAndAssociationsAsync([FromQuery] CommitteesAndAssociationsSpecificationsParameters parameters)
            => Ok(await _serviceManager.CommitteesAndAssociationsService.GetAllCommitteesAndAssociationsAsync(parameters));

        [ProducesResponseType(typeof(CommitteesAndAssociationsResponseDto), StatusCodes.Status200OK)]
        [HttpGet("CommitteeOrAssociation/{id:int}")]
        public async Task<ActionResult<CommitteesAndAssociationsResponseDto>> GetCommitteeOrAssociationByIdAsync(int id)
            => Ok(await _serviceManager.CommitteesAndAssociationsService.GetCommitteeOrAssociationByIdAsync(id));

        [ProducesResponseType(typeof(CommitteesAndAssociationsResponseDto), StatusCodes.Status200OK)]
        [HttpPost("CreateCommitteeOrAssociation")]
        public async Task<ActionResult<CommitteesAndAssociationsResponseDto>> CreateCommitteeOrAssociationAsync(CommitteeOrAssociationCreateDto committeesAndAssociationsCreateDto)
            => Ok(await _serviceManager.CommitteesAndAssociationsService.CreateCommitteeOrAssociationAsync(committeesAndAssociationsCreateDto));

        [ProducesResponseType(typeof(CommitteesAndAssociationsResponseDto), StatusCodes.Status200OK)]
        [HttpPut("UpdateCommitteeOrAssociation/{committeeOrAssociationId:int}")]
        public async Task<ActionResult<CommitteesAndAssociationsResponseDto>> UpdateCommitteeOrAssociationAsync( int committeeOrAssociationId, CommitteeOrAssociationUpdateDto committeesAndAssociationsUpdateDto)
            => Ok(await _serviceManager.CommitteesAndAssociationsService.UpdateCommitteeOrAssociationAsync(committeeOrAssociationId, committeesAndAssociationsUpdateDto));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("DeleteCommitteeOrAssociation/{id:int}")]
        public async Task<ActionResult> DeleteCommitteeOrAssociationAsync(int id)
        {
            await _serviceManager.CommitteesAndAssociationsService.DeleteCommitteeOrAssociationAsync(id);
            return NoContent();
        }
        #endregion

        #region Reviewing Articles
        [ProducesResponseType(typeof(PaginatedResult<ReviewingArticlesDto>), StatusCodes.Status200OK)]
        [HttpGet("ReviewingArticles")]
        public async Task<ActionResult<PaginatedResult<ReviewingArticlesDto>>> GetAllReviewingArticlesAsync([FromQuery] ReviewingArticlesSpecificationsParameters parameters)
            => Ok(await _serviceManager.ReviewingArticlesService.GetAllReviewingArticlesAsync(parameters));

        [ProducesResponseType(typeof(ReviewingArticlesDto), StatusCodes.Status200OK)]
        [HttpGet("ReviewingArticle/{id:int}")]
        public async Task<ActionResult<ReviewingArticlesDto>> GetReviewingArticleByIdAsync(int id)
            => Ok(await _serviceManager.ReviewingArticlesService.GetReviewingArticleByIdAsync(id));

        [ProducesResponseType(typeof(ReviewingArticlesDto), StatusCodes.Status200OK)]
        [HttpPost("CreateReviewingArticle")]
        public async Task<ActionResult<ReviewingArticlesDto>> CreateReviewingArticleAsync(ReviewingArticleCreateDto reviewingArticleCreateDto)
            => Ok(await _serviceManager.ReviewingArticlesService.CreateReviewingArticleAsync(reviewingArticleCreateDto));

        [ProducesResponseType(typeof(ReviewingArticlesDto), StatusCodes.Status200OK)]
        [HttpPut("UpdateReviewingArticle/{reviewingArticleId:int}")]
        public async Task<ActionResult<ReviewingArticlesDto>> UpdateReviewingArticleAsync( int reviewingArticleId, ReviewArticleUpdateDto reviewingArticlesUpdateDto)
            => Ok(await _serviceManager.ReviewingArticlesService.UpdateReviewingArticleAsync(reviewingArticleId, reviewingArticlesUpdateDto));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("DeleteReviewingArticle/{id:int}")]
        public async Task<ActionResult> DeleteReviewingArticleAsync(int id)
        {
            await _serviceManager.ReviewingArticlesService.DeleteReviewingArticleAsync(id);
            return NoContent();
        }
        #endregion

        #region Participation In Magazines
        [ProducesResponseType(typeof(PaginatedResult<ParticipationInMagazinesResponseDto>), StatusCodes.Status200OK)]
        [HttpGet("ParticipationInMagazines")]
        public async Task<ActionResult<PaginatedResult<ParticipationInMagazinesResponseDto>>> GetAllParticipationInMagazinesAsync([FromQuery] ParticipationInMagazinesSpecificationsParameters parameters)
            => Ok(await _serviceManager.ParticipationInMagazinesService.GetAllParticipationInMagazinesAsync(parameters));

        [ProducesResponseType(typeof(ParticipationInMagazinesResponseDto), StatusCodes.Status200OK)]
        [HttpGet("ParticipationInMagazine/{id:int}")]
        public async Task<ActionResult<ParticipationInMagazinesResponseDto>> GetParticipationInMagazineByIdAsync(int id)
            => Ok(await _serviceManager.ParticipationInMagazinesService.GetParticipationInMagazineByIdAsync(id));

        [ProducesResponseType(typeof(ParticipationInMagazinesResponseDto), StatusCodes.Status200OK)]
        [HttpPost("CreateParticipationInMagazine")]
        public async Task<ActionResult<ParticipationInMagazinesResponseDto>> CreateParticipationInMagazineAsync(ParticipationInMagazineCreateDto participationInMagazineCreateDto)
            => Ok(await _serviceManager.ParticipationInMagazinesService.CreateParticipationInMagazineAsync(participationInMagazineCreateDto));

        [ProducesResponseType(typeof(ParticipationInMagazinesResponseDto), StatusCodes.Status200OK)]
        [HttpPut("UpdateParticipationInMagazine/{ParticipationInMagazineId:int}")]
        public async Task<ActionResult<ParticipationInMagazinesResponseDto>> UpdateParticipationInMagazineAsync(int ParticipationInMagazineId, ParticipationInMagazineUpdateDto participationInMagazineUpdateDto)
            => Ok(await _serviceManager.ParticipationInMagazinesService.UpdateParticipationInMagazineAsync(ParticipationInMagazineId, participationInMagazineUpdateDto));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("DeleteParticipationInMagazine/{id:int}")]
        public async Task<ActionResult> DeleteParticipationInMagazineAsync(int id)
        {
            await _serviceManager.ParticipationInMagazinesService.DeleteParticipationInMagazineAsync(id);
            return NoContent();
        }
        #endregion

        #region Projects
        [ProducesResponseType(typeof(PaginatedResult<ProjectsResponseDto>), StatusCodes.Status200OK)]
        [HttpGet("Projects")]
        public async Task<ActionResult<PaginatedResult<ProjectsResponseDto>>> GetAllProjectsAsync([FromQuery] ProjectsSpecifcationsParameters parameters)
            => Ok(await _serviceManager.ProjectsService.GetAllProjectsAsync(parameters));

        [ProducesResponseType(typeof(ParticipationInMagazinesResponseDto), StatusCodes.Status200OK)]
        [HttpGet("Project/{id:int}")]
        public async Task<ActionResult<ProjectsResponseDto>> GetProjectByIdAsync(int id)
            => Ok(await _serviceManager.ProjectsService.GetProjectByIdAsync(id));

        [ProducesResponseType(typeof(ProjectsResponseDto), StatusCodes.Status200OK)]
        [HttpPost("CreateProject")]
        public async Task<ActionResult<ProjectsResponseDto>> CreateProjectAsync(ProjectCreateDto projectCreateDto)
            => Ok(await _serviceManager.ProjectsService.CreateProjectAsync(projectCreateDto));

        [ProducesResponseType(typeof(ProjectsResponseDto), StatusCodes.Status200OK)]
        [HttpPut("UpdateProject/{projectId:int}")]
        public async Task<ActionResult<ProjectsResponseDto>> UpdateProjectAsync( int projectId, ProjectUpdateDto projectUpdateDto)
            => Ok(await _serviceManager.ProjectsService.UpdateProjectAsync(projectId, projectUpdateDto));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("DeleteProject/{id:int}")]
        public async Task<ActionResult> DeleteProjectAsync(int id)
        {
            await _serviceManager.ProjectsService.DeleteProjectAsync(id);
            return NoContent();
        }
        #endregion
    }
}
