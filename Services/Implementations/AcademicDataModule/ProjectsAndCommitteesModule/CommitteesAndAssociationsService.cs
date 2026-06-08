using Domain.Entities.AcademicDataModule.ProjectsAndCommitteesModule;
using Microsoft.Extensions.Logging;
using Services.Abstraction.Contracts.AcademicDataModule.ProjectsAndCommitteesModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.Dtos.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.Enums.Logging;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Services.Implementations.AcademicDataModule.ProjectsAndCommitteesModule
{
	public class CommitteesAndAssociationsService(
	  IUnitOfWork unitOfWork,
	  IAuthenticationService authenticationService,
	  IMapper mapper,
	  ILogger<CommitteesAndAssociationsService> _logger)
	  : BaseService<CommitteesAndAssociations, int>(unitOfWork, authenticationService, mapper),
		ICommitteesAndAssociationsService
	{
		protected override string EntityName => "Committees And Associations";

		public async Task<PaginatedResult<CommitteesAndAssociationsResponseDto>> GetAllCommitteesAndAssociationsAsync(
			CommitteesAndAssociationsSpecificationsParameters parameters,
			string? facultyMemberEmail = null)
		{
			var currentUser = await GetCurrentUserAsync();
			var email = facultyMemberEmail ?? currentUser.Email;

			#region Log
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(email);
			var committeesLogs = new LogEntry
			{
				Category = Category.FacultyMemberProjectsAndCommittees.ToString(),
				CategoryAction = CategoryAction.CommitteesAndAssociationsActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion

			var committees = await Repo.GetAllAsync(
				new CommitteesAndAssociationsSpecifications(parameters, email));

			if (committees is null)
			{
				#region Log
				committeesLogs.RenderedMessage = $"Committees and associations not found for user: {userOfData.UserName}.";
				committeesLogs.Level = "Warning";
				committeesLogs.Timestamp = DateTime.Now;
				committeesLogs.AdditionalData = (facultyMemberEmail is null) ? $"User tried to get their committees and associations data, but no committees and/or associations data was found in the database for user with email: {email}."
					: $"Admin: {currentUser.UserName} tried to get user: {userOfData.UserName} committees and associations data, but no committees and/or associations data was found in the database for user: {userOfData.UserName}";
				_logger.LogWarning(committeesLogs.ToString());
				#endregion
				throw NotFound();
			}

			var mapped = Mapper.Map<IEnumerable<CommitteesAndAssociationsResponseDto>>(committees);

			var totalCount = await Repo.CountAsync(
				new CommitteesAndAssociationsCountSpecifications(parameters, email));

			#region Log
			committeesLogs.RenderedMessage = $"Committees and associations data retrieved for user: {userOfData.UserName}.";
			committeesLogs.Level = "Information";
			committeesLogs.Timestamp = DateTime.Now;
			committeesLogs.AdditionalData = (facultyMemberEmail is null) ? $"User retrieved their committees and associations data successfully, total count of committees and associations data retrieved: {totalCount}."
				: $"Admin: {currentUser.UserName} retrieved user: {userOfData.UserName} committees and associations data successfully, total count of committees and associations data retrieved: {totalCount}.";
			_logger.LogInformation("{@LogDetails}", committeesLogs);
			#endregion

			return new PaginatedResult<CommitteesAndAssociationsResponseDto>(
				parameters.PageIndex,
				mapped.Count(),
				totalCount,
				mapped);
		}

		public async Task<CommitteesAndAssociationsResponseDto> GetCommitteeOrAssociationByIdAsync(
			int id,
			string? facultyMemberEmail = null)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(facultyMemberEmail);
			var committeeLogs = new LogEntry
			{
				Category = Category.FacultyMemberProjectsAndCommittees.ToString(),
				CategoryAction = CategoryAction.CommitteesAndAssociationsActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion

			var committee = await Repo.GetAsync(
				new CommitteesAndAssociationsSpecifications(id));
			if (committee is null)
			{
				#region Log
				committeeLogs.Timestamp = DateTime.Now;
				committeeLogs.Level = "Warning";
				committeeLogs.RenderedMessage = $"Committee or association not found for user: {userOfData.UserName}.";
				committeeLogs.AdditionalData = (facultyMemberEmail is null) ? $"User tried to get their committee or association data with id: {id}, but no committee or association data with this id was found in the database."
					: $"Admin: {currentUser.UserName} tried to get user: {userOfData.UserName} committee or association data with id: {id}, but no committee or association data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", committeeLogs);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						committee.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				committeeLogs.Timestamp = DateTime.Now;
				committeeLogs.Level = "Warning";
				committeeLogs.RenderedMessage = $"User unauthorized to access committee or association data.";
				committeeLogs.AdditionalData = $"User tried to get committee or association data with id: {id} that does not belong to them, committee or association data faculty member id: {committee.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", committeeLogs);
				#endregion
				throw;
			}

			#region Log
			committeeLogs.Timestamp = DateTime.Now;
			committeeLogs.Level = "Information";
			committeeLogs.RenderedMessage = $"Committee or association data retrieved for user: {userOfData.UserName}.";
			committeeLogs.AdditionalData = (facultyMemberEmail is null) ? $"User retrieved their committee or association data with id: {id} successfully."
				: $"Admin: {currentUser.UserName} retrieved user: {userOfData.UserName} committee or association data with id: {id} successfully.";
			_logger.LogInformation("{@LogDetails}", committeeLogs);
			#endregion
			return Mapper.Map<CommitteesAndAssociationsResponseDto>(committee);
		}

		public async Task<CommitteesAndAssociationsResponseDto> CreateCommitteeOrAssociationAsync(
			CommitteeOrAssociationCreateDto dto,
			string? facultyMemberEmail = null)
		{
			var currentUser = await GetCurrentUserAsync();
			var email = facultyMemberEmail ?? currentUser.Email;

			#region Log
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(email);
			var committeeLogs = new LogEntry
			{
				Category = Category.FacultyMemberProjectsAndCommittees.ToString(),
				CategoryAction = CategoryAction.CommitteesAndAssociationsActions.ToString(),
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
				committeeLogs.Timestamp = DateTime.Now;
				committeeLogs.Level = "Warning";
				committeeLogs.RenderedMessage = $"Faculty Member not found.";
				committeeLogs.AdditionalData = (facultyMemberEmail is null) ? $"User tried to create a committee or association for a faculty member that does not exist in database, no faculty member found with email: {email}."
					: $"Admin: {currentUser.UserName} tried to create a committee or association for user: {userOfData.UserName}, but no faculty member found with email: {email}.";
				_logger.LogWarning("{@LogDetails}", committeeLogs);
				#endregion
				throw;
			}

			var committee = Mapper.Map<CommitteesAndAssociations>(dto);
			committee.FacultyMemberId = facultyMember.Id;

			await Repo.AddAsync(committee);
			await SaveChangesAsync();

			var response = Mapper.Map<CommitteesAndAssociationsResponseDto>(committee);
			#region Log
			committeeLogs.Timestamp = DateTime.Now;
			committeeLogs.Level = "Information";
			committeeLogs.RenderedMessage = (facultyMemberEmail is null) ? $"User: {userOfData.UserName} created a committee or association."
				: $"Admin: {currentUser.UserName} created a committee or association for user: {userOfData.UserName}";
			committeeLogs.AdditionalData = (facultyMemberEmail is null) ? $"User created a committee or association with id: {response.Id} and name: {response.NameOfCommitteeOrAssociation} successfully."
				: $"Admin: {currentUser.UserName} created a committee or association with id: {response.Id} and name: {response.NameOfCommitteeOrAssociation} for user: {userOfData.UserName} successfully.";
			_logger.LogInformation("{@LogDetails}", committeeLogs);
			#endregion
			return response;
		}

		public async Task<CommitteesAndAssociationsResponseDto> UpdateCommitteeOrAssociationAsync(
			int id,
			CommitteeOrAssociationUpdateDto dto,
			string? facultyMemberEmail = null)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(facultyMemberEmail);
			var committeeLogs = new LogEntry
			{
				Category = Category.FacultyMemberProjectsAndCommittees.ToString(),
				CategoryAction = CategoryAction.CommitteesAndAssociationsActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion
			var jsonOptions = new JsonSerializerOptions
			{
				WriteIndented = true,
				Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
			};
			var committee = await Repo.GetAsync(
				new CommitteesAndAssociationsSpecifications(id));
			if(committee is null)
			{
				#region Log
				committeeLogs.Timestamp = DateTime.Now;
				committeeLogs.Level = "Warning";
				committeeLogs.RenderedMessage = $"Committee or association not found for user: {userOfData.UserName}.";
				committeeLogs.AdditionalData = (facultyMemberEmail is null) ? $"User tried to update their committee or association data with id: {id}, but no committee or association data with this id was found in the database."
					: $"Admin: {currentUser.UserName} tried to update user: {userOfData.UserName} committee or association data with id: {id}, but no committee or association data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", committeeLogs);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						committee.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				committeeLogs.Timestamp = DateTime.Now;
				committeeLogs.Level = "Warning";
				committeeLogs.RenderedMessage = $"User unauthorized to update committee or association data.";
				committeeLogs.AdditionalData = $"User tried to update committee or association data with id: {id} that does not belong to them, committee or association data faculty member id: {committee.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", committeeLogs);
				#endregion
				throw;
			}

			var oldData = Mapper.Map<CommitteesAndAssociationsResponseDto>(committee);
			Mapper.Map(dto, committee);

			Repo.Update(committee);
			await SaveChangesAsync();

			var newData = Mapper.Map<CommitteesAndAssociationsResponseDto>(committee);
			#region Log
			committeeLogs.Timestamp = DateTime.Now;
			committeeLogs.Level = "Information";
			committeeLogs.RenderedMessage = $"Committee or association data updated for user: {userOfData.UserName}.";
			committeeLogs.AdditionalData = (facultyMemberEmail is null) ? $"User updated their committee or association data with id: {id} successfully.\nOld Data: {JsonSerializer.Serialize(oldData, jsonOptions)}\nNew Data: {JsonSerializer.Serialize(newData, jsonOptions)}."
				: $"Admin: {currentUser.UserName} updated user: {userOfData.UserName} committee or association data with id: {id} successfully.\nOld Data: {JsonSerializer.Serialize(oldData, jsonOptions)}\nNew Data: {JsonSerializer.Serialize(newData, jsonOptions)}.";
			_logger.LogInformation("{@LogDetails}", committeeLogs);
			#endregion
			return newData;
		}

		public async Task DeleteCommitteeOrAssociationAsync(
			int id,
			string? facultyMemberEmail = null)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(facultyMemberEmail);
			var committeeLogs = new LogEntry
			{
				Category = Category.FacultyMemberProjectsAndCommittees.ToString(),
				CategoryAction = CategoryAction.CommitteesAndAssociationsActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion
			var committee = await Repo.GetAsync(
				new CommitteesAndAssociationsSpecifications(id));
			if (committee is null)
			{
				#region Log
				committeeLogs.Timestamp = DateTime.Now;
				committeeLogs.Level = "Warning";
				committeeLogs.RenderedMessage = $"Committee or association not found for user: {userOfData.UserName}.";
				committeeLogs.AdditionalData = (facultyMemberEmail is null) ? $"User tried to delete their committee or association data with id: {id}, but no committee or association data with this id was found in the database."
					: $"Admin: {currentUser.UserName} tried to delete user: {userOfData.UserName} committee or association data with id: {id}, but no committee or association data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", committeeLogs);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						committee.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				committeeLogs.Timestamp = DateTime.Now;
				committeeLogs.Level = "Warning";
				committeeLogs.RenderedMessage = $"User unauthorized to delete committee or association data.";
				committeeLogs.AdditionalData = $"User tried to delete committee or association data with id: {id} that does not belong to them, committee or association data faculty member id: {committee.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", committeeLogs);
				#endregion
				throw;
			}

			committee.IsDeleted = true;

			Repo.Update(committee);
			await SaveChangesAsync();
			#region Log
			committeeLogs.Timestamp = DateTime.Now;
			committeeLogs.Level = "Information";
			committeeLogs.RenderedMessage = $"Committee or association data deleted for user: {userOfData.UserName}.";
			committeeLogs.AdditionalData = (facultyMemberEmail is null) ? $"User deleted their committee or association data with id: {id} successfully."
				: $"Admin: {currentUser.UserName} deleted user: {userOfData.UserName} committee or association data with id: {id} successfully.";
			_logger.LogInformation("{@LogDetails}", committeeLogs);
			#endregion
		}
	}
}
