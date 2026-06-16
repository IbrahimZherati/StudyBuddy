using StudyBuddy.Shared.DTOs.MessageDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.API.Hubs.PrivateChatHub
{
    public interface IPrivateChatClient
    {
        Task ReceiveMessage(GetMessageDTO message);
        Task ReadMessage(Guid Id);
        Task UserDisconnect(Guid Id);
        Task UserConnect(Guid Id);

    }
}
