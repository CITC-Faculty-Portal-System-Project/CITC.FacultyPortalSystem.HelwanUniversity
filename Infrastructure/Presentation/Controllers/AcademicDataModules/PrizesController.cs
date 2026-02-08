using Microsoft.AspNetCore.Authorization;
using Shared;
using Shared.Dtos.AcademicDataModule.PrizesModule;
using Shared.SpecificationParameters.AcademicDataModule.PrizesModule;

namespace Presentation.Controllers.AcademicDataModules
{
    [Authorize]
    public class PrizesController(IServiceManager _serviceManager) : ApiController
    {
        #region Prizes and Rewards
        [ProducesResponseType(typeof(PaginatedResult<PrizesAndRewardsResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("PrizesAndRewards")]
        public async Task<ActionResult<PaginatedResult<PrizesAndRewardsResponseDTO>>> GetAllPrizesAndRewardsAsync([FromQuery] PrizesAndRewardsSpecificationParameters parameters)
            => Ok(await _serviceManager.PrizesAndRewardsService.GetAllPrizesAndRewardsAsync(parameters));

        [ProducesResponseType(typeof(PrizesAndRewardsResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("PrizesOrReward/{id:int}")]
        public async Task<ActionResult<PrizesAndRewardsResponseDTO>> GetPrizeOrRewardByIdAsync(int id)
            => Ok(await _serviceManager.PrizesAndRewardsService.GetPrizeOrRewardByIdAsync(id));

        [ProducesResponseType(typeof(PrizesAndRewardsResponseDTO), StatusCodes.Status200OK)]
        [HttpPost("CreatePrizeOrReward")]
        public async Task<ActionResult<PrizesAndRewardsResponseDTO>> CreatePrizeOrRewardAsync(PrizesAndRewardsCreateDTO prizesAndRewardsCreateDTO)
            => Ok(await _serviceManager.PrizesAndRewardsService.CreatePrizeOrRewardAsync(prizesAndRewardsCreateDTO));

        [ProducesResponseType(typeof(PrizesAndRewardsResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("UpdatePrizeOrReward/{prizesAndRewardsId:int}")]
        public async Task<ActionResult<PrizesAndRewardsResponseDTO>> UpdatePrizeOrRewardAsync(int prizesAndRewardsId, PrizesAndRewardsUpdateDTO prizesAndRewardsUpdateDTO)
            => Ok(await _serviceManager.PrizesAndRewardsService.UpdatePrizeOrRewardAsync(prizesAndRewardsId, prizesAndRewardsUpdateDTO));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("DeletePrizeOrReward/{id:int}")]
        public async Task<ActionResult> DeletePrizeOrRewardAsync(int id)
        {
            await _serviceManager.PrizesAndRewardsService.DeletePrizeOrRewardAsync(id);
            return NoContent();
        }
        #endregion

        #region Manifestations Of Scientific Appreciation
        [ProducesResponseType(typeof(PaginatedResult<ManifestationsOfScientificAppreciationResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("ManifestationsOfScientificAppreciation")]
        public async Task<ActionResult<PaginatedResult<ManifestationsOfScientificAppreciationResponseDTO>>> GetAllManifestationsOfScientificAppreciationAsync([FromQuery] ManifestationsOfScientificAppreciationSpecificationParameters parameters)
            => Ok(await _serviceManager.ManifestationsOfScientificAppreciationService.GetAllManifestationsOfScientificAppreciationAsync (parameters));

        [ProducesResponseType(typeof(ManifestationsOfScientificAppreciationResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("ManifestationsOfScientificAppreciation/{id:int}")]
        public async Task<ActionResult<ManifestationsOfScientificAppreciationResponseDTO>> GetManifestationOfScientificAppreciationByIdAsync(int id)
            => Ok(await _serviceManager.ManifestationsOfScientificAppreciationService.GetManifestationOfScientificAppreciationByIdAsync(id));

        [ProducesResponseType(typeof(ManifestationsOfScientificAppreciationResponseDTO), StatusCodes.Status200OK)]
        [HttpPost("CreateManifestationOfScientificAppreciation")]
        public async Task<ActionResult<ManifestationsOfScientificAppreciationResponseDTO>> CreateManifestationOfScientificAppreciationAsync(ManifestationsOfScientificAppreciationCreateDTO manifestationsOfScientificAppreciationCreateDTO)
            => Ok(await _serviceManager.ManifestationsOfScientificAppreciationService.CreateManifestationOfScientificAppreciationAsync(manifestationsOfScientificAppreciationCreateDTO));

        [ProducesResponseType(typeof(ManifestationsOfScientificAppreciationResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("UpdateManifestationOfScientificAppreciation/{manifestationsOfScientificAppreciationId:int}")]
        public async Task<ActionResult<ManifestationsOfScientificAppreciationResponseDTO>> UpdateManifestationOfScientificAppreciationAsync(int manifestationsOfScientificAppreciationId, ManifestationsOfScientificAppreciationUpdateDTO manifestationsOfScientificAppreciationUpdateDTO)
            => Ok(await _serviceManager.ManifestationsOfScientificAppreciationService.UpdateManifestationOfScientificAppreciationAsync(manifestationsOfScientificAppreciationId, manifestationsOfScientificAppreciationUpdateDTO));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("DeleteManifestationOfScientificAppreciation/{id:int}")]
        public async Task<ActionResult> DeleteManifestationOfScientificAppreciationAsync(int id)
        {
            await _serviceManager.ManifestationsOfScientificAppreciationService.DeleteManifestationOfScientificAppreciationAsync(id);
            return NoContent();
        }
        #endregion
    }
}
