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
	public class TeachingExperiencesService(
	 IUnitOfWork unitOfWork,
	 IAuthenticationService authenticationService,
	 IMapper mapper,
	 ILogger<TeachingExperiencesService> _logger)
	 : BaseService<TeachingExperiences, int>(unitOfWork, authenticationService, mapper),
	   ITeachingExperiencesService
	{
		protected override string EntityName => "Teaching Experiences";

		public async Task<PaginatedResult<TeachingExperiencesResponseDTO>> GetAllTeachingExperiencesAsync(
			TeachingExperiencesSpecificationParameters parameters,
			string? facultyMemberEmail = null)
		{
			var currentUser = await GetCurrentUserAsync();
			var email = facultyMemberEmail ?? currentUser.Email;

			#region Log
			var teachingExperiencesLog = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.TeachingExperiencesActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName,
			};
			#endregion

			var teachingExperiences = await Repo.GetAllAsync(
				new TeachingExperiencesSpecifications(parameters, email));

			if (teachingExperiences is null)
			{
				#region Log
				teachingExperiencesLog.RenderedMessage = $"Teaching experiences not found for user: {currentUser.UserName}.";
				teachingExperiencesLog.Level = "Warning";
				teachingExperiencesLog.Timestamp = DateTime.Now;
				teachingExperiencesLog.AdditionalData = $"User tried to get their teaching experiences data, but no teaching experiences data was found in the database for user with email : {email}.";
				_logger.LogWarning("{@LogDetails}", teachingExperiencesLog);
				#endregion
				throw NotFound();
			}

			var mapped = Mapper.Map<IEnumerable<TeachingExperiencesResponseDTO>>(teachingExperiences);

			var totalCount = await Repo.CountAsync(
				new TeachingExperiencesCountSpecifications(parameters, email));

			#region Log
			teachingExperiencesLog.RenderedMessage = $"Teaching experiences data retrieved for user: {currentUser.UserName}.";
			teachingExperiencesLog.Level = "Information";
			teachingExperiencesLog.Timestamp = DateTime.Now;
			teachingExperiencesLog.AdditionalData = $"User retrieved their teaching experiences data successfully, total count of teaching experiences data retrieved: {totalCount}.";
			_logger.LogInformation("{@LogDetails}", teachingExperiencesLog);
			#endregion

			return new PaginatedResult<TeachingExperiencesResponseDTO>(
				parameters.PageIndex,
				mapped.Count(),
				totalCount,
				mapped);
		}

		public async Task<TeachingExperiencesResponseDTO> GetTeachingExperienceByIdAsync(
			int id,
			string? facultyMemberEmail = null)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var teachingExperienceLog = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.TeachingExperiencesActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName,
			};
			#endregion

			var teachingExperience = await Repo.GetAsync(
			new TeachingExperiencesSpecifications(id));
			if(teachingExperience is null)
			{
				#region Log
				teachingExperienceLog.Timestamp = DateTime.Now;
				teachingExperienceLog.Level = "Warning";
				teachingExperienceLog.RenderedMessage = $"Teaching experiences not found for user: {currentUser.UserName}.";
				teachingExperienceLog.AdditionalData = $"User tried to get their teaching experiences data with id: {id}, but no teaching experiences data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", teachingExperienceLog);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						teachingExperience.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				teachingExperienceLog.Timestamp = DateTime.Now;
				teachingExperienceLog.Level = "Warning";
				teachingExperienceLog.RenderedMessage = $"User unauthorized to access teaching experiences data.";
				teachingExperienceLog.AdditionalData = $"User tried to get teaching experiences data with id: {id} that does not belong to them, teaching experiences data faculty member id: {teachingExperience.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", teachingExperienceLog);
				#endregion
				throw;
			}

			#region Log
			teachingExperienceLog.Timestamp = DateTime.Now;
			teachingExperienceLog.Level = "Information";
			teachingExperienceLog.RenderedMessage = $"Teaching experiences data retrieved for user: {currentUser.UserName}.";
			teachingExperienceLog.AdditionalData = $"User retrieved their teaching experiences data with id: {id} successfully.";
			_logger.LogInformation("{@LogDetails}", teachingExperienceLog);
			#endregion
			return Mapper.Map<TeachingExperiencesResponseDTO>(teachingExperience);
		}

		public async Task<TeachingExperiencesResponseDTO> CreateTeachingExperienceAsync(
			TeachingExperiencesCreateDTO teachingExperienceCreateDto,
			string? facultyMemberEmail = null)
		{
			var currentUser = await GetCurrentUserAsync();
			var email = facultyMemberEmail ?? currentUser.Email;

			#region Log
			var teachingExperienceLog = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.TeachingExperiencesActions.ToString(),
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
				teachingExperienceLog.Timestamp = DateTime.Now;
				teachingExperienceLog.Level = "Warning";
				teachingExperienceLog.RenderedMessage = $"Faculty Member not found.";
				teachingExperienceLog.AdditionalData = $"User tried to create a teaching experience for a faculty member that does not exist in database, no faculty member found with email : {email}.";
				_logger.LogWarning("{@LogDetails}", teachingExperienceLog);
				#endregion
				throw;
			}

			var teachingExperience = Mapper.Map<TeachingExperiences>(teachingExperienceCreateDto);
			teachingExperience.FacultyMemberId = facultyMember.Id;

			await Repo.AddAsync(teachingExperience);
			await SaveChangesAsync();

			var response = Mapper.Map<TeachingExperiencesResponseDTO>(teachingExperience);
			#region Log
			teachingExperienceLog.Timestamp = DateTime.Now;
			teachingExperienceLog.Level = "Information";
			teachingExperienceLog.RenderedMessage = $"User: {currentUser.UserName} created a teaching experience.";
			teachingExperienceLog.AdditionalData = $"User created a teaching experience with id: {response.Id} for course: {response.CourseName} successfully.";
			_logger.LogInformation("{@LogDetails}", teachingExperienceLog);
			#endregion
			return response;
		}

		public async Task<TeachingExperiencesResponseDTO> UpdateTeachingExperienceAsync(
			int teachingExperienceId,
			TeachingExperiencesUpdateDTO teachingExperienceUpdateDto,
			string? facultyMemberEmail = null)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var teachingExperienceLog = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.TeachingExperiencesActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName,
			};
			#endregion
			var jsonOptions = new JsonSerializerOptions
			{
				WriteIndented = true,
				Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
			};
			var teachingExperience = await Repo.GetAsync(
				new TeachingExperiencesSpecifications(teachingExperienceId));
			if(teachingExperience is null)
			{
				#region Log
				teachingExperienceLog.Timestamp = DateTime.Now;
				teachingExperienceLog.Level = "Warning";
				teachingExperienceLog.RenderedMessage = $"Teaching experience not found for user: {currentUser.UserName}.";
				teachingExperienceLog.AdditionalData = $"User tried to update their teaching experience data with id: {teachingExperienceId}, but no teaching experience data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", teachingExperienceLog);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						teachingExperience.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				teachingExperienceLog.Timestamp = DateTime.Now;
				teachingExperienceLog.Level = "Warning";
				teachingExperienceLog.RenderedMessage = $"User unauthorized to update teaching experience data.";
				teachingExperienceLog.AdditionalData = $"User tried to update teaching experience data with id: {teachingExperienceId} that does not belong to them, teaching experience data faculty member id: {teachingExperience.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", teachingExperienceLog);
				#endregion
				throw;
			}

			var oldData = Mapper.Map<TeachingExperiencesResponseDTO>(teachingExperience);
			Mapper.Map(teachingExperienceUpdateDto, teachingExperience);

			Repo.Update(teachingExperience);
			await SaveChangesAsync();
			var newData = Mapper.Map<TeachingExperiencesResponseDTO>(teachingExperience);
			#region Log
			teachingExperienceLog.Timestamp = DateTime.Now;
			teachingExperienceLog.Level = "Information";
			teachingExperienceLog.RenderedMessage = $"Teaching experience data updated for user: {currentUser.UserName}.";
			teachingExperienceLog.AdditionalData = $"User updated their teaching experience data with id: {teachingExperienceId} successfully.\nOld Data: {JsonSerializer.Serialize(oldData, jsonOptions)}\nNew Data: {JsonSerializer.Serialize(newData, jsonOptions)}.";
			_logger.LogInformation("{@LogDetails}", teachingExperienceLog);
			#endregion
			return newData;
		}

		public async Task DeleteTeachingExperienceAsync(
			int teachingExperienceId,
			string? facultyMemberEmail = null)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var teachingExperienceLog = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.TeachingExperiencesActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName,
			};
			#endregion
			var teachingExperience = await Repo.GetAsync(
				new TeachingExperiencesSpecifications(teachingExperienceId));
			if(teachingExperience is null)
			{
				#region Log
				teachingExperienceLog.Timestamp = DateTime.Now;
				teachingExperienceLog.Level = "Warning";
				teachingExperienceLog.RenderedMessage = $"Teaching experience not found for user: {currentUser.UserName}.";
				teachingExperienceLog.AdditionalData = $"User tried to delete their teaching experience data with id: {teachingExperienceId}, but no teaching experience data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", teachingExperienceLog);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						teachingExperience.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				teachingExperienceLog.Timestamp = DateTime.Now;
				teachingExperienceLog.Level = "Warning";
				teachingExperienceLog.RenderedMessage = $"User unauthorized to delete teaching experience data.";
				teachingExperienceLog.AdditionalData = $"User tried to delete teaching experience data with id: {teachingExperienceId} that does not belong to them, teaching experience data faculty member id: {teachingExperience.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", teachingExperienceLog);
				#endregion
				throw;
			}

			teachingExperience.IsDeleted = true;

			Repo.Update(teachingExperience);
			await SaveChangesAsync();
			#region Log
			teachingExperienceLog.Timestamp = DateTime.Now;
			teachingExperienceLog.Level = "Information";
			teachingExperienceLog.RenderedMessage = $"Teaching experience data deleted for user: {currentUser.UserName}.";
			teachingExperienceLog.AdditionalData = $"User deleted their teaching experience data with id: {teachingExperienceId} successfully.";
			_logger.LogInformation("{@LogDetails}", teachingExperienceLog);
			#endregion
		}
	}
}