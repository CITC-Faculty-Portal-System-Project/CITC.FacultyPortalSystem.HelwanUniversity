using Domain.Entities.AcademicDataModule.WritingsAndPatents;
using Microsoft.Extensions.Logging;
using Services.Abstraction.Contracts.AcademicDataModule.WritingsAndPatentsModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.WritingsAndPatentsModule;
using Shared.Dtos.AcademicDataModule.WritingsAndPatentsModule;
using Shared.Enums.Logging;
using Shared.SpecificationParameters.AcademicDataModule.WritingsAndPatentsModule;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Services.Implementations.AcademicDataModule.WritingsAndPatentsModule
{
	public class PatentsService(
	 IUnitOfWork unitOfWork,
	 IAuthenticationService authenticationService,
	 IMapper mapper,
	 ILogger<PatentsService> _logger)
	 : BaseService<Patents, int>(unitOfWork, authenticationService, mapper),
	   IPatentsService
	{
		protected override string EntityName => "Patents";

		public async Task<PaginatedResult<PatentsResponseDTO>> GetAllPatentsAsync(
			PatentsSpecificationParameters parameters,
			string? facultyMemberEmail = null)
		{
			var currentUser = await GetCurrentUserAsync();
			var email = facultyMemberEmail ?? currentUser.Email;

			#region Log
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(email);
			var patentsLog = new LogEntry
			{
				Category = Category.FacultyMemberWritingsAndPatents.ToString(),
				CategoryAction = CategoryAction.PatentsActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion

			var patents = await Repo.GetAllAsync(
				new PatentsSpecifications(parameters, email));

			if (patents is null)
			{
				#region Log
				patentsLog.RenderedMessage = $"Patents not found for user: {currentUser.UserName}.";
				patentsLog.Level = "Warning";
				patentsLog.Timestamp = DateTime.Now;
				patentsLog.AdditionalData = $"User tried to get their patents data, but no patents data was found in the database for user with email : {email}.";
				_logger.LogWarning("{@LogDetails}", patentsLog);
				#endregion
				throw NotFound();
			}

			var mapped = Mapper.Map<IEnumerable<PatentsResponseDTO>>(patents);

			var totalCount = await Repo.CountAsync(
				new PatentsCountSpecifications(parameters, email));

			#region Log
			patentsLog.RenderedMessage = $"Patents data retrieved for user: {currentUser.UserName}.";
			patentsLog.Level = "Information";
			patentsLog.Timestamp = DateTime.Now;
			patentsLog.AdditionalData = $"User retrieved their patents data successfully, total count of patents data retrieved: {totalCount}.";
			_logger.LogInformation("{@LogDetails}", patentsLog);
			#endregion

			return new PaginatedResult<PatentsResponseDTO>(
				parameters.PageIndex,
				mapped.Count(),
				totalCount,
				mapped);
		}

		public async Task<PatentsResponseDTO> GetPatentByIdAsync(
			int id,
			string? facultyMemberEmail = null)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var patentsLog = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.PatentsActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion

			var patent = await Repo.GetAsync(
				new PatentsSpecifications(id));
			if (patent is null)
			{
				#region Log
				patentsLog.Timestamp = DateTime.Now;
				patentsLog.Level = "Warning";
				patentsLog.RenderedMessage = $"Patent not found for user: {currentUser.UserName}.";
				patentsLog.AdditionalData = $"User tried to get their patent data with id: {id}, but no patent data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", patentsLog);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						patent.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				patentsLog.Timestamp = DateTime.Now;
				patentsLog.Level = "Warning";
				patentsLog.RenderedMessage = $"User unauthorized to access patent data.";
				patentsLog.AdditionalData = $"User tried to get patent data with id: {id} that does not belong to them, patent data faculty member id: {patent.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", patentsLog);
				#endregion
				throw;
			}

			#region Log
			patentsLog.Timestamp = DateTime.Now;
			patentsLog.Level = "Information";
			patentsLog.RenderedMessage = $"Patent data retrieved for user: {currentUser.UserName}.";
			patentsLog.AdditionalData = $"User retrieved their patent data with id: {id} successfully.";
			_logger.LogInformation("{@LogDetails}", patentsLog);
			#endregion
			return Mapper.Map<PatentsResponseDTO>(patent);
		}

		public async Task<PatentsResponseDTO> CreatePatentAsync(
			PatentsCreateDTO patentCreateDto,
			string? facultyMemberEmail = null)
		{
			var currentUser = await GetCurrentUserAsync();
			var email = facultyMemberEmail ?? currentUser.Email;

			#region Log
			var patentsLog = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.PatentsActions.ToString(),
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
				patentsLog.Timestamp = DateTime.Now;
				patentsLog.Level = "Warning";
				patentsLog.RenderedMessage = $"Faculty Member not found.";
				patentsLog.AdditionalData = $"User tried to create a patent for a faculty member that does not exist in database, no faculty member found with email : {email}.";
				_logger.LogWarning("{@LogDetails}", patentsLog);
				#endregion
				throw;
			}

			var patent = Mapper.Map<Patents>(patentCreateDto);
			patent.FacultyMemberId = facultyMember.Id;

			await Repo.AddAsync(patent);
			await SaveChangesAsync();

			var response = Mapper.Map<PatentsResponseDTO>(patent);
			#region Log
			patentsLog.Timestamp = DateTime.Now;
			patentsLog.Level = "Information";
			patentsLog.RenderedMessage = $"User: {currentUser.UserName} created a patent.";
			patentsLog.AdditionalData = $"User created a patent with id: {response.Id} and name: {response.NameOfPatent} successfully.";
			_logger.LogInformation("{@LogDetails}", patentsLog);
			#endregion
			return response;
		}

		public async Task<PatentsResponseDTO> UpdatePatentAsync(
			int patentId,
			PatentsUpdateDTO patentUpdateDto,
			string? facultyMemberEmail = null)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var patentsLog = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.PatentsActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion
			var jsonOptions = new JsonSerializerOptions
			{
				WriteIndented = true,
				Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
			};
			var patent = await Repo.GetAsync(
				new PatentsSpecifications(patentId));
			if (patent is null)
			{
				#region Log
				patentsLog.Timestamp = DateTime.Now;
				patentsLog.Level = "Warning";
				patentsLog.RenderedMessage = $"Patent not found for user: {currentUser.UserName}.";
				patentsLog.AdditionalData = $"User tried to update their patent data with id: {patentId}, but no patent data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", patentsLog);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						patent.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				patentsLog.Timestamp = DateTime.Now;
				patentsLog.Level = "Warning";
				patentsLog.RenderedMessage = $"User unauthorized to update patent data.";
				patentsLog.AdditionalData = $"User tried to update patent data with id: {patentId} that does not belong to them, patent data faculty member id: {patent.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", patentsLog);
				#endregion
				throw;
			}

			var oldData = Mapper.Map<PatentsResponseDTO>(patent);
			Mapper.Map(patentUpdateDto, patent);

			Repo.Update(patent);
			await SaveChangesAsync();

			var newData = Mapper.Map<PatentsResponseDTO>(patent);
			#region Log
			patentsLog.Timestamp = DateTime.Now;
			patentsLog.Level = "Information";
			patentsLog.RenderedMessage = $"Patent data updated for user: {currentUser.UserName}.";
			patentsLog.AdditionalData = $"User updated their patent data with id: {patentId} successfully.\nOld Data: {JsonSerializer.Serialize(oldData, jsonOptions)}\nNew Data: {JsonSerializer.Serialize(newData, jsonOptions)}.";
			_logger.LogInformation("{@LogDetails}", patentsLog);
			#endregion
			return newData;
		}

		public async Task DeletePatentAsync(
			int patentId,
			string? facultyMemberEmail = null)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var patentsLog = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.PatentsActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion
			var patent = await Repo.GetAsync(
				new PatentsSpecifications(patentId));
			if (patent is null)
			{
				#region Log
				patentsLog.Timestamp = DateTime.Now;
				patentsLog.Level = "Warning";
				patentsLog.RenderedMessage = $"Patent not found for user: {currentUser.UserName}.";
				patentsLog.AdditionalData = $"User tried to delete their patent data with id: {patentId}, but no patent data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", patentsLog);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						patent.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				patentsLog.Timestamp = DateTime.Now;
				patentsLog.Level = "Warning";
				patentsLog.RenderedMessage = $"User unauthorized to delete patent data.";
				patentsLog.AdditionalData = $"User tried to delete patent data with id: {patentId} that does not belong to them, patent data faculty member id: {patent.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", patentsLog);
				#endregion
				throw;
			}

			patent.IsDeleted = true;

			Repo.Update(patent);
			await SaveChangesAsync();
			#region Log
			patentsLog.Timestamp = DateTime.Now;
			patentsLog.Level = "Information";
			patentsLog.RenderedMessage = $"Patent data deleted for user: {currentUser.UserName}.";
			patentsLog.AdditionalData = $"User deleted their patent data with id: {patentId} successfully.";
			_logger.LogInformation("{@LogDetails}", patentsLog);
			#endregion
		}
	}
}
