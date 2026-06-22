using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using StudyBuddy.Application.Abstractions;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.NotificationDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Infrastructure.Services
{
    public class SignalRNotificationService : INotificationSendService
    {
        // Static property to hold the live context passed from the Web API layer
        private static IHubContext? _liveHubContext;
        private readonly UserManager<AppUser> userManager;
        private readonly IRepo<ClientUser> clientUserRepo;

        // This method will be called once by your Web API Program.cs at startup
        public static void Initialize(IHubContext hubContext)
        {
            _liveHubContext = hubContext;
        }

        public SignalRNotificationService(UserManager<AppUser> userManager , IRepo<ClientUser> clientUserRepo)
        {
            this.userManager = userManager;
            this.clientUserRepo = clientUserRepo;
        }

        public async Task<Result> Send(Notification notification)
        {
            try
            {
                if (_liveHubContext == null)
                {
                    return Result.Failure("SignalR Notification Service is not initialized yet.");
                }

                var notificationDTO = notification.Adapt<GetNotificationDTO>();

                // Keep your target user ID logic exactly as you have it
                int targetUserId = notificationDTO.ToClientUserId;
                var client = await clientUserRepo.GetByIdAsync(targetUserId);
                // Send using the dynamic non-generic context instance passed from Web API
                await _liveHubContext.Clients.User(client.UserId.ToString())
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
