
using StudyBuddy.Shared.DTOs.NotificationDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Domain.Services.Notifications
{
    public interface INotificationDomainService
    {
        Task<Result> Create(CreateNotificationDTO notificationDTO);
        Task<Result> Update(UpdateNotificationDTO notificationDTO);
        Task<Result> Delete(Guid Id);
    } 
}
