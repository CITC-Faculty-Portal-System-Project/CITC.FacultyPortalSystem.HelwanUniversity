using Microsoft.AspNetCore.Authorization;
using Services.Abstraction.Contracts.AcademicDataModule.ResearchesModule;
using Shared;
using Shared.Dtos.ResearchesModule;
using Shared.Dtos.ResearchesModule.ResearchesDOIandORCIDLoadService;
using Shared.SpecificationParameters.ResearchesModule;
using Services.Abstraction.Enums;
using Shared.Dtos.AttachmentsModule;


namespace Presentation.Controllers
{
    [Authorize]
    public class ResearchesAndThesesController(IServiceManager _serviceManager 
        , IResearchesDOIandORCIDLoadService _researchesDOIandORCIDLoadService ) : ApiController
    {

        [ProducesResponseType(typeof(PaginatedResult<ResearchCardResponseDTO>) , StatusCodes.Status200OK)]
        [HttpGet("RecommendedResearches")]
        public async Task<ActionResult<PaginatedResult<ResearchCardResponseDTO>>> GetAllRecommendedResearches
                     ([FromQuery] RecommendedResearchesSpecificationParameters parameters)
               => Ok(await _serviceManager.ResearchesService.GetAllRecommendedResearches(parameters));

        [ProducesResponseType(typeof(ResearcherProfileResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("ResearcherProfile")]
        public async Task<ActionResult<ResearcherProfileResponseDTO>> GetResearcherProfile()
              => Ok(await _serviceManager.ResearcherProfileService.GetResearcherProfile());


        [ProducesResponseType(typeof(DOIResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("ResearchSearchDOI")]
        public async Task<ActionResult<DOIResponseDTO>> GetResearchByDOI([FromQuery] string doi)
             => Ok(await _researchesDOIandORCIDLoadService.GetByDoiAsync(doi));


        [ProducesResponseType(typeof(ResearcherDataGetByORCIDResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("ContributorDataWithORCID")]
        public async Task<ActionResult<ResearcherDataGetByORCIDResponseDTO>> GetContributorDataByORCIDAsync([FromQuery] string orcid)
            => Ok(await _researchesDOIandORCIDLoadService.GetContributorNameByORCIDAsync(orcid));


        [ProducesResponseType(typeof(ResearchResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("ResearchFindByTitle")]
        public async Task<ActionResult<ResearchResponseDTO>> FindResearchByTitle(string title)
            => Ok(await _serviceManager.ResearchesService.GetResearchByTitle(title));


        [ProducesResponseType(typeof(ResearchCardResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("ApproveRecommendedResearch/{researchId}")]
        public async Task<ActionResult<ResearchCardResponseDTO>> ApproveRecommendedResearch
                   (int researchId)
             => Ok(await _serviceManager.ResearchesService.ConfirmRecommendedResearch(researchId));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("RemoveResearch/{researchId}")]
        public async Task<ActionResult> RemoveResearch(int researchId)
        {
            await _serviceManager.ResearchesService.DeleteResearch(researchId);
            return NoContent();
        }


        [ProducesResponseType(typeof(SupervisingThesesAddDTO), StatusCodes.Status201Created)]
        [HttpPost("AddThesesSupervising")]
        public async Task<ActionResult<SupervisingThesesAddDTO>> AddThsesSupervising(SupervisingThesesAddDTO supervisingThesesDTO)
            => Ok(await _serviceManager.ThesesSupervisingService.AddThesesSupervising(supervisingThesesDTO));


        [ProducesResponseType(typeof(SupervisingThsesResponseDTO), StatusCodes.Status201Created)]
        [HttpGet("ThesesSupervising/{thesesId}")]
        public async Task<ActionResult<SupervisingThsesResponseDTO>> GetThsesSupervisingById(int thesesId)
         => Ok(await _serviceManager.ThesesSupervisingService.GetThesesSupervisingById(thesesId));

        [ProducesResponseType(typeof(PaginatedResult<SupervisingThsesResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("ThesesSupervising")]
        public async Task<ActionResult<PaginatedResult<SupervisingThsesResponseDTO>>> GetAllThesesSupervisings([FromQuery]ThesesSupervisingSpecificationParameters parameters)
        => Ok(await _serviceManager.ThesesSupervisingService.GetAllSupervisings(parameters));


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("RemoveThesesSupervising/{thesesId}")]
        public async Task<ActionResult> RemoveThesesSupervising(int thesesId)
        {
            await _serviceManager.ThesesSupervisingService.DeleteThesesSupervising(thesesId);
            return NoContent();
        }

        [ProducesResponseType(typeof(SupervisingThsesResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("UpdateThesesSupervising/{thesesId}")]
        public async Task<ActionResult<SupervisingThsesResponseDTO>> UpdateThesesSupervising
                (int thesesId, SupervisingThesesUpdateDTO supervisingThesesUpdateDTO)
            => Ok(await _serviceManager.ThesesSupervisingService.UpdateThesesSupervising(thesesId, supervisingThesesUpdateDTO));

        [ProducesResponseType(typeof(ThesesResponseDTO), StatusCodes.Status200OK)]
        [HttpPost("AddTheses")]
        public async Task<ActionResult<ThesesResponseDTO>> AddTheses
             (ThesesDTO theses)
                => Ok(await _serviceManager.ThesesService.AddTheses(theses));

        [ProducesResponseType(typeof(ThesesResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("Theses/{thesesId}")]
        public async Task<ActionResult<ThesesResponseDTO>> GetThesesById
             (int thesesId)
                => Ok(await _serviceManager.ThesesService.GetThesesById(thesesId));


        [ProducesResponseType(typeof(PaginatedResult<ThesesResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("Theses")]
        public async Task<ActionResult<PaginatedResult<ThesesResponseDTO>>> GetAllTheses
            ([FromQuery] ThesesSpecificationParameters parameters)
               => Ok(await _serviceManager.ThesesService.GetAllTheses(parameters));

    }
}
