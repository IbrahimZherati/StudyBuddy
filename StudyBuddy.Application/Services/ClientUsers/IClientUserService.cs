using StudyBuddy.Shared.DTOs.ClientUserDTO;
using StudyBuddy.Shared.DTOs.FriendRequestDTO;
using StudyBuddy.Shared.DTOs.GroupChatDTO;
using StudyBuddy.Shared.DTOs.GroupInviteDTOs;
using StudyBuddy.Shared.DTOs.NotificationDTO;
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
        Task<Result<InfoClientUserDTO>> Update(int clientId, UpdateClientUserDTO clientUserDTO, string rootPath);
        Task<Result<GetProfileClientUserDTO>> GetProfile(int currentId,Guid userId);
        Task<Result<GetProfileClientUserDTO>> GetProfileByClientId(int currentId , int clientId);
        Task<Result> FriendRequest(int clientUserId, int requestClientUserId);
        Task<Result> AcceptFriendRequestByRequestId(int clientUserId, int requestId);
        Task<Result> AcceptFriendRequestByClientId(int currentId, int fromClientId);
        Task<Result<DataResponse<FriendInfoDTO>>> GetFriends(int clientUserId, int skip , int take);
        Task<Result<DataResponse<JoinedGroupInfo>>> GetGroups(int clientUserId, int skip , int take);
        Task<Result<DataResponse<GetFriendRequestDTO>>> GetFriendRequest(int clientUserId , int skip , int take);
        Task<Result<DataResponse<InfoClientUserDTO>>> GetFriendsFriends(int clientUserId, int skip , int take);
        Task<Result<DataResponse<GetGroupInviteDTO>>> GetInvitesRequest(int clientUserId , int skip , int take);
        Task<Result> AcceptGroupInviteRequest(int clientUserId, int requestId);
        Task<Result> GroupInviteRequest(int clientUserId, int requestClientUserId, int groupId);
        Task<Result<DataResponse<GetNotificationDTO>>> GetAllNotifications(int clientUserId,int skip , int take , Order orderby);
        Task<Result<DataResponse<GetNotificationDTO>>> GetFriendRequestNotifications(int clientUserId,int skip , int take , Order orderby);
        Task<Result<DataResponse<GetNotificationDTO>>> GetGroupInviteNotifications(int clientUserId,int skip , int take , Order orderby);
        Task<Result<DataResponse<GetNotificationDTO>>> GetMessageChatNotifications(int clientUserId,int skip , int take , Order orderby);
        
    }
}
