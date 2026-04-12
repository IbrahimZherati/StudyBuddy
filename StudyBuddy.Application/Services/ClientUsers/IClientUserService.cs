using StudyBuddy.Shared.DTOs.ClientUserDTO;
using StudyBuddy.Shared.DTOs.FriendRequestDTO;
using StudyBuddy.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Application.Services.ClientUsers
{
    public interface IClientUserService
    {
        Task<Result<InfoClientUserDTO>> Update(int clientId, UpdateClientUserDTO clientUserDTO);
        Task<Result<GetProfileClientUserDTO>> GetProfile(Guid userId);
        Task<Result> FriendReqesut(int clientUserId, int reqesutClientUserId);
        Task<Result> AcceptFriendReqesut(int clientUserId, int requestId);
        Task<Result<DataResponse<GetFriendRequestDTO>>> GetFriendRequest(int clientUserId , int skip , int take);
        
    }
}
