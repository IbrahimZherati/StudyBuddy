
using StudyBuddy.Shared.DTOs.GroupChatDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Domain.Services.GroupChats
{
    public interface IGroupChatDomainService
    {
        Task<Result> Create(int currentId, CreateGroupChatDTO groupChatDTO);
        Task<Result> Update(int currentId,UpdateGroupChatDTO groupChatDTO);
        Task<Result> AddMemberToGroupChat(int clientId, int groupId);
        Task<Result> GetGroupMemberCount(int groupId);
        Task<Result> RemoveMemberFromGroupChat(int currentId,int clientId, int groupId);
        Task<Result> Delete(int currentId,int Id);
    } 
}
