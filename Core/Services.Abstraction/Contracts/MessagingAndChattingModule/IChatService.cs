using Shared.Dtos.MessagingAndChattingModule;
using Shared.SpecificationParameters.MessagingAndChattingModule;

namespace Services.Abstraction.Contracts.MessagingAndChattingModule
{
    public interface IChatService
    {
        Task<MessageResponseDTO> SendMessageAsync(
                  MessageSendDTO request);

        Task<CursorPaginatedResult<MessageResponseDTO, long>> GetConversationMessagesAsync(
          MessageSpecificationParameters parameters);

        Task<MessageResponseDTO> MarkMessageAsDeliveredAsync(long messageId);
        Task<MessageResponseDTO> MarkMessageAsReadAsync(long messageId);

    }
}
