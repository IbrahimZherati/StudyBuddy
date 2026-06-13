using StudyBuddy.Shared.DTOs.MessageDTO;
using StudyBuddy.Shared.Results;

public interface IPrivateChatHub
{
    Task<Result> SendMessage(CreateMessageDTO messageDTO);
    Task<Result> ReadMessage(Guid Id);
}