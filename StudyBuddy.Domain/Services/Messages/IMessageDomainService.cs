
using StudyBuddy.Shared.DTOs.MessageDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Domain.Services.Messages
{
    public interface IMessageDomainService
    {
        Task<Result> Create(CreateMessageDTO messageDTO);
        Task<Result> Update(UpdateMessageDTO messageDTO);
        Task<Result> Delete(Guid Id);
        Task<Result> GetMessagesForPrivateChat(int FirstClientId, int SecondClientId);
    } 
}
