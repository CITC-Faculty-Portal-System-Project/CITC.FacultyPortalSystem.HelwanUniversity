using Shared.Dtos.MissionsModule;
using Shared.SpecificationParameters.MissionsModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.MissionsModule
{
    public interface ITrainingProgramsService
    {
        public Task<PaginatedResult<TrainingProgramsResponseDto>> GetAllTrainingProgramsAsync(TrainingProgramsSpecificationParameters parameters);
        public Task<TrainingProgramsResponseDto> GetTrainingProgramByIdAsync(int id);
        public Task<TrainingProgramsResponseDto> CreateTrainingProgramAsync(TrainingProgramsCreateDto trainingProgramsCreateDto);
        public Task<TrainingProgramsResponseDto> UpdateTrainingProgramAsync(int id, TrainingProgramsUpdateDto trainingProgramsUpdateDto);
        public Task DeleteTrainingProgramAsync(int id);
    }
}
