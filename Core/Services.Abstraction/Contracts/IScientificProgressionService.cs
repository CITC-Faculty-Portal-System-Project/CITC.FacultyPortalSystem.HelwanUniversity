using Shared.Dtos.ScientificProgressionModule;
using Shared.SpecificationParameters.ScientificProgressionModule;

namespace Services.Abstraction.Contracts
{
    public interface IScientificProgressionService
    {
        #region Academic Qualifications
        public Task<PaginatedResult<AcademicQualificationResponseDto>> GetAllAcademicQualificationsAsync(AcademicQualificationsSpecificationParamters parameters);
        public Task<AcademicQualificationResponseDto> GetAcademicQualificationByIdAsync(int id);
        public Task<AcademicQualificationResponseDto> CreateAcademicQualificationAsync(AcademicQualificationCreateDto academicQualificationCreateDto);
        public Task<AcademicQualificationResponseDto> UpdateAcademicQualificationAsync(int academicQualificationId, AcademicQualificationsUpdateDto academicQualificationsUpdateDto);
        public Task DeleteAcademicQualificationAsync(int academicQualificationId);
        #endregion

        #region Job Ranks
        public Task<PaginatedResult<JobRankResponseDto>> GetAllJobRanksAsync(JobRanksSpecificationsParameters parameters);
        public Task<JobRankResponseDto> GetJobRankByIdAsync(int id);
        public Task<JobRankResponseDto> CreateJobRankAsync(JobRankCreateDto jobRanksCreateDto);
        public Task<JobRankResponseDto> UpdateJobRankAsync(int jobRankId, JobRankUpdateDto jobRanksUpdateDto);
        public Task DeleteJobRankAsync(int jobRankId);
        #endregion

        #region Administrative Positions
        public Task<PaginatedResult<AdministrativePositionDto>> GetAllAdministrativePositionsAsync(AdministrativePositionsSpecificationParameters parameters);
        public Task<AdministrativePositionDto> GetAdministrativePositionByIdAsync(int id);
        public Task<AdministrativePositionDto> CreateAdministrativePositionAsync(AdministrativePositionCreateDto administrativePositionCreateDto);
        public Task<AdministrativePositionDto> UpdateAdministrativePositionAsync(int administrativePositionId, AdministrativePositionDto administrativePositionUpdateDto);
        public Task DeleteAdministrativePositionAsync(int administrativePositionId);
        #endregion
    }
}
