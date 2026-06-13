// Infrastructure/Services/SignalRNotificationService.cs
using Mapster;
using Microsoft.AspNetCore.SignalR;
using StudyBuddy.Application.Abstractions;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Infrastructure.Hubs;
using StudyBuddy.Shared.DTOs.NotificationDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Infrastructure.Services
{
    public class SignalRNotificationService : INotificationSendService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public SignalRNotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task<Result> Send(Notification notification)
        {
            try
            {
                // Send directly to user by their ID (Identity handles mapping)
                var notificationDTO = notification.Adapt<GetNotificationDTO>();
                await _hubContext.Clients.User(notification.ToClientUserId.ToString())
                    .SendAsync("ReceiveNotification", notificationDTO);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"Failed to send notification: {ex.Message}");
            }
        }
    }
}