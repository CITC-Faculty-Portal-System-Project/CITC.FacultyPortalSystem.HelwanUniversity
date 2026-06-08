using Domain.Entities.AcademicDataModule.ScientificProgressionModule;
using Microsoft.AspNetCore.Mvc;
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
	public class JobRanksService(
	 IUnitOfWork unitOfWork,
	 IAuthenticationService authenticationService,
	 IMapper mapper,
	 ILogger<JobRanksService> _logger)
	 : BaseService<JobRanks, int>(unitOfWork, authenticationService, mapper),
	   IJobRanksService
	{
		protected override string EntityName => "Job Ranks";

		public async Task<PaginatedResult<JobRankResponseDto>> GetAllAsync(
			JobRanksSpecificationsParameters parameters,
			string? facultyMemberEmail = null)
		{
			var currentUser = await GetCurrentUserAsync();
			var email = facultyMemberEmail ?? currentUser.Email;

			#region Log
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(email);
			var rankLog = new LogEntry
			{
				Category = Category.FacultyMemberScientificProgression.ToString(),
				CategoryAction = CategoryAction.JobRanksActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion

			var jobRanks = await Repo.GetAllAsync(
				new JobRanksSpecifications(parameters, email));

			if (jobRanks is null)
			{
				#region Log
				rankLog.RenderedMessage = $"Job ranks not found for user: {userOfData.UserName}.";
				rankLog.Level = "Warning";
				rankLog.Timestamp = DateTime.Now;
				rankLog.AdditionalData = (facultyMemberEmail is null) ? $"User tried to get their job ranks data, but no job ranks data was found in the database for user with email: {email}." :
					$"Admin: {currentUser.UserName} tried to get user: {userOfData.UserName} job ranks data, but no job ranks data was found in the database for user: {userOfData.UserName}";
				_logger.LogWarning("{@LogDetails}", rankLog);
				#endregion
				throw NotFound();
			}

			var mapped = Mapper.Map<IEnumerable<JobRankResponseDto>>(jobRanks);

			var totalCount = await Repo.CountAsync(
				new JobRanksCountSpecifications(parameters, email));

			#region Log
			rankLog.RenderedMessage = $"Job ranks data retrieved for user: {userOfData.UserName}.";
			rankLog.Level = "Information";
			rankLog.Timestamp = DateTime.Now;
			rankLog.AdditionalData = (facultyMemberEmail is null) ? $"User retrieved their job ranks data successfully, total count of job ranks data retrieved: {totalCount}."
				: $"Admin: {currentUser.UserName} retrieved user: {userOfData.UserName} job ranks data successfully, total count of job ranks data retrieved: {totalCount}.";
			_logger.LogInformation("{@LogDetails}", rankLog);
			#endregion

			return new PaginatedResult<JobRankResponseDto>(
				parameters.PageIndex,
				mapped.Count(),
				totalCount,
				mapped);
		}

		public async Task<JobRankResponseDto> GetByIdAsync(
			int id,
			string? facultyMemberEmail = null)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(facultyMemberEmail);
			var rankLog = new LogEntry
			{
				Category = Category.FacultyMemberScientificProgression.ToString(),
				CategoryAction = CategoryAction.JobRanksActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion

			var jobRank = await Repo.GetAsync(new JobRanksSpecifications(id));
			if(jobRank is null)
			{
				#region Log
				rankLog.Timestamp = DateTime.Now;
				rankLog.Level = "Warning";
				rankLog.RenderedMessage = $"Job rank not found for user: {userOfData.UserName}.";
				rankLog.AdditionalData = (facultyMemberEmail is null) ? $"User tried to get their job rank data with id: {id}, but no job rank data with this id was found in the database."
					: $"Admin: {currentUser.UserName} tried to get user: {userOfData.UserName} job rank data with id: {id}, but no job rank data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", rankLog);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						jobRank.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				rankLog.Timestamp = DateTime.Now;
				rankLog.Level = "Warning";
				rankLog.RenderedMessage = $"User unauthorized to access job rank data.";
				rankLog.AdditionalData = $"User tried to get job rank data with id: {id} that does not belong to them, job rank data faculty member id: {jobRank.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", rankLog);
				#endregion
				throw;
			}

			#region Log
			rankLog.Timestamp = DateTime.Now;
			rankLog.Level = "Information";
			rankLog.RenderedMessage = $"Job rank data retrieved for user: {userOfData.UserName}.";
			rankLog.AdditionalData = (facultyMemberEmail is null) ? $"User retrieved their job rank data with id: {id} successfully."
				: $"Admin: {currentUser.UserName} retrieved user: {userOfData.UserName} job rank data with id: {id} successfully.";
			_logger.LogInformation("{@LogDetails}", rankLog);
			#endregion
			return Mapper.Map<JobRankResponseDto>(jobRank);
		}

		public async Task<JobRankResponseDto> CreateAsync(
			JobRankCreateDto dto,
			string? facultyMemberEmail = null)
		{
			var currentUser = await GetCurrentUserAsync();
			var email = facultyMemberEmail ?? currentUser.Email;

			#region Log
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(email);
			var rankLog = new LogEntry
			{
				Category = Category.FacultyMemberScientificProgression.ToString(),
				CategoryAction = CategoryAction.JobRanksActions.ToString(),
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
				rankLog.Timestamp = DateTime.Now;
				rankLog.Level = "Warning";
				rankLog.RenderedMessage = $"Faculty Member not found.";
				rankLog.AdditionalData = (facultyMemberEmail is null) ? $"User tried to create a job rank for a faculty member that does not exist in database, no faculty member found with email: {email}."
					: $"Admin: {currentUser.UserName} tried to create a job rank for user: {userOfData.UserName}, but no faculty member found with email: {email}.";
				_logger.LogWarning("{@LogDetails}", rankLog);
				#endregion
				throw;
			}

			var jobRank = Mapper.Map<JobRanks>(dto);
			jobRank.FacultyMemberId = facultyMember.Id;

			await Repo.AddAsync(jobRank);
			await SaveChangesAsync();

			var response = Mapper.Map<JobRankResponseDto>(jobRank);
			#region Log
			rankLog.Timestamp = DateTime.Now;
			rankLog.Level = "Information";
			rankLog.RenderedMessage = (facultyMemberEmail is null) ? $"User: {userOfData.UserName} created a job rank."
				: $"Admin: {currentUser.UserName} created a job rank for user: {userOfData.UserName}";
			rankLog.AdditionalData = (facultyMemberEmail is null) ? $"User created a job rank with id: {response.Id} successfully."
				: $"Admin: {currentUser.UserName} created a job rank with id: {response.Id} for user: {userOfData.UserName} successfully.";
			_logger.LogInformation("{@LogDetails}", rankLog);
			#endregion
			return response;
		}

		public async Task<JobRankResponseDto> UpdateAsync(
			int id,
			JobRankUpdateDto dto,
			string? facultyMemberEmail = null)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(facultyMemberEmail);
			var rankLog = new LogEntry
			{
				Category = Category.FacultyMemberScientificProgression.ToString(),
				CategoryAction = CategoryAction.JobRanksActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion
			var jsonOptions = new JsonSerializerOptions
			{
				WriteIndented = true,
				Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
			};
			var jobRank = await Repo.GetAsync(new JobRanksSpecifications(id));
			if(jobRank is null)
			{
				#region Log
				rankLog.Timestamp = DateTime.Now;
				rankLog.Level = "Warning";
				rankLog.RenderedMessage = $"Contribution to community service not found for user: {userOfData.UserName}.";
				rankLog.AdditionalData = (facultyMemberEmail is null) ? $"User tried to update their contribution to community service data with id: {id}, but no contribution to community service data with this id was found in the database."
					: $"Admin: {currentUser.UserName} tried to update user: {userOfData.UserName} contribution to community service data with id: {id}, but no contribution to community service data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", rankLog);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						jobRank.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				rankLog.Timestamp = DateTime.Now;
				rankLog.Level = "Warning";
				rankLog.RenderedMessage = $"User unauthorized to update job rank data.";
				rankLog.AdditionalData = $"User tried to update job rank data with id: {id} that does not belong to them, job rank data faculty member id: {jobRank.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", rankLog);
				#endregion
				throw;
			}

			var oldData = Mapper.Map<JobRankResponseDto>(jobRank);
			Mapper.Map(dto, jobRank);

			Repo.Update(jobRank);
			await SaveChangesAsync();

			var newData = Mapper.Map<JobRankResponseDto>(jobRank);
			#region Log
			rankLog.Timestamp = DateTime.Now;
			rankLog.Level = "Information";
			rankLog.RenderedMessage = $"Job rank data updated for user: {userOfData.UserName}.";
			rankLog.AdditionalData = (facultyMemberEmail is null) ? $"User updated their job rank data with id: {id} successfully.\nOld Data: {JsonSerializer.Serialize(oldData, jsonOptions)}\nNew Data: {JsonSerializer.Serialize(newData, jsonOptions)}."
				: $"Admin: {currentUser.UserName} updated user: {userOfData.UserName} job rank data with id: {id} successfully.\nOld Data: {JsonSerializer.Serialize(oldData, jsonOptions)}\nNew Data: {JsonSerializer.Serialize(newData, jsonOptions)}.";
			_logger.LogInformation("{@LogDetails}", rankLog);
			#endregion
			return newData;
		}

		public async Task DeleteAsync(
			int id,
			string? facultyMemberEmail = null)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(facultyMemberEmail);
			var rankLog = new LogEntry
			{
				Category = Category.FacultyMemberScientificProgression.ToString(),
				CategoryAction = CategoryAction.JobRanksActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion
			var jobRank = await Repo.GetAsync(new JobRanksSpecifications(id));
			if(jobRank is null)
			{
				#region Log
				rankLog.Timestamp = DateTime.Now;
				rankLog.Level = "Warning";
				rankLog.RenderedMessage = $"Job rank not found for user: {userOfData.UserName}.";
				rankLog.AdditionalData = (facultyMemberEmail is null) ? $"User tried to delete their job rank data with id: {id}, but no job rank data with this id was found in the database."
					: $"Admin: {currentUser.UserName} tried to delete user: {userOfData.UserName} job rank data with id: {id}, but no job rank data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", rankLog);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						jobRank.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				rankLog.Timestamp = DateTime.Now;
				rankLog.Level = "Warning";
				rankLog.RenderedMessage = $"User unauthorized to delete job rank data.";
				rankLog.AdditionalData = $"User tried to delete job rank data with id: {id} that does not belong to them, job rank data faculty member id: {jobRank.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", rankLog);
				#endregion
				throw;
			}

			jobRank.IsDeleted = true;

			Repo.Update(jobRank);
			await SaveChangesAsync();
			#region Log
			rankLog.Timestamp = DateTime.Now;
			rankLog.Level = "Information";
			rankLog.RenderedMessage = $"Job rank data deleted for user: {userOfData.UserName}.";
			rankLog.AdditionalData = (facultyMemberEmail is null) ? $"User deleted their job rank data with id: {id} successfully."
				: $"Admin: {currentUser.UserName} deleted user: {userOfData.UserName} job rank data with id: {id} successfully.";
			_logger.LogInformation("{@LogDetails}", rankLog);
			#endregion
		}
	}
}