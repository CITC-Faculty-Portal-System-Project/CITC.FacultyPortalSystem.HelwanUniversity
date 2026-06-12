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
	public class SeminarsAndConferncesService(
	   IUnitOfWork unitOfWork,
	   IAuthenticationService authenticationService,
	   IMapper mapper,
	   ILogger<SeminarsAndConferncesService> _logger)
	   : BaseService<ConferencesAndSeminars, int>(unitOfWork, authenticationService, mapper),
		 ISeminarsAndConferencesService
	{
		protected override string EntityName => "Seminars And Conferences";


		public async Task<PaginatedResult<ConferencesAndSeminarsResponseDto>> GetAllSeminarsAndConferencesAsync(
			SeminarsAndConferncesSpecificationParameters parameters,
			string? facultyMemberEmail = null)
		{
			var currentUser = await GetCurrentUserAsync();
			var email = facultyMemberEmail ?? currentUser.Email;

			#region Log
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(email);
			var conferencesAndSeminarsLogs = new LogEntry
			{
				Category = Category.FacultyMemberMissions.ToString(),
				CategoryAction = CategoryAction.ConferencesAndSeminarsActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName,
			};
			#endregion

			var conferencesAndSeminars = await Repo.GetAllAsync(
				new ConferncesAndSeminarsSpecification(parameters, email));

			if (conferencesAndSeminars is null)
			{
				#region Log
				conferencesAndSeminarsLogs.RenderedMessage = $"Seminars and Conferences not found for user: {currentUser.UserName}.";
				conferencesAndSeminarsLogs.Level = "Warning";
				conferencesAndSeminarsLogs.Timestamp = DateTime.Now;
				conferencesAndSeminarsLogs.AdditionalData = $"User tried to get their seminars and conferences data, but no seminars and conferences service data was found in the database for user with email : {email}.";
				_logger.LogWarning("{@LogDetails}", conferencesAndSeminarsLogs);
				#endregion
				throw NotFound();
			}

			var mapped = Mapper.Map<IEnumerable<ConferencesAndSeminarsResponseDto>>(conferencesAndSeminars);

			var totalCount = await Repo.CountAsync(
				new ConferncesAndSeminarsCountSpecification(parameters, email));

			#region Log
			conferencesAndSeminarsLogs.RenderedMessage = $"Seminars and conferences data retrieved for user: {currentUser.UserName}.";
			conferencesAndSeminarsLogs.Level = "Information";
			conferencesAndSeminarsLogs.Timestamp = DateTime.Now;
			conferencesAndSeminarsLogs.AdditionalData = $"User retrieved their seminars and conferences data successfully, total count of seminars and conferences data retrieved: {totalCount}.";
			_logger.LogInformation("{@LogDetails}", conferencesAndSeminarsLogs);
			#endregion

			return new PaginatedResult<ConferencesAndSeminarsResponseDto>(
				parameters.PageIndex,
				mapped.Count(),
				totalCount,
				mapped);
		}

		public async Task<ConferencesAndSeminarsResponseDto> GetSeminarOrConferenceByIdAsync(
			int id,
			string? facultyMemberEmail = null)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var conferencesAndSeminarsLogs = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.ConferencesAndSeminarsActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName,
			};
			#endregion

			var conferenceOrSeminar = await Repo.GetAsync(
				new ConferncesAndSeminarsSpecification(id));
			if (conferenceOrSeminar is null)
			{
				#region Log
				conferencesAndSeminarsLogs.Timestamp = DateTime.Now;
				conferencesAndSeminarsLogs.Level = "Warning";
				conferencesAndSeminarsLogs.RenderedMessage = $"Seminar or conference not found for user: {currentUser.UserName}.";
				conferencesAndSeminarsLogs.AdditionalData = $"User tried to get their seminar or conference data with id: {id}, but no seminar or conference data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", conferencesAndSeminarsLogs);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						conferenceOrSeminar.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				conferencesAndSeminarsLogs.Timestamp = DateTime.Now;
				conferencesAndSeminarsLogs.Level = "Warning";
				conferencesAndSeminarsLogs.RenderedMessage = $"User unauthorized to access seminar or conference data.";
				conferencesAndSeminarsLogs.AdditionalData = $"User tried to get seminar or conference data with id: {id} that does not belong to them, seminar or conference data faculty member id: {conferenceOrSeminar.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", conferencesAndSeminarsLogs);
				#endregion
				throw;
			}

			#region Log
			conferencesAndSeminarsLogs.Timestamp = DateTime.Now;
			conferencesAndSeminarsLogs.Level = "Information";
			conferencesAndSeminarsLogs.RenderedMessage = $"Seminar or conference data retrieved for user: {currentUser.UserName}.";
			conferencesAndSeminarsLogs.AdditionalData = $"User retrieved their seminar or conference data with id: {id} successfully.";
			_logger.LogInformation("{@LogDetails}", conferencesAndSeminarsLogs);
			#endregion
			return Mapper.Map<ConferencesAndSeminarsResponseDto>(conferenceOrSeminar);
		}

		public async Task<ConferencesAndSeminarsResponseDto> CreateSeminarOrConferenceAsync(
			ConferencesAndSeminarsCreateDto conferencesAndSeminarsCreateDto,
			string? facultyMemberEmail = null)
		{
			var currentUser = await GetCurrentUserAsync();
			var email = facultyMemberEmail ?? currentUser.Email;

			#region Log
			var conferencesAndSeminarsLogs = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.ConferencesAndSeminarsActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName,
			};
			#endregion

			FacultyMember facultyMember = null!;
			try
			{
				facultyMember = await GetFacultyMemberByEmailAsync(email);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				conferencesAndSeminarsLogs.Timestamp = DateTime.Now;
				conferencesAndSeminarsLogs.Level = "Warning";
				conferencesAndSeminarsLogs.RenderedMessage = $"Faculty Member not found.";
				conferencesAndSeminarsLogs.AdditionalData = $"User tried to create a seminar or conference for a faculty member that does not exist in database, no faculty member found with email : {email}.";
				_logger.LogWarning("{@LogDetails}", conferencesAndSeminarsLogs);
				#endregion
				throw;
			}

			var conferenceOrSeminar = Mapper.Map<ConferencesAndSeminars>(conferencesAndSeminarsCreateDto);
			conferenceOrSeminar.FacultyMemberId = facultyMember.Id;

			await Repo.AddAsync(conferenceOrSeminar);
			await SaveChangesAsync();

			var response = Mapper.Map<ConferencesAndSeminarsResponseDto>(conferenceOrSeminar);
			#region Log
			conferencesAndSeminarsLogs.Timestamp = DateTime.Now;
			conferencesAndSeminarsLogs.Level = "Information";
			conferencesAndSeminarsLogs.RenderedMessage = $"User: {currentUser.UserName} created a {response.Type.ToString()}.";
			conferencesAndSeminarsLogs.AdditionalData = $"User created a {response.Type.ToString()} with id: {response.Id} and Name: {response.Name} successfully.";
			_logger.LogInformation("{@LogDetails}", conferencesAndSeminarsLogs);
			#endregion
			return response;
		}

		public async Task<ConferencesAndSeminarsResponseDto> UpdateSeminarOrConferenceAsync(
			int id,
			ConferencesAndSeminarsUpdateDto conferencesAndSeminarsUpdateDto,
			string? facultyMemberEmail = null)
		{
			#region Log
            var currentUser = await GetCurrentUserAsync();
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(facultyMemberEmail);
			var conferencesAndSeminarsLogs = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.ConferencesAndSeminarsActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName,
			};
			#endregion
			var jsonOptions = new JsonSerializerOptions
			{
				WriteIndented = true,
				Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
			};
			var conferenceOrSeminar = await Repo.GetAsync(
				new ConferncesAndSeminarsSpecification(id));
			if (conferenceOrSeminar is null)
			{
				#region Log
				conferencesAndSeminarsLogs.Timestamp = DateTime.Now;
				conferencesAndSeminarsLogs.Level = "Warning";
				conferencesAndSeminarsLogs.RenderedMessage = $"Seminar or conference not found for user: {currentUser.UserName}.";
				conferencesAndSeminarsLogs.AdditionalData = $"User tried to update their seminar or conference data with id: {id}, but no seminar or conference data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", conferencesAndSeminarsLogs);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						conferenceOrSeminar.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				conferencesAndSeminarsLogs.Timestamp = DateTime.Now;
				conferencesAndSeminarsLogs.Level = "Warning";
				conferencesAndSeminarsLogs.RenderedMessage = $"User unauthorized to update seminar or conference data.";
				conferencesAndSeminarsLogs.AdditionalData = $"User tried to update seminar or conference data with id: {id} that does not belong to them, seminar or conference data faculty member id: {conferenceOrSeminar.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", conferencesAndSeminarsLogs);
				#endregion
				throw;
			}

			var oldData = Mapper.Map<ConferencesAndSeminarsResponseDto>(conferenceOrSeminar);
			Mapper.Map(conferencesAndSeminarsUpdateDto, conferenceOrSeminar);

			Repo.Update(conferenceOrSeminar);
			await SaveChangesAsync();

			var newData = Mapper.Map<ConferencesAndSeminarsResponseDto>(conferenceOrSeminar);
			#region Log
			conferencesAndSeminarsLogs.Timestamp = DateTime.Now;
			conferencesAndSeminarsLogs.Level = "Information";
			conferencesAndSeminarsLogs.RenderedMessage = $"Seminar or conference data updated for user: {currentUser.UserName}.";
			conferencesAndSeminarsLogs.AdditionalData = $"User updated their seminar or conference data with id: {id} successfully.\nOld Data: {JsonSerializer.Serialize(oldData, jsonOptions)}\nNew Data: {JsonSerializer.Serialize(newData, jsonOptions)}.";
			_logger.LogInformation("{@LogDetails}", conferencesAndSeminarsLogs);
			#endregion
			return newData;
		}

		public async Task DeleteSeminarOrConferenceAsync(
			int id,
			string? facultyMemberEmail = null)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var conferencesAndSeminarsLogs = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.ConferencesAndSeminarsActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName,
			};
			#endregion
			var conferenceOrSeminar = await Repo.GetAsync(
				new ConferncesAndSeminarsSpecification(id));
			if (conferenceOrSeminar is null)
			{
				#region Log
				conferencesAndSeminarsLogs.Timestamp = DateTime.Now;
				conferencesAndSeminarsLogs.Level = "Warning";
				conferencesAndSeminarsLogs.RenderedMessage = $"Seminar or conference not found for user: {currentUser.UserName}.";
				conferencesAndSeminarsLogs.AdditionalData = $"User tried to delete their seminar or conference data with id: {id}, but no seminar or conference data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", conferencesAndSeminarsLogs);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						conferenceOrSeminar.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				conferencesAndSeminarsLogs.Timestamp = DateTime.Now;
				conferencesAndSeminarsLogs.Level = "Warning";
				conferencesAndSeminarsLogs.RenderedMessage = $"User unauthorized to delete seminar or conference data.";
				conferencesAndSeminarsLogs.AdditionalData = $"User tried to delete seminar or conference data with id: {id} that does not belong to them, seminar or conference data faculty member id: {conferenceOrSeminar.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", conferencesAndSeminarsLogs);
				#endregion
				throw;
			}

			conferenceOrSeminar.IsDeleted = true;

			Repo.Update(conferenceOrSeminar);
			await SaveChangesAsync();
			#region Log
			conferencesAndSeminarsLogs.Timestamp = DateTime.Now;
			conferencesAndSeminarsLogs.Level = "Information";
			conferencesAndSeminarsLogs.RenderedMessage = $"Seminar or conference data deleted for user: {currentUser.UserName}.";
			conferencesAndSeminarsLogs.AdditionalData = $"User deleted their seminar or conference data with id: {id} successfully.";
			_logger.LogInformation("{@LogDetails}", conferencesAndSeminarsLogs);
			#endregion
		}
	}
}
