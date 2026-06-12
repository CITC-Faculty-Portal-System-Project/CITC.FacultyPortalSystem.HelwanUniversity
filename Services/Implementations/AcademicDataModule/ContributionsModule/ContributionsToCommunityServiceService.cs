using Domain.Entities.AcademicDataModule.ContributionsModule;
using Microsoft.Extensions.Logging;
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
	public class ContributionsToCommunityServiceService(
	  IUnitOfWork unitOfWork,
	  IAuthenticationService authenticationService,
	  IMapper mapper,
	  ILogger<ContributionsToCommunityServiceService> _logger)
	  : BaseService<ContributionsToCommunityService, int>(unitOfWork, authenticationService, mapper),
		IContributionsToCommunityServiceService
	{
		protected override string EntityName => "Contributions To Community Service";

		public async Task<PaginatedResult<ContributionsToCommunityServiceResponseDTO>> GetAllContributionsToCommunityServiceAsync(
			ContributionsToCommunityServiceSpecificationParameters parameters,
			string? facultyMemberEmail = null)
		{
			var currentUser = await GetCurrentUserAsync();
			var email = facultyMemberEmail ?? currentUser.Email;

			#region Log
			var contributionsLog = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.ContributionsToCommunityServiceActions.ToString(),
				UserName = currentUser.UserName,
				UserIP = GetUserIP(),
			};
			#endregion

			var contributions = await Repo.GetAllAsync(
				new ContributionsToCommunityServiceSpecifications(parameters, email));

			if (contributions is null)
			{
				#region Log
				contributionsLog.RenderedMessage = $"Contributions to community service not found for user: {currentUser.UserName}.";
				contributionsLog.Level = "Warning";
				contributionsLog.Timestamp = DateTime.Now;
				contributionsLog.AdditionalData = $"User tried to get their contributions to community service data, but no contributions to community service data was found in the database for user with email : {email}.";
				_logger.LogWarning("{@LogDetails}", contributionsLog);
				#endregion
				throw NotFound();
			}

			var mapped = Mapper.Map<IEnumerable<ContributionsToCommunityServiceResponseDTO>>(contributions);

			var totalCount = await Repo.CountAsync(
				new ContributionsToCommunityServiceCountSpecifications(parameters, email));

			#region Log
			contributionsLog.RenderedMessage = $"Contributions to community service data retrieved for user: {currentUser.UserName}.";
			contributionsLog.Level = "Information";
			contributionsLog.Timestamp = DateTime.Now;
			contributionsLog.AdditionalData = (facultyMemberEmail is null) ? $"User retrieved their contributions to community service data successfully, total count of contributions to community service data retrieved: {totalCount}."
				: $"Admin: {currentUser.UserName} retrieved user: {userOfData.UserName} contributions to community service data successfully, total count of contributions to community service data retrieved: {totalCount}.";
			_logger.LogInformation("{@LogDetails}", contributionsLog);
			#endregion

			return new PaginatedResult<ContributionsToCommunityServiceResponseDTO>(
				parameters.PageIndex,
				mapped.Count(),
				totalCount,
				mapped);
		}

		public async Task<ContributionsToCommunityServiceResponseDTO> GetContributionToCommunityServiceByIdAsync(
			int id,
			string? facultyMemberEmail = null)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var contributionsLog = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.ContributionsToCommunityServiceActions.ToString(),
				UserName = currentUser.UserName,
				UserIP = GetUserIP(),
			};
			#endregion

			var contribution = await Repo.GetAsync(
				new ContributionsToCommunityServiceSpecifications(id));
			if (contribution is null)
			{
				#region Log
				contributionsLog.Timestamp = DateTime.Now;
				contributionsLog.Level = "Warning";
				contributionsLog.RenderedMessage = $"Contribution to community service not found for user: {userOfData.UserName}.";
				contributionsLog.AdditionalData = (facultyMemberEmail is null) ? $"User tried to get their contribution to community service data with id: {id}, but no contribution to community service data with this id was found in the database." 
					: $"Admin: {currentUser.UserName} tried to get user: {userOfData.UserName} contribution to community service data with id: {id}, but no contribution to community service data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", contributionsLog);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						contribution.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				contributionsLog.Timestamp = DateTime.Now;
				contributionsLog.Level = "Warning";
				contributionsLog.RenderedMessage = $"User unauthorized to access contribution to community service data.";
				contributionsLog.AdditionalData = $"User tried to get contribution to community service data with id: {id} that does not belong to them, contribution to community service data faculty member id: {contribution.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", contributionsLog);
				#endregion
				throw;
			}

			#region Log
			contributionsLog.Timestamp = DateTime.Now;
			contributionsLog.Level = "Information";
			contributionsLog.RenderedMessage = $"Contribution to community service data retrieved for user: {currentUser.UserName}.";
			contributionsLog.AdditionalData = $"User retrieved their contribution to community service data with id: {id} successfully.";
			_logger.LogInformation("{@LogDetails}", contributionsLog);
			#endregion
			return Mapper.Map<ContributionsToCommunityServiceResponseDTO>(contribution);
		}

		public async Task<ContributionsToCommunityServiceResponseDTO> CreateContributionToCommunityServiceAsync(
			ContributionsToCommunityServiceCreateDTO contributionsToCommunityServiceCreateDto,
			string? facultyMemberEmail = null)
		{
			var currentUser = await GetCurrentUserAsync();
			var email = facultyMemberEmail ?? currentUser.Email;

			#region Log
			var contributionLog = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.ContributionsToCommunityServiceActions.ToString(),
				UserName = currentUser.UserName,
				UserIP = GetUserIP()
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
				contributionLog.Timestamp = DateTime.Now;
				contributionLog.Level = "Warning";
				contributionLog.RenderedMessage = $"Faculty Member not found.";
				contributionLog.AdditionalData = $"User tried to create a contribution to community service for a faculty member that does not exist in database, no faculty member found with email : {email}.";
				_logger.LogWarning("{@LogDetails}", contributionLog);
				#endregion
				throw;
			}

			var contribution = Mapper.Map<ContributionsToCommunityService>(contributionsToCommunityServiceCreateDto);
			contribution.FacultyMemberId = facultyMember.Id;

			await Repo.AddAsync(contribution);
			await SaveChangesAsync();

			var response = Mapper.Map<ContributionsToCommunityServiceResponseDTO>(contribution);
			#region Log
			contributionLog.Timestamp = DateTime.Now;
			contributionLog.Level = "Information";
			contributionLog.RenderedMessage = $"User: {currentUser.UserName} created a contribution to community service.";
			contributionLog.AdditionalData = $"User created a contribution to community service with id: {response.Id} and title: {response.ContributionTitle} successfully.";
			_logger.LogInformation("{@LogDetails}", contributionLog);
			#endregion
			return response;
		}

		public async Task<ContributionsToCommunityServiceResponseDTO> UpdateContributionToCommunityServiceAsync(
			int contributionToCommunityServiceId,
			ContributionsToCommunityServiceUpdateDTO contributionsToCommunityServiceUpdateDto,
			string? facultyMemberEmail = null)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var contributionLog = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.ContributionsToCommunityServiceActions.ToString(),
				UserName = currentUser.UserName,
				UserIP = GetUserIP()
			};
			#endregion
			var jsonOptions = new JsonSerializerOptions
			{
				WriteIndented = true,
				Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
			};
			var contribution = await Repo.GetAsync(
			new ContributionsToCommunityServiceSpecifications(contributionToCommunityServiceId));
			if (contribution is null)
			{
				#region Log
				contributionLog.Timestamp = DateTime.Now;
				contributionLog.Level = "Warning";
				contributionLog.RenderedMessage = $"Contribution to community service not found for user: {currentUser.UserName}.";
				contributionLog.AdditionalData = $"User tried to update their contribution to community service data with id: {contributionToCommunityServiceId}, but no contribution to community service data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", contributionLog);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						contribution.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				contributionLog.Timestamp = DateTime.Now;
				contributionLog.Level = "Warning";
				contributionLog.RenderedMessage = $"User unauthorized to update contribution to community service data.";
				contributionLog.AdditionalData = $"User tried to update contribution to community service data with id: {contributionToCommunityServiceId} that does not belong to them, contribution to community service data faculty member id: {contribution.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", contributionLog);
				#endregion
				throw;
			}

			var oldData = Mapper.Map<ContributionsToCommunityServiceResponseDTO>(contribution);
			Mapper.Map(contributionsToCommunityServiceUpdateDto, contribution);

			Repo.Update(contribution);
			await SaveChangesAsync();

			var newData = Mapper.Map<ContributionsToCommunityServiceResponseDTO>(contribution);
			#region Log
			contributionLog.Timestamp = DateTime.Now;
			contributionLog.Level = "Information";
			contributionLog.RenderedMessage = $"Contribution to community service data updated for user: {currentUser.UserName}.";
			contributionLog.AdditionalData = $"User updated their contribution to community service data with id: {contributionToCommunityServiceId} successfully.\nOld Data: {JsonSerializer.Serialize(oldData, jsonOptions)}\nNew Data: {JsonSerializer.Serialize(newData, jsonOptions)}.";
			_logger.LogInformation("{@LogDetails}", contributionLog);
			#endregion
			return newData;
		}

		public async Task DeleteContributionToCommunityServiceAsync(
			int contributionToCommunityServiceId,
			string? facultyMemberEmail = null)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var contributionLog = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.ContributionsToCommunityServiceActions.ToString(),
				UserName = currentUser.UserName,
				UserIP = GetUserIP()
			};
			#endregion
			var contribution = await Repo.GetAsync(
				new ContributionsToCommunityServiceSpecifications(contributionToCommunityServiceId));
			if (contribution is null)
			{
				#region Log
				contributionLog.Timestamp = DateTime.Now;
				contributionLog.Level = "Warning";
				contributionLog.RenderedMessage = $"Contribution to community service not found for user: {currentUser.UserName}.";
				contributionLog.AdditionalData = $"User tried to delete their contribution to community service data with id: {contributionToCommunityServiceId}, but no contribution to community service data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", contributionLog);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						contribution.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				contributionLog.Timestamp = DateTime.Now;
				contributionLog.Level = "Warning";
				contributionLog.RenderedMessage = $"User unauthorized to delete contribution to community service data.";
				contributionLog.AdditionalData = $"User tried to delete contribution to community service data with id: {contributionToCommunityServiceId} that does not belong to them, contribution to community service data faculty member id: {contribution.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", contributionLog);
				#endregion
				throw;
			}

			contribution.IsDeleted = true;

			Repo.Update(contribution);
			await SaveChangesAsync();
			#region Log
			contributionLog.Timestamp = DateTime.Now;
			contributionLog.Level = "Information";
			contributionLog.RenderedMessage = $"Contribution to community service data deleted for user: {currentUser.UserName}.";
			contributionLog.AdditionalData = $"User deleted their contribution to community service data with id: {contributionToCommunityServiceId} successfully.";
			_logger.LogInformation("{@LogDetails}", contributionLog);
			#endregion
		}
	}
}