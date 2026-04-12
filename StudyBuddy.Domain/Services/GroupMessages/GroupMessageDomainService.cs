
using StudyBuddy.Application.DTOs.Shared;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.GroupMessageDTO;
using StudyBuddy.Shared.Helpers.ErrorMessages;
using StudyBuddy.Shared.Results;
using System.Text.RegularExpressions;
namespace StudyBuddy.Domain.Services.GroupMessages
{
    public class GroupMessageDomainService : IGroupMessageDomainService
    {
        private readonly IRepo<GroupMessage> groupMessageRepo;
        private readonly IRepo<GroupChat> groupChatRepo;
        private readonly IRepo<ClientUser> clientUserRepo;


        public GroupMessageDomainService(IRepo<GroupMessage> groupMessageRepo
        ,IRepo<GroupChat> groupChatRepo
        ,IRepo<ClientUser> clientUserRepo
        )
        {
            this.groupMessageRepo = groupMessageRepo;
            this.groupChatRepo = groupChatRepo;
            this.clientUserRepo = clientUserRepo;

        }

        public async Task<Result> Create(CreateGroupMessageDTO groupMessageDTO)
        {
            
            if (!await groupChatRepo.ExistsAsync(g => g.Id == groupMessageDTO.GroupChatId))
                return Result.Failure(Error.GroupChatNotFound);


            if (!await clientUserRepo.ExistsAsync(f => f.Id == groupMessageDTO.FromClientUserId))
                return Result.Failure(Error.ClientUserNotFound);


            return Result.Success();
        }

        public async Task<Result> Delete(Guid Id)
        {
            if(!await groupMessageRepo.ExistsAsync(a => a.Id == Id))
                return Result.Failure(Error.MessageNotFound);
            return Result.Success();
        }

        public async Task<Result> GetMessagesForGroup(int groupId)
        {
            if (!await groupChatRepo.ExistsAsync(g => g.Id == groupId))
                return Result.Failure(Error.GroupChatNotFound);
            return Result.Success();
        }

        public async Task<Result> Update(UpdateGroupMessageDTO groupMessageDTO)
        { 
            if (!await groupMessageRepo.ExistsAsync(a => a.Id == groupMessageDTO.Id))
                return Result.Failure(Error.MessageNotFound);
            
            if (!await groupChatRepo.ExistsAsync(g => g.Id == groupMessageDTO.GroupChatId))
                return Result.Failure(Error.GroupChatNotFound);


            if (!await clientUserRepo.ExistsAsync(f => f.Id == groupMessageDTO.FromClientUserId))
                return Result.Failure(Error.ClientUserNotFound);


            return Result.Success();
        }
    }
}
