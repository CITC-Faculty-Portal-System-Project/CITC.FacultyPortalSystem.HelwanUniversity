using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Shared;
using Shared.Dtos.MessagingAndChattingModule;
using Shared.Hubs;
using Shared.SpecificationParameters.MessagingAndChattingModule;

namespace Presentation.Controllers.MessagingAndChattingModule
{
    [Authorize]
    public class MessagingController(IServiceManager _serviceManager , IHubContext<ChatHub> _hubContext) : ApiController
    {
        #region Chat

        [ProducesResponseType(typeof(MessageResponseDTO), StatusCodes.Status200OK)]
        [HttpPost("Message")]
        public async Task<ActionResult<MessageResponseDTO>> SendMessage(MessageSendDTO request)
        {
            var result = await _serviceManager.ChatService.SendMessageAsync(request);

            await _hubContext.Clients.Group(request.ConversationId.ToString())
               .SendAsync("ConversationUpdated", result);

            await _hubContext.Clients.User(request.RecieverId.ToString())
                .SendAsync("ReceiveMessage", result);

            return Ok(result);
        }

        [ProducesResponseType(typeof(CursorPaginatedResult<MessageResponseDTO, long>), StatusCodes.Status200OK)]
        [HttpGet("Conversation/{conversationId}")]
        public async Task<ActionResult<CursorPaginatedResult<MessageResponseDTO, long>>> GetConversationMessages(
            int conversationId, [FromQuery] long? cursor, [FromQuery] int take = 20)
        {
           var result = await _serviceManager.ChatService.GetConversationMessagesAsync(
                new MessageSpecificationParameters
                {
                    ConversationId = conversationId,
                    BeforeMessageId = cursor,
                    Take = take
                });

            return Ok(result);

        }

        [ProducesResponseType(typeof(MessageResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("Message/{messageId}/Delivered")]
        public async Task<ActionResult<MessageResponseDTO>> MarkMessageAsDelivered(long messageId)
        {
            var result = await _serviceManager.ChatService.MarkMessageAsDeliveredAsync(messageId);

            await _hubContext.Clients.Group(result.ConversationId.ToString())
               .SendAsync("MessageDelivered", result);

            return Ok(result);
        }

        [ProducesResponseType(typeof(MessageResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("Message/{messageId}/Read")]
        public async Task<ActionResult<MessageResponseDTO>> MarkMessageAsRead(long messageId)
        {
           var result = await _serviceManager.ChatService.MarkMessageAsReadAsync(messageId);

            await _hubContext.Clients.Group(result.ConversationId.ToString())
               .SendAsync("MessageRead", result);

            return Ok(result);

        }

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
