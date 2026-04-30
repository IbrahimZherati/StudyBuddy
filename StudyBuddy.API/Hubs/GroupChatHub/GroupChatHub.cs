using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalRSwaggerGen.Attributes;
using StudyBuddy.Application.Services.GroupMessages;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.GroupMessageDTO;
using StudyBuddy.Shared.Helpers.ErrorMessages;
using StudyBuddy.Shared.Results;
using System.Collections.Concurrent;

namespace StudyBuddy.API.Hubs.GroupChatHub
{
    [SignalRHub]
    [Authorize]
    public class GroupChatHub : Hub<IGroupChatClient>, IGroupChatHub
    {
        private static readonly ConcurrentDictionary<string, HashSet<string>> _groupUsers = new();
        private static readonly ConcurrentDictionary<string, HashSet<string>> _userConnections = new();
        private readonly IRepo<ClientUser> clientUserRepo;
        private readonly IRepo<GroupChat> groupChatRepo;
        private readonly IGroupMessageService groupMessageService;

        public GroupChatHub(
            IRepo<ClientUser> clientUserRepo,
            IRepo<GroupChat> groupChatRepo,
            IGroupMessageService groupMessageService)
        {
            this.clientUserRepo = clientUserRepo;
            this.groupChatRepo = groupChatRepo;
            this.groupMessageService = groupMessageService;
        }

        [SignalRMethod]
        public async Task<Result> JoinGroup(int groupId)
        {
            var currentUserId = Guid.Parse(Context.UserIdentifier!);
            var groupKey = groupId.ToString();
            var connectionId = Context.ConnectionId;

            await Groups.AddToGroupAsync(connectionId, groupKey);

            _groupUsers.AddOrUpdate(groupKey,
                new HashSet<string> { currentUserId .ToString()},
                (key, existing) => { existing.Add(currentUserId.ToString()); return existing; });

          
            var client = await clientUserRepo.GetQuery().FirstOrDefaultAsync(c => c.UserId == currentUserId);
            if (client == null)
                return Result.Failure(Error.ClientUserNotFound);

            await Clients.Group(groupKey).UserJoined(client.UserName);

            return Result.Success();
        }

        [SignalRMethod]
        public async Task<Result> LeaveGroup(int groupId)
        {
            var currentUserId = Guid.Parse(Context.UserIdentifier!);
            var groupKey = groupId.ToString();
            var connectionId = Context.ConnectionId;

            await Groups.RemoveFromGroupAsync(connectionId, groupKey);

            if (_groupUsers.TryGetValue(groupKey, out var users))
            {
                users.Remove(currentUserId.ToString());
                if (users.Count == 0)
                {
                    _groupUsers.TryRemove(groupKey, out _);
                }
            }

            var client = await clientUserRepo.GetQuery().FirstOrDefaultAsync(c => c.UserId == currentUserId);
            if (client == null)
                return Result.Failure(Error.ClientUserNotFound);

            await Clients.Group(groupKey).UserLeft(client.UserName);

            return Result.Success();
        }

        [SignalRMethod]
        public async Task<Result> SendMessage(CreateGroupMessageDTO messageDTO)
        {
            var currentUserId = Guid.Parse(Context.UserIdentifier!);
            var groupKey = messageDTO.GroupChatId.ToString();

            var sender = await clientUserRepo.GetQuery()
           .FirstOrDefaultAsync(c => c.UserId == currentUserId);
            if (sender == null)
                return Result.Failure(Error.ClientUserNotFound);

          

            var toGroup = await groupChatRepo.GetQuery()
                .FirstOrDefaultAsync(c => c.Id == messageDTO.GroupChatId);

            if (toGroup == null)
                return Result.Failure(Error.GroupChatNotFound);

            var result = await groupMessageService.Create(sender.Id ,messageDTO);
            if(!result.IsSuccess)
                return Result.Failure(result.Error ?? Error.CreateFailed);

          

            var message = new GetGroupMessageDTO
            {
                Text = messageDTO.Text,
            };

            await Clients.Group(groupKey).ReceiveGroupMessage(message);

            return Result.Success();
        }

        [SignalRHidden]
        public override async Task OnConnectedAsync()
        {
            var currentUserId = Guid.Parse(Context.UserIdentifier!);
            var connectionId = Context.ConnectionId;

            _userConnections.AddOrUpdate(currentUserId.ToString(),
                new HashSet<string> { connectionId }, (key, existing) => {
                    existing.Add(connectionId);return existing;
                });
        }

        [SignalRHidden]
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var currentUserId = Guid.Parse(Context.UserIdentifier!);
            var connectionId = Context.ConnectionId;

            if (_userConnections.TryGetValue(currentUserId.ToString(), out var connections))
            {
                connections.Remove(connectionId);
                if (connections.Count == 0)
                {
                    _userConnections.TryRemove(currentUserId.ToString(), out _);

                    foreach (var group in _groupUsers.Keys)
                    {
                        if (_groupUsers.TryGetValue(group, out var users))
                        {
                            users.Remove(currentUserId.ToString());
                        }
                    }
                }
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}