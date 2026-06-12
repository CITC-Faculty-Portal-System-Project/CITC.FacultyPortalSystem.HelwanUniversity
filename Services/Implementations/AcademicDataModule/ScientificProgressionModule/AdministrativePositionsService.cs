using Domain.Entities.AcademicDataModule.ScientificProgressionModule;
using Microsoft.Extensions.Logging;
using Services.Abstraction.Contracts.AcademicDataModule.ScientificProgressionModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.ScientificProgressionModule;
using Shared.Dtos.AcademicDataModule.ScientificProgressionModule;
using Shared.Enums.Logging;
using Shared.SpecificationParameters.AcademicDataModule.ScientificProgressionModule;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Services.Implementations.AcademicDataModule.ScientificProgressionModule
{
	public class AdministrativePositionsService(
		IUnitOfWork unitOfWork,
		IAuthenticationService authenticationService,
		IMapper mapper,
		ILogger<AdministrativePositionsService> _logger)
		: BaseService<AdministrativePositions, int>(unitOfWork, authenticationService, mapper),
		  IAdministrativePositionsService
	{
		protected override string EntityName => "Administrative Positions";

		public async Task<PaginatedResult<AdministrativePositionDto>> GetAllAdministrativePositionsAsync(
			AdministrativePositionsSpecificationParameters parameters,
			string? facultyMemberEmail = null)
		{
			var currentUser = await GetCurrentUserAsync();
			var email = facultyMemberEmail ?? currentUser.Email;

			#region Log
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(email);
			var positionLogs = new LogEntry
			{
				Category = Category.FacultyMemberScientificProgression.ToString(),
				CategoryAction = CategoryAction.AdministrativePositionsActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion

			var positions = await Repo.GetAllAsync(
				new AdministrativePositionsSpecifications(parameters, email));
			if (positions is null)
			{
				#region Log
				positionLogs.RenderedMessage = $"Administrative positions not found for user: {currentUser.UserName}.";
				positionLogs.Level = "Warning";
				positionLogs.Timestamp = DateTime.Now;
				positionLogs.AdditionalData = $"User tried to get their administrative positions data, but no administrative positions data was found in the database for user with email : {email}.";
				_logger.LogWarning("{@LogDetails}", positionLogs);
				#endregion
				throw NotFound();
			}

			var mapped = Mapper.Map<IEnumerable<AdministrativePositionDto>>(positions);

			var totalCount = await Repo.CountAsync(
				new AdministrativePositionsCountSpecifications(parameters, email));

			#region Log
			positionLogs.RenderedMessage = $"Administrative positions data retrieved for user: {currentUser.UserName}.";
			positionLogs.Level = "Information";
			positionLogs.Timestamp = DateTime.Now;
			positionLogs.AdditionalData = $"User retrieved their administrative positions data successfully, total count of administrative positions data retrieved: {totalCount}.";
			_logger.LogInformation("{@LogDetails}", positionLogs);
			#endregion

			return new PaginatedResult<AdministrativePositionDto>(
				parameters.PageIndex,
				mapped.Count(),
				totalCount,
				mapped);
		}

		public async Task<AdministrativePositionDto> GetAdministrativePositionByIdAsync(
			int id,
			string? facultyMemberEmail = null)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var positionLogs = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.AdministrativePositionsActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion

			var position = await Repo.GetAsync(new AdministrativePositionsSpecifications(id));
			if (position is null)
			{
				#region Log
				positionLogs.Timestamp = DateTime.Now;
				positionLogs.Level = "Warning";
				positionLogs.RenderedMessage = $"Administrative position not found for user: {currentUser.UserName}.";
				positionLogs.AdditionalData = $"User tried to get their administrative position data with id: {id}, but no administrative position data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", positionLogs);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						position.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				positionLogs.Timestamp = DateTime.Now;
				positionLogs.Level = "Warning";
				positionLogs.RenderedMessage = $"User unauthorized to access administrative position data.";
				positionLogs.AdditionalData = $"User tried to get administrative position data with id: {id} that does not belong to them, administrative position data faculty member id: {position.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", positionLogs);
				#endregion
				throw;
			}

			#region Log
			positionLogs.Timestamp = DateTime.Now;
			positionLogs.Level = "Information";
			positionLogs.RenderedMessage = $"Administrative position data retrieved for user: {currentUser.UserName}.";
			positionLogs.AdditionalData = $"User retrieved their administrative position data with id: {id} successfully.";
			_logger.LogInformation("{@LogDetails}", positionLogs);
			#endregion
			return Mapper.Map<AdministrativePositionDto>(position);
		}

		public async Task<AdministrativePositionDto> CreateAdministrativePositionAsync(
			AdministrativePositionCreateDto dto,
			string? facultyMemberEmail = null)
		{
			var currentUser = await GetCurrentUserAsync();
			var email = facultyMemberEmail ?? currentUser.Email;

			#region Log
			var positionLogs = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.AdministrativePositionsActions.ToString(),
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
				positionLogs.Timestamp = DateTime.Now;
				positionLogs.Level = "Warning";
				positionLogs.RenderedMessage = $"Faculty Member not found.";
				positionLogs.AdditionalData = $"User tried to create an administrative position for a faculty member that does not exist in database, no faculty member found with email : {email}.";
				_logger.LogWarning("{@LogDetails}", positionLogs);
				#endregion
				throw;
			}

			var position = Mapper.Map<AdministrativePositions>(dto);
			position.FacultyMemberId = facultyMember.Id;

			await Repo.AddAsync(position);
			await SaveChangesAsync();

			var response = Mapper.Map<AdministrativePositionDto>(position);
			#region Log
			positionLogs.Timestamp = DateTime.Now;
			positionLogs.Level = "Information";
			positionLogs.RenderedMessage = $"User: {currentUser.UserName} created an administrative position.";
			positionLogs.AdditionalData = $"User created a administrative position with id: {response.Id} and position: {response.Position} successfully.";
			_logger.LogInformation("{@LogDetails}", positionLogs);
			#endregion
			return response;
		}

		public async Task<AdministrativePositionDto> UpdateAdministrativePositionAsync(
			int id,
			AdministrativePositionDto dto,
			string? facultyMemberEmail = null)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var positionLogs = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.AdministrativePositionsActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion
			var jsonOptions = new JsonSerializerOptions
			{
				WriteIndented = true,
				Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
			};
			var position = await Repo.GetAsync(new AdministrativePositionsSpecifications(id));
			if (position is null)
			{
				#region Log
				positionLogs.Timestamp = DateTime.Now;
				positionLogs.Level = "Warning";
				positionLogs.RenderedMessage = $"administrative position not found for user: {currentUser.UserName}.";
				positionLogs.AdditionalData = $"User tried to update their administrative position data with id: {id}, but no administrative position data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", positionLogs);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						position.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				positionLogs.Timestamp = DateTime.Now;
				positionLogs.Level = "Warning";
				positionLogs.RenderedMessage = $"User unauthorized to update administrative position data.";
				positionLogs.AdditionalData = $"User tried to update administrative position data with id: {id} that does not belong to them, administrative position data faculty member id: {position.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", positionLogs);
				#endregion
				throw;
			}

			var oldData = Mapper.Map<AdministrativePositionDto>(position);
			Mapper.Map(dto, position);

			Repo.Update(position);
			await SaveChangesAsync();

			var newData = Mapper.Map<AdministrativePositionDto>(position);
			#region Log
			positionLogs.Timestamp = DateTime.Now;
			positionLogs.Level = "Information";
			positionLogs.RenderedMessage = $"Administrative position data updated for user: {currentUser.UserName}.";
			positionLogs.AdditionalData = $"User updated their administrative position data with id: {id} successfully.\nOld Data: {JsonSerializer.Serialize(oldData, jsonOptions)}\nNew Data: {JsonSerializer.Serialize(newData, jsonOptions)}.";
			_logger.LogInformation("{@LogDetails}", positionLogs);
			#endregion
			return Mapper.Map<AdministrativePositionDto>(position);
		}

		public async Task DeleteAdministrativePositionAsync(
			int id,
			string? facultyMemberEmail = null)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var positionLogs = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.AdministrativePositionsActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion
			var position = await Repo.GetAsync(new AdministrativePositionsSpecifications(id));
			if (position is null)
			{
				#region Log
				positionLogs.Timestamp = DateTime.Now;
				positionLogs.Level = "Warning";
				positionLogs.RenderedMessage = $"Administrative position not found for user: {currentUser.UserName}.";
				positionLogs.AdditionalData = $"User tried to delete their administrative position data with id: {id}, but no administrative position data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", positionLogs);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						position.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				positionLogs.Timestamp = DateTime.Now;
				positionLogs.Level = "Warning";
				positionLogs.RenderedMessage = $"User unauthorized to delete administrative position data.";
				positionLogs.AdditionalData = $"User tried to delete administrative position data with id: {id} that does not belong to them, administrative position data faculty member id: {position.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", positionLogs);
				#endregion
				throw;
			}

			position.IsDeleted = true;

			Repo.Update(position);
			await SaveChangesAsync();
			#region Log
			positionLogs.Timestamp = DateTime.Now;
			positionLogs.Level = "Information";
			positionLogs.RenderedMessage = $"Administrative position data deleted for user: {currentUser.UserName}.";
			positionLogs.AdditionalData = $"User deleted their administrative position data with id: {id} successfully.";
			_logger.LogInformation("{@LogDetails}", positionLogs);
			#endregion
		}
	}
}