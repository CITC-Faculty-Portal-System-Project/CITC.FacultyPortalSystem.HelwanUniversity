using Shared.Dtos.AcademicDataModule.MissionsModule;
using Shared.SpecificationParameters.AcademicDataModule.MissionsModule;

namespace Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.MissionsModule
{
    public interface ITrainingProgramsHelper
    {
        Task<PaginatedResult<TrainingProgramsResponseDto>> GetAllTrainingProgramsAsync(
            TrainingProgramsSpecificationParameters parameters,
            string facultyMemberEmail);

        Task<TrainingProgramsResponseDto> GetTrainingProgramByIdAsync(int id);

        Task<TrainingProgramsResponseDto> CreateTrainingProgramAsync(
            TrainingProgramsCreateDto trainingProgramsCreateDto,
            string facultyMemberEmail);

        Task<TrainingProgramsResponseDto> UpdateTrainingProgramAsync(
            int id,
            TrainingProgramsUpdateDto trainingProgramsUpdateDto);

        Task DeleteTrainingProgramAsync(int id);
    }
}
