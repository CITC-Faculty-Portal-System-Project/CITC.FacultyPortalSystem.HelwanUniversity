using Domain.Entities.AdminModule;
using Domain.Entities.IdentityModule.Users;
using Services.Abstraction.Contracts.TicketingModule;
using Services.Global;
using Services.Helpers.TicketingModuleHelpers;
using Services.Specifications.IdnetityModuleSpecifications;
using Services.Specifications.TicketingSpecifications;
using Shared.Dtos.TicketingModule;
using Shared.Enums.TicketingModule;
using Shared.SpecificationParameters.TicketingModule;

namespace Services.Implementations.TicketingModule
{
    public class TicketingService(IUnitOfWork unitOfWork,
    IMapper mapper,
    IAuthenticationService authenticationService)
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
            
            var currrentUser = await GetCurrentUserAsync();
            
            var ticketEntity = await Repo.GetAsync(new TicketSpecifications(ticketId)) 
                ?? throw NotFound();


            if (!await CanAssignAdminToTicketAsync(assignment!.AssignedToId!.Value, ticketEntity.Type))
                throw new ForbiddenException
                    ("Can't Assign This Ticket to this Admin As he/She don't have the required permissions");

            assignment.AssignedById = currrentUser.UserId;
            assignment.AssignedByUsername = currrentUser.UserName;

            ticketEntity.Status = (Domain.Enums.TicketStatus)TicketStatus.InProgress;

            Mapper.Map(assignment , ticketEntity);
            Repo.Update(ticketEntity);
            await UnitOfWork.SaveChangesAsync();

            return Mapper.Map<TicketResponseDTO>(ticketEntity);
        }

        public async Task<TicketResponseDTO> CreateTicketAsync(TicketCreateDTO ticket)
        {
            var currentUser = await GetCurrentUserAsync();
            ticket.SenderId = currentUser.UserId;
            ticket.SenderUsername = currentUser.UserName;

            var ticketEntity = Mapper.Map<Ticket>(ticket);
            ticketEntity.Status = (Domain.Enums.TicketStatus)TicketStatus.Opened;
            
            await Repo.AddAsync(ticketEntity);
            await UnitOfWork.SaveChangesAsync();

            return Mapper.Map<TicketResponseDTO>(ticketEntity);
        }

        public async Task<PaginatedResult<TicketResponseDTO>> GetAllMemberTicketsAsync(TicketSepcificationParameters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            return await GetTicketsByScopeAsync(
                parameters,
                currentUser.UserId,
                TicketViewScope.Sender);
        }

        public async Task<PaginatedResult<TicketResponseDTO>> GetAllSystemTicketsAsync(TicketSepcificationParameters parameters)
        {

            var ticketsEntities = await Repo.GetAllAsync(
                new TicketSpecifications(parameters))
                ?? throw NotFound();

            var totalCount = await Repo.CountAsync(
                new TicketCountSpecifications(parameters));

            var mapped = Mapper.Map<IEnumerable<TicketResponseDTO>>(ticketsEntities);

            return new PaginatedResult<TicketResponseDTO>(
                parameters.PageIndex,
                mapped.Count(),
                totalCount,
                mapped);

        }

        public async Task<IEnumerable<UserShowForAdminResponseDTO>> GetAllSuitableAdminsForTicketAsync(TicketType type)
        {
            
            var currentUser = await GetCurrentUserAsync();

            var usersRepo = UnitOfWork.GetRepository<User, Guid>();
            
            var ticketPermissions = TicketPermissionResolver
                .GetRequiredPermissionsForAssignment((Domain.Enums.TicketType)type);

            var suitableAdmins = await usersRepo.GetAllAsync(new UserSpecifications(ticketPermissions, currentUser.UserId))
                ?? throw new UserNotFoundException("No Suitable Admin Found For this ticket");

            return Mapper.Map<IEnumerable<UserShowForAdminResponseDTO>>(suitableAdmins);
                
        }

        public async Task<TicketResponseDTO> RevokeTicketAsync(int ticketId)
        {
            var currentUser = await GetCurrentUserAsync();

            var ticket = await Repo.GetAsync(new TicketSpecifications(ticketId))
                ?? throw NotFound();

            EnsureOwnership(ticket.SenderId, currentUser.UserId, EntityName);

            await ChangeTicketStatusAsync(
                ticket,
                [Domain.Enums.TicketStatus.Opened],
                Domain.Enums.TicketStatus.WithdrawByUser,
                "You can't revoke this ticket because it is already being processed.");

            return Mapper.Map<TicketResponseDTO>(ticket);
        }

        public async Task DeleteTicketAsync(int ticketId)
        {
            var currentUser = await GetCurrentUserAsync();

            var ticket = await Repo.GetAsync(new TicketSpecifications(ticketId)) ??
                throw NotFound();

            ticket.IsDeleted = true;
            ticket.DeletedAt = DateTime.UtcNow;
            ticket.DeletedBy = currentUser.UserId.ToString();

            await UnitOfWork.SaveChangesAsync();
        }

        public async Task<TicketResponseDTO> MarkTicketAsResolvedAsync(int ticketId)
        {
            var currentUser = await GetCurrentUserAsync();

            var ticket = await Repo.GetAsync(new TicketSpecifications(ticketId))
                ?? throw NotFound();

            EnsureOwnership(ticket.AssignedToId!.Value, currentUser.UserId, EntityName);

            await ChangeTicketStatusAsync(
                ticket,
                [Domain.Enums.TicketStatus.InProgress, Domain.Enums.TicketStatus.Reopened],
                Domain.Enums.TicketStatus.Resolved,
                "Only in-progress or reopened tickets can be marked as resolved.");

            return Mapper.Map<TicketResponseDTO>(ticket);
        }

        public async Task<TicketResponseDTO> CloseTicketAsync(int ticketId)
        {
            var currentUser = await GetCurrentUserAsync();

            var ticket = await Repo.GetAsync(new TicketSpecifications(ticketId))
                ?? throw NotFound();

            
            await ChangeTicketStatusAsync(
                ticket,
                [Domain.Enums.TicketStatus.Resolved],
                Domain.Enums.TicketStatus.Closed,
                "Only resolved tickets can be closed."
            );

            return Mapper.Map<TicketResponseDTO>(ticket);
        }

        public async Task<TicketResponseDTO> ReopenTicketAsync(int ticketId)
        {
            var currentUser = await GetCurrentUserAsync();

            var ticket = await Repo.GetAsync(new TicketSpecifications(ticketId))
                ?? throw NotFound();

            EnsureOwnership(ticket.SenderId, currentUser.UserId, EntityName);

            await ChangeTicketStatusAsync(
                ticket,
                [Domain.Enums.TicketStatus.Resolved , Domain.Enums.TicketStatus.Closed],
                Domain.Enums.TicketStatus.Reopened,
                "Only resolved or closed tickets can be reopened."
            );

            return Mapper.Map<TicketResponseDTO>(ticket);
        }

        public async Task<PaginatedResult<TicketResponseDTO>> GetAllSupportAdminAssignedTicketsAsync(TicketSepcificationParameters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            return await GetTicketsByScopeAsync(
                parameters,
                currentUser.UserId,
                TicketViewScope.Assignee);
        }

        public async Task<TicketResponseDTO> GetTicketByIdAsync(int ticketId)
        {
            var ticket = await Repo.GetAsync(new TicketSpecifications(ticketId))
                ?? throw NotFound();

            return Mapper.Map<TicketResponseDTO>(ticket);

        }
    }
}
