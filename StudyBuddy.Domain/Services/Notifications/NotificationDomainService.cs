
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.NotificationDTO;
using StudyBuddy.Shared.Results;
using StudyBuddy.Shared.Helpers.ErrorMessages;
namespace StudyBuddy.Domain.Services.Notifications
{
    public class NotificationDomainService : INotificationDomainService
    {
        private readonly IRepo<Notification> notificationRepo;
        private readonly IRepo<ClientUser> clientUserRepo;
        private readonly IRepo<NotificationType> notificationTypeRepo;


        public NotificationDomainService(IRepo<Notification> notificationRepo
        ,IRepo<ClientUser> clientUserRepo
        ,IRepo<NotificationType> notificationTypeRepo
        )
        {
            this.notificationRepo = notificationRepo;
            this.clientUserRepo = clientUserRepo;
            this.notificationTypeRepo = notificationTypeRepo;

        }

        public async Task<Result> Create(CreateNotificationDTO notificationDTO)
        {
            
            if (!await clientUserRepo.ExistsAsync(t => t.Id == notificationDTO.FromClientUserId))
                return Result.Failure(Error.ClientUserNotFound);


            if (!await clientUserRepo.ExistsAsync(f => f.Id == notificationDTO.ToClientUserId))
                return Result.Failure(Error.ClientUserNotFound);




            return Result.Success();
        }

        public async Task<Result> Delete(Guid Id)
        {
            if(!await notificationRepo.ExistsAsync(a => a.Id == Id))
                return Result.Failure(Error.NotificationNotFound);
            return Result.Success();
        }

        public async Task<Result> Update(UpdateNotificationDTO notificationDTO)
        { 
            if (!await notificationRepo.ExistsAsync(a => a.Id == notificationDTO.Id))
                return Result.Failure(Error.NotificationNotFound);
            
            if (!await clientUserRepo.ExistsAsync(t => t.Id == notificationDTO.FromClientUserId))
                return Result.Failure(Error.ClientUserNotFound);


            if (!await clientUserRepo.ExistsAsync(f => f.Id == notificationDTO.ToClientUserId))
                return Result.Failure(Error.ClientUserNotFound);


            return Result.Success();
        }
    }
}
