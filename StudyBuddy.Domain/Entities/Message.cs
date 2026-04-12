using Mapster;
using StudyBuddy.Shared.DTOs.MessageDTO;
using StudyBuddy.Shared.Helpers.ErrorMessages;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Domain.Entities;

public partial class Message : EntityBase<Guid>
{
     public string Text { get; private set; } = null!;
     public int ToClientUserId { get; private set; }
     public int FromClientUserId { get; private set; }
     public virtual ClientUser FromClientUser { get; private set; } = null!;
     public virtual ClientUser ToClientUser { get; private set; } = null!;

     private Message() { }

     public static Result<Message> Create(int clientId, CreateMessageDTO messageDTO)
     {
         var newMessage = new Message();
         messageDTO.Adapt(newMessage);
        newMessage.FromClientUserId = clientId;
         newMessage.CreateDate = DateTime.Now;
         return Result<Message>.Success(newMessage);
     }

     public Result<Message> Update(UpdateMessageDTO messageDTO)
     {
         messageDTO.Adapt(this);
         ModifyDate = DateTime.Now;
         return Result<Message>.Success(this);
     }


 }
