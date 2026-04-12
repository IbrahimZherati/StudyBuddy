using Mapster;
using StudyBuddy.Shared.DTOs.FriendRequestDTO;
using StudyBuddy.Shared.Helpers.ErrorMessages;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Domain.Entities;

public partial class FriendRequest : EntityBase<int>
{
    public int FromClientUserId { get; private set; }

    public int ToClientUserId { get; private set; }

    public bool? IsAccepted { get; private set; }

    public virtual ClientUser FromClientUser { get; private set; } = null!;

    public virtual ClientUser ToClientUser { get; private set; } = null!;


    private FriendRequest() { }

    public static Result<FriendRequest> Create(CreateFriendRequestDTO friendRequestDTO)
    {
        var newFriendRequest = new FriendRequest();
        friendRequestDTO.Adapt(newFriendRequest);
        newFriendRequest.CreateDate = DateTime.Now;
        return Result<FriendRequest>.Success(newFriendRequest);
    }

    public static Result<FriendRequest> Create(int clientUserId, int requestClientUserId)
    {
        if (clientUserId == requestClientUserId)
            return Result<FriendRequest>.Failure(Error.ClientUserIdCanNotSameRequestClientUserId);
        var newRequest = new FriendRequest();
        newRequest.FromClientUserId = clientUserId;
        newRequest.ToClientUserId = requestClientUserId;
        return Result<FriendRequest>.Success(newRequest);
    }

    public Result<FriendRequest> Update(UpdateFriendRequestDTO friendRequestDTO)
    {
        friendRequestDTO.Adapt(this);
        ModifyDate = DateTime.Now;
        return Result<FriendRequest>.Success(this);
    }


}
