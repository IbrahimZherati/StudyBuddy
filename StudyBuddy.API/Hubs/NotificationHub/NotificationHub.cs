using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalRSwaggerGen.Attributes;
using StudyBuddy.Application.Services.Notifications;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.NotificationDTO;
using StudyBuddy.Shared.Helpers.ErrorMessages;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.API.Hubs.NotificationHub
{
    [SignalRHub]
    public class NotificationHub : Hub, INotificationHub
    {
        private readonly INotificationService notificationService;
        private readonly IRepo<ClientUser> clientUserRepo;

        public NotificationHub(INotificationService notificationService,
            IRepo<ClientUser> clientUserRepo)
        {
            this.notificationService = notificationService;
            this.clientUserRepo = clientUserRepo;
        }

        [SignalRMethod]
        public async Task<Result> Send(CreateNotificationDTO notificationDTO)
        {
            var result = await notificationService.Create(notificationDTO);
            if (!result.IsSuccess)
                return Result.Failure(result.Error ?? Error.CreateFailed);

            var toUserId = await clientUserRepo.GetQuery()
                .Where(c => c.Id == notificationDTO.ToClientUserId)
                .Select(c => c.UserId.ToString())
                .FirstOrDefaultAsync();
            if(toUserId == null) 
                return Result.Failure(Error.UserNotFound);

            await Clients.User(toUserId).SendAsync("ReceiveNotification", notificationDTO);
            return Result.Success();
        }
    }
}
