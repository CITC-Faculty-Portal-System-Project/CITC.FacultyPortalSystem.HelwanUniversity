using Microsoft.AspNetCore.Authorization;
using Shared;
using Shared.Dtos.MissionsModule;
using Shared.SpceificationParameters.MissionsModule;
using Shared.SpecificationParameters.MissionsModule;

namespace Presentation.Controllers
{
    [Authorize]
    public class MissionsController(IServiceManager _serviceManager) : ApiController
    {
        #region Scientific Missions
        [ProducesResponseType(typeof(PaginatedResult<ScientificMissionResponseDto>), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("ScientificMissions")]
        public async Task<ActionResult<PaginatedResult<ScientificMissionResponseDto>>> GetAllScientificMissionsAsync([FromQuery] ScientificMissionSpecificationParamaters paramaters)
             => Ok(await _serviceManager.MissionsService.GetAllScientificMissionsAsync(paramaters));

        [ProducesResponseType(typeof(ScientificMissionResponseDto), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("ScientificMission/{id:int}")]
        public async Task<ActionResult<ScientificMissionResponseDto>> GetScientificMissionByIdAsync(int id)
            => Ok(await _serviceManager.MissionsService.GetScientificMissionByIdAsync(id));

        [ProducesResponseType(typeof(ScientificMissionResponseDto), StatusCodes.Status200OK)]
        [HttpPost("CreateScientificMission")]
        public async Task<ActionResult<ScientificMissionResponseDto>> CreateScientificMissionAsync(ScientificMissionCreateDto scientificMissionCreateDto)
             => Ok(await _serviceManager.MissionsService.CreateScientificMissionAsync(scientificMissionCreateDto));

        [ProducesResponseType(typeof(ScientificMissionResponseDto), StatusCodes.Status200OK)]
        [HttpPut("UpdateScientificMission/{id:int}")]
        public async Task<ActionResult<ScientificMissionResponseDto>> UpdateScientificMissionAsync(int id, ScientificMissionUpdateDto scientificMissionUpdateDto)
             => Ok(await _serviceManager.MissionsService.UpdateScientificMissionAsync(id, scientificMissionUpdateDto));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("DeleteScientificMission/{id:int}")]
        public async Task<ActionResult> DeleteScientificMissionAsync(int id)
             => Ok(await _serviceManager.MissionsService.DeleteScientificMissionAsync(id));
        #endregion

        #region Seminars And Conferences
        [ProducesResponseType(typeof(PaginatedResult<ConferencesAndSeminarsResponseDto>), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("ConferncesAndSeminars")]
        public async Task<ActionResult<PaginatedResult<ConferencesAndSeminarsResponseDto>>> GetAllSeminarsAndConferencesAsync(SeminarsAndConferncesSpecificationParameters parameters)
           => Ok(await _serviceManager.MissionsService.GetAllSeminarsAndConferencesAsync(parameters));

        [ProducesResponseType(typeof(ConferencesAndSeminarsResponseDto), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("ConfernceOrSeminar/{id:int}")]
        public async Task<ActionResult<ConferencesAndSeminarsResponseDto>> GetSeminarOrConferenceByIdAsync(int id)
           => Ok(await _serviceManager.MissionsService.GetSeminarOrConferenceByIdAsync(id));

        [ProducesResponseType(typeof(ConferencesAndSeminarsResponseDto), StatusCodes.Status200OK)]
        [HttpPost("CreateConfernceOrSeminar")]
        public async Task<ActionResult<ConferencesAndSeminarsResponseDto>> CreateSeminarOrConferenceAsync(ConferencesAndSeminarsCreateDto conferencesAndSeminarsCreateDto)
            => Ok(await _serviceManager.MissionsService.CreateSeminarOrConferenceAsync(conferencesAndSeminarsCreateDto));

        [ProducesResponseType(typeof(ConferencesAndSeminarsResponseDto), StatusCodes.Status200OK)]
        [HttpPut("UpdateConferncesOrSeminars/{id:int}")]
        public async Task<ActionResult<ConferencesAndSeminarsResponseDto>> UpdateSeminarOrConferenceAsync(int id, ConferencesAndSeminarsUpdateDto conferencesAndSeminarsUpdateDto)
           => Ok(await _serviceManager.MissionsService.UpdateSeminarOrConferenceAsync(id, conferencesAndSeminarsUpdateDto));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("DeleteConferncesOrSeminars/{id:int}")]
        public async Task<ActionResult<bool>> DeleteSeminarOrConferenceAsync(int id)
           => Ok(await _serviceManager.MissionsService.DeleteSeminarOrConferenceAsync(id));
        #endregion
    }
}
