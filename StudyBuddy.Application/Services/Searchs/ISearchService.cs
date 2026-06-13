using StudyBuddy.Shared.DTOs.ClientUserDTO;
using StudyBuddy.Shared.DTOs.GroupChatDTO;
using StudyBuddy.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Application.Services.Searchs
{
    public interface ISearchService
    {
        Task<Result<DataResponse<FriendInfoDTO>>> GetFriends(int clientId,int skip, int take, string? filter, bool sameMajor);
        Task<Result<DataResponse<InfoClientUserDTO>>> GetFriendRequest(int clientId ,int skip, int take, string? filter, bool sameMajor);

        Task<Result<DataResponse<JoinedGroupInfo>>> GetMyGroups(int clientId ,int skip, int take, string? filter, int? majorId);
        Task<Result<DataResponse<InfoGroupChatDTO>>> SuggestedGroups(int clientId, int skip, int take, string? filter, int? majorId);
        Task<Result<DataResponse<InfoClientUserDTO>>> SearchBuddy(int clientId, int skip, int take, string? filter, bool SameUniversity, bool SameInterest, bool SameMajor);

    }
}
