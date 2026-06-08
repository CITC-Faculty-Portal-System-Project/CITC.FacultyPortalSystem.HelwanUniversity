using Domain.Entities.AcademicDataModule.ProjectsAndCommitteesModule;
using Microsoft.AspNetCore.Mvc;
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
	public class ReviewingArticlesService(
		IUnitOfWork unitOfWork,
		IAuthenticationService authenticationService,
		IMapper mapper,
		ILogger<ReviewingArticlesService> _logger)
		: BaseService<ReviewingArticles, int>(unitOfWork, authenticationService, mapper),
		  IReviewingArticlesService
	{
		protected override string EntityName => "Reviewing Articles";

		public async Task<PaginatedResult<ReviewingArticlesDto>> GetAllReviewingArticlesAsync(
			ReviewingArticlesSpecificationsParameters parameters,
			string? facultyMemberEmail = null)
		{
			var currentUser = await GetCurrentUserAsync();
			var email = facultyMemberEmail ?? currentUser.Email;

			#region Log
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(email);
			var articlesLogs = new LogEntry
			{
				Category = Category.FacultyMemberProjectsAndCommittees.ToString(),
				CategoryAction = CategoryAction.ReviewingArticlesActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion

			var reviewingArticles = await Repo.GetAllAsync(
				new ReviewingArticlesSpecifications(parameters, email));
			if (reviewingArticles is null)
			{
				#region Log
				articlesLogs.RenderedMessage = $"Reviewing articles not found for user: {userOfData.UserName}.";
				articlesLogs.Level = "Warning";
				articlesLogs.Timestamp = DateTime.Now;
				articlesLogs.AdditionalData = (facultyMemberEmail is null) ? $"User tried to get their reviewing articles data, but no reviewing articles data was found in the database for user with email: {email}."
					: $"Admin: {currentUser.UserName} tried to get user: {userOfData.UserName} reviewing articles data, but no reviewing articles data was found in the database for user: {userOfData.UserName}";
				_logger.LogWarning("{@LogDetails}", articlesLogs);
				#endregion
				throw NotFound();
			}

			var mapped = Mapper.Map<IEnumerable<ReviewingArticlesDto>>(reviewingArticles);

			var totalCount = await Repo.CountAsync(
				new ReviewingArticlesCountSpecifications(parameters, email));

			#region Log
			articlesLogs.RenderedMessage = $"Reviewing articles data retrieved for user: {userOfData.UserName}.";
			articlesLogs.Level = "Information";
			articlesLogs.Timestamp = DateTime.Now;
			articlesLogs.AdditionalData = (facultyMemberEmail is null) ? $"User retrieved their reviewing articles data successfully, total count of reviewing articles data retrieved: {totalCount}."
				: $"Admin: {currentUser.UserName} retrieved user: {userOfData.UserName} reviewing articles data successfully, total count of reviewing articles data retrieved: {totalCount}.";
			_logger.LogInformation("{@LogDetails}", articlesLogs);
			#endregion

			return new PaginatedResult<ReviewingArticlesDto>(
				parameters.PageIndex,
				mapped.Count(),
				totalCount,
				mapped);
		}

		public async Task<ReviewingArticlesDto> GetReviewingArticleByIdAsync(
			int id,
			string? facultyMemberEmail = null)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(facultyMemberEmail);
			var articleLogs = new LogEntry
			{
				Category = Category.FacultyMemberProjectsAndCommittees.ToString(),
				CategoryAction = CategoryAction.ReviewingArticlesActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion

			var reviewingArticle = await Repo.GetAsync(
				new ReviewingArticlesSpecifications(id));
			if (reviewingArticle is null)
			{
				#region Log
				articleLogs.Timestamp = DateTime.Now;
				articleLogs.Level = "Warning";
				articleLogs.RenderedMessage = $"Reviewing article not found for user: {userOfData.UserName}.";
				articleLogs.AdditionalData = (facultyMemberEmail is null) ? $"User tried to get their reviewing article data with id: {id}, but no reviewing article data with this id was found in the database."
					: $"Admin: {currentUser.UserName} tried to get user: {userOfData.UserName} reviewing article data with id: {id}, but no reviewing article data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", articleLogs);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						reviewingArticle.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				articleLogs.Timestamp = DateTime.Now;
				articleLogs.Level = "Warning";
				articleLogs.RenderedMessage = $"User unauthorized to access reviewing article data.";
				articleLogs.AdditionalData = $"User tried to get reviewing article data with id: {id} that does not belong to them, reviewing article data faculty member id: {reviewingArticle.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", articleLogs);
				#endregion
				throw;
			}

			#region Log
			articleLogs.Timestamp = DateTime.Now;
			articleLogs.Level = "Information";
			articleLogs.RenderedMessage = $"Reviewing article data retrieved for user: {userOfData.UserName}.";
			articleLogs.AdditionalData = (facultyMemberEmail is null) ? $"User retrieved their reviewing article data with id: {id} successfully."
				: $"Admin: {currentUser.UserName} retrieved user: {userOfData.UserName} reviewing article data with id: {id} successfully.";
			_logger.LogInformation("{@LogDetails}", articleLogs);
			#endregion
			return Mapper.Map<ReviewingArticlesDto>(reviewingArticle);
		}

		public async Task<ReviewingArticlesDto> CreateReviewingArticleAsync(
			ReviewingArticleCreateDto dto,
			string? facultyMemberEmail = null)
		{
			var currentUser = await GetCurrentUserAsync();
			var email = facultyMemberEmail ?? currentUser.Email;

			#region Log
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(email);
			var articleLogs = new LogEntry
			{
				Category = Category.FacultyMemberProjectsAndCommittees.ToString(),
				CategoryAction = CategoryAction.ReviewingArticlesActions.ToString(),
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
				articleLogs.Timestamp = DateTime.Now;
				articleLogs.Level = "Warning";
				articleLogs.RenderedMessage = $"Faculty Member not found.";
				articleLogs.AdditionalData = (facultyMemberEmail is null) ? $"User tried to create a reviewing article for a faculty member that does not exist in database, no faculty member found with email: {email}."
					: $"Admin: {currentUser.UserName} tried to create a reviewing article for user: {userOfData.UserName}, but no faculty member found with email: {email}.";
				_logger.LogWarning("{@LogDetails}", articleLogs);
				#endregion
				throw;
			}

			var reviewingArticle = Mapper.Map<ReviewingArticles>(dto);
			reviewingArticle.FacultyMemberId = facultyMember.Id;

			await Repo.AddAsync(reviewingArticle);
			await SaveChangesAsync();

			var response = Mapper.Map<ReviewingArticlesDto>(reviewingArticle);
			#region Log
			articleLogs.Timestamp = DateTime.Now;
			articleLogs.Level = "Information";
			articleLogs.RenderedMessage = (facultyMemberEmail is null) ? $"User: {userOfData.UserName} created a reviewing article."
				: $"Admin: {currentUser.UserName} created a reviewing article for user: {userOfData.UserName}";
			articleLogs.AdditionalData = (facultyMemberEmail is null) ? $"User created a reviewing article with id: {response.Id} and title: {response.TitleOfArticle} successfully."
				: $"Admin: {currentUser.UserName} created a reviewing article with id: {response.Id} and title: {response.TitleOfArticle} for user: {userOfData.UserName} successfully.";
			_logger.LogInformation("{@LogDetails}", articleLogs);
			#endregion
			return response;
		}

		public async Task<ReviewingArticlesDto> UpdateReviewingArticleAsync(
			int id,
			ReviewArticleUpdateDto dto,
			string? facultyMemberEmail = null)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(facultyMemberEmail);
			var articleLogs = new LogEntry
			{
				Category = Category.FacultyMemberProjectsAndCommittees.ToString(),
				CategoryAction = CategoryAction.ReviewingArticlesActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion
			var jsonOptions = new JsonSerializerOptions
			{
				WriteIndented = true,
				Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
			};
			var reviewingArticle = await Repo.GetAsync(
				new ReviewingArticlesSpecifications(id));
			if (reviewingArticle is null)
			{
				#region Log
				articleLogs.Timestamp = DateTime.Now;
				articleLogs.Level = "Warning";
				articleLogs.RenderedMessage = $"Reviewing article not found for user: {userOfData.UserName}.";
				articleLogs.AdditionalData = (facultyMemberEmail is null) ? $"User tried to update their reviewing article data with id: {id}, but no reviewing article data with this id was found in the database."
					: $"Admin: {currentUser.UserName} tried to update user: {userOfData.UserName} reviewing article data with id: {id}, but no reviewing article data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", articleLogs);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						reviewingArticle.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				articleLogs.Timestamp = DateTime.Now;
				articleLogs.Level = "Warning";
				articleLogs.RenderedMessage = $"User unauthorized to update reviewing article data.";
				articleLogs.AdditionalData = $"User tried to update reviewing article data with id: {id} that does not belong to them, reviewing article data faculty member id: {reviewingArticle.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", articleLogs);
				#endregion
				throw;
			}

			var oldData = Mapper.Map<ReviewingArticlesDto>(reviewingArticle);
			Mapper.Map(dto, reviewingArticle);

			Repo.Update(reviewingArticle);
			await SaveChangesAsync();

			var newData = Mapper.Map<ReviewingArticlesDto>(reviewingArticle);
			#region Log
			articleLogs.Timestamp = DateTime.Now;
			articleLogs.Level = "Information";
			articleLogs.RenderedMessage = $"Reviewing article data updated for user: {userOfData.UserName}.";
			articleLogs.AdditionalData = (facultyMemberEmail is null) ? $"User updated their reviewing article data with id: {id} successfully.\nOld Data: {JsonSerializer.Serialize(oldData, jsonOptions)}\nNew Data: {JsonSerializer.Serialize(newData, jsonOptions)}."
				: $"Admin: {currentUser.UserName} updated user: {userOfData.UserName} reviewing article data with id: {id} successfully.\nOld Data: {JsonSerializer.Serialize(oldData, jsonOptions)}\nNew Data: {JsonSerializer.Serialize(newData, jsonOptions)}.";
			_logger.LogInformation("{@LogDetails}", articleLogs);
			#endregion
			return Mapper.Map<ReviewingArticlesDto>(reviewingArticle);
		}

		public async Task DeleteReviewingArticleAsync(
			int id,
			string? facultyMemberEmail = null)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var userOfData = (facultyMemberEmail is null) ? currentUser : await GetUserByEmailAsync(facultyMemberEmail);
			var articleLogs = new LogEntry
			{
				Category = Category.FacultyMemberProjectsAndCommittees.ToString(),
				CategoryAction = CategoryAction.ReviewingArticlesActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion
			var reviewingArticle = await Repo.GetAsync(
				new ReviewingArticlesSpecifications(id));
			if (reviewingArticle is null)
			{
				#region Log
				articleLogs.Timestamp = DateTime.Now;
				articleLogs.Level = "Warning";
				articleLogs.RenderedMessage = $"Reviewing article not found for user: {userOfData.UserName}.";
				articleLogs.AdditionalData = (facultyMemberEmail is null) ? $"User tried to delete their reviewing article data with id: {id}, but no reviewing article data with this id was found in the database."
					: $"Admin: {currentUser.UserName} tried to delete user: {userOfData.UserName} reviewing article data with id: {id}, but no reviewing article data with this id was found in the database.";
				_logger.LogWarning("{@LogDetails}", articleLogs);
				#endregion
				throw NotFound();
			}

			try
			{
				await EnsureOwnershipIfClientAsync(
						reviewingArticle.FacultyMemberId,
						facultyMemberEmail);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				articleLogs.Timestamp = DateTime.Now;
				articleLogs.Level = "Warning";
				articleLogs.RenderedMessage = $"User unauthorized to delete reviewing article data.";
				articleLogs.AdditionalData = $"User tried to delete reviewing article data with id: {id} that does not belong to them, reviewing article data faculty member id: {reviewingArticle.FacultyMemberId}, Logged in user id: {currentUser.UserId}.";
				_logger.LogWarning("{@LogDetails}", articleLogs);
				#endregion
				throw;
			}

			reviewingArticle.IsDeleted = true;

			Repo.Update(reviewingArticle);
			await SaveChangesAsync();
			#region Log
			articleLogs.Timestamp = DateTime.Now;
			articleLogs.Level = "Information";
			articleLogs.RenderedMessage = $"Reviewing article data deleted for user: {userOfData.UserName}.";
			articleLogs.AdditionalData = (facultyMemberEmail is null) ? $"User deleted their reviewing article data with id: {id} successfully."
				: $"Admin: {currentUser.UserName} deleted user: {userOfData.UserName} reviewing article data with id: {id} successfully.";
			_logger.LogInformation("{@LogDetails}", articleLogs);
			#endregion
		}
	}
}

