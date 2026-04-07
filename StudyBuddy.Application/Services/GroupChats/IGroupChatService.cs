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

        Task<Result> Create(CreateGroupChatDTO groupChatDTO);
        Task<Result> Update(UpdateGroupChatDTO groupChatDTO);
        Task<Result> Delete(int id);
        Task<Result<GetGroupChatDTO>> GetById(int id);
        Task<Result<DataResponse<GetGroupChatDTO>>> GetGroupForClient(int clientId , int skip, int take);
        Task<Result<int>> GetGroupMemberCount(int groupId);
        Task<Result> AddMemberToGroupChat(int clientId , int groupId);
        Task<Result> RemoveMemberFromGroupChat(int clientId , int groupId);

    }
}
