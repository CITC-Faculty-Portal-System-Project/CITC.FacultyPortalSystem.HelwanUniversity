using Domain.Entities.AdminModule;
using Domain.Entities.IdentityModule.Users;
using Domain.Entities.Messaging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Abstraction.Contracts.MessagingAndChattingModule;
using Services.Global;
using Services.Specifications.TicketingSpecifications;
using Shared.Dtos.MessagingAndChattingModule;

namespace Services.Implementations.MessagingAndChattingModule
{
    public class ConversationService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IAuthenticationService authenticationService,
        UserManager<User> userManager)
        : BaseService<Conversation, int>(unitOfWork, authenticationService, mapper),
          IConversationService
    {
        protected override string EntityName => "Conversation";

        #region Helpers

        private static void EnsureTicketParticipant(Guid userId, Ticket ticket)
        {
            var isSender = ticket.SenderId == userId;
            var isAssigned = ticket.AssignedToId == userId;

            if (!isSender && !isAssigned)
                throw new ForbiddenException("You can't access this ticket conversation.");
        }

        private static void ValidateConversationParticipants(
            ICollection<ConverstationParticipantsDTO>? participants,
            Ticket ticket)
        {
            if (participants is null || !participants.Any())
                return;

            var validParticipantIds = new HashSet<Guid> { ticket.SenderId };

            if (ticket.AssignedToId.HasValue)
                validParticipantIds.Add(ticket.AssignedToId.Value);

            var invalidParticipant = participants
                .FirstOrDefault(p => !validParticipantIds.Contains(p.UserId));

            if (invalidParticipant is not null)
                throw new ForbiddenException("One or more participants are not related to this ticket.");
        }

        private async Task<Ticket> GetValidatedTicketAsync(int ticketId, Guid currentUserId)
        {
            var ticketRepo = UnitOfWork.GetRepository<Ticket, int>();

            var ticket = await ticketRepo.GetAsync(new TicketSpecifications(ticketId))
                ?? throw new NotFoundException("Ticket Wasn't Found!");

            EnsureTicketParticipant(currentUserId, ticket);

            return ticket;
        }

        private async Task EnsureConversationDoesNotExistAsync(int ticketId)
        {
            var existingConversation = await Repo.GetAsync(new TicketConversationSpecifications(ticketId));

            if (existingConversation is not null)
                throw new UserAlreadyExistsException("Conversation already exists for this ticket.");
        }

        private async Task PopulateParticipantsUsernamesAsync(
            ICollection<ConverstationParticipantsDTO>? participants)
        {
            if (participants is null || !participants.Any())
                return;

            var participantIds = participants
                .Select(p => p.UserId)
                .Distinct()
                .ToList();

            var users = await userManager.Users
                .Where(u => participantIds.Contains(u.Id))
                .Select(u => new
                {
                    u.Id,
                    u.UserName
                })
                .ToListAsync();

            var usersDictionary = users.ToDictionary(u => u.Id, u => u.UserName);

            foreach (var participant in participants)
            {
                if (!usersDictionary.TryGetValue(participant.UserId, out var username) ||
                    string.IsNullOrWhiteSpace(username))
                {
                    throw new NotFoundException("Target User Wasn't Found!");
                }

                participant.Username = username;
            }
        }

        #endregion

        public async Task<ConversationResponseDTO> CreateConversationAsync(
            CreateConverstationDTO converstation)
        {
            var currentUser = await GetCurrentUserAsync();

            var ticket = await GetValidatedTicketAsync(
                converstation.TicketId!.Value,
                currentUser.UserId);

            await EnsureConversationDoesNotExistAsync(converstation.TicketId.Value);

            ValidateConversationParticipants(converstation.Participants, ticket);

            await PopulateParticipantsUsernamesAsync(converstation.Participants);

            var entity = Mapper.Map<Conversation>(converstation);

            await Repo.AddAsync(entity);
            await UnitOfWork.SaveChangesAsync();

            return Mapper.Map<ConversationResponseDTO>(entity);
        }

        public async Task<ConversationResponseDTO> GetTicketConversationAsync(int ticketId)
        {
            var currentUser = await GetCurrentUserAsync();

            await GetValidatedTicketAsync(ticketId, currentUser.UserId);

            var conversation = await Repo.GetAsync(new TicketConversationSpecifications(ticketId))
                ?? throw NotFound();

            return Mapper.Map<ConversationResponseDTO>(conversation);
        }
    }
}