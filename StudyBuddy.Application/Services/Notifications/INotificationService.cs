using StudyBuddy.Shared.DTOs.NotificationDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Application.Services.Notifications
{
    public interface INotificationService
    {
        Task<Result> Create(CreateNotificationDTO notificationDTO);
        Task<Result<GetNotificationDTO>> GetNotificationById(int id);
        Task<Result> Delete(int id);
        Task<Result<List<GetNotificationDTO>>> GetNotifications(int skip, int take , Order orderby);
    }
}