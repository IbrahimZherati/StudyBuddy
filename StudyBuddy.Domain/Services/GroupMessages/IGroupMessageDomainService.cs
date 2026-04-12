
using StudyBuddy.Shared.DTOs.GroupMessageDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Domain.Services.GroupMessages
{
    public interface IGroupMessageDomainService
    {
        Task<Result> Create(int clientId ,CreateGroupMessageDTO groupMessageDTO);
        Task<Result> Update(int clientId ,UpdateGroupMessageDTO groupMessageDTO);
        Task<Result> GetGroupMessageById(int clientId ,Guid Id);
        Task<Result> Delete(int clientId,Guid Id);
        Task<Result> GetMessagesForGroup(int clientId, int groupId);
    } 
}
