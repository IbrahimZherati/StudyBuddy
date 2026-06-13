
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
        private readonly IRepo<GroupMessage, Guid> groupMessageRepo;
        private readonly IRepo<GroupChat> groupChatRepo;
        private readonly IRepo<ClientUser> clientUserRepo;
        private readonly IRepo<ClientUserGroupChat> clientUserGroupChatRepo;

        public GroupMessageDomainService(IRepo<GroupMessage, Guid> groupMessageRepo
        , IRepo<GroupChat> groupChatRepo
        , IRepo<ClientUser> clientUserRepo
            , IRepo<ClientUserGroupChat> clientUserGroupChatRepo
        )
        {
            this.groupMessageRepo = groupMessageRepo;
            this.groupChatRepo = groupChatRepo;
            this.clientUserRepo = clientUserRepo;
            this.clientUserGroupChatRepo = clientUserGroupChatRepo;
        }

        public async Task<Result> Create(int clientId, CreateGroupMessageDTO groupMessageDTO)
        {

            if (!await groupChatRepo.ExistsAsync(g => g.Id == groupMessageDTO.GroupChatId))
                return Result.Failure(Error.GroupChatNotFound);


            if (!await clientUserRepo.ExistsAsync(f => f.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);


            return Result.Success();
        }

        public async Task<Result> Delete(int clientId, Guid Id)
        {
            var message = await groupMessageRepo.GetByIdAsync(Id);
            if (message == null)
                return Result.Failure(Error.MessageNotFound);

            if (!await clientUserRepo.ExistsAsync(f => f.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);

            if (message.FromClientUserId != clientId)
                return Result.Failure(Error.AccessDeniedNotOwner);

            return Result.Success();
        }

        public async Task<Result> GetGroupMessageById(int clientId, Guid Id)
        {
            var message = await groupMessageRepo.GetByIdAsync(Id);
            if (message == null)
                return Result.Failure(Error.MessageNotFound);

            if (!await clientUserRepo.ExistsAsync(f => f.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);

            if (message.FromClientUserId != clientId)
                return Result.Failure(Error.AccessDeniedNotOwner);

            return Result.Success();
        }

        public async Task<Result> GetMessagesForGroup(int clientId, int groupId)
        {
            if (!await clientUserGroupChatRepo.ExistsAsync(cg => cg.ClientUserId == clientId && cg.GroupChatId == groupId))
                return Result.Failure(Error.ClientUserNotInThisGroup);

            if (!await groupChatRepo.ExistsAsync(g => g.Id == groupId))
                return Result.Failure(Error.GroupChatNotFound);
            return Result.Success();
        }

        public async Task<Result> Read(int clientId, Guid MessageId)
        {
            var message = await groupMessageRepo.GetByIdAsync(MessageId);
            if (message == null)
                return Result.Failure(Error.MessageNotFound);

            if (!await clientUserRepo.ExistsAsync(f => f.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);

            if (message.FromClientUserId == clientId)
                return Result.Failure(Error.YouCannotReadYourOwnMessage);

            return Result.Success();

        }

        public async Task<Result> Update(int clientId, UpdateGroupMessageDTO groupMessageDTO)
        {
            if (!await groupChatRepo.ExistsAsync(g => g.Id == groupMessageDTO.GroupChatId))
                return Result.Failure(Error.GroupChatNotFound);
            var message = await groupMessageRepo.GetByIdAsync(groupMessageDTO.Id);
            if (message == null)
                return Result.Failure(Error.MessageNotFound);

            if (!await clientUserRepo.ExistsAsync(f => f.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);

            if (message.FromClientUserId != clientId)
                return Result.Failure(Error.AccessDeniedNotOwner);

            return Result.Success();
        }
    }
}
