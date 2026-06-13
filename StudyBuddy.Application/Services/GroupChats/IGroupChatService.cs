using StudyBuddy.Application.DTOs.Shared;
using StudyBuddy.Shared.DTOs.GroupChatDTO;
using StudyBuddy.Shared.DTOs.MessageDTO;
using StudyBuddy.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Application.Services.GroupChats
{
    public interface IGroupChatService
    {

        Task<Result<GetGroupChatDTO>> Create(int currentId,CreateGroupChatDTO groupChatDTO);
        Task<Result<GetGroupChatDTO>> Update(int currentId ,UpdateGroupChatDTO groupChatDTO);
        Task<Result> Delete(int currentId, int id);
        Task<Result<GetGroupChatDTO>> GetGroupChatById(int id);
        Task<Result<DataResponse<GetGroupChatDTO>>> GetGroupForClient(int clientId , int skip, int take);
        Task<Result<int>> GetGroupMemberCount(int groupId);
        Task<Result> RemoveMemberFromGroupChat(int currentId, int clientId , int groupId);

    }
}
