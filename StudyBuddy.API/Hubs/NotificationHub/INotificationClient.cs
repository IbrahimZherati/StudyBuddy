using StudyBuddy.Shared.DTOs.NotificationDTO;

namespace StudyBuddy.API.Hubs.NotificationHub;

public interface INotificationClient
{
    Task ReceiveNotification(CreateNotificationDTO notification);
}