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
        public async Task<ActionResult<AcademicQualificationResponseDto>> GetAllAcademicQualificationsAsync([FromQuery] AcademicQualificationsSpecificationParamters paramters)
            => Ok(await _serviceManager.ScientificProgressionService.GetAllAcademicQualificationsAsync(paramters));

        [ProducesResponseType(typeof(AcademicQualificationResponseDto), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("AcademicQualification/{id:int}")]
        public async Task<ActionResult<AcademicQualificationResponseDto>> GetAcademicQualificationByIdAsync(int id)
            => Ok(await _serviceManager.ScientificProgressionService.GetAcademicQualificationByIdAsync(id));

        [ProducesResponseType(typeof(AcademicQualificationResponseDto), StatusCodes.Status200OK)]
        [HttpPost("CreateAcademicQualification")]
        public async Task<ActionResult<AcademicQualificationResponseDto>> CreateAcademicQualificationAsync([FromQuery] string facultyMemberEmail, AcademicQualificationCreateDto academicQualificationCreateDto)
            => Ok(await _serviceManager.ScientificProgressionService.CreateAcademicQualificationAsync(facultyMemberEmail, academicQualificationCreateDto));

        [ProducesResponseType(typeof(AcademicQualificationResponseDto), StatusCodes.Status200OK)]
        [HttpPut("UpdateAcademicQualification")]
        public async Task<ActionResult<AcademicQualificationResponseDto>> UpdateAcademicQualificationAsync([FromQuery] int academicQualificationId, [FromQuery] string facultyMemberEmail, AcademicQualificationsUpdateDto academicQualificationsUpdateDto)
            => Ok(await _serviceManager.ScientificProgressionService.UpdateAcademicQualificationAsync(academicQualificationId, facultyMemberEmail, academicQualificationsUpdateDto));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("DeleteAcademicQualification/{id:int}")]
        public async Task<ActionResult> DeleteAcademicQualificationAsync(int id, [FromQuery] string facultyMemberEmail)
        {
            await _serviceManager.ScientificProgressionService.DeleteAcademicQualificationAsync(id, facultyMemberEmail);
            return NoContent();
        }
        #endregion

        #region Job Ranks
        [ProducesResponseType(typeof(PaginatedResult<JobRankResponseDto>), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("JobRanks")]
        public async Task<ActionResult<JobRankResponseDto>> GetAllJobRanksAsync([FromQuery] JobRanksSpecificationsParameters paramters)
            => Ok(await _serviceManager.ScientificProgressionService.GetAllJobRanksAsync(paramters));

        [ProducesResponseType(typeof(JobRankResponseDto), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("JobRank/{id:int}")]
        public async Task<ActionResult<JobRankResponseDto>> GetJobRankById(int id)
            => Ok(await _serviceManager.ScientificProgressionService.GetJobRankById(id));

        [ProducesResponseType(typeof(JobRankResponseDto), StatusCodes.Status200OK)]
        [HttpPost("CreateJobRank")]
        public async Task<ActionResult<JobRankResponseDto>> CreateJobRankAsync([FromQuery] string facultyMemberEmail, JobRankCreateDto jobRankCreateDto)
            => Ok(await _serviceManager.ScientificProgressionService.CreateJobRankAsync(facultyMemberEmail, jobRankCreateDto));

        [ProducesResponseType(typeof(JobRankResponseDto), StatusCodes.Status200OK)]
        [HttpPut("UpdateJobRank")]
        public async Task<ActionResult<JobRankResponseDto>> UpdateJobRankAsync([FromQuery] int jobRankId, [FromQuery] string facultyMemberEmail, JobRankUpdateDto jobRankUpdateDto)
            => Ok(await _serviceManager.ScientificProgressionService.UpdateJobRankAsync(jobRankId, facultyMemberEmail, jobRankUpdateDto));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("DeleteJobRank/{id:int}")]
        public async Task<ActionResult> DeleteJobRankAsync(int id, [FromQuery] string facultyMemberEmail)
        {
            await _serviceManager.ScientificProgressionService.DeleteJobRankAsync(id, facultyMemberEmail);
            return NoContent();
        }
        #endregion

        #region Administrative Positions
        [ProducesResponseType(typeof(PaginatedResult<AdministrativePositionDto>), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("AdministrativePosition")]
        public async Task<ActionResult<AdministrativePositionDto>> GetAllAdministrativePositionsAsync([FromQuery] AdministrativePositionsSpecificationParameters paramters)
            => Ok(await _serviceManager.ScientificProgressionService.GetAllAdministrativePositionsAsync(paramters));

        [ProducesResponseType(typeof(AdministrativePositionDto), StatusCodes.Status200OK)]
        [RedisCache]
        [HttpGet("AdministrativePosition/{id:int}")]
        public async Task<ActionResult<AdministrativePositionDto>> GetAdministrativePositionById(int id)
            => Ok(await _serviceManager.ScientificProgressionService.GetAdministrativePositionById(id));

        [ProducesResponseType(typeof(AdministrativePositionDto), StatusCodes.Status200OK)]
        [HttpPost("CreateAdministrativePosition")]
        public async Task<ActionResult<AdministrativePositionDto>> CreateAdministrativePositionAsync([FromQuery] string facultyMemberEmail, AdministrativePositionCreateDto administrativePositionCreateDto)
            => Ok(await _serviceManager.ScientificProgressionService.CreateAdministrativePositionAsync(facultyMemberEmail, administrativePositionCreateDto));

        [ProducesResponseType(typeof(AdministrativePositionDto), StatusCodes.Status200OK)]
        [HttpPut("UpdateAdministrativePosition")]
        public async Task<ActionResult<AdministrativePositionDto>> UpdateAdministrativePositionAsync([FromQuery] int administrativePositionId, [FromQuery] string facultyMemberEmail, AdministrativePositionDto administrativePositionUpdateDto)
            => Ok(await _serviceManager.ScientificProgressionService.UpdateAdministrativePositionAsync(administrativePositionId, facultyMemberEmail, administrativePositionUpdateDto));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("DeleteAdministrativePosition/{id:int}")]
        public async Task<ActionResult> DeleteAdministrativePositionAsync(int id, [FromQuery] string facultyMemberEmail)
        {
            await _serviceManager.ScientificProgressionService.DeleteAdministrativePositionAsync(id, facultyMemberEmail);
            return NoContent();
        }
        #endregion
    }
}
