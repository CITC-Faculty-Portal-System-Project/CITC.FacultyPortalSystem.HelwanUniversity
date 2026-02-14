using Microsoft.AspNetCore.Authorization;
using Shared;
using Shared.Dtos.AcademicDataModule.ExperiencesModule;
using Shared.SpecificationParameters.AcademicDataModule.ExperiencesModule;

namespace Presentation.Controllers.AcademicDataModules
{
    [Authorize]
    public class ExperiencesController(IServiceManager _serviceManager) : ApiController
    {
        #region General Experiences
        [ProducesResponseType(typeof(PaginatedResult<GeneralExperiencesResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("GeneralExperiences")]
        public async Task<ActionResult<PaginatedResult<GeneralExperiencesResponseDTO>>> GetAllGeneralExperiencesAsync([FromQuery] GeneralExperiencesSpecificationParameters parameters)
            => Ok(await _serviceManager.GeneralExperiencesService.GetAllGeneralExperiencesAsync(parameters));

        [ProducesResponseType(typeof(GeneralExperiencesResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("GeneralExperience/{id:int}")]
        public async Task<ActionResult<GeneralExperiencesResponseDTO>> GetGeneralExperienceByIdAsync(int id)
            => Ok(await _serviceManager.GeneralExperiencesService.GetGeneralExperienceByIdAsync(id));

        [ProducesResponseType(typeof(GeneralExperiencesResponseDTO), StatusCodes.Status200OK)]
        [HttpPost("CreateGeneralExperience")]
        public async Task<ActionResult<GeneralExperiencesResponseDTO>> CreateGeneralExperienceAsync(GeneralExperiencesCreateDTO generalExperiencesCreateDTO)
            => Ok(await _serviceManager.GeneralExperiencesService.CreateGeneralExperienceAsync(generalExperiencesCreateDTO));

        [ProducesResponseType(typeof(GeneralExperiencesResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("UpdateGeneralExperience/{generalExperienceId:int}")]
        public async Task<ActionResult<GeneralExperiencesResponseDTO>> UpdateGeneralExperienceAsync(int generalExperienceId, GeneralExperiencesUpdateDTO generalExperiencesUpdateDTO)
            => Ok(await _serviceManager.GeneralExperiencesService.UpdateGeneralExperienceAsync(generalExperienceId, generalExperiencesUpdateDTO));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("DeleteGeneralExperience/{id:int}")]
        public async Task<ActionResult> DeleteGeneralExperienceAsync(int id)
        {
            await _serviceManager.GeneralExperiencesService.DeleteGeneralExperienceAsync(id);
            return NoContent();
        }
        #endregion

        #region Teaching Experiences
        [ProducesResponseType(typeof(PaginatedResult<TeachingExperiencesResponseDTO>), StatusCodes.Status200OK)]
        [HttpGet("TeachingExperiences")]
        public async Task<ActionResult<PaginatedResult<TeachingExperiencesResponseDTO>>> GetAllTeachingExperiencesAsync([FromQuery] TeachingExperiencesSpecificationParameters parameters)
            => Ok(await _serviceManager.TeachingExperiencesService.GetAllTeachingExperiencesAsync(parameters));

        [ProducesResponseType(typeof(TeachingExperiencesResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("TeachingExperience/{id:int}")]
        public async Task<ActionResult<TeachingExperiencesResponseDTO>> GetTeachingExperienceByIdAsync(int id)
            => Ok(await _serviceManager.TeachingExperiencesService.GetTeachingExperienceByIdAsync(id));

        [ProducesResponseType(typeof(TeachingExperiencesResponseDTO), StatusCodes.Status200OK)]
        [HttpPost("CreateTeachingExperience")]
        public async Task<ActionResult<TeachingExperiencesResponseDTO>> CreateTeachingExperienceAsync(TeachingExperiencesCreateDTO teachingExperiencesCreateDTO)
            => Ok(await _serviceManager.TeachingExperiencesService.CreateTeachingExperienceAsync(teachingExperiencesCreateDTO));

        [ProducesResponseType(typeof(TeachingExperiencesResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("UpdateTeachingExperience/{teachingExperienceId:int}")]
        public async Task<ActionResult<TeachingExperiencesResponseDTO>> UpdateTeachingExperienceAsync(int teachingExperienceId, TeachingExperiencesUpdateDTO teachingExperiencesUpdateDTO)
            => Ok(await _serviceManager.TeachingExperiencesService.UpdateTeachingExperienceAsync(teachingExperienceId, teachingExperiencesUpdateDTO));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("DeleteTeachingExperience/{id:int}")]
        public async Task<ActionResult> DeleteTeachingExperienceAsync(int id)
        {
            await _serviceManager.TeachingExperiencesService.DeleteTeachingExperienceAsync(id);
            return NoContent();
        }
        #endregion
    }
}
