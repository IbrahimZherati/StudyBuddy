using StudyBuddy.Application.DTOs.Shared;
using StudyBuddy.Shared.DTOs.NotificationDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Application.Services.Notifications
{
    public interface INotificationService
    {
        Task<Result> Create(CreateNotificationDTO notificationDTO);
        Task<Result<GetNotificationDTO>> GetNotificationById(Guid id);
        Task<Result> Delete(Guid id);
        Task<Result<DataResponse<GetNotificationDTO>>> GetNotifications(int skip, int take , Order orderby);
    }
}