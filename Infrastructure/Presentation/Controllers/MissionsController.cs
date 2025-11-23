using Microsoft.AspNetCore.Authorization;
using Presentation.Attributes;
using Services.Abstraction.Contracts;
using Shared;
using Shared.Dtos.MissionsModule;
using Shared.SpceificationParameters.MissionsModule;
using System.Reflection.Metadata.Ecma335;
namespace Presentation.Controllers
{
    [Authorize]
    public class MissionsController(IServiceManager _serviceManager) : ApiController
    {
        [ProducesResponseType(typeof(MissionAddResponse), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpPost("AddMission")]
        public async Task<ActionResult<MissionAddResponse>> AddMission(MissionAddDto mission)
             => Ok(await _serviceManager.MissionService.AddAsync(mission));


        [ProducesResponseType(typeof(MissionEditResponseDto), StatusCodes.Status200OK)]
        [HttpPut("EditMission/{missionId}")]
        public async Task<ActionResult<MissionEditResponseDto>> EditMission(int missionId, MissionEditDto mission)
             => Ok(await _serviceManager.MissionService.EditAsync(missionId , mission));


        [ProducesResponseType(typeof(MissionResponseDto), StatusCodes.Status200OK)]
        [HttpGet("GetMissions")]
        public async Task<ActionResult<PaginatedResult<MissionResponseDto>>> GetMissions([FromQuery] MissionSpecificationParamaters paramaters)
             => Ok(await _serviceManager.MissionService.GetAllMissionsAsync(paramaters));


        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [HttpDelete("DeleteMission/{id}")]
        public async Task<ActionResult<bool>> DeleteMission(int id , string reason = "لا يوجد")
             => Ok(await _serviceManager.MissionService.DeleteMissionAsync(id , reason));


        [ProducesResponseType(typeof(MissionResponseDto), StatusCodes.Status200OK)]
        [HttpGet("GetMission/{id}")]
        public async Task<ActionResult<MissionResponseDto>> GetMissionById(int id)
            => Ok(await _serviceManager.MissionService.GetMissionByIdAsync(id));

    }
}
