using Domain.Entities.AcademicDataModule.MissionsModule;
using Microsoft.Extensions.Logging;
using Services.Abstraction.Contracts.AcademicDataModule.MissionsModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.MissionsModule;
using Shared.Dtos.AcademicDataModule.MissionsModule;
using Shared.Enums.Logging;
using Shared.SpecificationParameters.AcademicDataModule.MissionsModule;

namespace Services.Implementations.AcademicDataModule.MissionsModule
{
    public class TrainingProgramsService(
       IUnitOfWork unitOfWork,
       IAuthenticationService authenticationService,
       IMapper mapper,
       ILogger<TrainingProgramsService> _logger)
       : BaseService<TrainingPrograms, int>(unitOfWork, authenticationService, mapper),
         ITrainingProgramsService
    {
        protected override string EntityName => "Training Programs";
        private string deleteLater = "training program";

        public async Task<PaginatedResult<TrainingProgramsResponseDto>> GetAllTrainingProgramsAsync(
            TrainingProgramsSpecificationParameters parameters,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            #region Log
            var trainingProgramsLog = new LogEntry
            {
                Category = Category.FacultyMemberAcademicData.ToString(),
                CategoryAction = CategoryAction.TrainingProgramsActions.ToString(),
                UserIP = GetUserIP(),
                UserName = currentUser.UserName,
			};
            #endregion

            var trainingPrograms = await Repo.GetAllAsync(
                new TrainingProgramsSpecifications(parameters, email));
            if(trainingPrograms is null)
            {
				#region Log
				trainingProgramsLog.RenderedMessage = $"Training programs not found for user: {currentUser.UserName}.";
				trainingProgramsLog.Level = "Warning";
				trainingProgramsLog.Timestamp = DateTime.Now;
				trainingProgramsLog.AdditionalData = $"User tried to get their training programs data, but no training programs data was found in the database for user with email : {email}.";
				_logger.LogWarning("{@LogDetails}", trainingProgramsLog);
				#endregion
				throw NotFound();
			}

            var mapped = Mapper.Map<IEnumerable<TrainingProgramsResponseDto>>(trainingPrograms);

            var totalCount = await Repo.CountAsync(
                new TrainingProgramsCountSpecifications(parameters, email));

			#region Log
			trainingProgramsLog.RenderedMessage = $"Training programs data retrieved for user: {currentUser.UserName}.";
			trainingProgramsLog.Level = "Information";
			trainingProgramsLog.Timestamp = DateTime.Now;
			trainingProgramsLog.AdditionalData = $"User retrieved their training programs data successfully, total count of training programs data retrieved: {totalCount}.";
			_logger.LogInformation("{@LogDetails}", trainingProgramsLog);
			#endregion

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
            #region Log
            var currentUser = await GetCurrentUserAsync();
			var trainingProgramLog = new LogEntry
            {
                Category = Category.FacultyMemberAcademicData.ToString(),
                CategoryAction = CategoryAction.TrainingProgramsActions.ToString(),
                UserIP = GetUserIP(),
                UserName = currentUser.UserName,
            };
			#endregion

			var trainingProgram = await Repo.GetAsync(
                new TrainingProgramsSpecifications(id));
            if(trainingProgram is null)
            {
				#region Log
				trainingProgramLog.Timestamp = DateTime.Now;
				trainingProgramLog.Level = "Warning";
				trainingProgramLog.RenderedMessage = $"Training program not found for user: {currentUser.UserName}.";
				trainingProgramLog.AdditionalData = $"User tried to get their training program data with id: {id}, but no training program data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", trainingProgramLog);
				#endregion
				throw NotFound();
			}

            try
            {
                await EnsureOwnershipIfClientAsync(
                        trainingProgram.FacultyMemberId,
                        facultyMemberEmail);
            }
            catch (UnauthorizedAccessException)
            {
				#region Log
				trainingProgramLog.Timestamp = DateTime.Now;
				trainingProgramLog.Level = "Warning";
				trainingProgramLog.RenderedMessage = $"Training program not found for user: {currentUser.UserName}.";
				trainingProgramLog.AdditionalData = $"User tried to get their training program data with id: {id}, but no training program data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", trainingProgramLog);
				#endregion
				throw;
            }

			#region Log
			trainingProgramLog.Timestamp = DateTime.Now;
			trainingProgramLog.Level = "Information";
			trainingProgramLog.RenderedMessage = $"Training program data retrieved for user: {currentUser.UserName}.";
			trainingProgramLog.AdditionalData = $"User retrieved their training program data with id: {id} successfully.";
			_logger.LogInformation("{@LogDetails}", trainingProgramLog);
			#endregion
			return Mapper.Map<TrainingProgramsResponseDto>(trainingProgram);
        }

        public async Task<TrainingProgramsResponseDto> CreateTrainingProgramAsync(
            TrainingProgramsCreateDto trainingProgramsCreateDto,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            #region Log
            var trainingProgramLog = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.TrainingProgramsActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName,
			};
			#endregion

			FacultyMember facultyMember = null!;
            try
            {
                facultyMember = await GetFacultyMemberByEmailAsync(email);
            }
            catch (NotFoundException)
            {
                #region Log

                #endregion
                throw;
            }

            var trainingProgram = Mapper.Map<TrainingPrograms>(trainingProgramsCreateDto);
            trainingProgram.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(trainingProgram);
            await SaveChangesAsync();

            var response = Mapper.Map<TrainingProgramsResponseDto>(trainingProgram);
            #region Log

            #endregion
            return response;
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