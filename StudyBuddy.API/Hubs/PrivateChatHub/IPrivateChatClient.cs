using StudyBuddy.Shared.DTOs.MessageDTO;

namespace StudyBuddy.API.Hubs.PrivateChatHub
{
    public interface IPrivateChatClient
    {
        Task ReceiveMessage(GetMessageDTO message);
    }
}
