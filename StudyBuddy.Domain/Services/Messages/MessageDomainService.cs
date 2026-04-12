
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.MessageDTO;
using StudyBuddy.Shared.Results;
using StudyBuddy.Shared.Helpers.ErrorMessages;
namespace StudyBuddy.Domain.Services.Messages
{
    public class MessageDomainService : IMessageDomainService
    {
        private readonly IRepo<Message> messageRepo;
        private readonly IRepo<ClientUser> clientUserRepo;


        public MessageDomainService(IRepo<Message> messageRepo
        ,IRepo<ClientUser> clientUserRepo
        )
        {
            this.messageRepo = messageRepo;
            this.clientUserRepo = clientUserRepo;

        }

        public async Task<Result> Create(CreateMessageDTO messageDTO)
        {
            
            if (!await clientUserRepo.ExistsAsync(t => t.Id == messageDTO.ToClientUserId))
                return Result.Failure(Error.ToClientUserNotFound);


            if (!await clientUserRepo.ExistsAsync(f => f.Id == messageDTO.FromClientUserId))
                return Result.Failure(Error.FromClientUserNotFound);


            return Result.Success();
        }

        public async Task<Result> Delete(Guid Id)
        {
            if(!await messageRepo.ExistsAsync(a => a.Id == Id))
                return Result.Failure(Error.MessageNotFound);
            return Result.Success();
        }

        public async Task<Result> GetMessagesForPrivateChat(int FirstClientId, int SecondClientId)
        {
            if(!await clientUserRepo.ExistsAsync(t => t.Id == FirstClientId))
                return Result.Failure(Error.FirstClientUserNotFound);
            if (!await clientUserRepo.ExistsAsync(c => c.Id == SecondClientId))
                return Result.Failure(Error.SecondClientUserNotFound);

            return Result.Success();
        }

        public async Task<Result> Update(UpdateMessageDTO messageDTO)
        { 
            if (!await messageRepo.ExistsAsync(a => a.Id == messageDTO.Id))
                return Result.Failure(Error.MessageNotFound);
            
            if (!await clientUserRepo.ExistsAsync(t => t.Id == messageDTO.ToClientUserId))
                return Result.Failure(Error.ToClientUserNotFound);


            if (!await clientUserRepo.ExistsAsync(f => f.Id == messageDTO.FromClientUserId))
                return Result.Failure(Error.FromClientUserNotFound);

            return Result.Success();
        }
    }
}
