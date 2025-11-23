using Microsoft.AspNetCore.Authorization;
using Presentation.Attributes;
using Services.Abstraction.Contracts;
using Shared.Dtos.LookUpItem;
namespace Presentation.Controllers
{
    [Authorize]
    public class LookUpItemsController(IServiceManager _serviceManager) : ApiController
    {
        [ProducesResponseType(typeof(LookUpItemResponseDto), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("AcademicQualifications")]
        public async Task<ActionResult<IEnumerable<LookupItemDto>>> GetAcademicQualifications()
            => Ok(await _serviceManager.LookUpItemService.GetLookUpItemByType("AcademicQualification"));

        [ProducesResponseType(typeof(LookUpItemResponseDto), StatusCodes.Status200OK)]
        [HttpGet("MagazineParticipationRoles")]
        public async Task<ActionResult<IEnumerable<LookupItemDto>>> GetMagazineParticipationRoles()
            => Ok(await _serviceManager.LookUpItemService.GetLookUpItemByType("MagazineParticipationRole"));

        [ProducesResponseType(typeof(LookUpItemResponseDto), StatusCodes.Status200OK)]
        [HttpGet("AuthorRoles")]
        public async Task<ActionResult<IEnumerable<LookupItemDto>>> GetAuthorRoles()
            => Ok(await _serviceManager.LookUpItemService.GetLookUpItemByType("AuthorRole"));


        [ProducesResponseType(typeof(LookUpItemResponseDto), StatusCodes.Status200OK)]
        [HttpGet("AcademicGrades")]
        public async Task<ActionResult<IEnumerable<LookupItemDto>>> GetAcademicGrades()
             => Ok(await _serviceManager.LookUpItemService.GetLookUpItemByType("AcademicGrade"));


        [ProducesResponseType(typeof(LookUpItemResponseDto), StatusCodes.Status200OK)]
        [HttpGet("Rewards")]
        public async Task<ActionResult<IEnumerable<LookupItemDto>>> GetRewards()
              => Ok(await _serviceManager.LookUpItemService.GetLookUpItemByType("Rewards"));

        [ProducesResponseType(typeof(LookUpItemResponseDto), StatusCodes.Status200OK)]
        [HttpGet("Dispatch")]
        public async Task<ActionResult<IEnumerable<LookupItemDto>>> GetDispatches()
            => Ok(await _serviceManager.LookUpItemService.GetLookUpItemByType("Dispatch"));

        [ProducesResponseType(typeof(LookUpItemResponseDto), StatusCodes.Status200OK)]
        [HttpGet("ContributionTypes")]
        public async Task<ActionResult<IEnumerable<LookupItemDto>>> GetContributionTypes()
            => Ok(await _serviceManager.LookUpItemService.GetLookUpItemByType("ContributionTypes"));


        [ProducesResponseType(typeof(LookUpItemResponseDto), StatusCodes.Status200OK)]
        [HttpGet("EmploymentDegrees")]
        public async Task<ActionResult<IEnumerable<LookupItemDto>>> GetEmploymentDegrees()
            => Ok(await _serviceManager.LookUpItemService.GetLookUpItemByType("EmploymentDegrees"));

        [ProducesResponseType(typeof(LookUpItemResponseDto), StatusCodes.Status200OK)]
        [HttpGet("SmemiarParticipationTypes")]
        public async Task<ActionResult<IEnumerable<LookupItemDto>>> GetSmemiarParticipationTypes()
             => Ok(await _serviceManager.LookUpItemService.GetLookUpItemByType("SmemiarParticipationType"));


        [ProducesResponseType(typeof(LookUpItemResponseDto), StatusCodes.Status200OK)]
        [HttpGet("ComiteeParticipationDegrees")]
        public async Task<ActionResult<IEnumerable<LookupItemDto>>> GetComiteeParticipationDegrees()
             => Ok(await _serviceManager.LookUpItemService.GetLookUpItemByType("ComiteeParticipationDegree"));

        [ProducesResponseType(typeof(LookUpItemResponseDto), StatusCodes.Status200OK)]
        [HttpGet("TypesofComitee")]
        public async Task<ActionResult<IEnumerable<LookupItemDto>>> GetTypesofComitee()
            => Ok(await _serviceManager.LookUpItemService.GetLookUpItemByType("TypeofComitee"));

        [ProducesResponseType(typeof(LookUpItemResponseDto), StatusCodes.Status200OK)]
        [HttpGet("ProjectTypes")]
        public async Task<ActionResult<IEnumerable<LookupItemDto>>> GetProjectTypes()
            => Ok(await _serviceManager.LookUpItemService.GetLookUpItemByType("ProjectType"));

        [ProducesResponseType(typeof(LookUpItemResponseDto), StatusCodes.Status200OK)]
        [HttpGet("ProjectRoles")]
        public async Task<ActionResult<IEnumerable<LookupItemDto>>> GetProjectRoles()
            => Ok(await _serviceManager.LookUpItemService.GetLookUpItemByType("ProjectRole"));



    }
}
