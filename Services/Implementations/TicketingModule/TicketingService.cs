using Domain.Entities.AdminModule;
using Domain.Entities.IdentityModule.Users;
using Microsoft.Extensions.Logging;
using Services.Abstraction.Contracts.TicketingModule;
using Services.Global;
using Services.Helpers.TicketingModuleHelpers;
using Services.Specifications.IdnetityModuleSpecifications;
using Services.Specifications.TicketingSpecifications;
using Shared.Dtos.TicketingModule;
using Shared.Enums.Logging;
using Shared.Enums.TicketingModule;
using Shared.SpecificationParameters.TicketingModule;
using System.Net.NetworkInformation;

namespace Services.Implementations.TicketingModule
{
	public class TicketingService(IUnitOfWork unitOfWork,
	IMapper mapper,
	IAuthenticationService authenticationService,
	ILogger<TicketingService> _logger)
			: BaseService<Ticket, int>(unitOfWork, authenticationService, mapper),
			ITicketingService
	{

		#region Helpers
		private async Task<bool> CanAssignAdminToTicketAsync(Guid adminId, Domain.Enums.TicketType ticketType)
		{
			var userRepo = UnitOfWork.GetRepository<User, Guid>();

			var admin = await userRepo.GetAsync(new UserSpecifications(adminId));

			if (admin is null)
				return false;

			var requiredPermissions =
				TicketPermissionResolver.GetRequiredPermissionsForAssignment(ticketType);

			var adminPermissionCodes = PermissionHelper.GetAllPermissionCodes(admin);

			return requiredPermissions.All(code => adminPermissionCodes.Contains(code));
		}

		private async Task ChangeTicketStatusAsync(
			Ticket ticket,
			IEnumerable<Domain.Enums.TicketStatus> allowedFromStatuses,
			Domain.Enums.TicketStatus toStatus,
			string forbiddenMessage)
		{
			if (!allowedFromStatuses.Contains(ticket.Status))
				throw new ForbiddenException(forbiddenMessage);

			ticket.Status = toStatus;
			Repo.Update(ticket);
			await UnitOfWork.SaveChangesAsync();
		}

		private async Task<PaginatedResult<TicketResponseDTO>> GetTicketsByScopeAsync(
			TicketSepcificationParameters parameters,
			Guid userId,
			TicketViewScope scope)
		{
			var tickets = await Repo.GetAllAsync(
				new TicketGetByUserTypeSpecification(parameters, userId, scope));

			var count = await Repo.CountAsync(
				new TicketGetByUserTypeCountSpecification(parameters, userId, scope));


			var mapped = Mapper.Map<IEnumerable<TicketResponseDTO>>(tickets);


			return new PaginatedResult<TicketResponseDTO>(
			   parameters.PageIndex,
			   mapped.Count(),
			   count,
			   mapped);
		}
		#endregion

		protected override string EntityName => "Ticket";

		public async Task<TicketResponseDTO> AssignTicketToSupportAdminAsync(int ticketId, TicketUpdateDTO assignment)
		{
			var currentUser = await GetCurrentUserAsync();
			#region Log
			var ticketingLog = new LogEntry
			{
				Category = Category.TicketingSupport.ToString(),
				CategoryAction = CategoryAction.TicketingActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName,
			};
			#endregion

			var ticketEntity = await Repo.GetAsync(new TicketSpecifications(ticketId));
			if (ticketEntity is null)
			{
				#region Log
				ticketingLog.RenderedMessage = $"Failed to assign ticket.";
				ticketingLog.Level = "Warning";
				ticketingLog.Timestamp = DateTime.Now;
				ticketingLog.AdditionalData = $"Failed to assign ticket with id: {ticketId} because the ticket was not found.";
				_logger.LogWarning("{@LogDetails}", ticketingLog);
				#endregion
				throw NotFound();
			}

			if (!await CanAssignAdminToTicketAsync(assignment!.AssignedToId, ticketEntity.Type))
			{
				#region Log
				ticketingLog.RenderedMessage = $"Failed to assign ticket.";
				ticketingLog.Level = "Warning";
				ticketingLog.Timestamp = DateTime.Now;
				ticketingLog.AdditionalData = $"Failed to assign ticket with id: {ticketId} to admin with id: {assignment.AssignedToId} because they doesn't have the required permissions.";
				_logger.LogWarning("{@LogDetails}", ticketingLog);
				#endregion
				throw new ForbiddenException
					("Can't Assign This Ticket to this Admin As he/She don't have the required permissions");
			}

			assignment.AssignedById = currentUser.UserId;
			assignment.AssignedByUsername = currentUser.UserName;

			ticketEntity.Status = (Domain.Enums.TicketStatus)TicketStatus.InProgress;
			ticketEntity.Priority = (Domain.Enums.TicketPriority)assignment.Priority;

			Mapper.Map(assignment, ticketEntity);
			Repo.Update(ticketEntity);
			await UnitOfWork.SaveChangesAsync();

			var result = Mapper.Map<TicketResponseDTO>(ticketEntity);
			#region Log
			ticketingLog.RenderedMessage = $"Ticket assigned successfully.";
			ticketingLog.Level = "Information";
			ticketingLog.Timestamp = DateTime.Now;
			ticketingLog.AdditionalData = $"Ticket with id: {ticketId} and title: {result.Title} was assigned to admin with id: {assignment.AssignedToId} " +
				$"and username: {result.AssigneeUsername} with priority {result.Priority.ToString()} successfully.";
			_logger.LogInformation("{@LogDetails}", ticketingLog);
			#endregion
			return result;
		}

		public async Task<TicketResponseDTO> CreateTicketAsync(TicketCreateDTO ticket)
		{
			var currentUser = await GetCurrentUserAsync();
			#region Log
			var ticketingLog = new LogEntry
			{
				Category = Category.TicketingSupport.ToString(),
				CategoryAction = CategoryAction.TicketingActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName,
			};
			#endregion
			ticket.SenderId = currentUser.UserId;
			ticket.SenderUsername = currentUser.UserName;

			var ticketEntity = Mapper.Map<Ticket>(ticket);
			ticketEntity.Status = (Domain.Enums.TicketStatus)TicketStatus.Opened;
			ticketEntity.Priority = (Domain.Enums.TicketPriority)TicketPriority.Unspecified;

			await Repo.AddAsync(ticketEntity);
			await UnitOfWork.SaveChangesAsync();
			#region Log
			ticketingLog.RenderedMessage = $"Ticket created successfully.";
			ticketingLog.Level = "Information";
			ticketingLog.Timestamp = DateTime.Now;
			ticketingLog.AdditionalData = $"Ticket with id: {ticketEntity.Id} and title: {ticketEntity.Title} was created by user with username: {currentUser.UserName} successfully.";
			_logger.LogInformation("{@LogDetails}", ticketingLog);
			#endregion
			return Mapper.Map<TicketResponseDTO>(ticketEntity);
		}

		public async Task<PaginatedResult<TicketResponseDTO>> GetAllMemberTicketsAsync(TicketSepcificationParameters parameters)
		{
			var currentUser = await GetCurrentUserAsync();
			#region Log
			var ticketingLog = new LogEntry
			{
				Category = Category.TicketingSupport.ToString(),
				CategoryAction = CategoryAction.TicketingActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName,
				RenderedMessage = $"User retrieved their tickets successfully.",
				Level = "Information",
				Timestamp = DateTime.Now,
				AdditionalData = $"Tickets for user with id {currentUser.UserId} were retrieved successfully."
			};
			_logger.LogInformation("{@LogDetails}", ticketingLog);
			#endregion
			return await GetTicketsByScopeAsync(
				parameters,
				currentUser.UserId,
				TicketViewScope.Sender);
		}

		public async Task<PaginatedResult<TicketResponseDTO>> GetAllSystemTicketsAsync(TicketSepcificationParameters parameters)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var ticketLog = new LogEntry
			{
				Category = Category.TicketingSupport.ToString(),
				CategoryAction = CategoryAction.TicketingActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion

			var ticketsEntities = await Repo.GetAllAsync(
				new TicketSpecifications(parameters));
			if (ticketsEntities is null)
			{
				#region Log
				ticketLog.Timestamp = DateTime.Now;
				ticketLog.Level = "Warning";
				ticketLog.RenderedMessage = $"No tickets found in the system.";
				ticketLog.AdditionalData = $"Admin: {currentUser.UserName} tried to get all system tickets, but no tickets were found in the system.";
				_logger.LogWarning("{@LogDetails}", ticketLog);
				#endregion
				throw NotFound();
			}


			var totalCount = await Repo.CountAsync(
				new TicketCountSpecifications(parameters));

			var mapped = Mapper.Map<IEnumerable<TicketResponseDTO>>(ticketsEntities);

			#region Log
			ticketLog.Timestamp = DateTime.Now;
			ticketLog.Level = "Information";
			ticketLog.RenderedMessage = $"All system tickets retrieved successfully.";
			ticketLog.AdditionalData = $"Admin: {currentUser.UserName} retrieved all system tickets successfully. Total tickets retrieved: {totalCount}.";
			_logger.LogInformation("{@LogDetails}", ticketLog);
			#endregion

			return new PaginatedResult<TicketResponseDTO>(
				parameters.PageIndex,
				mapped.Count(),
				totalCount,
				mapped);

		}

		public async Task<IEnumerable<UserShowForAdminResponseDTO>> GetAllSuitableAdminsForTicketAsync(TicketType type)
		{
			var currentUser = await GetCurrentUserAsync();
			#region Log
			var ticketingLog = new LogEntry
			{
				Category = Category.TicketingSupport.ToString(),
				CategoryAction = CategoryAction.TicketingActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion

			var usersRepo = UnitOfWork.GetRepository<User, Guid>();

			var ticketPermissions = TicketPermissionResolver
				.GetRequiredPermissionsForAssignment((Domain.Enums.TicketType)type);

			var suitableAdmins = await usersRepo.GetAllAsync(new UserSpecifications(ticketPermissions, currentUser.UserId));
			if (suitableAdmins is null)
			{
				#region Log
				ticketingLog.Timestamp = DateTime.Now;
				ticketingLog.Level = "Warning";
				ticketingLog.RenderedMessage = $"No suitable admins found for ticket type {type}.";
				ticketingLog.AdditionalData = $"Admin: {currentUser.UserName} tried to get the suitable admins for ticket of type {type}, but no suitable admins were found.";
				_logger.LogWarning("{@LogDetails}", ticketingLog);
				#endregion
				throw new UserNotFoundException("No Suitable Admin Found For this ticket");
			}

			#region Log
			ticketingLog.Timestamp = DateTime.Now;
			ticketingLog.Level = "Information";
			ticketingLog.RenderedMessage = $"Suitable admins for ticket type {type} retrieved successfully.";
			ticketingLog.AdditionalData = $"Admin: {currentUser.UserName} retrieved the suitable admins for ticket of type {type} successfully. Total suitable admins found: {suitableAdmins.Count()}.";
			_logger.LogInformation("{@LogDetails}", ticketingLog);
			#endregion
			return Mapper.Map<IEnumerable<UserShowForAdminResponseDTO>>(suitableAdmins);
		}

		public async Task<TicketResponseDTO> RevokeTicketAsync(int ticketId)
		{
			var currentUser = await GetCurrentUserAsync();
			#region Log
			var ticketingLog = new LogEntry
			{
				Category = Category.TicketingSupport.ToString(),
				CategoryAction = CategoryAction.TicketingActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion
			var ticket = await Repo.GetAsync(new TicketSpecifications(ticketId));
			if (ticket is null)
			{
				#region Log
				ticketingLog.Timestamp = DateTime.Now;
				ticketingLog.Level = "Warning";
				ticketingLog.RenderedMessage = $"Failed to revoke ticket.";
				ticketingLog.AdditionalData = $"Failed to revoke ticket with id: {ticketId} because the ticket was not found.";
				_logger.LogWarning("{@LogDetails}", ticketingLog);
				#endregion
				throw NotFound();
			}

			try
			{
				EnsureOwnership(ticket.SenderId, currentUser.UserId, EntityName);

				await ChangeTicketStatusAsync(
					ticket,
					[Domain.Enums.TicketStatus.Opened],
					Domain.Enums.TicketStatus.WithdrawByUser,
					"You can't revoke this ticket because it is already being processed.");
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				ticketingLog.Timestamp = DateTime.Now;
				ticketingLog.Level = "Warning";
				ticketingLog.RenderedMessage = $"Failed to revoke ticket.";
				ticketingLog.AdditionalData = $"Failed to revoke ticket with id: {ticketId} because user with id: {currentUser.UserId} is not the sender of the ticket [unauthorized].";
				_logger.LogWarning("{@LogDetails}", ticketingLog);
				#endregion
				throw;
			}
			catch (ForbiddenException)
			{
				#region Log
				ticketingLog.Timestamp = DateTime.Now;
				ticketingLog.Level = "Warning";
				ticketingLog.RenderedMessage = $"Failed to revoke ticket.";
				ticketingLog.AdditionalData = $"Failed to revoke ticket with id: {ticketId} because this ticket is already being processed";
				_logger.LogWarning("{@LogDetails}", ticketingLog);
				#endregion
				throw;
			}

			#region Log
			ticketingLog.Timestamp = DateTime.Now;
			ticketingLog.Level = "Information";
			ticketingLog.RenderedMessage = $"Ticket revoked successfully.";
			ticketingLog.AdditionalData = $"Ticket with id: {ticketId} and title: {ticket.Title} was revoked by user with username: {currentUser.UserName} successfully.";
			_logger.LogInformation("{@LogDetails}", ticketingLog);
			#endregion
			return Mapper.Map<TicketResponseDTO>(ticket);
		}

		public async Task DeleteTicketAsync(int ticketId)
		{
			var currentUser = await GetCurrentUserAsync();
			#region Log
			var ticketingLog = new LogEntry
			{
				Category = Category.TicketingSupport.ToString(),
				CategoryAction = CategoryAction.TicketingActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion
			var ticket = await Repo.GetAsync(new TicketSpecifications(ticketId));
			if (ticket is null)
			{
				#region Log
				ticketingLog.Timestamp = DateTime.Now;
				ticketingLog.Level = "Warning";
				ticketingLog.RenderedMessage = $"Failed to delete ticket.";
				ticketingLog.AdditionalData = $"Failed to delete ticket with id: {ticketId} because the ticket was not found.";
				_logger.LogWarning("{@LogDetails}", ticketingLog);
				#endregion
				throw NotFound();
			}

			ticket.IsDeleted = true;
			ticket.DeletedAt = DateTime.UtcNow;
			ticket.DeletedBy = currentUser.UserId.ToString();

			#region Log
			ticketingLog.Timestamp = DateTime.Now;
			ticketingLog.Level = "Information";
			ticketingLog.RenderedMessage = $"Ticket deleted successfully.";
			ticketingLog.AdditionalData = $"Ticket with id: {ticketId} and title: {ticket.Title} was deleted by user with username: {currentUser.UserName} successfully.";
			_logger.LogInformation("{@LogDetails}", ticketingLog);
			#endregion
			await UnitOfWork.SaveChangesAsync();
		}

		public async Task<TicketResponseDTO> MarkTicketAsResolvedAsync(int ticketId)
		{
			var currentUser = await GetCurrentUserAsync();
			#region Log
			var ticketingLog = new LogEntry
			{
				Category = Category.TicketingSupport.ToString(),
				CategoryAction = CategoryAction.TicketingActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion
			var ticket = await Repo.GetAsync(new TicketSpecifications(ticketId));
			if (ticket is null)
			{
				#region Log
				ticketingLog.Timestamp = DateTime.Now;
				ticketingLog.Level = "Warning";
				ticketingLog.RenderedMessage = $"Failed to mark ticket as resolved.";
				ticketingLog.AdditionalData = $"Failed to mark ticket with id: {ticketId} as resolved because the ticket was not found.";
				_logger.LogWarning("{@LogDetails}", ticketingLog);
				#endregion
				throw NotFound();
			}

			try
			{
				EnsureOwnership(ticket.AssignedToId!.Value, currentUser.UserId, EntityName);

				await ChangeTicketStatusAsync(
					ticket,
					[Domain.Enums.TicketStatus.InProgress, Domain.Enums.TicketStatus.Reopened],
					Domain.Enums.TicketStatus.Resolved,
					"Only in-progress or reopened tickets can be marked as resolved.");
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				ticketingLog.Timestamp = DateTime.Now;
				ticketingLog.Level = "Warning";
				ticketingLog.RenderedMessage = $"Failed to mark ticket as resolved.";
				ticketingLog.AdditionalData = $"Failed to mark ticket with id: {ticketId} as resolved because the current user with id: {currentUser.UserId} is not the assignee of the ticket [unauthorized].";
				_logger.LogWarning("{@LogDetails}", ticketingLog);
				#endregion
				throw;
			}
			catch (ForbiddenException)
			{
				#region Log
				ticketingLog.Timestamp = DateTime.Now;
				ticketingLog.Level = "Warning";
				ticketingLog.RenderedMessage = $"Failed to mark ticket as resolved.";
				ticketingLog.AdditionalData = $"Failed to mark ticket with id: {ticketId} as resolved because only in-progress or reopened tickets can be marked as resolved.";
				_logger.LogWarning("{@LogDetails}", ticketingLog);
				#endregion
				throw;
			}

			#region Log
			ticketingLog.Timestamp = DateTime.Now;
			ticketingLog.Level = "Information";
			ticketingLog.RenderedMessage = $"Ticket marked as resolved successfully.";
			ticketingLog.AdditionalData = $"Ticket with id: {ticketId} and title: {ticket.Title} was marked as resolved by user with username: {currentUser.UserName} successfully.";
			_logger.LogInformation("{@LogDetails}", ticketingLog);
			#endregion
			return Mapper.Map<TicketResponseDTO>(ticket);
		}

		public async Task<TicketResponseDTO> CloseTicketAsync(int ticketId)
		{
			var currentUser = await GetCurrentUserAsync();
			#region Log
			var ticketingLog = new LogEntry
			{
				Category = Category.TicketingSupport.ToString(),
				CategoryAction = CategoryAction.TicketingActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion
			var ticket = await Repo.GetAsync(new TicketSpecifications(ticketId));
			if (ticket is null)
			{
				#region Log
				ticketingLog.Timestamp = DateTime.Now;
				ticketingLog.Level = "Warning";
				ticketingLog.RenderedMessage = $"Failed to close ticket.";
				ticketingLog.AdditionalData = $"Failed to close ticket with id: {ticketId} because the ticket was not found.";
				_logger.LogWarning("{@LogDetails}", ticketingLog);
				#endregion
				throw NotFound();
			}

			try
			{
				await ChangeTicketStatusAsync(
						ticket,
						[Domain.Enums.TicketStatus.Resolved],
						Domain.Enums.TicketStatus.Closed,
						"Only resolved tickets can be closed."
					);
			}
			catch (ForbiddenException)
			{
				#region Log
				ticketingLog.Timestamp = DateTime.Now;
				ticketingLog.Level = "Warning";
				ticketingLog.RenderedMessage = $"Failed to close ticket.";
				ticketingLog.AdditionalData = $"Failed to close ticket with id: {ticketId} because only resolved tickets can be closed.";
				_logger.LogWarning("{@LogDetails}", ticketingLog);
				#endregion
				throw;
			}

			#region Log
			ticketingLog.Timestamp = DateTime.Now;
			ticketingLog.Level = "Information";
			ticketingLog.RenderedMessage = $"Ticket closed successfully.";
			ticketingLog.AdditionalData = $"Ticket with id: {ticketId} and title: {ticket.Title} was closed by user with username: {currentUser.UserName} successfully.";
			_logger.LogInformation("{@LogDetails}", ticketingLog);
			#endregion
			return Mapper.Map<TicketResponseDTO>(ticket);
		}

		public async Task<TicketResponseDTO> ReopenTicketAsync(int ticketId)
		{
			var currentUser = await GetCurrentUserAsync();
			#region Log
			var ticketingLog = new LogEntry
			{
				Category = Category.TicketingSupport.ToString(),
				CategoryAction = CategoryAction.TicketingActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion

			var ticket = await Repo.GetAsync(new TicketSpecifications(ticketId));
			if (ticket is null)
			{
				#region Log
				ticketingLog.Timestamp = DateTime.Now;
				ticketingLog.Level = "Warning";
				ticketingLog.RenderedMessage = $"Failed to reopen ticket.";
				ticketingLog.AdditionalData = $"Failed to reopen ticket with id: {ticketId} because the ticket was not found.";
				_logger.LogWarning("{@LogDetails}", ticketingLog);
				#endregion
				throw NotFound();
			}

			try
			{
				EnsureOwnership(ticket.SenderId, currentUser.UserId, EntityName);

				await ChangeTicketStatusAsync(
					ticket,
					[Domain.Enums.TicketStatus.Resolved, Domain.Enums.TicketStatus.Closed],
					Domain.Enums.TicketStatus.Reopened,
					"Only resolved or closed tickets can be reopened."
				);
			}
			catch (UnauthorizedAccessException)
			{
				#region Log
				ticketingLog.Timestamp = DateTime.Now;
				ticketingLog.Level = "Warning";
				ticketingLog.RenderedMessage = $"Failed to reopen ticket.";
				ticketingLog.AdditionalData = $"Failed to reopen ticket with id: {ticketId} because user with id: {currentUser.UserId} is not the sender of the ticket [unauthorized].";
				_logger.LogWarning("{@LogDetails}", ticketingLog);
				#endregion
				throw;
			}
			catch (ForbiddenException)
			{
				#region Log
				ticketingLog.Timestamp = DateTime.Now;
				ticketingLog.Level = "Warning";
				ticketingLog.RenderedMessage = $"Failed to reopen ticket.";
				ticketingLog.AdditionalData = $"Failed to reopen ticket with id: {ticketId} because only resolved or closed tickets can be reopened.";
				_logger.LogWarning("{@LogDetails}", ticketingLog);
				#endregion
				throw;
			}

			#region Log
			ticketingLog.Timestamp = DateTime.Now;
			ticketingLog.Level = "Information";
			ticketingLog.RenderedMessage = $"Ticket reopened successfully.";
			ticketingLog.AdditionalData = $"Ticket with id: {ticketId} and title: {ticket.Title} was reopened by user with username: {currentUser.UserName} successfully.";
			_logger.LogInformation("{@LogDetails}", ticketingLog);
			#endregion
			return Mapper.Map<TicketResponseDTO>(ticket);
		}

		public async Task<PaginatedResult<TicketResponseDTO>> GetAllSupportAdminAssignedTicketsAsync(TicketSepcificationParameters parameters)
		{
			var currentUser = await GetCurrentUserAsync();
			#region Log
			var ticketingLog = new LogEntry
			{
				Category = Category.TicketingSupport.ToString(),
				CategoryAction = CategoryAction.TicketingActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName,
				RenderedMessage = $"Assigned tickets for support admin retrived successfully.",
				Level = "Information",
				Timestamp = DateTime.Now,
				AdditionalData = $"Assigned tickets for support admin with id: {currentUser.UserId} were retrieved successfully."
			};
			_logger.LogInformation("{@LogDetails}", ticketingLog);
			#endregion
			return await GetTicketsByScopeAsync(
				parameters,
				currentUser.UserId,
				TicketViewScope.Assignee);
		}

		public async Task<TicketResponseDTO> GetTicketByIdAsync(int ticketId)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var ticketingLog = new LogEntry
			{
				Category = Category.TicketingSupport.ToString(),
				CategoryAction = CategoryAction.TicketingActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName
			};
			#endregion
			var ticket = await Repo.GetAsync(new TicketSpecifications(ticketId));
			if (ticket is null)
			{
				#region Log
				ticketingLog.Timestamp = DateTime.Now;
				ticketingLog.Level = "Warning";
				ticketingLog.RenderedMessage = $"Failed to retrieve ticket.";
				ticketingLog.AdditionalData = $"Failed to retrieve ticket with id: {ticketId} because the ticket was not found.";
				_logger.LogWarning("{@LogDetails}", ticketingLog);
				#endregion
				throw NotFound();
			}

			#region Log
			ticketingLog.Timestamp = DateTime.Now;
			ticketingLog.Level = "Information";
			ticketingLog.RenderedMessage = $"Ticket retrieved successfully.";
			ticketingLog.AdditionalData = $"Ticket with id: {ticketId} and title: {ticket.Title} was retrieved by user with username: {currentUser.UserName} successfully.";
			_logger.LogInformation("{@LogDetails}", ticketingLog);
			#endregion
			return Mapper.Map<TicketResponseDTO>(ticket);
		}
	}
}
