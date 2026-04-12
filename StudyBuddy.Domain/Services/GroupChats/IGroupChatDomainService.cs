
using StudyBuddy.Shared.DTOs.GroupChatDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Domain.Services.GroupChats
{
    public interface IGroupChatDomainService
    {
        Task<Result> Create(CreateGroupChatDTO groupChatDTO);
        Task<Result> Update(UpdateGroupChatDTO groupChatDTO);
        Task<Result> AddMemberToGroupChat(int clientId, int groupId);
        Task<Result> GetGroupMemberCount(int groupId);
        Task<Result> RemoveMemberFromGroupChat(int clientId, int groupId);
        Task<Result> Delete(int Id);
    } 
}
