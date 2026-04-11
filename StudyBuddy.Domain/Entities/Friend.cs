using Mapster;
using StudyBuddy.Shared.DTOs.FriendDTO;
using StudyBuddy.Shared.Helpers.ErrorMessages;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Domain.Entities;

public partial class Friend : EntityBase<int>
{
     public int ClientUserId { get; private set; }
     public int FriendId { get; private set; }
     public virtual ClientUser ClientUser { get; private set; } = null!;
     public virtual ClientUser FriendNavigation { get; private set; } = null!;

     private Friend() { }

     public static Result<Friend> Create(CreateFriendDTO friendDTO)
     {
         var newFriend = new Friend();
         friendDTO.Adapt(newFriend);
         newFriend.CreateDate = DateTime.Now;
         return Result<Friend>.Success(newFriend);
     }

     public Result<Friend> Update(UpdateFriendDTO friendDTO)
     {
         friendDTO.Adapt(this);
         ModifyDate = DateTime.Now;
         return Result<Friend>.Success(this);
     }


 }
