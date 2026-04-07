using Mapster;
using Microsoft.EntityFrameworkCore;
using StudyBuddy.Application.DTOs.Shared;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.CityDTO;
using StudyBuddy.Shared.DTOs.NotificationDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Application.Services.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly IRepo<Notification> notificationRepo;
        private readonly IRepo<ClientUser> clientUserRepo;
        private readonly IRepo<NotificationType> notificationTypeRepo;

        public NotificationService(IRepo<Notification> notificationRepo,
            IRepo<ClientUser> clientUserRepo,
            IRepo<NotificationType> notificationTypeRepo)
        {
            this.notificationRepo = notificationRepo;
            this.clientUserRepo = clientUserRepo;
            this.notificationTypeRepo = notificationTypeRepo;
        }

        public async Task<Result> Create(CreateNotificationDTO notificationDTO)
        {
            if(!await clientUserRepo.ExistsAsync(c => c.Id == notificationDTO.ToClientUserId))
                return Result.Failure(Error.UserNotFound);
            if(!await clientUserRepo.ExistsAsync(c => c.Id == notificationDTO.FromClientUserId))
                return Result.Failure(Error.UserNotFound);
            if(!await notificationTypeRepo.ExistsAsync(n => n.Id == notificationDTO.NotificationTypeId))
                return Result.Failure(Error.NotificationTypeNotFound);

            var notification = new Notification();
            notificationDTO.Adapt(notification);
            notification.CreateDate = DateTime.Now;
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

        public async Task<Result> Delete(int id)
        {
            var notification = await notificationRepo.GetByIdAsync(id);
            if (notification == null)
                return Result.Failure(Error.ItemNotFound);
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

        public async Task<Result<GetNotificationDTO>> GetNotificationById(int id)
        {
            var notification = await notificationRepo.GetByIdAsync(id);
            if (notification == null)
                return Result<GetNotificationDTO>.Failure(Error.ItemNotFound);
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