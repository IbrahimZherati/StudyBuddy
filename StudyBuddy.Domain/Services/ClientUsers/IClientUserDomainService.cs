
using StudyBuddy.Shared.DTOs.ClientUserDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Domain.Services.ClientUsers
{
    public interface IClientUserDomainService
    {
        Task<Result> Update(int clientId, UpdateClientUserDTO clientUserDTO);
        Task<Result> FriendReqesut(int clientUserId, int requestClientUserId);
        Task<Result> AcceptFriendReqesutByRequestId(int clientUserId ,int requestId);
        Task<Result> AcceptFriendReqesutByClientId(int currentId , int fromClientId);
        Task<Result> RejectFriendReqesutByRequestId(int clientUserId ,int requestId);
        Task<Result> RejectFriendReqesutByClientId(int currentId , int fromClientId);
        Task<Result> CancelFriendReqesutByRequestId(int clientUserId ,int requestId);
        Task<Result> CancelFriendReqesutByClientId(int currentId , int toClientId);
        Task<Result> AcceptGroupInviteReqesut(int clientUserId ,int requestId);
        Task<Result> GroupInviteReqesut(int clientUserId, int requestClientUserId);
    } 
}
