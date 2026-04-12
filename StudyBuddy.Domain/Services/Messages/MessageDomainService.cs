
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.MessageDTO;
using StudyBuddy.Shared.Results;
using StudyBuddy.Shared.Helpers.ErrorMessages;
namespace StudyBuddy.Domain.Services.Messages
{
    public class MessageDomainService : IMessageDomainService
    {
        private readonly IRepo<Message, Guid> messageRepo;
        private readonly IRepo<ClientUser> clientUserRepo;


        public MessageDomainService(IRepo<Message, Guid> messageRepo
        , IRepo<ClientUser> clientUserRepo
        )
        {
            this.messageRepo = messageRepo;
            this.clientUserRepo = clientUserRepo;

        }

        public async Task<Result> Create(int clientId, CreateMessageDTO messageDTO)
        {

            if (!await clientUserRepo.ExistsAsync(t => t.Id == messageDTO.ToClientUserId))
                return Result.Failure(Error.ToClientUserNotFound);


            if (!await clientUserRepo.ExistsAsync(f => f.Id == clientId))
                return Result.Failure(Error.FromClientUserNotFound);


            return Result.Success();
        }

        public async Task<Result> Delete(int clientId, Guid Id)
        {
            var message = await messageRepo.GetByIdAsync(Id);
            if (message == null)
                return Result.Failure(Error.MessageNotFound);

            if (!await clientUserRepo.ExistsAsync(f => f.Id == clientId))
                return Result.Failure(Error.FromClientUserNotFound);

            if (message.FromClientUserId != clientId)
                return Result.Failure(Error.AccessDeniedNotOwner);

            return Result.Success();
        }

        public async Task<Result> GetMessageById(int clientId, Guid Id)
        {
            var message = await messageRepo.GetByIdAsync(Id);
            if (message == null)
                return Result.Failure(Error.MessageNotFound);

            if (!await clientUserRepo.ExistsAsync(f => f.Id == clientId))
                return Result.Failure(Error.FromClientUserNotFound);

            if (message.FromClientUserId != clientId)
                return Result.Failure(Error.AccessDeniedNotOwner);

            return Result.Success();
        }

        public async Task<Result> GetMessagesForPrivateChat(int FirstClientId, int SecondClientId)
        {
            if (!await clientUserRepo.ExistsAsync(t => t.Id == FirstClientId))
                return Result.Failure(Error.FirstClientUserNotFound);
            if (!await clientUserRepo.ExistsAsync(c => c.Id == SecondClientId))
                return Result.Failure(Error.SecondClientUserNotFound);

            return Result.Success();
        }

        public async Task<Result> Update(int clientId, UpdateMessageDTO messageDTO)
        {
            var message = await messageRepo.GetByIdAsync(messageDTO.Id);
            if (message == null)
                return Result.Failure(Error.MessageNotFound);

            if (!await clientUserRepo.ExistsAsync(f => f.Id == clientId))
                return Result.Failure(Error.FromClientUserNotFound);

            if (message.FromClientUserId != clientId)
                return Result.Failure(Error.AccessDeniedNotOwner);

            if (!await clientUserRepo.ExistsAsync(t => t.Id == messageDTO.ToClientUserId))
                return Result.Failure(Error.ToClientUserNotFound);




            return Result.Success();
        }
    }
}
