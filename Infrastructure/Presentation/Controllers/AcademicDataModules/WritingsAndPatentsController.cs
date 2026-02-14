using Microsoft.AspNetCore.Authorization;
using Shared;
using Shared.Dtos.AcademicDataModule.WritingsAndPatentsModule;
using Shared.SpecificationParameters.AcademicDataModule.WritingsAndPatentsModule;

namespace Presentation.Controllers.AcademicDataModules
{
    [Authorize]
    public class WritingsAndPatentsController(IServiceManager _serviceManager) : ApiController
    {
        #region Scientific Writings
        [ProducesResponseType(typeof(PaginatedResult<ScientificWritingsResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("ScientificWritings")]
        public async Task<ActionResult<PaginatedResult<ScientificWritingsResponseDTO>>> GetAllScientificWritingsAsync([FromQuery] ScientificWritingsSpecificationParameters parameters)
            => Ok(await _serviceManager.ScientificWritingsService.GetAllScientificWritingsAsync(parameters));

        [ProducesResponseType(typeof(ScientificWritingsResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("ScientificWriting/{id:int}")]
        public async Task<ActionResult<ScientificWritingsResponseDTO>> GetScientificWritingByIdAsync(int id)
            => Ok(await _serviceManager.ScientificWritingsService.GetScientificWritingByIdAsync(id));

        [ProducesResponseType(typeof(ScientificWritingsResponseDTO), StatusCodes.Status200OK)]
        [HttpPost("CreateScientificWriting")]
        public async Task<ActionResult<ScientificWritingsResponseDTO>> CreateScientificWritingAsync(ScientificWritingsCreateDTO scientificWritingsCreateDTO)
            => Ok(await _serviceManager.ScientificWritingsService.CreateScientificWritingAsync(scientificWritingsCreateDTO));

        [ProducesResponseType(typeof(ScientificWritingsResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("UpdateScientificWriting/{scientificWritingId:int}")]
        public async Task<ActionResult<ScientificWritingsResponseDTO>> UpdateScientificWritingAsync(int scientificWritingId, ScientificWritingsUpdateDTO scientificWritingsUpdateDTO)
            => Ok(await _serviceManager.ScientificWritingsService.UpdateScientificWritingAsync(scientificWritingId, scientificWritingsUpdateDTO));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("DeleteScientificWriting/{id:int}")]
        public async Task<ActionResult> DeleteScientificWritingAsync(int id)
        {
            await _serviceManager.ScientificWritingsService.DeleteScientificWritingAsync(id);
            return NoContent();
        }
        #endregion

        #region Patents
        [ProducesResponseType(typeof(PaginatedResult<PatentsResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("Patents")]
        public async Task<ActionResult<PaginatedResult<PatentsResponseDTO>>> GetAllPatentsAsync([FromQuery] PatentsSpecificationParameters parameters)
            => Ok(await _serviceManager.PatentsService.GetAllPatentsAsync(parameters));

        [ProducesResponseType(typeof(PatentsResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("Patent/{id:int}")]
        public async Task<ActionResult<PatentsResponseDTO>> GetPatentByIdAsync(int id)
            => Ok(await _serviceManager.PatentsService.GetPatentByIdAsync(id));

        [ProducesResponseType(typeof(PatentsResponseDTO), StatusCodes.Status200OK)]
        [HttpPost("CreatePatent")]
        public async Task<ActionResult<PatentsResponseDTO>> CreatePatentAsync(PatentsCreateDTO patentsCreateDTO)
            => Ok(await _serviceManager.PatentsService.CreatePatentAsync(patentsCreateDTO));

        [ProducesResponseType(typeof(PatentsResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("UpdatePatent/{patentId:int}")]
        public async Task<ActionResult<PatentsResponseDTO>> UpdatePatentAsync(int patentId, PatentsUpdateDTO patentsUpdateDTO)
            => Ok(await _serviceManager.PatentsService.UpdatePatentAsync(patentId, patentsUpdateDTO));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("DeletePatent/{id:int}")]
        public async Task<ActionResult> DeletePatentAsync(int id)
        {
            await _serviceManager.PatentsService.DeletePatentAsync(id);
            return NoContent();
        }
        #endregion
    }
}
