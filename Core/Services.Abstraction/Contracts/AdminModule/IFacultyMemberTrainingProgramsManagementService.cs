using Shared.Dtos.AcademicDataModule.MissionsModule;
using Shared.SpecificationParameters.AcademicDataModule.MissionsModule;

namespace Services.Abstraction.Contracts.AdminModule
{
    public interface IFacultyMemberTrainingProgramsManagementService
    {
        Task<PaginatedResult<TrainingProgramsResponseDto>> GetFacultyMemberTrainingProgramsAsync(
           TrainingProgramsSpecificationParameters parameters,
           string facultyMemberEmail);

        Task<TrainingProgramsResponseDto> GetFacultyMemberTrainingProgramByIdAsync(int id);

        Task<TrainingProgramsResponseDto> CreateFacultyMemberTrainingProgramAsync(
            TrainingProgramsCreateDto trainingProgramsCreateDto,
            string facultyMemberEmail);

        Task<TrainingProgramsResponseDto> UpdateFacultyMemberTrainingProgramAsync(
            int id,
            TrainingProgramsUpdateDto trainingProgramsUpdateDto);

        Task DeleteFacultyMemberTrainingProgramAsync(int id);
    }
}
