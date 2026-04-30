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
    public class NotificationHub : Hub<INotificationClient>
    {
        private readonly INotificationService notificationService;
        private readonly IRepo<ClientUser> clientUserRepo;

        public NotificationHub(INotificationService notificationService,
            IRepo<ClientUser> clientUserRepo)
        {
            this.notificationService = notificationService;
            this.clientUserRepo = clientUserRepo;
        }

       
    }
}
