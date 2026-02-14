using Microsoft.AspNetCore.Authorization;
using Shared;
using Shared.Dtos.AcademicDataModule.ContributionsModule;
using Shared.SpecificationParameters.AcademicDataModule.ContributionsModule;

namespace Presentation.Controllers.AcademicDataModules
{
    [Authorize]
    public class ContributionsController(IServiceManager _serviceManager) : ApiController
    {
        #region Contributions To University
        [ProducesResponseType(typeof(PaginatedResult<ContributionsToUniversityResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("ContributionsToUniversity")]
        public async Task<ActionResult<PaginatedResult<ContributionsToUniversityResponseDTO>>> GetAllContributionsToUniversityAsync([FromQuery] ContributionsToUniversitySpecificationParameters parameters)
            => Ok(await _serviceManager.ContributionsToUniversityService.GetAllContributionsToUniversityAsync(parameters));

        [ProducesResponseType(typeof(ContributionsToUniversityResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("ContributionToUniversity/{id:int}")]
        public async Task<ActionResult<ContributionsToUniversityResponseDTO>> GetContributionToUniversityByIdAsync(int id)
            => Ok(await _serviceManager.ContributionsToUniversityService.GetContributionToUniversityByIdAsync(id));

        [ProducesResponseType(typeof(ContributionsToUniversityResponseDTO), StatusCodes.Status200OK)]
        [HttpPost("CreateContributionToUniversity")]
        public async Task<ActionResult<ContributionsToUniversityResponseDTO>> CreateContributionAsync(ContributionsToUniversityCreateDTO contributionsCreateDTO)
            => Ok(await _serviceManager.ContributionsToUniversityService.CreateContributionToUniversityAsync(contributionsCreateDTO));

        [ProducesResponseType(typeof(ContributionsToUniversityResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("UpdateContributionToUniversity/{contributionId:int}")]
        public async Task<ActionResult<ContributionsToUniversityResponseDTO>> UpdateContributionAsync(int contributionId, ContributionsToUniversityUpdateDTO contributionsUpdateDTO)
            => Ok(await _serviceManager.ContributionsToUniversityService.UpdateContributionToUniversityAsync(contributionId, contributionsUpdateDTO));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("DeleteContributionToUniversity/{id:int}")]
        public async Task<ActionResult> DeleteContributionAsync(int id)
        {
            await _serviceManager.ContributionsToUniversityService.DeleteContributionToUniversityAsync(id);
            return NoContent();
        }
        #endregion

        #region Contributions To Community Service
        [ProducesResponseType(typeof(PaginatedResult<ContributionsToCommunityServiceResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("ContributionsToCommunityService")]
        public async Task<ActionResult<PaginatedResult<ContributionsToCommunityServiceResponseDTO>>> GetAllContributionsToCommunityServiceAsync([FromQuery] ContributionsToCommunityServiceSpecificationParameters parameters)
            => Ok(await _serviceManager.ContributionsToCommunityService.GetAllContributionsToCommunityServiceAsync(parameters));

        [ProducesResponseType(typeof(ContributionsToCommunityServiceResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("ContributionToCommunityService/{id:int}")]
        public async Task<ActionResult<ContributionsToCommunityServiceResponseDTO>> GetContributionToCommunityServiceByIdAsync(int id)
            => Ok(await _serviceManager.ContributionsToCommunityService.GetContributionToCommunityServiceByIdAsync(id));

        [ProducesResponseType(typeof(ContributionsToCommunityServiceResponseDTO), StatusCodes.Status200OK)]
        [HttpPost("CreateContributionToCommunityService")]
        public async Task<ActionResult<ContributionsToCommunityServiceResponseDTO>> CreateContributionAsync(ContributionsToCommunityServiceCreateDTO contributionsCreateDTO)
            => Ok(await _serviceManager.ContributionsToCommunityService.CreateContributionToCommunityServiceAsync(contributionsCreateDTO));

        [ProducesResponseType(typeof(ContributionsToCommunityServiceResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("UpdateContributionToCommunityService/{contributionId:int}")]
        public async Task<ActionResult<ContributionsToCommunityServiceResponseDTO>> UpdateContributionAsync(int contributionId, ContributionsToCommunityServiceUpdateDTO contributionsUpdateDTO)
            => Ok(await _serviceManager.ContributionsToCommunityService.UpdateContributionToCommunityServiceAsync(contributionId, contributionsUpdateDTO));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("DeleteContributionToCommunityService/{id:int}")]
        public async Task<ActionResult> DeleteContributionToCommunityServiceAsync(int id)
        {
            await _serviceManager.ContributionsToCommunityService.DeleteContributionToCommunityServiceAsync(id);
            return NoContent();
        }
        #endregion

        #region Participation In Quality Works
        [ProducesResponseType(typeof(PaginatedResult<ParticipationInQualityWorksResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("ParticipationsInQualityWorks")]
        public async Task<ActionResult<PaginatedResult<ParticipationInQualityWorksResponseDTO>>> GetAllParticipationsInQualityWorksAsync([FromQuery] ParticipationInQualityWorksSpecificationParameters parameters)
            => Ok(await _serviceManager.ParticipationInQualityWorksService.GetAllParticipationsInQualityWorksAsync(parameters));

        [ProducesResponseType(typeof(ParticipationInQualityWorksResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("ParticipationInQualityWorks/{id:int}")]
        public async Task<ActionResult<ParticipationInQualityWorksResponseDTO>> GetParticipationInQualityWorksByIdAsync(int id)
            => Ok(await _serviceManager.ParticipationInQualityWorksService.GetParticipationInQualityWorksByIdAsync(id));

        [ProducesResponseType(typeof(ParticipationInQualityWorksResponseDTO), StatusCodes.Status200OK)]
        [HttpPost("CreateParticipationInQualityWorks")]
        public async Task<ActionResult<ParticipationInQualityWorksResponseDTO>> CreateParticipationAsync(ParticipationInQualityWorksCreateDTO participationCreateDTO)
            => Ok(await _serviceManager.ParticipationInQualityWorksService.CreateParticipationInQualityWorksAsync(participationCreateDTO));

        [ProducesResponseType(typeof(ParticipationInQualityWorksResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("UpdateParticipationInQualityWorks/{participationId:int}")]
        public async Task<ActionResult<ParticipationInQualityWorksResponseDTO>> UpdateParticipationAsync(int participationId, ParticipationInQualityWorksUpdateDTO participationUpdateDTO)
            => Ok(await _serviceManager.ParticipationInQualityWorksService.UpdateParticipationInQualityWorksAsync(participationId, participationUpdateDTO));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("DeleteParticipationInQualityWorks/{id:int}")]
        public async Task<ActionResult> DeleteParticipationInQualityWorksAsync(int id)
        {
            await _serviceManager.ParticipationInQualityWorksService.DeleteParticipationInQualityWorksAsync(id);
            return NoContent();
        }
        #endregion
    }
}