using Mapster;
using StudyBuddy.Shared.DTOs.GroupMessageDTO;
using StudyBuddy.Shared.Helpers.ErrorMessages;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Domain.Entities;

public partial class GroupMessage : EntityBase<Guid>
{
     public string Text { get; private set; } = null!;
     public int GroupChatId { get; private set; }
     public int FromClientUserId { get; private set; }
     public virtual ClientUser FromClientUser { get; private set; } = null!;
     public virtual GroupChat GroupChat { get; private set; } = null!;

     private GroupMessage() { }

     public static Result<GroupMessage> Create(int clientId, CreateGroupMessageDTO groupMessageDTO)
     {
         var newGroupMessage = new GroupMessage();
         groupMessageDTO.Adapt(newGroupMessage);
        newGroupMessage.FromClientUserId = clientId;
         newGroupMessage.CreateDate = DateTime.Now;
         return Result<GroupMessage>.Success(newGroupMessage);
     }

     public Result<GroupMessage> Update(UpdateGroupMessageDTO groupMessageDTO)
     {
         groupMessageDTO.Adapt(this);
         ModifyDate = DateTime.Now;
         return Result<GroupMessage>.Success(this);
     }


 }
