using StudyBuddy.Shared.DTOs.GroupMessageDTO;
using StudyBuddy.Shared.DTOs.MessageDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.API.Hubs.GroupChatHub
{
    public interface IGroupChatHub
    {
        Task<Result> JoinGroup(int groupId);
        Task<Result> LeaveGroup(int groupId);
        Task<Result> SendMessage(CreateGroupMessageDTO messageDTO);

        Task<Result> Read(int groupId,Guid MessageId);

    }
}
