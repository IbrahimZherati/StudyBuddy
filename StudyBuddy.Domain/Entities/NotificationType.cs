using Mapster;
using StudyBuddy.Shared.DTOs.NotificationTypeDTO;
using StudyBuddy.Shared.Helpers.ErrorMessages;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Domain.Entities;

public partial class NotificationType : EntityBase<int>
{
    public string Type { get; private set; } = null!;

    private readonly List<Notification> _notifications = new();
    public virtual IReadOnlyCollection<Notification> Notifications => _notifications;


    private NotificationType() { }

    public static Result<NotificationType> Create(CreateNotificationTypeDTO notificationTypeDTO)
    {
        var newNotificationType = new NotificationType();
        notificationTypeDTO.Adapt(newNotificationType);
        newNotificationType.CreateDate = DateTime.Now;
        return Result<NotificationType>.Success(newNotificationType);
    }

    public static NotificationType Create(string Type)
    {
        var newNotificationType = new NotificationType();
        newNotificationType.Type = Type;
        newNotificationType.CreateDate = DateTime.Now;
        return newNotificationType;
    }

    public Result<NotificationType> Update(UpdateNotificationTypeDTO notificationTypeDTO)
    {
        notificationTypeDTO.Adapt(this);
        ModifyDate = DateTime.Now;
        return Result<NotificationType>.Success(this);
    }


}
