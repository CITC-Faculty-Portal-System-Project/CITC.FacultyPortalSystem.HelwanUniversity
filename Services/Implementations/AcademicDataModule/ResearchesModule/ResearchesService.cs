using Domain.Entities.AcademicDataModule.ResearchesModule;
using Microsoft.Extensions.Logging;
using Services.Abstraction.Contracts.AcademicDataModule.ResearchesModule;
using Services.Global;
using Services.Helpers.CollectionSyncingHelpers;
using Services.Helpers.PaginationHelpers;
using Services.Specifications.ResearchesModule;
using Shared.Dtos.ResearchesModule;
using Shared.Enums.Logging;
using Shared.SpecificationParameters.ResearchesModule;

namespace Services.Implementations.AcademicDataModule.ResearchesModule
{
	public class ResearchesService(
	  IUnitOfWork unitOfWork,
	  IMapper mapper,
	  IAuthenticationService authenticationService,
	  ILogger<ResearchesService> _logger)
	  : BaseService<Research, int>(unitOfWork, authenticationService, mapper),
		IResearchesService
	{
		protected override string EntityName => "Researches";

		#region Helpers

		private async Task AttachUniversityContributorsAsync(
			Research entity,
			IUnitOfWork unitOfWork
			, Guid targetFacultyMemberId)
		{
			if (entity.Contributions is null || !entity.Contributions.Any())
				return;

			var personalDataRepo = unitOfWork.GetRepository<PersonalData, int>();
			var facultyMemberRepo = unitOfWork.GetRepository<FacultyMember, Guid>();

			foreach (var cont in entity.Contributions)
			{
				if (string.IsNullOrWhiteSpace(cont.MemberAcademicName))
					continue;

				var teammate = await personalDataRepo.GetAsync(
					new PersonalDataWithNameSpecification(cont.MemberAcademicName));

				if (teammate?.FacultyMember is null)
					continue;

				if (teammate.FacultyMember.Id != targetFacultyMemberId)
				{
					cont.ContributorType = Domain.Enums.ContributorType.FromUniverstity;

					teammate.FacultyMember.ResearchContributions ??= new List<ResearchContribution>();
					teammate.FacultyMember.ResearchContributions.Add(cont);
				}
			}
		}


		private async Task<bool> EnsureUniqueResearch(string? DOI)
		{
			var research = await Repo.GetAsync(new ResearchSpecifications(DOI));
			if (research is not null) return true;

			return false;
		}


		#endregion

		public async Task<ResearchResponseDTO> AddResearch(
			ResearchDTO research,
			Guid? facultyMemberId = null)
		{
			var personalDataRepo = UnitOfWork.GetRepository<PersonalData, int>();
			var facultyMemberRepo = UnitOfWork.GetRepository<FacultyMember, Guid>();

			var currentUser = await GetCurrentUserAsync();
			#region Log
			var researchLog = new LogEntry
			{
				Category = Category.FacultyMemberResearchesAndTheses.ToString(),
				CategoryAction = CategoryAction.ResearchesActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion
			var targetFacultyMemberId = facultyMemberId ?? currentUser.UserId;

			if (facultyMemberId is null)
			{
				try
				{
					EnsureOwnership(targetFacultyMemberId, currentUser.UserId, EntityName);
				}
				catch (UnauthorizedAccessException)
				{
					#region Log
					researchLog.Timestamp = DateTime.Now;
					researchLog.Level = "Warning";
					researchLog.RenderedMessage = $"User unauthorized to add a research.";
					researchLog.AdditionalData = $"User tried to add a research for faculty member with id: {targetFacultyMemberId}, Logged in user id: {currentUser.UserId}.";
					_logger.LogWarning("{@LogDetails}", researchLog);
					#endregion
					throw;
				}
			}

			if (await EnsureUniqueResearch(research.DOI))
			{
				#region Log
				researchLog.Timestamp = DateTime.Now;
				researchLog.Level = "Warning";
				researchLog.RenderedMessage = $"Research with the same DOI already exists.";
				researchLog.AdditionalData = $"User tried to add a research with DOI: {research.DOI}, which already exists in the system.";
				_logger.LogWarning("{@LogDetails}", researchLog);
				#endregion
				throw new ResearchAlreadyExistsException(research.DOI!);
			}

			var entity = Mapper.Map<Research>(research);
			entity.Source = Domain.Enums.ResearchSource.Internal;
			entity.CreatedBy = targetFacultyMemberId.ToString();

			await AttachUniversityContributorsAsync(entity, UnitOfWork, targetFacultyMemberId);

			var currentContributor = await facultyMemberRepo.GetByIdAsync(targetFacultyMemberId);
			if (currentContributor is null)
			{
				#region Log
				researchLog.Timestamp = DateTime.Now;
				researchLog.Level = "Warning";
				researchLog.RenderedMessage = $"Faculty Member not found.";
				researchLog.AdditionalData = $"User tried to add a research for a faculty member that does not exist in database, no faculty member found with email : {currentUser.Email}.";
				_logger.LogWarning("{@LogDetails}", researchLog);
				#endregion
				throw new NotFoundException("Faculty Member is Not Found.");
			}

			currentContributor.ResearchContributions ??= new List<ResearchContribution>();

			currentContributor.ResearchContributions.Add(new ResearchContribution
			{
				Contributor = currentContributor,
				Research = entity,
				MemberAcademicName = currentContributor.PersonalData?.NameInComposition
									 ?? currentContributor.PersonalData?.NameEn ?? currentContributor.PersonalData?.NameAr
									 ?? currentContributor.Name,
				IsConfirmed = true,
				IsTheMajorResearcher = true,
			});

			await Repo.AddAsync(entity);
			await SaveChangesAsync();

			var response = Mapper.Map<ResearchResponseDTO>(entity);
			#region Log
			researchLog.Timestamp = DateTime.Now;
			researchLog.Level = "Information";
			researchLog.RenderedMessage = $"User: {currentUser.UserName} added a research.";
			researchLog.AdditionalData = $"User added a research with id: {response.Id} and DOI: {response.DOI} successfully.";
			_logger.LogInformation("{@LogDetails}", researchLog);
			#endregion
			return response;
		}

		public async Task<ResearchResponseDTO> ConfirmRecommendedResearch(
			int researchId,
			Guid? facultyMemberId = null)
		{
			var currentUser = await GetCurrentUserAsync();
			var targetFacultyMemberId = facultyMemberId ?? currentUser.UserId;
			#region Log
			var researchLog = new LogEntry
			{
				Category = Category.FacultyMemberResearchesAndTheses.ToString(),
				CategoryAction = CategoryAction.ResearchesActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion
			var researchEntity = await Repo.GetAsync(
				new RecommendedResearchesSpecifications(researchId, targetFacultyMemberId));
			if (researchEntity is null)
			{
				#region Log
				researchLog.Timestamp = DateTime.Now;
				researchLog.Level = "Warning";
				researchLog.RenderedMessage = $"Recommended research not found for user: {currentUser.UserName}.";
				researchLog.AdditionalData = $"User tried to confirm a recommended research, but no recommended research with id : {researchId} found for user with id: {targetFacultyMemberId}.";
				_logger.LogWarning("{@LogDetails}", researchLog);
				#endregion
				throw NotFound();
			}

			if ((targetFacultyMemberId == currentUser.UserId)
				&& (!researchEntity.Contributions!.Any(c => c.ContributorId == targetFacultyMemberId)))
			{
				#region Log
				researchLog.Timestamp = DateTime.Now;
				researchLog.Level = "Warning";
				researchLog.RenderedMessage = $"User unauthorized to confirm this research.";
				researchLog.AdditionalData = $"User tried to confirm a recommended research with id: {researchId}, but the user of id: {targetFacultyMemberId} is not a contributor in this research.";
				_logger.LogWarning("{@LogDetails}", researchLog);
				#endregion
				throw new UnauthorizedException("You Can't Modify this research!");
			}

			researchEntity.Contributions!
				.SingleOrDefault(c => c.ContributorId == targetFacultyMemberId)!.IsConfirmed = true;

			Repo.Update(researchEntity);
			await SaveChangesAsync();

			var response = Mapper.Map<ResearchResponseDTO>(researchEntity);
			#region Log
			researchLog.Timestamp = DateTime.Now;
			researchLog.Level = "Information";
			researchLog.RenderedMessage = $"User: {currentUser.UserName} confirmed a recommended research.";
			researchLog.AdditionalData = $"User confirmed a recommended research with id: {response.Id} and DOI: {response.DOI} successfully.";
			_logger.LogInformation("{@LogDetails}", researchLog);
			#endregion
			return response;
		}

		public async Task DeleteResearch(
			int researchId,
			Guid? facultyMemberId = null)
		{
			var currentUser = await GetCurrentUserAsync();
			var targetFacultyMemberId = facultyMemberId ?? currentUser.UserId;

			#region Log
			var researchLog = new LogEntry
			{
				Category = Category.FacultyMemberResearchesAndTheses.ToString(),
				CategoryAction = CategoryAction.ResearchesActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion

			var researchEntity = await Repo.GetAsync(
				new ResearchSpecifications(researchId, targetFacultyMemberId));

			if (researchEntity is null)
			{
				#region Log
				researchLog.Timestamp = DateTime.Now;
				researchLog.Level = "Warning";
				researchLog.RenderedMessage = $"Research not found for user: {currentUser.UserName}.";
				researchLog.AdditionalData = $"User tried to delete research, but no research with id : {researchId} found for user with id: {targetFacultyMemberId}.";
				_logger.LogWarning("{@LogDetails}", researchLog);
				#endregion
				throw NotFound();
			}

			if ((targetFacultyMemberId == currentUser.UserId)
				&& (!researchEntity.Contributions!.Any(c => c.ContributorId == targetFacultyMemberId)))
			{
				#region Log
				researchLog.Timestamp = DateTime.Now;
				researchLog.Level = "Warning";
				researchLog.RenderedMessage = $"User unauthorized to delete this research.";
				researchLog.AdditionalData = $"User tried to delete a research with id: {researchId}, but the user of id: {targetFacultyMemberId} is not a contributor in this research.";
				_logger.LogWarning("{@LogDetails}", researchLog);
				#endregion
				throw new UnauthorizedException("You Can't Modify this research!");
			}

			var researcherContribution = researchEntity.Contributions!
				.FirstOrDefault(c => c.ContributorId == targetFacultyMemberId);

			researcherContribution!.IsDeleted = true;
			researcherContribution.DeletedAt = DateTime.Now;
			researcherContribution.DeletedBy = currentUser.UserName;

			Repo.Update(researchEntity);
			await SaveChangesAsync();
			#region Log
			researchLog.Timestamp = DateTime.Now;
			researchLog.Level = "Information";
			researchLog.RenderedMessage = $"Research deleted for user: {currentUser.UserName}.";
			researchLog.AdditionalData = $"User deleted a research with id: {researchEntity.Id} and DOI: {researchEntity.DOI} successfully.";
			_logger.LogInformation("{@LogDetails}", researchLog);
			#endregion
		}

		public async Task<CursorPaginatedResult<ResearchResponseDTO, int>> GetAllRecommendedResearches(
			ResearchCursoredPaginationSpecificationParameters parameters,
			Guid? facultyMemberId = null)
		{
			var currentUser = await GetCurrentUserAsync();
			var targetFacultyMemberId = facultyMemberId ?? currentUser.UserId;

			#region Log
			var researchLog = new LogEntry
			{
				Category = Category.FacultyMemberResearchesAndTheses.ToString(),
				CategoryAction = CategoryAction.ResearchesActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion

			var recommendedResearchesEntities = await Repo.GetAllAsync(
				new RecommendedResearchesSpecifications(parameters, targetFacultyMemberId));
			if (recommendedResearchesEntities is null)
			{
				#region Log
				researchLog.Timestamp = DateTime.Now;
				researchLog.Level = "Warning";
				researchLog.RenderedMessage = $"Recommended researches not found for user: {currentUser.UserName}.";
				researchLog.AdditionalData = $"User tried to get their recommended researches, but no recommended researches found for user with id: {targetFacultyMemberId}.";
				_logger.LogWarning("{@LogDetails}", researchLog);
				#endregion
				throw NotFound();
			}

			var totalCount = await Repo.CountAsync(
				new RecommendedResearchesCountSpecifications(parameters, targetFacultyMemberId));

			var (orderedResearches, hasMore, nextCursor) =
				CursorPaginationHelper.ProcessCursorPagination(
					recommendedResearchesEntities.ToList(),
					parameters.Take,
					m => m.Id,
					m => m.CreatedAt
				);

			var mapped = Mapper.Map<IEnumerable<ResearchResponseDTO>>(recommendedResearchesEntities);

			#region Log
			researchLog.RenderedMessage = $"Recommended researches retrieved for user: {currentUser.UserName}.";
			researchLog.Level = "Information";
			researchLog.Timestamp = DateTime.Now;
			researchLog.AdditionalData = $"User retrieved their recommended researches successfully, total count of recommended researches retrieved: {totalCount}.";
			_logger.LogInformation("{@LogDetails}", researchLog);
			#endregion

			return new CursorPaginatedResult<ResearchResponseDTO, int>
			{
				Items = mapped,
				HasMore = hasMore,
				NextCursor = nextCursor,
				Count = totalCount
			};
		}

		public async Task<PaginatedResult<ResearchResponseDTO>> GetAllResearches(
			ResearchSpecificationParameters parameters,
			Guid? facultyMemberId = null)
		{
			var currentUser = await GetCurrentUserAsync();
			var targetFacultyMemberId = facultyMemberId ?? currentUser.UserId;

			#region Log
			var researchLog = new LogEntry
			{
				Category = Category.FacultyMemberResearchesAndTheses.ToString(),
				CategoryAction = CategoryAction.ResearchesActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion

			var researchesEntities = await Repo.GetAllAsync(
				new ResearchSpecifications(parameters, targetFacultyMemberId));
			if (researchesEntities is null)
			{
				#region Log
				researchLog.Timestamp = DateTime.Now;
				researchLog.Level = "Warning";
				researchLog.RenderedMessage = $"User researches not found for user: {currentUser.UserName}.";
				researchLog.AdditionalData = $"User tried to get their researches, but no researches found for user with id: {targetFacultyMemberId}.";
				_logger.LogWarning("{@LogDetails}", researchLog);
				#endregion
				throw NotFound();
			}

			var totalCount = await Repo.CountAsync(
				new ResearchCountSpecifications(parameters, targetFacultyMemberId));

			var mapped = Mapper.Map<IEnumerable<ResearchResponseDTO>>(researchesEntities);

			#region Log
			researchLog.RenderedMessage = $"Researches retrieved for user: {currentUser.UserName}.";
			researchLog.Level = "Information";
			researchLog.Timestamp = DateTime.Now;
			researchLog.AdditionalData = $"User retrieved their researches successfully, total count of researches retrieved: {totalCount}.";
			_logger.LogInformation("{@LogDetails}", researchLog);
			#endregion
			return new PaginatedResult<ResearchResponseDTO>(
				parameters.PageIndex,
				mapped.Count(),
				totalCount,
				mapped);
		}

		public async Task<ResearchResponseDTO> GetResarchById(
			int researchId,
			Guid? facultyMemberId = null)
		{
			var currentUser = await GetCurrentUserAsync();
			var targetFacultyMemberId = facultyMemberId ?? currentUser.UserId;

			#region Log
			var researchLog = new LogEntry
			{
				Category = Category.FacultyMemberResearchesAndTheses.ToString(),
				CategoryAction = CategoryAction.ResearchesActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion

			var research = await Repo.GetAsync(
				new ResearchSpecifications(researchId, targetFacultyMemberId));
			if (research is null)
			{
				#region Log
				researchLog.Timestamp = DateTime.Now;
				researchLog.Level = "Warning";
				researchLog.RenderedMessage = $"Research not found for user: {currentUser.UserName}.";
				researchLog.AdditionalData = $"User tried to get research with id: {researchId}, but no research with this id was found for user with id: {targetFacultyMemberId}.";
				_logger.LogWarning("{@LogDetails}", researchLog);
				#endregion
				throw NotFound();
			}

			#region Log
			researchLog.Timestamp = DateTime.Now;
			researchLog.Level = "Information";
			researchLog.RenderedMessage = $"Research retrieved for user: {currentUser.UserName}.";
			researchLog.AdditionalData = $"User retrieved research with id: {researchId} successfully.";
			_logger.LogInformation("{@LogDetails}", researchLog);
			#endregion
			return Mapper.Map<ResearchResponseDTO>(research);
		}

		public async Task<ResearchResponseDTO> GetResearchByTitle(
			string title,
			Guid? facultyMemberId = null)
		{
			var currentUser = await GetCurrentUserAsync();
			var targetFacultyMemberId = facultyMemberId ?? currentUser.UserId;

			#region Log
			var researchLog = new LogEntry
			{
				Category = Category.FacultyMemberResearchesAndTheses.ToString(),
				CategoryAction = CategoryAction.ResearchesActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion

			var researchEntity = await Repo.GetAsync(
				new ResearchSpecifications(title, targetFacultyMemberId));
			if (researchEntity is null)
			{
				#region Log
				researchLog.Timestamp = DateTime.Now;
				researchLog.Level = "Warning";
				researchLog.RenderedMessage = $"Research not found for user: {currentUser.UserName}.";
				researchLog.AdditionalData = $"User tried to get research with title: {title}, but no research with this title was found for user with id: {targetFacultyMemberId}.";
				_logger.LogWarning("{@LogDetails}", researchLog);
				#endregion
				throw NotFound();
			}

			#region Log
			researchLog.Timestamp = DateTime.Now;
			researchLog.Level = "Information";
			researchLog.RenderedMessage = $"Research retrieved for user: {currentUser.UserName}.";
			researchLog.AdditionalData = $"User retrieved research with title: {title} successfully.";
			_logger.LogInformation("{@LogDetails}", researchLog);
			#endregion
			return Mapper.Map<ResearchResponseDTO>(researchEntity);
		}

		public async Task RejectResearch(
			int researchId,
			Guid? facultyMemberId = null)
		{
			var currentUser = await GetCurrentUserAsync();
			var targetFacultyMemberId = facultyMemberId ?? currentUser.UserId;

			#region Log
			var researchLog = new LogEntry
			{
				Category = Category.FacultyMemberResearchesAndTheses.ToString(),
				CategoryAction = CategoryAction.ResearchesActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion

			var researchEntity = await Repo.GetAsync(
				new RecommendedResearchesSpecifications(researchId, targetFacultyMemberId));
			if (researchEntity is null)
			{
				#region Log
				researchLog.Timestamp = DateTime.Now;
				researchLog.Level = "Warning";
				researchLog.RenderedMessage = $"Recommended research not found for user: {currentUser.UserName}.";
				researchLog.AdditionalData = $"User tried to reject a recommended research, but no recommended research with id : {researchId} found for user with id: {targetFacultyMemberId}.";
				_logger.LogWarning("{@LogDetails}", researchLog);
				#endregion
				throw NotFound();
			}

			if ((targetFacultyMemberId == currentUser.UserId)
				&& (!researchEntity.Contributions!.Any(c => c.ContributorId == targetFacultyMemberId)))
			{
				#region Log
				researchLog.Timestamp = DateTime.Now;
				researchLog.Level = "Warning";
				researchLog.RenderedMessage = $"User unauthorized to reject this research.";
				researchLog.AdditionalData = $"User tried to reject a recommended research with id: {researchId}, but the user of id: {targetFacultyMemberId} is not a contributor in this research.";
				_logger.LogWarning("{@LogDetails}", researchLog);
				#endregion
				throw new UnauthorizedException("You Can't Modify this research!");
			}

			researchEntity.Contributions!
				.SingleOrDefault(c => c.ContributorId == targetFacultyMemberId)!.IsDeleted = true;

			Repo.Update(researchEntity);
			await SaveChangesAsync();
			#region Log
			researchLog.Timestamp = DateTime.Now;
			researchLog.Level = "Information";
			researchLog.RenderedMessage = $"User: {currentUser.UserName} rejected a recommended research.";
			researchLog.AdditionalData = $"User rejected a recommended research with id: {researchId} and DOI: {researchEntity.DOI} successfully.";
			_logger.LogInformation("{@LogDetails}", researchLog);
			#endregion
		}

		public async Task<ResearchResponseDTO> UpdateResearch(
			int researchId,
			ResearchUpdateDTO researchUpdate,
			Guid? facultyMemberId = null)
		{
			var currentUser = await GetCurrentUserAsync();
			var targetFacultyMemberId = facultyMemberId ?? currentUser.UserId;

			#region Log
			var researchLog = new LogEntry
			{
				Category = Category.FacultyMemberResearchesAndTheses.ToString(),
				CategoryAction = CategoryAction.ResearchesActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion

			var researchEntity = await Repo.GetAsync(
				new ResearchSpecifications(researchId, targetFacultyMemberId));
			if (researchEntity is null)
			{
				#region Log
				researchLog.Timestamp = DateTime.Now;
				researchLog.Level = "Warning";
				researchLog.RenderedMessage = $"Research not found for user: {currentUser.UserName}.";
				researchLog.AdditionalData = $"User tried to update a research, but no research with id: {researchId} found for user with id: {targetFacultyMemberId}.";
				_logger.LogWarning("{@LogDetails}", researchLog);
				#endregion
				throw NotFound();
			}

			if (researchEntity.Contributions!
				.Any(c => c.ContributorId == targetFacultyMemberId && c.IsTheMajorResearcher == false))
			{
				#region Log
				researchLog.Timestamp = DateTime.Now;
				researchLog.Level = "Warning";
				researchLog.RenderedMessage = $"User unauthorized to update this research.";
				researchLog.AdditionalData = $"User tried to update a research with id: {researchId}, but the user of id: {targetFacultyMemberId} is not a contributor in this research.";
				_logger.LogWarning("{@LogDetails}", researchLog);
				#endregion
				throw new ForbiddenException("You Can't Modify this research data as you aren't a major researcher!");
			}

			try
			{
				CollectionSync.Sync<
						ResearchContribution,
						ResearchContributionDTO,
						ResearchContributionDTO,
						ResearchContributionResponseDTO,
						int
					>(
						current: researchEntity.Contributions!,
						toAdd: researchUpdate.ResearchContributionsToAdd,
						toUpdate: researchUpdate.ResearchContributionsToUpdate,
						toDelete: researchUpdate.ResearchContributionsToDelete,

						childKey: rc => rc.Id,
						deleteKey: d => d.Id,

						mapAdd: d => Mapper.Map<ResearchContribution>(d),

						mapUpdate: (dto, entity) =>
						{
							if (entity.IsConfirmed)
								throw new ForbiddenException("Confirmed contribution can't be updated");

							if (entity.ContributorId.ToString() == researchEntity.CreatedBy)
								throw new ForbiddenException("You can't modify the creator contribution");

							Mapper.Map(dto, entity);
						},

						onDelete: e =>
						{
							if (e.IsConfirmed)
								throw new ForbiddenException("Confirmed contribution can't be deleted");

							if (e.ContributorId.ToString() == researchEntity.CreatedBy)
								throw new ForbiddenException("You Can't Delete this Contributor as he/she is the creator of the research");

							e.IsDeleted = true;
						},

						onUpdateNotFound: id =>
							throw new NotFoundException("ResearchContribution not found"),

						onDeleteNotFound: id =>
							throw new NotFoundException("ResearchContribution not found for delete")
					);
			}
			catch (Exception ex)
			{
				#region Log
				researchLog.Timestamp = DateTime.Now;
				researchLog.Level = "Warning";
				researchLog.RenderedMessage = $"Failed to update research with id: {researchId}";
				researchLog.AdditionalData = $"{ex.Message}";
				researchLog.Exception = ex.ToString();
				_logger.LogWarning("{@LogDetails}", researchLog);
				#endregion
				throw;
			}

			Mapper.Map(researchUpdate, researchEntity);

			await AttachUniversityContributorsAsync(researchEntity, UnitOfWork, targetFacultyMemberId);

			Repo.Update(researchEntity);
			await SaveChangesAsync();

			#region Log
			researchLog.Timestamp = DateTime.Now;
			researchLog.Level = "Information";
			researchLog.RenderedMessage = $"Research updated for user: {currentUser.UserName}.";
			researchLog.AdditionalData = $"User updated a research with id: {researchId} and DOI: {researchEntity.DOI} successfully.";
			_logger.LogInformation("{@LogDetails}", researchLog);
			#endregion
			return Mapper.Map<ResearchResponseDTO>(researchEntity);
		}
	}
}