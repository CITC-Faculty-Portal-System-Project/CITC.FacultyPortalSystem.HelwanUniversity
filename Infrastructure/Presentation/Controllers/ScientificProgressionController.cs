using Shared;
using Shared.Dtos.ScientificProgressionModule;
using Shared.SpecificationParameters.ScientificProgressionModule;


namespace Presentation.Controllers
{
    public class ScientificProgressionController(IServiceManager _serviceManager) : ApiController
    {
        #region Academic Qualifications
        [ProducesResponseType(typeof(PaginatedResult<AcademicQualificationResponseDto>), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("AcademicQualifications")]
        public async Task<ActionResult<PaginatedResult<AcademicQualificationResponseDto>>> GetAllAcademicQualificationsAsync([FromQuery] AcademicQualificationsSpecificationParamters paramters)
            => Ok(await _serviceManager.ScientificProgressionService.GetAllAcademicQualificationsAsync(paramters));

        [ProducesResponseType(typeof(AcademicQualificationResponseDto), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("AcademicQualification/{id:int}")]
        public async Task<ActionResult<AcademicQualificationResponseDto>> GetAcademicQualificationByIdAsync(int id)
            => Ok(await _serviceManager.ScientificProgressionService.GetAcademicQualificationByIdAsync(id));

        [ProducesResponseType(typeof(AcademicQualificationResponseDto), StatusCodes.Status200OK)]
        [HttpPost("CreateAcademicQualification")]
        public async Task<ActionResult<AcademicQualificationResponseDto>> CreateAcademicQualificationAsync(AcademicQualificationCreateDto academicQualificationCreateDto)
            => Ok(await _serviceManager.ScientificProgressionService.CreateAcademicQualificationAsync(academicQualificationCreateDto));

        [ProducesResponseType(typeof(AcademicQualificationResponseDto), StatusCodes.Status200OK)]
        [HttpPut("UpdateAcademicQualification/{academicQualificationId:int}")]
        public async Task<ActionResult<AcademicQualificationResponseDto>> UpdateAcademicQualificationAsync(int academicQualificationId, AcademicQualificationsUpdateDto academicQualificationsUpdateDto)
            => Ok(await _serviceManager.ScientificProgressionService.UpdateAcademicQualificationAsync(academicQualificationId, academicQualificationsUpdateDto));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("DeleteAcademicQualification/{id:int}")]
        public async Task<ActionResult> DeleteAcademicQualificationAsync(int id)
        {
            await _serviceManager.ScientificProgressionService.DeleteAcademicQualificationAsync(id);
            return NoContent();
        }
        #endregion

        #region Job Ranks
        [ProducesResponseType(typeof(PaginatedResult<JobRankResponseDto>), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("JobRanks")]
        public async Task<ActionResult<PaginatedResult<JobRankResponseDto>>> GetAllJobRanksAsync([FromQuery] JobRanksSpecificationsParameters paramters)
            => Ok(await _serviceManager.ScientificProgressionService.GetAllJobRanksAsync(paramters));

        [ProducesResponseType(typeof(JobRankResponseDto), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("JobRank/{id:int}")]
        public async Task<ActionResult<JobRankResponseDto>> GetJobRankByIdAsync(int id)
            => Ok(await _serviceManager.ScientificProgressionService.GetJobRankByIdAsync(id));

        [ProducesResponseType(typeof(JobRankResponseDto), StatusCodes.Status200OK)]
        [HttpPost("CreateJobRank")]
        public async Task<ActionResult<JobRankResponseDto>> CreateJobRankAsync(JobRankCreateDto jobRankCreateDto)
            => Ok(await _serviceManager.ScientificProgressionService.CreateJobRankAsync(jobRankCreateDto));

        [ProducesResponseType(typeof(JobRankResponseDto), StatusCodes.Status200OK)]
        [HttpPut("UpdateJobRank/{jobRankId:int}")]
        public async Task<ActionResult<JobRankResponseDto>> UpdateJobRankAsync(int jobRankId, JobRankUpdateDto jobRankUpdateDto)
            => Ok(await _serviceManager.ScientificProgressionService.UpdateJobRankAsync(jobRankId, jobRankUpdateDto));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("DeleteJobRank/{id:int}")]
        public async Task<ActionResult> DeleteJobRankAsync(int id, [FromQuery] string facultyMemberEmail)
        {
            await _serviceManager.ScientificProgressionService.DeleteJobRankAsync(id);
            return NoContent();
        }
        #endregion

        #region Administrative Positions
        [ProducesResponseType(typeof(PaginatedResult<AdministrativePositionDto>), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("AdministrativePosition")]
        public async Task<ActionResult<PaginatedResult<AdministrativePositionDto>>> GetAllAdministrativePositionsAsync([FromQuery] AdministrativePositionsSpecificationParameters paramters)
            => Ok(await _serviceManager.ScientificProgressionService.GetAllAdministrativePositionsAsync(paramters));

        [ProducesResponseType(typeof(AdministrativePositionDto), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("AdministrativePosition/{id:int}")]
        public async Task<ActionResult<AdministrativePositionDto>> GetAdministrativePositionByIdAsync(int id)
            => Ok(await _serviceManager.ScientificProgressionService.GetAdministrativePositionByIdAsync(id));

        [ProducesResponseType(typeof(AdministrativePositionDto), StatusCodes.Status200OK)]
        [HttpPost("CreateAdministrativePosition")]
        public async Task<ActionResult<AdministrativePositionDto>> CreateAdministrativePositionAsync(AdministrativePositionCreateDto administrativePositionCreateDto)
            => Ok(await _serviceManager.ScientificProgressionService.CreateAdministrativePositionAsync(administrativePositionCreateDto));

        [ProducesResponseType(typeof(AdministrativePositionDto), StatusCodes.Status200OK)]
        [HttpPut("UpdateAdministrativePosition/{administrativePositionId:int}")]
        public async Task<ActionResult<AdministrativePositionDto>> UpdateAdministrativePositionAsync(int administrativePositionId, AdministrativePositionDto administrativePositionUpdateDto)
            => Ok(await _serviceManager.ScientificProgressionService.UpdateAdministrativePositionAsync(administrativePositionId, administrativePositionUpdateDto));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("DeleteAdministrativePosition/{id:int}")]
        public async Task<ActionResult> DeleteAdministrativePositionAsync(int id)
        {
            await _serviceManager.ScientificProgressionService.DeleteAdministrativePositionAsync(id);
            return NoContent();
        }
        #endregion
    }
}
