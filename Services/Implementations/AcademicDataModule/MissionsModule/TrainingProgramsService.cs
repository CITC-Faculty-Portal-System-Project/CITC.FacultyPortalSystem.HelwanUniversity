using Domain.Entities.AcademicDataModule.MissionsModule;
using Services.Abstraction.Contracts.AcademicDataModule.MissionsModule;
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
        IValidationService validationService)
                : BaseService<TrainingPrograms, int>(unitOfWork, authenticationService, mapper, validationService), ITrainingProgramsService
    {
        protected override string EntityName => "Training Programs";
        public async Task<PaginatedResult<TrainingProgramsResponseDto>> GetAllTrainingProgramsAsync(TrainingProgramsSpecificationParameters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            var trainingPrograms = await Repo.GetAllAsync(new TrainingProgramsSpecifications(parameters, currentUser.Email))
                ?? throw NotFound();

            var trainingProgramsResult = Mapper.Map<IEnumerable<TrainingProgramsResponseDto>>(trainingPrograms);

            var currentPageCount = trainingPrograms.Count();

            var totalCount = await Repo.CountAsync(new TrainingProgramsCountSpecifications(parameters, currentUser.Email));

            return new PaginatedResult<TrainingProgramsResponseDto>(parameters.PageIndex, currentPageCount, totalCount, trainingProgramsResult);
        }

        public async Task<TrainingProgramsResponseDto> GetTrainingProgramByIdAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var trainingProgram = await Repo.GetAsync(new TrainingProgramsSpecifications(id))
                ?? throw new NotFoundException("errors.TrainingProgram.notFound" , id);

            EnsureOwnership(trainingProgram.FacultyMemberId, currentUser.UserId, EntityName);

            return Mapper.Map<TrainingProgramsResponseDto>(trainingProgram);
        }

        public async Task<TrainingProgramsResponseDto> CreateTrainingProgramAsync(TrainingProgramsCreateDto trainingProgramsCreateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var trainingProgram = Mapper.Map<TrainingPrograms>(trainingProgramsCreateDto);
            trainingProgram.FacultyMemberId = currentUser.UserId;

            await Repo.AddAsync(trainingProgram);

            await SaveChangesAsync();

            return Mapper.Map<TrainingProgramsResponseDto>(trainingProgram);
        }

        public async Task<TrainingProgramsResponseDto> UpdateTrainingProgramAsync(int id, TrainingProgramsUpdateDto trainingProgramsUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var trainingProgram = await Repo.GetAsync(new TrainingProgramsSpecifications(id))
                ?? throw NotFound();

            EnsureOwnership(trainingProgram.FacultyMemberId, currentUser.UserId, "Training Program");

            Mapper.Map(trainingProgramsUpdateDto, trainingProgram);

            Repo.Update(trainingProgram);
            await SaveChangesAsync();

            return Mapper.Map<TrainingProgramsResponseDto>(trainingProgram);
        }

        public async Task DeleteTrainingProgramAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var trainingProgram = await Repo.GetAsync(new TrainingProgramsSpecifications(id))
                ?? throw NotFound();

            EnsureOwnership(trainingProgram.FacultyMemberId, currentUser.UserId, EntityName);

            trainingProgram.IsDeleted = true;

            Repo.Update(trainingProgram);
            await SaveChangesAsync();
        }
    }
}