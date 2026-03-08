using Domain.Entities.AcademicDataModule.MissionsModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.MissionsModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.MissionsModule;
using Shared.Dtos.AcademicDataModule.MissionsModule;
using Shared.SpecificationParameters.AcademicDataModule.MissionsModule;

namespace Services.Implementations.SharedLogicBetweenAdminAndFacultyMember.MissionsModule
{
    public class TrainingProgramsHelper(
        IUnitOfWork unitOfWork,
        IAuthenticationService authenticationService,
        IMapper mapper)
        : BaseService<TrainingPrograms, int>(unitOfWork, authenticationService, mapper),
          ITrainingProgramsHelper
    {
        protected override string EntityName => "Training Programs";

        public async Task<PaginatedResult<TrainingProgramsResponseDto>> GetAllTrainingProgramsAsync(
            TrainingProgramsSpecificationParameters parameters,
            string facultyMemberEmail)
        {
            var trainingPrograms = await Repo.GetAllAsync(
                new TrainingProgramsSpecifications(parameters, facultyMemberEmail));

            var trainingProgramsResult =
                Mapper.Map<IEnumerable<TrainingProgramsResponseDto>>(trainingPrograms);

            var currentPageCount = trainingProgramsResult.Count();

            var totalCount = await Repo.CountAsync(
                new TrainingProgramsCountSpecifications(parameters, facultyMemberEmail));

            return new PaginatedResult<TrainingProgramsResponseDto>(
                parameters.PageIndex,
                currentPageCount,
                totalCount,
                trainingProgramsResult);
        }

        public async Task<TrainingProgramsResponseDto> GetTrainingProgramByIdAsync(int id)
        {
            var trainingProgram = await Repo.GetAsync(new TrainingProgramsSpecifications(id))
                ?? throw NotFound();

            return Mapper.Map<TrainingProgramsResponseDto>(trainingProgram);
        }

        public async Task<TrainingProgramsResponseDto> CreateTrainingProgramAsync(
            TrainingProgramsCreateDto trainingProgramsCreateDto,
            string facultyMemberEmail)
        {
            var facultyMember = await GetFacultyMemberByEmailAsync(facultyMemberEmail);

            var trainingProgram = Mapper.Map<TrainingPrograms>(trainingProgramsCreateDto);
            trainingProgram.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(trainingProgram);
            await SaveChangesAsync();

            return Mapper.Map<TrainingProgramsResponseDto>(trainingProgram);
        }

        public async Task<TrainingProgramsResponseDto> UpdateTrainingProgramAsync(
            int id,
            TrainingProgramsUpdateDto trainingProgramsUpdateDto)
        {
            var trainingProgram = await Repo.GetAsync(new TrainingProgramsSpecifications(id))
                ?? throw NotFound();

            Mapper.Map(trainingProgramsUpdateDto, trainingProgram);

            Repo.Update(trainingProgram);
            await SaveChangesAsync();

            return Mapper.Map<TrainingProgramsResponseDto>(trainingProgram);
        }

        public async Task DeleteTrainingProgramAsync(int id)
        {
            var trainingProgram = await Repo.GetAsync(new TrainingProgramsSpecifications(id))
                ?? throw NotFound();

            trainingProgram.IsDeleted = true;

            Repo.Update(trainingProgram);
            await SaveChangesAsync();
        }
    }
}
