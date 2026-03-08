using Domain.Entities.AcademicDataModule.MissionsModule;
using Services.Abstraction.Contracts.AcademicDataModule.MissionsModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.MissionsModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.MissionsModule;
using Shared.Dtos.AcademicDataModule.MissionsModule;
using Shared.SpecificationParameters.AcademicDataModule.MissionsModule;

namespace Services.Implementations.AcademicDataModule.MissionsModule
{
    public class TrainingProgramsService(
           IUnitOfWork unitOfWork,
           IMapper mapper,
           IAuthenticationService authenticationService,
           ITrainingProgramsHelper trainingProgramsHelper)
           : BaseService<TrainingPrograms, int>(unitOfWork, authenticationService, mapper),
             ITrainingProgramsService
    {
        private readonly ITrainingProgramsHelper _helper = trainingProgramsHelper;

        protected override string EntityName => "Training Programs";

        public async Task<PaginatedResult<TrainingProgramsResponseDto>> GetAllTrainingProgramsAsync(
            TrainingProgramsSpecificationParameters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            return await _helper.GetAllTrainingProgramsAsync(parameters, currentUser.Email);
        }

        public async Task<TrainingProgramsResponseDto> GetTrainingProgramByIdAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var trainingProgram = await Repo.GetAsync(new TrainingProgramsSpecifications(id))
                ?? throw new NotFoundException("Training Program is Not Found.");

            EnsureOwnership(trainingProgram.FacultyMemberId, currentUser.UserId, EntityName);

            return await _helper.GetTrainingProgramByIdAsync(id);
        }

        public async Task<TrainingProgramsResponseDto> CreateTrainingProgramAsync(
            TrainingProgramsCreateDto trainingProgramsCreateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            return await _helper.CreateTrainingProgramAsync(trainingProgramsCreateDto, currentUser.Email);
        }

        public async Task<TrainingProgramsResponseDto> UpdateTrainingProgramAsync(
            int id,
            TrainingProgramsUpdateDto trainingProgramsUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var trainingProgram = await Repo.GetAsync(new TrainingProgramsSpecifications(id))
                ?? throw NotFound();

            EnsureOwnership(trainingProgram.FacultyMemberId, currentUser.UserId, "Training Program");

            return await _helper.UpdateTrainingProgramAsync(id, trainingProgramsUpdateDto);
        }

        public async Task DeleteTrainingProgramAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var trainingProgram = await Repo.GetAsync(new TrainingProgramsSpecifications(id))
                ?? throw NotFound();

            EnsureOwnership(trainingProgram.FacultyMemberId, currentUser.UserId, EntityName);

            await _helper.DeleteTrainingProgramAsync(id);
        }
    }
}