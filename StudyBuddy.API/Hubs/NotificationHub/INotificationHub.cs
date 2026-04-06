using StudyBuddy.Shared.DTOs.NotificationDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.API.Hubs.NotificationHub
{
    public interface INotificationHub
    {
        Task<Result> Send(CreateNotificationDTO notificationDTO);
    }
}
