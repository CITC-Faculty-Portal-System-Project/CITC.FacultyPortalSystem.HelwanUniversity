using Domain.Entities.IdentityModule.Users;
using Domain.Entities.Messaging;
using Microsoft.AspNetCore.Identity;
using Services.Abstraction.Contracts.MessagingAndChattingModule;
using Services.Abstraction.EncryptionServices;
using Services.Global;
using Services.Helpers.PaginationHelpers;
using Services.Specifications.MessagingAndChattingModule;
using Shared.Dtos.MessagingAndChattingModule;
using Shared.SpecificationParameters.MessagingAndChattingModule;

namespace Services.Implementations.MessagingAndChattingModule
{
    public class ChatService(IUnitOfWork unitOfWork,
    IMapper mapper,
    IAuthenticationService authenticationService , IMessageEncryptionService _messageEncryptionService
        , UserManager<User> userManager)
            : BaseService<Message, long>(unitOfWork, authenticationService, mapper),
            IChatService
    {

        #region Helpers

        public static void EnsureConversationParticipant(Guid userId , Conversation conversation)
        {
            if (!conversation.Participants.Any(c => c.UserId == userId))
                throw new ForbiddenException
                    ("You can't acesess this conversation as you aren't participant at it");

        }

        #endregion


        protected override string EntityName => "Message";

        public async Task<MessageResponseDTO> SendMessageAsync(
         MessageSendDTO request)
        {
            var currentUser = await GetCurrentUserAsync();
            var conversationRepo = UnitOfWork.GetRepository<Conversation, int>();

            var conversation = await conversationRepo.GetAsync(new ConverstationSpecifications(request.ConversationId))
                        ?? throw NotFound();

            EnsureConversationParticipant(currentUser.UserId, conversation);

            var targetMessageReciever = await userManager.FindByIdAsync(request.RecieverId.ToString());

            if (targetMessageReciever is null)
                throw new NotFoundException("Desired Message Reciever Wasn't Found!");

            if (request.Content == "")
            {
                request.MessageType = Shared.Enums.MessagingAndChattingModule.MessageType.Attachment;
                request.Content = "Attachment";
            }

            var encrypted = _messageEncryptionService.Encrypt(request.Content);
            encrypted.ConversationId = request.ConversationId;
            encrypted.RecieverId = request.RecieverId;
            encrypted.SenderId = currentUser.UserId;
            encrypted.SenderUsername = currentUser.UserName;
            encrypted.RecieverUsername = targetMessageReciever.UserName!;


            var entity = Mapper.Map<Message>(encrypted);

            await Repo.AddAsync(entity);
            await UnitOfWork.SaveChangesAsync();

            var response = Mapper.Map<MessageResponseDTO>(entity);
            response.Content = request.Content;

            return response;

        }

        public async Task<CursorPaginatedResult<MessageResponseDTO, long>> GetConversationMessagesAsync(
            MessageSpecificationParameters parameters)
        {

            var currentUser = await GetCurrentUserAsync();
            var conversationRepo = UnitOfWork.GetRepository<Conversation , int>();
  
            var messages = await Repo.GetAllAsync(new MessageSpecifications(parameters));
            var messagesCount = await Repo.CountAsync(new MessagesCountSpecifications(parameters));

            if(messages.Count() > 0)
                EnsureConversationParticipant(currentUser.UserId, messages.FirstOrDefault()!.Conversation!);

            var targetConverstaion = await conversationRepo.GetAsync(new ConverstationSpecifications(parameters.ConversationId))
                ?? throw NotFound();

            EnsureConversationParticipant(currentUser.UserId, targetConverstaion);


            var (orderedMessages, hasMore, nextCursor) =
                CursorPaginationHelper.ProcessCursorPagination(
                    messages.ToList(),
                    parameters.Take,
                    m => m.Id,
                    m => m.CreatedAt
                );


            var items = messages.Select(m =>
            {
                var dto = Mapper.Map<MessageResponseDTO>(m);

                dto.Content = _messageEncryptionService.Decrypt(
                    m.Ciphertext!,
                    m.Nonce!,
                    m.Tag!,
                    m.KeyVersion);

                return dto;
            });

            return new CursorPaginatedResult<MessageResponseDTO, long>
            {
                Items = items,
                HasMore = hasMore,
                NextCursor = nextCursor,
                Count = messagesCount
            };
        }

        public async Task<MessageResponseDTO> MarkMessageAsDeliveredAsync(long messageId)
        {

            var currentUser = await GetCurrentUserAsync();

            var message = await Repo.GetAsync(new MessageSpecifications(messageId))
                ?? throw NotFound();

            
            EnsureConversationParticipant(currentUser.UserId, message.Conversation!);


            message.DeleiverdAt = DateTime.UtcNow;
            Repo.Update(message);
            await UnitOfWork.SaveChangesAsync();


            var response = Mapper.Map<MessageResponseDTO>(message);
            response.Content = _messageEncryptionService.Decrypt(
                    message.Ciphertext!,
                    message.Nonce!,
                    message.Tag!,
                    message.KeyVersion);


            return response;
        }

        public async Task<MessageResponseDTO> MarkMessageAsReadAsync(long messageId)
        {

            var currentUser = await GetCurrentUserAsync();

            var message = await Repo.GetAsync(new MessageSpecifications(messageId)) ??
                throw NotFound();

            EnsureConversationParticipant(currentUser.UserId, message.Conversation!);

            message.ReadAt = DateTime.UtcNow;
            Repo.Update(message);
            await UnitOfWork.SaveChangesAsync();

            var response = Mapper.Map<MessageResponseDTO>(message);
            response.Content = _messageEncryptionService.Decrypt(
                    message.Ciphertext!,
                    message.Nonce!,
                    message.Tag!,
                    message.KeyVersion);


            return response;

        }
    }
}
