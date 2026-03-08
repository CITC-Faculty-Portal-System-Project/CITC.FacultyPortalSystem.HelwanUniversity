using Services.Abstraction.Contracts.AdminModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.MissionsModule;
using Shared.Dtos.AcademicDataModule.MissionsModule;
using Shared.SpecificationParameters.AcademicDataModule.MissionsModule;

namespace Services.Implementations.AdminModule
{
    public class FacultyMemberTrainingProgramsManagementService(ITrainingProgramsHelper _helper)
        : IFacultyMemberTrainingProgramsManagementService
    {

        public Task<PaginatedResult<TrainingProgramsResponseDto>> GetFacultyMemberTrainingProgramsAsync(
            TrainingProgramsSpecificationParameters parameters,
            string facultyMemberEmail)
            => _helper.GetAllTrainingProgramsAsync(parameters, facultyMemberEmail);

        public Task<TrainingProgramsResponseDto> GetFacultyMemberTrainingProgramByIdAsync(int id)
            => _helper.GetTrainingProgramByIdAsync(id);

        public Task<TrainingProgramsResponseDto> CreateFacultyMemberTrainingProgramAsync(
            TrainingProgramsCreateDto trainingProgramsCreateDto,
            string facultyMemberEmail)
            => _helper.CreateTrainingProgramAsync(trainingProgramsCreateDto, facultyMemberEmail);

        public Task<TrainingProgramsResponseDto> UpdateFacultyMemberTrainingProgramAsync(
            int id,
            TrainingProgramsUpdateDto trainingProgramsUpdateDto)
            => _helper.UpdateTrainingProgramAsync(id, trainingProgramsUpdateDto);

        public Task DeleteFacultyMemberTrainingProgramAsync(int id)
            => _helper.DeleteTrainingProgramAsync(id);
    }
}
