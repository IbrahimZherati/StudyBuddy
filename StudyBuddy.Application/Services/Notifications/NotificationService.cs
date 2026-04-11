using Mapster;
using Microsoft.EntityFrameworkCore;
using StudyBuddy.Application.DTOs.Shared;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Domain.Services.Notifications;
using StudyBuddy.Shared.DTOs.CityDTO;
using StudyBuddy.Shared.DTOs.NotificationDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Application.Services.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly IRepo<Notification,Guid> notificationRepo;
        private readonly IRepo<ClientUser> clientUserRepo;
        private readonly IRepo<NotificationType> notificationTypeRepo;
        private readonly INotificationDomainService notificationDomainService;

        public NotificationService(IRepo<Notification,Guid> notificationRepo,
            IRepo<ClientUser> clientUserRepo,
            IRepo<NotificationType> notificationTypeRepo,
            INotificationDomainService notificationDomainService)
        {
            this.notificationRepo = notificationRepo;
            this.clientUserRepo = clientUserRepo;
            this.notificationTypeRepo = notificationTypeRepo;
            this.notificationDomainService = notificationDomainService;
        }

        public async Task<Result> Create(CreateNotificationDTO notificationDTO)
        {
            var valid = await notificationDomainService.Create(notificationDTO);
            if (!valid.IsSuccess)
                return Result.Failure(valid.Error!);

            var result = Notification.Create(notificationDTO);

            if (!result.IsSuccess)
                return Result.Failure(result.Error!);

            if (result.Value == null)
                return Result.Failure(Error.CreateFailed);

            var notification = result.Value;
            await notificationRepo.AddAsync(notification);
            try
            {
                await notificationRepo.SaveAsync();
            }
            catch
            {
                return Result.Failure(Error.CreateFailed);
            }
            return Result.Success();
        }

        public async Task<Result> Delete(Guid id)
        {
            var valid = await notificationDomainService.Delete(id);
            if (!valid.IsSuccess)
                return Result.Failure(valid.Error!);
            var notification = await notificationRepo.GetByIdAsync(id);
            if (notification == null)
                return Result.Failure(Error.NotificationNotFound);
            notificationRepo.Remove(notification);
            try
            {
                await notificationRepo.SaveAsync();
            }
            catch
            {
                return Result.Failure(Error.DeleteFailed);
            }
            return Result.Success();
        }

        public async Task<Result<GetNotificationDTO>> GetNotificationById(Guid id)
        {
            var notification = await notificationRepo.GetByIdAsync(id);
            if (notification == null)
                return Result<GetNotificationDTO>.Failure(Error.NotificationNotFound);
            var notificationDTO = notification.Adapt<GetNotificationDTO>();
            return Result<GetNotificationDTO>.Success(notificationDTO);
        }

        public async Task<Result<DataResponse<GetNotificationDTO>>> GetNotifications(int skip, int take , Order orderby)
        {
            var result = notificationRepo.GetQuery();
                

            if(orderby == Order.Asc)
                result = result.OrderBy(n => n.CreateDate);
            else
                result = result.OrderByDescending(n => n.CreateDate);

            var query = result.ProjectToType<GetNotificationDTO>();

            var data = new DataResponse<GetNotificationDTO>();
            data.Count = await query.CountAsync();
            data.Data = await query.Skip(skip).Take(take).ToListAsync();
            return Result<DataResponse<GetNotificationDTO>>.Success(data);
        }

    }
}