using Mapster;
using StudyBuddy.Shared.DTOs.FriendRequestDTO;
using StudyBuddy.Shared.Helpers.ErrorMessages;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Domain.Entities;

public partial class FriendRequest : EntityBase<int>
{
     public int ClientUserId { get; private set; }
     public int FriendId { get; private set; }
     public bool? IsAccepted { get; private set; }
     public virtual ClientUser ClientUser { get; private set; } = null!;
     public virtual ClientUser Friend { get; private set; } = null!;

     private FriendRequest() { }

     public static Result<FriendRequest> Create(CreateFriendRequestDTO friendRequestDTO)
     {
         var newFriendRequest = new FriendRequest();
         friendRequestDTO.Adapt(newFriendRequest);
         newFriendRequest.CreateDate = DateTime.Now;
         return Result<FriendRequest>.Success(newFriendRequest);
     }

     public Result<FriendRequest> Update(UpdateFriendRequestDTO friendRequestDTO)
     {
         friendRequestDTO.Adapt(this);
         ModifyDate = DateTime.Now;
         return Result<FriendRequest>.Success(this);
     }


 }
