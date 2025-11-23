using Microsoft.AspNetCore.Authorization;
using Presentation.Attributes;
using Services.Abstraction.Contracts;
using Shared;
using Shared.Dtos.ConfrencesAndSeminarsModule;
using Shared.SpecificationParameters.SemiarsAndConferncesModule;

namespace Presentation.Controllers
{

    [Authorize]
    public class ConferncesAndSeminarsController(IServiceManager _serviceManager) : ApiController
    {
        [RedisCache]
        [ProducesResponseType(typeof(ConferncesAndSeminarsResponseDto), StatusCodes.Status200OK)]
        [HttpPost("AddConfernceOrSeminar")]
        public async Task<ActionResult<ConferncesAndSeminarsResponseDto>> AddConfernceOrSeminar(ConfrencesAndSeminarsAddDto confrences)
            => Ok(await _serviceManager.SeminarsAndConfrencesService.AddAsync(confrences));

        [ProducesResponseType(typeof(PaginatedResult<ConferncesAndSeminarsResponseDto>), StatusCodes.Status200OK)]
        [HttpGet("GetConferncesOrSeminars")]
        public async Task<ActionResult<PaginatedResult<ConferncesAndSeminarsResponseDto>>> GetAllConferncesOrSeminars
                                        (SeminarsAndConferncesSpecificationParameters parameters)
           => Ok(await _serviceManager.SeminarsAndConfrencesService.GetAsync(parameters));


        [ProducesResponseType(typeof(ConferncesAndSeminarsResponseDto), StatusCodes.Status200OK)]
        [HttpGet("GetConferncesOrSeminars/{id}")]
        public async Task<ActionResult<ConferncesAndSeminarsResponseDto>> GetConferncesOrSeminarsById(int id)
           => Ok(await _serviceManager.SeminarsAndConfrencesService.GetByIdAsync(id));

        [ProducesResponseType(typeof(ConferncesAndSeminarsResponseDto), StatusCodes.Status200OK)]
        [HttpPut("UpdateConferncesOrSeminars/{id}")]
        public async Task<ActionResult<ConferncesAndSeminarsResponseDto>> UpdateConferncesOrSeminars(int id , ConfrencesAndSeminarsEditDto editDto)
           => Ok(await _serviceManager.SeminarsAndConfrencesService.UpdateAsync(id , editDto));


        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [HttpDelete("DeleteConferncesOrSeminars/{id}")]
        public async Task<ActionResult<bool>> DeleteConferncesOrSeminars(int id, string reason = "لا يوجد")
           => Ok(await _serviceManager.SeminarsAndConfrencesService.DeleteAsync(id, reason));

    }

}
