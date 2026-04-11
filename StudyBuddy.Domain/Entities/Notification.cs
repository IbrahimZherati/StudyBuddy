using Mapster;
using StudyBuddy.Shared.DTOs.NotificationDTO;
using StudyBuddy.Shared.Helpers.ErrorMessages;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Domain.Entities;

public partial class Notification : EntityBase<Guid>
{
     public int ToClientUserId { get; private set; }
     public int FromClientUserId { get; private set; }
     public string Description { get; private set; } = null!;
     public string Title { get; private set; } = null!;
     public int NotificationTypeId { get; private set; }
     public virtual ClientUser FromClientUser { get; private set; } = null!;
     public virtual NotificationType NotificationType { get; private set; } = null!;
     public virtual ClientUser ToClientUser { get; private set; } = null!;

     private Notification() { }

     public static Result<Notification> Create(CreateNotificationDTO notificationDTO)
     {
         var newNotification = new Notification();
         notificationDTO.Adapt(newNotification);
         newNotification.CreateDate = DateTime.Now;
         return Result<Notification>.Success(newNotification);
     }

     public Result<Notification> Update(UpdateNotificationDTO notificationDTO)
     {
         notificationDTO.Adapt(this);
         ModifyDate = DateTime.Now;
         return Result<Notification>.Success(this);
     }


 }
