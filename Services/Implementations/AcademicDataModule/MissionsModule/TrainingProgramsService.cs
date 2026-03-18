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
       IAuthenticationService authenticationService,
       IMapper mapper)
       : BaseService<TrainingPrograms, int>(unitOfWork, authenticationService, mapper),
         ITrainingProgramsService
    {
        protected override string EntityName => "Training Programs";

        public async Task<PaginatedResult<TrainingProgramsResponseDto>> GetAllTrainingProgramsAsync(
            TrainingProgramsSpecificationParameters parameters,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            var trainingPrograms = await Repo.GetAllAsync(
                new TrainingProgramsSpecifications(parameters, email))
                ?? throw NotFound();

            var mapped = Mapper.Map<IEnumerable<TrainingProgramsResponseDto>>(trainingPrograms);

            var totalCount = await Repo.CountAsync(
                new TrainingProgramsCountSpecifications(parameters, email));

            return new PaginatedResult<TrainingProgramsResponseDto>(
                parameters.PageIndex,
                mapped.Count(),
                totalCount,
                mapped);
        }

        public async Task<TrainingProgramsResponseDto> GetTrainingProgramByIdAsync(
            int id,
            string? facultyMemberEmail = null)
        {
            var trainingProgram = await Repo.GetAsync(
                new TrainingProgramsSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                trainingProgram.FacultyMemberId,
                facultyMemberEmail);

            return Mapper.Map<TrainingProgramsResponseDto>(trainingProgram);
        }

        public async Task<TrainingProgramsResponseDto> CreateTrainingProgramAsync(
            TrainingProgramsCreateDto trainingProgramsCreateDto,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            var facultyMember = await GetFacultyMemberByEmailAsync(email);

            var trainingProgram = Mapper.Map<TrainingPrograms>(trainingProgramsCreateDto);
            trainingProgram.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(trainingProgram);
            await SaveChangesAsync();

            return Mapper.Map<TrainingProgramsResponseDto>(trainingProgram);
        }

        public async Task<TrainingProgramsResponseDto> UpdateTrainingProgramAsync(
            int id,
            TrainingProgramsUpdateDto trainingProgramsUpdateDto,
            string? facultyMemberEmail = null)
        {
            var trainingProgram = await Repo.GetAsync(
                new TrainingProgramsSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                trainingProgram.FacultyMemberId,
                facultyMemberEmail);

            Mapper.Map(trainingProgramsUpdateDto, trainingProgram);

            Repo.Update(trainingProgram);
            await SaveChangesAsync();

            return Mapper.Map<TrainingProgramsResponseDto>(trainingProgram);
        }

        public async Task DeleteTrainingProgramAsync(
            int id,
            string? facultyMemberEmail = null)
        {
            var trainingProgram = await Repo.GetAsync(
                new TrainingProgramsSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                trainingProgram.FacultyMemberId,
                facultyMemberEmail);

            trainingProgram.IsDeleted = true;

            Repo.Update(trainingProgram);
            await SaveChangesAsync();
        }
    }
}