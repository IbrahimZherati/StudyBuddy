using StudyBuddy.Shared.DTOs.GroupMessageDTO;

namespace StudyBuddy.API.Hubs.GroupChatHub
{
    public interface IGroupChatClient
    {
        Task UserJoined(string userName);
        Task UserLeft(string userName);
        Task ReceiveGroupMessage(GetGroupMessageDTO messageDTO);
        Task ReadMessage(Guid MessageId);
    }
}
