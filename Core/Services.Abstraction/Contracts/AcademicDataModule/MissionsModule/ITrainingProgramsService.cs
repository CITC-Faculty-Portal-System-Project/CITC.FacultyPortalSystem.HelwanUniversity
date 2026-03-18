using Shared.Dtos.AcademicDataModule.MissionsModule;
using Shared.SpecificationParameters.AcademicDataModule.MissionsModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.MissionsModule
{
    public interface ITrainingProgramsService
    {
        Task<PaginatedResult<TrainingProgramsResponseDto>> GetAllTrainingProgramsAsync(
       TrainingProgramsSpecificationParameters parameters,
       string? facultyMemberEmail = null);

        Task<TrainingProgramsResponseDto> GetTrainingProgramByIdAsync(
            int id,
            string? facultyMemberEmail = null);

        Task<TrainingProgramsResponseDto> CreateTrainingProgramAsync(
            TrainingProgramsCreateDto trainingProgramsCreateDto,
            string? facultyMemberEmail = null);

        Task<TrainingProgramsResponseDto> UpdateTrainingProgramAsync(
            int id,
            TrainingProgramsUpdateDto trainingProgramsUpdateDto,
            string? facultyMemberEmail = null);

        Task DeleteTrainingProgramAsync(
            int id,
            string? facultyMemberEmail = null);
    }
}
