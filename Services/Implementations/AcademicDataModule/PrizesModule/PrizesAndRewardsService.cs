using Domain.Entities.AcademicDataModule.PrizesModule;
using Microsoft.Extensions.Logging;
using Services.Abstraction.Contracts.AcademicDataModule.PrizesModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.PrizesModule;
using Shared.Dtos.AcademicDataModule.PrizesModule;
using Shared.Enums.Logging;
using Shared.SpecificationParameters.AcademicDataModule.PrizesModule;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Services.Implementations.AcademicDataModule.PrizesModule
{
	public class PrizesAndRewardsService(
	   IUnitOfWork unitOfWork,
	   IAuthenticationService authenticationService,
	   IMapper mapper,
	   ILogger<PrizesAndRewardsService> _logger)
	   : BaseService<PrizesAndRewards, int>(unitOfWork, authenticationService, mapper),
		 IPrizesAndRewardsService
	{
		protected override string EntityName => "Prizes and Rewards";

		public async Task<PaginatedResult<PrizesAndRewardsResponseDTO>> GetAllPrizesAndRewardsAsync(
			PrizesAndRewardsSpecificationParameters parameters,
			string? facultyMemberEmail = null)
		{
			var currentUser = await GetCurrentUserAsync();
			var email = facultyMemberEmail ?? currentUser.Email;

			#region Log
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(email);
			var prizesLogs = new LogEntry
			{
				Category = Category.FacultyMemberPrizesAndRewards.ToString(),
				CategoryAction = CategoryAction.PrizesAndRewardsActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion

			var prizes = await Repo.GetAllAsync(
				new PrizesAndRewardsSpecifications(parameters, email));

			if (prizes is null)
			{
				#region Log
				prizesLogs.RenderedMessage = $"Prizes and rewards not found for user: {currentUser.UserName}.";
				prizesLogs.Level = "Warning";
				prizesLogs.Timestamp = DateTime.Now;
				prizesLogs.AdditionalData = $"User tried to get their prizes and rewards data, but no prizes and/or rewards data was found in the database for user with email : {email}.";
				_logger.LogWarning("{@LogDetails}", prizesLogs);
				#endregion
				throw NotFound();
			}

			var mapped = Mapper.Map<IEnumerable<PrizesAndRewardsResponseDTO>>(prizes);

			var totalCount = await Repo.CountAsync(
				new PrizesAndRewardsCountSpecifications(parameters, email));

			#region Log
			prizesLogs.RenderedMessage = $"Prizes and rewards data retrieved for user: {currentUser.UserName}.";
			prizesLogs.Level = "Information";
			prizesLogs.Timestamp = DateTime.Now;
			prizesLogs.AdditionalData = $"User retrieved their prizes and rewards data successfully, total count of prizes and rewards data retrieved: {totalCount}.";
			_logger.LogInformation("{@LogDetails}", prizesLogs);
			#endregion

			return new PaginatedResult<PrizesAndRewardsResponseDTO>(
				parameters.PageIndex,
				mapped.Count(),
				totalCount,
				mapped);
		}

		public async Task<PrizesAndRewardsResponseDTO> GetPrizeOrRewardByIdAsync(
			int id,
			string? facultyMemberEmail = null)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var prizesLogs = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.PrizesAndRewardsActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion

			var prize = await Repo.GetAsync(
				new PrizesAndRewardsSpecifications(id));
			if (prize is null)
			{
				#region Log
				prizesLogs.Timestamp = DateTime.Now;
				prizesLogs.Level = "Warning";
				prizesLogs.RenderedMessage = $"Prize or reward not found for user: {currentUser.UserName}.";
				prizesLogs.AdditionalData = $"User tried to get their prize or reward data with id: {id}, but no prize or reward data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", prizesLogs);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						prize.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				prizesLogs.Timestamp = DateTime.Now;
				prizesLogs.Level = "Warning";
				prizesLogs.RenderedMessage = $"User unauthorized to access prize or reward data.";
				prizesLogs.AdditionalData = $"User tried to get prize or reward data with id: {id} that does not belong to them, prize or reward data faculty member id: {prize.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", prizesLogs);
				#endregion
				throw;
			}

			#region Log
			prizesLogs.Timestamp = DateTime.Now;
			prizesLogs.Level = "Information";
			prizesLogs.RenderedMessage = $"Prize or reward data retrieved for user: {currentUser.UserName}.";
			prizesLogs.AdditionalData = $"User retrieved their prize or reward data with id: {id} successfully.";
			_logger.LogInformation("{@LogDetails}", prizesLogs);
			#endregion
			return Mapper.Map<PrizesAndRewardsResponseDTO>(prize);
		}

		public async Task<PrizesAndRewardsResponseDTO> CreatePrizeOrRewardAsync(
			PrizesAndRewardsCreateDTO dto,
			string? facultyMemberEmail = null)
		{
			var currentUser = await GetCurrentUserAsync();
			var email = facultyMemberEmail ?? currentUser.Email;

			#region Log
			var prizesLogs = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.PrizesAndRewardsActions.ToString(),
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
				prizesLogs.Timestamp = DateTime.Now;
				prizesLogs.Level = "Warning";
				prizesLogs.RenderedMessage = $"Faculty Member not found.";
				prizesLogs.AdditionalData = $"User tried to create a prize or reward for a faculty member that does not exist in database, no faculty member found with email : {email}.";
				_logger.LogWarning("{@LogDetails}", prizesLogs);
				#endregion
				throw;
			}

			var prize = Mapper.Map<PrizesAndRewards>(dto);
			prize.FacultyMemberId = facultyMember.Id;

			await Repo.AddAsync(prize);
			await SaveChangesAsync();

			var response = Mapper.Map<PrizesAndRewardsResponseDTO>(prize);
			#region Log
			prizesLogs.Timestamp = DateTime.Now;
			prizesLogs.Level = "Information";
			prizesLogs.RenderedMessage = $"User: {currentUser.UserName} created a prize or reward.";
			prizesLogs.AdditionalData = $"User created a prize or reward with id: {response.Id} and awarding authority: {response.AwardingAuthority} successfully.";
			_logger.LogInformation("{@LogDetails}", prizesLogs);
			#endregion
			return response;
		}

		public async Task<PrizesAndRewardsResponseDTO> UpdatePrizeOrRewardAsync(
			int id,
			PrizesAndRewardsUpdateDTO dto,
			string? facultyMemberEmail = null)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var prizesLogs = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.PrizesAndRewardsActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion
			var jsonOptions = new JsonSerializerOptions
			{
				WriteIndented = true,
				Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
			};
			var prize = await Repo.GetAsync(
				new PrizesAndRewardsSpecifications(id));
			if (prize is null)
			{
				#region Log
				prizesLogs.Timestamp = DateTime.Now;
				prizesLogs.Level = "Warning";
				prizesLogs.RenderedMessage = $"Prize or reward not found for user: {currentUser.UserName}.";
				prizesLogs.AdditionalData = $"User tried to update their prize or reward data with id: {id}, but no prize or reward data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", prizesLogs);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						prize.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				prizesLogs.Timestamp = DateTime.Now;
				prizesLogs.Level = "Warning";
				prizesLogs.RenderedMessage = $"User unauthorized to update prize or reward data.";
				prizesLogs.AdditionalData = $"User tried to update prize or reward data with id: {id} that does not belong to them, prize or reward data faculty member id: {prize.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", prizesLogs);
				#endregion
				throw;
			}

			var oldData = Mapper.Map<PrizesAndRewardsResponseDTO>(prize);
			Mapper.Map(dto, prize);

			Repo.Update(prize);
			await SaveChangesAsync();

			var newData = Mapper.Map<PrizesAndRewardsResponseDTO>(prize);
			#region Log
			prizesLogs.Timestamp = DateTime.Now;
			prizesLogs.Level = "Information";
			prizesLogs.RenderedMessage = $"Prize or reward data updated for user: {currentUser.UserName}.";
			prizesLogs.AdditionalData = $"User updated their prize or reward data with id: {id} successfully.\nOld Data: {JsonSerializer.Serialize(oldData, jsonOptions)}\nNew Data: {JsonSerializer.Serialize(newData, jsonOptions)}.";
			_logger.LogInformation("{@LogDetails}", prizesLogs);
			#endregion
			return Mapper.Map<PrizesAndRewardsResponseDTO>(prize);
		}

		public async Task DeletePrizeOrRewardAsync(
			int id,
			string? facultyMemberEmail = null)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var prizesLogs = new LogEntry
			{
				Category = Category.FacultyMemberAcademicData.ToString(),
				CategoryAction = CategoryAction.PrizesAndRewardsActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion
			var prize = await Repo.GetAsync(
				new PrizesAndRewardsSpecifications(id));
			if (prize is null)
			{
				#region Log
				prizesLogs.Timestamp = DateTime.Now;
				prizesLogs.Level = "Warning";
				prizesLogs.RenderedMessage = $"Prize or reward not found for user: {currentUser.UserName}.";
				prizesLogs.AdditionalData = $"User tried to delete their prize or reward data with id: {id}, but no prize or reward data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", prizesLogs);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
					   prize.FacultyMemberId,
					   facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				prizesLogs.Timestamp = DateTime.Now;
				prizesLogs.Level = "Warning";
				prizesLogs.RenderedMessage = $"User unauthorized to delete prize or reward data.";
				prizesLogs.AdditionalData = $"User tried to delete prize or reward data with id: {id} that does not belong to them, prize or reward data faculty member id: {prize.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", prizesLogs);
				#endregion
				throw;
			}

			prize.IsDeleted = true;

			Repo.Update(prize);
			await SaveChangesAsync();
			#region Log
			prizesLogs.Timestamp = DateTime.Now;
			prizesLogs.Level = "Information";
			prizesLogs.RenderedMessage = $"Prize or reward data deleted for user: {currentUser.UserName}.";
			prizesLogs.AdditionalData = $"User deleted their prize or reward data with id: {id} successfully.";
			_logger.LogInformation("{@LogDetails}", prizesLogs);
			#endregion
		}
	}
}