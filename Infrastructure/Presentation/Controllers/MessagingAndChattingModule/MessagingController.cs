using Microsoft.AspNetCore.Authorization;
using Shared;
using Shared.Dtos.MessagingAndChattingModule;
using Shared.SpecificationParameters.MessagingAndChattingModule;

namespace Presentation.Controllers.MessagingAndChattingModule
{
    [Authorize]
    public class MessagingController(IServiceManager _serviceManager) : ApiController
    {
        #region Chat

        [ProducesResponseType(typeof(MessageResponseDTO), StatusCodes.Status200OK)]
        [HttpPost("Message")]
        public async Task<ActionResult<MessageResponseDTO>> SendMessage(MessageSendDTO request)
            => Ok(await _serviceManager.ChatService.SendMessageAsync(request));

        [ProducesResponseType(typeof(CursorPaginatedResult<MessageResponseDTO, long>), StatusCodes.Status200OK)]
        [HttpGet("Conversation/{conversationId}")]
        public async Task<ActionResult<CursorPaginatedResult<MessageResponseDTO, long>>> GetConversationMessages(
            int conversationId, [FromQuery] long? cursor, [FromQuery] int take = 20)
            => Ok(await _serviceManager.ChatService.GetConversationMessagesAsync(
                new MessageSpecificationParameters
                {
                    ConversationId = conversationId,
                    BeforeMessageId = cursor,
                    Take = take
                }));

        [ProducesResponseType(typeof(MessageResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("Message/{messageId}/Delivered")]
        public async Task<ActionResult<MessageResponseDTO>> MarkMessageAsDelivered(long messageId)
            => Ok(await _serviceManager.ChatService.MarkMessageAsDeliveredAsync(messageId));

        [ProducesResponseType(typeof(MessageResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("Message/{messageId}/Read")]
        public async Task<ActionResult<MessageResponseDTO>> MarkMessageAsRead(long messageId)
            => Ok(await _serviceManager.ChatService.MarkMessageAsReadAsync(messageId));

        #endregion

        #region Conversation

        [ProducesResponseType(typeof(ConversationResponseDTO), StatusCodes.Status200OK)]
        [HttpPost("Conversation")]
        public async Task<ActionResult<ConversationResponseDTO>> CreateConversation(
            CreateConverstationDTO request)
            => Ok(await _serviceManager.ConversationService.CreateConversationAsync(request));

        [ProducesResponseType(typeof(ConversationResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("TicketConversation/{ticketId}")]
        public async Task<ActionResult<ConversationResponseDTO>> GetTicketConversation(int ticketId)
            => Ok(await _serviceManager.ConversationService.GetTicketConversationAsync(ticketId));

        #endregion
    }
}
