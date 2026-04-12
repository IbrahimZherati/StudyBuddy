
using StudyBuddy.Shared.DTOs.MessageDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Domain.Services.Messages
{
    public interface IMessageDomainService
    {
        Task<Result> Create(int clientId, CreateMessageDTO messageDTO);
        Task<Result> Update(int clientId, UpdateMessageDTO messageDTO);
        Task<Result> Delete(int clientId, Guid Id);
        Task<Result> GetMessageById(int clientId, Guid Id);
        Task<Result> GetMessagesForPrivateChat(int FirstClientId, int SecondClientId);
    } 
}
