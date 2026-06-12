using Domain.Entities.AcademicDataModule.ContributionsModule;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols;
using Services.Abstraction.Contracts.AcademicDataModule.ContributionsModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.ContributionsModule;
using Shared.Dtos.AcademicDataModule.ContributionsModule;
using Shared.Enums.Logging;
using Shared.SpecificationParameters.AcademicDataModule.ContributionsModule;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Services.Implementations.AcademicDataModule.ContributionsModule
{
	public class ParticipationInQualityWorksService(
	IUnitOfWork unitOfWork,
	IAuthenticationService authenticationService,
	IMapper mapper,
	ILogger<ParticipationInQualityWorksService> _logger)
	: BaseService<ParticipationInQualityWorks, int>(unitOfWork, authenticationService, mapper),
	  IParticipationInQualityWorksService
	{
		protected override string EntityName => "Participation In Quality Works";

		public async Task<PaginatedResult<ParticipationInQualityWorksResponseDTO>> GetAllParticipationsInQualityWorksAsync(
			ParticipationInQualityWorksSpecificationParameters parameters,
			string? facultyMemberEmail = null)
		{
			var currentUser = await GetCurrentUserAsync();
			var email = facultyMemberEmail ?? currentUser.Email;

			#region Log
			var participationsLog = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.ParticipationInQualityWorksActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName,
			};
			#endregion

			var participations = await Repo.GetAllAsync(
				new ParticipationInQualityWorksSpecifications(parameters, email));

			if (participations is null)
			{
				#region Log
				participationsLog.RenderedMessage = $"Participations in quality works not found for user: {currentUser.UserName}.";
				participationsLog.Level = "Warning";
				participationsLog.Timestamp = DateTime.Now;
				participationsLog.AdditionalData = $"User tried to get their Participations in quality works data, but no Participations in quality works data was found in the database for user with email : {email}.";
				_logger.LogWarning("{@LogDetails}", participationsLog);
				#endregion
				throw NotFound();
			}

			var mapped = Mapper.Map<IEnumerable<ParticipationInQualityWorksResponseDTO>>(participations);

			var totalCount = await Repo.CountAsync(
				new ParticipationInQualityWorksCountSpecifications(parameters, email));

			#region Log
			participationsLog.RenderedMessage = $"Participations in quality works data retrieved for user: {currentUser.UserName}.";
			participationsLog.Level = "Information";
			participationsLog.Timestamp = DateTime.Now;
			participationsLog.AdditionalData = $"User retrieved their participations in quality works data successfully, total count of Participations in quality works data retrieved: {totalCount}.";
			_logger.LogInformation("{@LogDetails}", participationsLog);
			#endregion

			return new PaginatedResult<ParticipationInQualityWorksResponseDTO>(
				parameters.PageIndex,
				mapped.Count(),
				totalCount,
				mapped);
		}

		public async Task<ParticipationInQualityWorksResponseDTO> GetParticipationInQualityWorksByIdAsync(
			int id,
			string? facultyMemberEmail = null)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var participationLog = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.ParticipationInQualityWorksActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName,
			};
			#endregion

			var participation = await Repo.GetAsync(
				new ParticipationInQualityWorksSpecifications(id));

			if (participation is null)
			{
				#region Log
				participationLog.Timestamp = DateTime.Now;
				participationLog.Level = "Warning";
				participationLog.RenderedMessage = $"Participation in quality Work not found for user: {currentUser.UserName}.";
				participationLog.AdditionalData = $"User tried to get their participation in quality work data with id: {id}, but no participation in quality work data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", participationLog);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						participation.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				participationLog.Timestamp = DateTime.Now;
				participationLog.Level = "Warning";
				participationLog.RenderedMessage = $"User unauthorized to access participation in quality work data.";
				participationLog.AdditionalData = $"User tried to get participation in quality work data with id: {id} that does not belong to them, contribution to community service data faculty member id: {participation.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", participationLog);
				#endregion
				throw;
			}

			#region Log
			participationLog.Timestamp = DateTime.Now;
			participationLog.Level = "Information";
			participationLog.RenderedMessage = $"Participation in quality work data retrieved for user: {currentUser.UserName}.";
			participationLog.AdditionalData = $"User retrieved their participation in quality work data with id: {id} successfully.";
			_logger.LogInformation("{@LogDetails}", participationLog);
			#endregion
			return Mapper.Map<ParticipationInQualityWorksResponseDTO>(participation);
		}

		public async Task<ParticipationInQualityWorksResponseDTO> CreateParticipationInQualityWorksAsync(
			ParticipationInQualityWorksCreateDTO participationInQualityWorksCreateDto,
			string? facultyMemberEmail = null)
		{
			var currentUser = await GetCurrentUserAsync();
			var email = facultyMemberEmail ?? currentUser.Email;

			#region Log
			var participationLog = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.ParticipationInQualityWorksActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
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
				participationLog.Timestamp = DateTime.Now;
				participationLog.Level = "Warning";
				participationLog.RenderedMessage = $"Faculty Member not found.";
				participationLog.AdditionalData = $"User tried to create a participation in quality work for a faculty member that does not exist in database, no faculty member found with email : {email}.";
				_logger.LogWarning("{@LogDetails}", participationLog);
				#endregion
				throw;
			}

			var participation = Mapper.Map<ParticipationInQualityWorks>(participationInQualityWorksCreateDto);
			participation.FacultyMemberId = facultyMember.Id;

			await Repo.AddAsync(participation);
			await SaveChangesAsync();
			var response = Mapper.Map<ParticipationInQualityWorksResponseDTO>(participation);
			#region Log
			participationLog.Timestamp = DateTime.Now;
			participationLog.Level = "Information";
			participationLog.RenderedMessage = $"User: {currentUser.UserName} created a participation in quality work.";
			participationLog.AdditionalData = $"User created a participation in quality work with id: {response.Id} and title: {response.ParticipationTitle} successfully.";
			_logger.LogInformation("{@LogDetails}", participationLog);
			#endregion
			return response;
		}

		public async Task<ParticipationInQualityWorksResponseDTO> UpdateParticipationInQualityWorksAsync(
			int participationInQualityWorksId,
			ParticipationInQualityWorksUpdateDTO participationInQualityWorksUpdateDto,
			string? facultyMemberEmail = null)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var participationLog = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.ParticipationInQualityWorksActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion
			var jsonOptions = new JsonSerializerOptions
			{
				WriteIndented = true,
				Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
			};
			var participation = await Repo.GetAsync(
				new ParticipationInQualityWorksSpecifications(participationInQualityWorksId));

			if (participation is null)
			{
				#region Log
				participationLog.Timestamp = DateTime.Now;
				participationLog.Level = "Warning";
				participationLog.RenderedMessage = $"Participation in quality work not found for user: {currentUser.UserName}.";
				participationLog.AdditionalData = $"User tried to update their participation in quality work data with id: {participationInQualityWorksId}, but no participation in quality work data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", participationLog);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						participation.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				participationLog.Timestamp = DateTime.Now;
				participationLog.Level = "Warning";
				participationLog.RenderedMessage = $"User unauthorized to update participation in quality work data.";
				participationLog.AdditionalData = $"User tried to update participation in quality work data with id: {participationInQualityWorksId} that does not belong to them, participation in quality work data faculty member id: {participation.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", participationLog);
				#endregion
				throw;
			}

			var oldData = Mapper.Map<ParticipationInQualityWorksResponseDTO>(participation);
			Mapper.Map(participationInQualityWorksUpdateDto, participation);

			Repo.Update(participation);
			await SaveChangesAsync();

			var newData = Mapper.Map<ParticipationInQualityWorksResponseDTO>(participation);
			#region Log
			participationLog.Timestamp = DateTime.Now;
			participationLog.Level = "Information";
			participationLog.RenderedMessage = $"Participation in quality work data updated for user: {currentUser.UserName}.";
			participationLog.AdditionalData = $"User updated their participation in quality work data with id: {participationInQualityWorksId} successfully.\nOld Data: {JsonSerializer.Serialize(oldData, jsonOptions)}\nNew Data: {JsonSerializer.Serialize(newData, jsonOptions)}.";
			_logger.LogInformation("{@LogDetails}", participationLog);
			#endregion
			return newData;
		}

		public async Task DeleteParticipationInQualityWorksAsync(
			int participationInQualityWorksId,
			string? facultyMemberEmail = null)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var participationLog = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.ParticipationInQualityWorksActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion
			var participation = await Repo.GetAsync(
				new ParticipationInQualityWorksSpecifications(participationInQualityWorksId));
			if (participation is null)
			{
				#region Log
				participationLog.Timestamp = DateTime.Now;
				participationLog.Level = "Warning";
				participationLog.RenderedMessage = $"Participation in quality work not found for user: {currentUser.UserName}.";
				participationLog.AdditionalData = $"User tried to delete their participation in quality work data with id: {participationInQualityWorksId}, but no participation in quality work data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", participationLog);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						participation.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				participationLog.Timestamp = DateTime.Now;
				participationLog.Level = "Warning";
				participationLog.RenderedMessage = $"User unauthorized to delete participation in quality work data.";
				participationLog.AdditionalData = $"User tried to delete participation in quality work data with id: {participationInQualityWorksId} that does not belong to them, participation in quality work data faculty member id: {participation.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", participationLog);
				#endregion
				throw;
			}

			participation.IsDeleted = true;

			Repo.Update(participation);
			await SaveChangesAsync();
			#region Log
			participationLog.Timestamp = DateTime.Now;
			participationLog.Level = "Information";
			participationLog.RenderedMessage = $"Participation in quality work data deleted for user: {currentUser.UserName}.";
			participationLog.AdditionalData = $"User deleted their participation in quality work data with id: {participationInQualityWorksId} successfully.";
			_logger.LogInformation("{@LogDetails}", participationLog);
			#endregion
		}
	}
}
