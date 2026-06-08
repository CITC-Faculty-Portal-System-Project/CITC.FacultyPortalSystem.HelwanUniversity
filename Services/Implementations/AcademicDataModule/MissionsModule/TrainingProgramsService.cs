using Domain.Entities.AcademicDataModule.MissionsModule;
using Microsoft.Extensions.Logging;
using Services.Abstraction.Contracts.AcademicDataModule.MissionsModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.MissionsModule;
using Shared.Dtos.AcademicDataModule.MissionsModule;
using Shared.Enums.Logging;
using Shared.SpecificationParameters.AcademicDataModule.MissionsModule;
using System.Text.Encodings.Web;
using System.Text.Json;

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

        public async Task<PaginatedResult<TrainingProgramsResponseDto>> GetAllTrainingProgramsAsync(
            TrainingProgramsSpecificationParameters parameters,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

			#region Log
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(email);
			var trainingProgramsLog = new LogEntry
            {
                Category = Category.FacultyMemberMissions.ToString(),
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
				trainingProgramsLog.RenderedMessage = $"Training programs not found for user: {userOfData.UserName}.";
				trainingProgramsLog.Level = "Warning";
				trainingProgramsLog.Timestamp = DateTime.Now;
				trainingProgramsLog.AdditionalData = (facultyMemberEmail is null) ? $"User tried to get their training programs data, but no training programs data was found in the database for user with email: {email}."
					: $"Admin: {currentUser.UserName} tried to get user: {userOfData.UserName} training programs data, but no training programs data was found in the database for user: {userOfData.UserName}";
				_logger.LogWarning("{@LogDetails}", trainingProgramsLog);
				#endregion
				throw NotFound();
			}

            var mapped = Mapper.Map<IEnumerable<TrainingProgramsResponseDto>>(trainingPrograms);

            var totalCount = await Repo.CountAsync(
                new TrainingProgramsCountSpecifications(parameters, email));

			#region Log
			trainingProgramsLog.RenderedMessage = $"Training programs data retrieved for user: {userOfData.UserName}.";
			trainingProgramsLog.Level = "Information";
			trainingProgramsLog.Timestamp = DateTime.Now;
			trainingProgramsLog.AdditionalData = (facultyMemberEmail is null) ? $"User retrieved their training programs data successfully, total count of training programs data retrieved: {totalCount}."
				: $"Admin: {currentUser.UserName} retrieved user: {userOfData.UserName} training programs data successfully, total count of training programs data retrieved: {totalCount}.";
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
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(facultyMemberEmail);
			var trainingProgramLog = new LogEntry
            {
                Category = Category.FacultyMemberMissions.ToString(),
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
				trainingProgramLog.RenderedMessage = $"Training program not found for user: {userOfData.UserName}.";
				trainingProgramLog.AdditionalData = (facultyMemberEmail is null) ? $"User tried to get their training program data with id: {id}, but no training program data with this id was found in the database."
					: $"Admin: {currentUser.UserName} tried to get user: {userOfData.UserName} training program data with id: {id}, but no training program data with this id was found in the database.";
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
			trainingProgramLog.RenderedMessage = $"Training program data retrieved for user: {userOfData.UserName}.";
			trainingProgramLog.AdditionalData = (facultyMemberEmail is null) ? $"User retrieved their training program data with id: {id} successfully."
				: $"Admin: {currentUser.UserName} retrieved user: {userOfData.UserName} training program data with id: {id} successfully.";
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
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(email);
			var trainingProgramLog = new LogEntry
			{
				Category = Category.FacultyMemberMissions.ToString(),
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
				trainingProgramLog.Timestamp = DateTime.Now;
				trainingProgramLog.Level = "Warning";
				trainingProgramLog.RenderedMessage = $"Faculty Member not found.";
				trainingProgramLog.AdditionalData = (facultyMemberEmail is null) ? $"User tried to create a training program for a faculty member that does not exist in database, no faculty member found with email: {email}."
					: $"Admin: {currentUser.UserName} tried to create a training program for user: {userOfData.UserName}, but no faculty member found with email: {email}.";
				_logger.LogWarning("{@LogDetails}", trainingProgramLog);
				#endregion
				throw;
            }

            var trainingProgram = Mapper.Map<TrainingPrograms>(trainingProgramsCreateDto);
            trainingProgram.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(trainingProgram);
            await SaveChangesAsync();

            var response = Mapper.Map<TrainingProgramsResponseDto>(trainingProgram);
            #region Log
            trainingProgramLog.Timestamp = DateTime.Now;
			trainingProgramLog.Level = "Information";
            trainingProgramLog.RenderedMessage = (facultyMemberEmail is null) ? $"User: {userOfData.UserName} created a training program."
				: $"Admin: {currentUser.UserName} created a training program for user: {userOfData.UserName}";
            trainingProgramLog.AdditionalData = (facultyMemberEmail is null) ? $"User created a training program with id: {response.Id} and Name: {response.TrainingProgramName} successfully."
				: $"Admin: {currentUser.UserName} created a training program with id: {response.Id} and Name: {response.TrainingProgramName} for user: {userOfData.UserName} successfully.";
			_logger.LogInformation("{@LogDetails}", trainingProgramLog);
			#endregion
			return response;
        }

        public async Task<TrainingProgramsResponseDto> UpdateTrainingProgramAsync(
            int id,
            TrainingProgramsUpdateDto trainingProgramsUpdateDto,
            string? facultyMemberEmail = null)
        {
			#region Log
            var currentUser = await GetCurrentUserAsync();
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(facultyMemberEmail);
			var trainingProgramLog = new LogEntry
			{
				Category = Category.FacultyMemberMissions.ToString(),
				CategoryAction = CategoryAction.TrainingProgramsActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName,
			};
			#endregion
			var jsonOptions = new JsonSerializerOptions
			{
				WriteIndented = true,
				Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
			};
            var trainingProgram = await Repo.GetAsync(
                new TrainingProgramsSpecifications(id));
            if(trainingProgram is null)
            {
                #region Log
                trainingProgramLog.Timestamp = DateTime.Now;
				trainingProgramLog.Level = "Warning";
				trainingProgramLog.RenderedMessage = $"Training program not found for user: {userOfData.UserName}.";
				trainingProgramLog.AdditionalData = (facultyMemberEmail is null) ? $"User tried to update their training program data with id: {id}, but no training program data with this id was found in the database."
					: $"Admin: {currentUser.UserName} tried to update user: {userOfData.UserName} training program data with id: {id}, but no training program data with this id was found in the database.";
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
				trainingProgramLog.RenderedMessage = $"User unauthorized to update training program data.";
				trainingProgramLog.AdditionalData = $"User tried to update training program data with id: {id} that does not belong to them, training program data faculty member id: {trainingProgram.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", trainingProgramLog);
				#endregion
				throw;
            }

            var oldData = Mapper.Map<TrainingProgramsResponseDto>(trainingProgram);
			Mapper.Map(trainingProgramsUpdateDto, trainingProgram);

            Repo.Update(trainingProgram);
            await SaveChangesAsync();

            var newData = Mapper.Map<TrainingProgramsResponseDto>(trainingProgram);
            #region Log
            trainingProgramLog.Timestamp = DateTime.Now;
			trainingProgramLog.Level = "Information";
			trainingProgramLog.RenderedMessage = $"Training program data updated for user: {userOfData.UserName}.";
			trainingProgramLog.AdditionalData = (facultyMemberEmail is null) ? $"User updated their training program data with id: {id} successfully.\nOld Data: {JsonSerializer.Serialize(oldData, jsonOptions)}\nNew Data: {JsonSerializer.Serialize(newData, jsonOptions)}."
				: $"Admin: {currentUser.UserName} updated user: {userOfData.UserName} training program data with id: {id} successfully.\nOld Data: {JsonSerializer.Serialize(oldData, jsonOptions)}\nNew Data: {JsonSerializer.Serialize(newData, jsonOptions)}.";
			_logger.LogInformation("{@LogDetails}", trainingProgramLog);
			#endregion
			return Mapper.Map<TrainingProgramsResponseDto>(trainingProgram);
        }

        public async Task DeleteTrainingProgramAsync(
            int id,
            string? facultyMemberEmail = null)
        {
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(facultyMemberEmail);
			var trainingProgramLog = new LogEntry
			{
				Category = Category.FacultyMemberMissions.ToString(),
				CategoryAction = CategoryAction.TrainingProgramsActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName,
			};
			#endregion
			var trainingProgram = await Repo.GetAsync(
                new TrainingProgramsSpecifications(id));
			if (trainingProgram is null)
            {
				#region Log
				trainingProgramLog.Timestamp = DateTime.Now;
				trainingProgramLog.Level = "Warning";
				trainingProgramLog.RenderedMessage = $"Training program not found for user: {userOfData.UserName}.";
				trainingProgramLog.AdditionalData = (facultyMemberEmail is null) ? $"User tried to delete their training program data with id: {id}, but no training program data with this id was found in the database."
					: $"Admin: {currentUser.UserName} tried to delete user: {userOfData.UserName} training program data with id: {id}, but no training program data with this id was found in the database.";
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
				trainingProgramLog.RenderedMessage = $"User unauthorized to delete training program data.";
				trainingProgramLog.AdditionalData = $"User tried to delete training program data with id: {id} that does not belong to them, training program data faculty member id: {trainingProgram.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", trainingProgramLog);
				#endregion
				throw;
            }

            trainingProgram.IsDeleted = true;

            Repo.Update(trainingProgram);
            await SaveChangesAsync();
			#region Log
			trainingProgramLog.Timestamp = DateTime.Now;
			trainingProgramLog.Level = "Information";
			trainingProgramLog.RenderedMessage = $"Training program data deleted for user: {userOfData.UserName}.";
			trainingProgramLog.AdditionalData = (facultyMemberEmail is null) ? $"User deleted their training program data with id: {id} successfully."
				: $"Admin: {currentUser.UserName} deleted user: {userOfData.UserName} training program data with id: {id} successfully.";
			_logger.LogInformation("{@LogDetails}", trainingProgramLog);
			#endregion
		}
    }
}