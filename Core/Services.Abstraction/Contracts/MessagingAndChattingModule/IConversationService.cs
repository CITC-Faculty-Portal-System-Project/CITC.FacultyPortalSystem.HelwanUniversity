using Shared.Dtos.MessagingAndChattingModule;

namespace Services.Abstraction.Contracts.MessagingAndChattingModule
{
    public interface IConversationService
    {
        Task<ConversationResponseDTO> CreateConversationAsync(CreateConverstationDTO converstation);
        Task<ConversationResponseDTO> GetTicketConversationAsync(int ticketId);

    }
}
