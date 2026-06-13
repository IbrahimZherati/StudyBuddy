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
    public class SearchService : ISearchService
    {
        public Task<Result<DataResponse<InfoClientUserDTO>>> GetFriendRequest(int skip, int take, string? filter, bool sameMajor)
        {
            throw new NotImplementedException();
        }

        public Task<Result<DataResponse<FriendInfoDTO>>> GetFriends(int skip, int take, string? filter, bool sameMajor)
        {
            throw new NotImplementedException();
        }

        public Task<Result<DataResponse<JoinedGroupInfo>>> GetMyGroups(int skip, int take, string? filter, string? major, string? university)
        {
            throw new NotImplementedException();
        }

        public Task<Result<DataResponse<InfoClientUserDTO>>> SearchBuddy(int skip, int take, string? filter, bool SameUniversity, bool SameInterest, bool SameMajor)
        {
            throw new NotImplementedException();
        }

        public Task<Result<DataResponse<InfoGroupChatDTO>>> SuggestedGroups(int skip, int take, string? filter, string? major, string? university)
        {
            throw new NotImplementedException();
        }
    }
}
