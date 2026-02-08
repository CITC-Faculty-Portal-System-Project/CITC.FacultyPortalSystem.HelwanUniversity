using Microsoft.AspNetCore.Authorization;
using Shared;
using Shared.Dtos.AcademicDataModule.ScientificProgressionModule;
using Shared.SpecificationParameters.AcademicDataModule.ScientificProgressionModule;

namespace Presentation.Controllers.AcademicDataModules
{
    [Authorize]
    public class ScientificProgressionController(IServiceManager _serviceManager) : ApiController
    {
        #region Academic Qualifications
        [ProducesResponseType(typeof(PaginatedResult<AcademicQualificationResponseDto>), StatusCodes.Status200OK)]
        [HttpGet("AcademicQualifications")]
        public async Task<ActionResult<PaginatedResult<AcademicQualificationResponseDto>>> GetAllAcademicQualificationsAsync([FromQuery] AcademicQualificationsSpecificationParamters paramters)
            => Ok(await _serviceManager.AcademicQualificationsService.GetAllAcademicQualificationsAsync(paramters));

        [ProducesResponseType(typeof(AcademicQualificationResponseDto), StatusCodes.Status200OK)]
        [HttpGet("AcademicQualification/{id:int}")]
        public async Task<ActionResult<AcademicQualificationResponseDto>> GetAcademicQualificationByIdAsync(int id)
            => Ok(await _serviceManager.AcademicQualificationsService.GetAcademicQualificationByIdAsync(id));

        [ProducesResponseType(typeof(AcademicQualificationResponseDto), StatusCodes.Status200OK)]
        [HttpPost("CreateAcademicQualification")]
        public async Task<ActionResult<AcademicQualificationResponseDto>> CreateAcademicQualificationAsync(AcademicQualificationCreateDto academicQualificationCreateDto)
            => Ok(await _serviceManager.AcademicQualificationsService.CreateAcademicQualificationAsync(academicQualificationCreateDto));

        [ProducesResponseType(typeof(AcademicQualificationResponseDto), StatusCodes.Status200OK)]
        [HttpPut("UpdateAcademicQualification/{academicQualificationId:int}")]
        public async Task<ActionResult<AcademicQualificationResponseDto>> UpdateAcademicQualificationAsync(int academicQualificationId, AcademicQualificationsUpdateDto academicQualificationsUpdateDto)
            => Ok(await _serviceManager.AcademicQualificationsService.UpdateAcademicQualificationAsync(academicQualificationId, academicQualificationsUpdateDto));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("DeleteAcademicQualification/{id:int}")]
        public async Task<ActionResult> DeleteAcademicQualificationAsync(int id)
        {
            await _serviceManager.AcademicQualificationsService.DeleteAcademicQualificationAsync(id);
            return NoContent();
        }
        #endregion

        #region Job Ranks
        [ProducesResponseType(typeof(PaginatedResult<JobRankResponseDto>), StatusCodes.Status200OK)]
        [HttpGet("JobRanks")]
        public async Task<ActionResult<PaginatedResult<JobRankResponseDto>>> GetAllJobRanksAsync([FromQuery] JobRanksSpecificationsParameters paramters)
            => Ok(await _serviceManager.JobRanksService.GetAllJobRanksAsync(paramters));

        [ProducesResponseType(typeof(JobRankResponseDto), StatusCodes.Status200OK)]
        [HttpGet("JobRank/{id:int}")]
        public async Task<ActionResult<JobRankResponseDto>> GetJobRankByIdAsync(int id)
            => Ok(await _serviceManager.JobRanksService.GetJobRankByIdAsync(id));

        [ProducesResponseType(typeof(JobRankResponseDto), StatusCodes.Status200OK)]
        [HttpPost("CreateJobRank")]
        public async Task<ActionResult<JobRankResponseDto>> CreateJobRankAsync(JobRankCreateDto jobRankCreateDto)
            => Ok(await _serviceManager.JobRanksService.CreateJobRankAsync(jobRankCreateDto));

        [ProducesResponseType(typeof(JobRankResponseDto), StatusCodes.Status200OK)]
        [HttpPut("UpdateJobRank/{jobRankId:int}")]
        public async Task<ActionResult<JobRankResponseDto>> UpdateJobRankAsync(int jobRankId, JobRankUpdateDto jobRankUpdateDto)
            => Ok(await _serviceManager.JobRanksService.UpdateJobRankAsync(jobRankId, jobRankUpdateDto));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("DeleteJobRank/{id:int}")]
        public async Task<ActionResult> DeleteJobRankAsync(int id)
        {
            await _serviceManager.JobRanksService.DeleteJobRankAsync(id);
            return NoContent();
        }
        #endregion

        #region Administrative Positions
        [ProducesResponseType(typeof(PaginatedResult<AdministrativePositionDto>), StatusCodes.Status200OK)]
        [HttpGet("AdministrativePosition")]
        public async Task<ActionResult<PaginatedResult<AdministrativePositionDto>>> GetAllAdministrativePositionsAsync([FromQuery] AdministrativePositionsSpecificationParameters paramters)
            => Ok(await _serviceManager.AdministrativePositionsService.GetAllAdministrativePositionsAsync(paramters));

        [ProducesResponseType(typeof(AdministrativePositionDto), StatusCodes.Status200OK)]
        [HttpGet("AdministrativePosition/{id:int}")]
        public async Task<ActionResult<AdministrativePositionDto>> GetAdministrativePositionByIdAsync(int id)
            => Ok(await _serviceManager.AdministrativePositionsService.GetAdministrativePositionByIdAsync(id));

        [ProducesResponseType(typeof(AdministrativePositionDto), StatusCodes.Status200OK)]
        [HttpPost("CreateAdministrativePosition")]
        public async Task<ActionResult<AdministrativePositionDto>> CreateAdministrativePositionAsync(AdministrativePositionCreateDto administrativePositionCreateDto)
            => Ok(await _serviceManager.AdministrativePositionsService.CreateAdministrativePositionAsync(administrativePositionCreateDto));

        [ProducesResponseType(typeof(AdministrativePositionDto), StatusCodes.Status200OK)]
        [HttpPut("UpdateAdministrativePosition/{administrativePositionId:int}")]
        public async Task<ActionResult<AdministrativePositionDto>> UpdateAdministrativePositionAsync(int administrativePositionId, AdministrativePositionDto administrativePositionUpdateDto)
            => Ok(await _serviceManager.AdministrativePositionsService.UpdateAdministrativePositionAsync(administrativePositionId, administrativePositionUpdateDto));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("DeleteAdministrativePosition/{id:int}")]
        public async Task<ActionResult> DeleteAdministrativePositionAsync(int id)
        {
            await _serviceManager.AdministrativePositionsService.DeleteAdministrativePositionAsync(id);
            return NoContent();
        }
        #endregion
    }
}
