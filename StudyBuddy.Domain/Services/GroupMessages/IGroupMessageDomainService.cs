
using StudyBuddy.Shared.DTOs.GroupMessageDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Domain.Services.GroupMessages
{
    public interface IGroupMessageDomainService
    {
        Task<Result> Create(CreateGroupMessageDTO groupMessageDTO);
        Task<Result> Update(UpdateGroupMessageDTO groupMessageDTO);
        Task<Result> Delete(Guid Id);
        Task<Result> GetMessagesForGroup(int groupId);
    } 
}
