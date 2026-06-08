using Domain.Entities.AcademicDataModule.ExperiencesModule;
using Microsoft.Extensions.Logging;
using Services.Abstraction.Contracts.AcademicDataModule.ExperiencesModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.ExperiencesModule;
using Shared.Dtos.AcademicDataModule.ExperiencesModule;
using Shared.Enums.Logging;
using Shared.SpecificationParameters.AcademicDataModule.ExperiencesModule;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Services.Implementations.AcademicDataModule.ExperiencesModule
{
	public class GeneralExperiencesService(
	  IUnitOfWork unitOfWork,
	  IAuthenticationService authenticationService,
	  IMapper mapper,
	  ILogger<GeneralExperiencesService> _logger)
	  : BaseService<GeneralExperiences, int>(unitOfWork, authenticationService, mapper),
		IGeneralExperiencesService
	{
		protected override string EntityName => "General Experiences";

		public async Task<PaginatedResult<GeneralExperiencesResponseDTO>> GetAllGeneralExperiencesAsync(
			GeneralExperiencesSpecificationParameters parameters,
			string? facultyMemberEmail = null)
		{
			var currentUser = await GetCurrentUserAsync();
			var email = facultyMemberEmail ?? currentUser.Email;

			#region Log
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(email);
			var experiencesLog = new LogEntry
			{
				Category = Category.FacultyMemberExperiences.ToString(),
				CategoryAction = CategoryAction.GeneralExperiencesActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion

			var generalExperiences = await Repo.GetAllAsync(
				new GeneralExperiencesSpecifications(parameters, email));
			if (generalExperiences is null)
			{
				#region Log
				experiencesLog.RenderedMessage = $"General experiences not found for user: {userOfData.UserName}.";
				experiencesLog.Level = "Warning";
				experiencesLog.Timestamp = DateTime.Now;
				experiencesLog.AdditionalData = (facultyMemberEmail is null) ? $"User tried to get their general experiences data, but no general experiences data was found in the database for user with email : {email}."
					: $"Admin: {currentUser.UserName} tried to get user: {userOfData.UserName} general experiences data, but no general experiences data was found in the database for user: {userOfData.UserName}";
				_logger.LogWarning("{@LogDetails}", experiencesLog);
				#endregion
				throw NotFound();
			}

			var mapped = Mapper.Map<IEnumerable<GeneralExperiencesResponseDTO>>(generalExperiences);

			var totalCount = await Repo.CountAsync(
				new GeneralExperiencesCountSpecifications(parameters, email));

			#region Log
			experiencesLog.RenderedMessage = $"General experiences data retrieved for user: {userOfData.UserName}.";
			experiencesLog.Level = "Information";
			experiencesLog.Timestamp = DateTime.Now;
			experiencesLog.AdditionalData = (facultyMemberEmail is null) ? $"User retrieved their general experiences data successfully, total count of general experiences data retrieved: {totalCount}."
				: $"Admin: {currentUser.UserName} retrieved user: {userOfData.UserName} general experiences data successfully, total count of general experiences data retrieved: {totalCount}.";
			_logger.LogInformation("{@LogDetails}", experiencesLog);
			#endregion

			return new PaginatedResult<GeneralExperiencesResponseDTO>(
				parameters.PageIndex,
				mapped.Count(),
				totalCount,
				mapped);
		}

		public async Task<GeneralExperiencesResponseDTO> GetGeneralExperienceByIdAsync(
			int id,
			string? facultyMemberEmail = null)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(facultyMemberEmail);
			var experiencesLog = new LogEntry
			{
				Category = Category.FacultyMemberExperiences.ToString(),
				CategoryAction = CategoryAction.GeneralExperiencesActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion

			var generalExperience = await Repo.GetAsync(
				new GeneralExperiencesSpecifications(id));
			if (generalExperience is null)
			{
				#region Log
				experiencesLog.Timestamp = DateTime.Now;
				experiencesLog.Level = "Warning";
				experiencesLog.RenderedMessage = $"General experience not found for user: {userOfData.UserName}.";
				experiencesLog.AdditionalData = (facultyMemberEmail is null) ? $"User tried to get their general experience data with id: {id}, but no general experience data with this id was found in the database."
					: $"Admin: {currentUser.UserName} tried to get user: {userOfData.UserName} general experience data with id: {id}, but no general experience data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", experiencesLog);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						generalExperience.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				experiencesLog.Timestamp = DateTime.Now;
				experiencesLog.Level = "Warning";
				experiencesLog.RenderedMessage = $"User unauthorized to access general experience data.";
				experiencesLog.AdditionalData = $"User tried to get general experience data with id: {id} that does not belong to them, general experience data faculty member id: {generalExperience.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", experiencesLog);
				#endregion
				throw;
			}

			#region Log
			experiencesLog.Timestamp = DateTime.Now;
			experiencesLog.Level = "Information";
			experiencesLog.RenderedMessage = $"General experience data retrieved for user: {userOfData.UserName}.";
			experiencesLog.AdditionalData = (facultyMemberEmail is null) ? $"User retrieved their general experience data with id: {id} successfully."
				: $"Admin: {currentUser.UserName} retrieved user: {userOfData.UserName} general experience data with id: {id} successfully.";
			_logger.LogInformation("{@LogDetails}", experiencesLog);
			#endregion
			return Mapper.Map<GeneralExperiencesResponseDTO>(generalExperience);
		}

		public async Task<GeneralExperiencesResponseDTO> CreateGeneralExperienceAsync(
			GeneralExperiencesCreateDTO generalExperienceCreateDto,
			string? facultyMemberEmail = null)
		{
			var currentUser = await GetCurrentUserAsync();
			var email = facultyMemberEmail ?? currentUser.Email;

			#region Log
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(email);
			var generalExperienceLog = new LogEntry
			{
				Category = Category.FacultyMemberExperiences.ToString(),
				CategoryAction = CategoryAction.GeneralExperiencesActions.ToString(),
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
				generalExperienceLog.Timestamp = DateTime.Now;
				generalExperienceLog.Level = "Warning";
				generalExperienceLog.RenderedMessage = $"Faculty Member not found.";
				generalExperienceLog.AdditionalData = (facultyMemberEmail is null) ? $"User tried to create a general experience for a faculty member that does not exist in database, no faculty member found with email : {email}."
					: $"Admin: {currentUser.UserName} tried to create a general experience for user: {userOfData.UserName}, but no faculty member found with email : {email}.";
				_logger.LogWarning("{@LogDetails}", generalExperienceLog);
				#endregion
				throw;
			}

			var generalExperience = Mapper.Map<GeneralExperiences>(generalExperienceCreateDto);
			generalExperience.FacultyMemberId = facultyMember.Id;

			await Repo.AddAsync(generalExperience);
			await SaveChangesAsync();

			var response = Mapper.Map<GeneralExperiencesResponseDTO>(generalExperience);
			#region Log
			generalExperienceLog.Timestamp = DateTime.Now;
			generalExperienceLog.Level = "Information";
			generalExperienceLog.RenderedMessage = (facultyMemberEmail is null) ? $"User: {userOfData.UserName} created a general experience."
				: $"Admin: {currentUser.UserName} created a general experience for user: {userOfData.UserName}";
			generalExperienceLog.AdditionalData = (facultyMemberEmail is null) ? $"User created a general experience with id: {response.Id} and title: {response.ExperienceTitle} successfully."
				: $"Admin: {currentUser.UserName} created a general experience with id: {response.Id} and title: {response.ExperienceTitle} for user: {userOfData.UserName} successfully.";

			_logger.LogInformation("{@LogDetails}", generalExperienceLog);
			#endregion
			return response;
		}

		public async Task<GeneralExperiencesResponseDTO> UpdateGeneralExperienceAsync(
			int generalExperienceId,
			GeneralExperiencesUpdateDTO generalExperienceUpdateDto,
			string? facultyMemberEmail = null)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(facultyMemberEmail);
			var generalExperienceLog = new LogEntry
			{
				Category = Category.FacultyMemberExperiences.ToString(),
				CategoryAction = CategoryAction.GeneralExperiencesActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName,
			};
			#endregion
			var jsonOptions = new JsonSerializerOptions
			{
				WriteIndented = true,
				Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
			};
			var generalExperience = await Repo.GetAsync(
				new GeneralExperiencesSpecifications(generalExperienceId));
			if (generalExperience is null)
			{
				#region Log
				generalExperienceLog.Timestamp = DateTime.Now;
				generalExperienceLog.Level = "Warning";
				generalExperienceLog.RenderedMessage = $"General experience not found for user: {userOfData.UserName}.";
				generalExperienceLog.AdditionalData = (facultyMemberEmail is null) ? $"User tried to update their general experience data with id: {generalExperienceId}, but no general experience data with this id was found in the database."
					: $"Admin: {currentUser.UserName} tried to update user: {userOfData.UserName} general experience data with id: {generalExperienceId}, but no general experience data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", generalExperienceLog);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						generalExperience.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				generalExperienceLog.Timestamp = DateTime.Now;
				generalExperienceLog.Level = "Warning";
				generalExperienceLog.RenderedMessage = $"User unauthorized to update general experience data.";
				generalExperienceLog.AdditionalData = $"User tried to update general experience data with id: {generalExperienceId} that does not belong to them, general experience data faculty member id: {generalExperience.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", generalExperienceLog);
				#endregion
				throw;
			}

			var oldData = Mapper.Map<GeneralExperiencesResponseDTO>(generalExperience);
			Mapper.Map(generalExperienceUpdateDto, generalExperience);

			Repo.Update(generalExperience);
			await SaveChangesAsync();

			var newData = Mapper.Map<GeneralExperiencesResponseDTO>(generalExperience);
			#region Log
			generalExperienceLog.Timestamp = DateTime.Now;
			generalExperienceLog.Level = "Information";
			generalExperienceLog.RenderedMessage = $"General experience data updated for user: {userOfData.UserName}.";
			generalExperienceLog.AdditionalData = (facultyMemberEmail is null) ? $"User updated their general experience data with id: {generalExperienceId} successfully.\nOld Data: {JsonSerializer.Serialize(oldData, jsonOptions)}\nNew Data: {JsonSerializer.Serialize(newData, jsonOptions)}."
				: $"Admin: {currentUser.UserName} updated user: {userOfData.UserName} general experience data with id: {generalExperienceId} successfully.\nOld Data: {JsonSerializer.Serialize(oldData, jsonOptions)}\nNew Data: {JsonSerializer.Serialize(newData, jsonOptions)}.";
			_logger.LogInformation("{@LogDetails}", generalExperienceLog);
			#endregion
			return newData;
		}

		public async Task DeleteGeneralExperienceAsync(
			int generalExperienceId,
			string? facultyMemberEmail = null)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(facultyMemberEmail);
			var generalExperienceLog = new LogEntry
			{
				Category = Category.FacultyMemberExperiences.ToString(),
				CategoryAction = CategoryAction.GeneralExperiencesActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName,
			};
			#endregion
			var generalExperience = await Repo.GetAsync(
				new GeneralExperiencesSpecifications(generalExperienceId));
			if (generalExperience is null)
			{
				#region Log
				generalExperienceLog.Timestamp = DateTime.Now;
				generalExperienceLog.Level = "Warning";
				generalExperienceLog.RenderedMessage = $"General experience not found for user: {userOfData.UserName}.";
				generalExperienceLog.AdditionalData = (facultyMemberEmail is null) ? $"User tried to delete their general experience data with id: {generalExperienceId}, but no general experience data with this id was found in the database."
					: $"Admin: {currentUser.UserName} tried to delete user: {userOfData.UserName} general experience data with id: {generalExperienceId}, but no general experience data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", generalExperienceLog);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						generalExperience.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				generalExperienceLog.Timestamp = DateTime.Now;
				generalExperienceLog.Level = "Warning";
				generalExperienceLog.RenderedMessage = $"User unauthorized to delete general experience data.";
				generalExperienceLog.AdditionalData = $"User tried to delete general experience data with id: {generalExperienceId} that does not belong to them, general experience data faculty member id: {generalExperience.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", generalExperienceLog);
				#endregion
				throw;
			}

			generalExperience.IsDeleted = true;

			Repo.Update(generalExperience);
			await SaveChangesAsync();
			#region Log
			generalExperienceLog.Timestamp = DateTime.Now;
			generalExperienceLog.Level = "Information";
			generalExperienceLog.RenderedMessage = $"General experience data deleted for user: {userOfData.UserName}.";
			generalExperienceLog.AdditionalData = (facultyMemberEmail is null) ? $"User deleted their general experience data with id: {generalExperienceId} successfully."
				: $"Admin: {currentUser.UserName} deleted user: {userOfData.UserName} general experience data with id: {generalExperienceId} successfully.";
			_logger.LogInformation("{@LogDetails}", generalExperienceLog);
			#endregion
		}
	}
}

