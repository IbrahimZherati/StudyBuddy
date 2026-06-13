using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalRSwaggerGen.Attributes;
using StudyBuddy.API.Hubs;
using StudyBuddy.API.Hubs.PrivateChatHub;
using StudyBuddy.Application.Services.Messages;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.MessageDTO;
using StudyBuddy.Shared.Helpers.ErrorMessages;
using StudyBuddy.Shared.Results;
using System.Text.RegularExpressions;

[SignalRHub]
[Authorize]
public class PrivateChatHub : Hub<IPrivateChatClient>, IPrivateChatHub
{
    private readonly IRepo<ClientUser> clientUserRepo;
    private readonly IRepo<Message> messageRepo;
    private readonly IMessageService messageService;

    public PrivateChatHub(
        IRepo<ClientUser> clientUserRepo,

        IMessageService messageService)
    {
        this.clientUserRepo = clientUserRepo;

        this.messageService = messageService;
    }

    public async Task<Result> ReadMessage(Guid Id)
    {
        var UserId = Guid.Parse(Context.UserIdentifier!);
        var reader = await clientUserRepo.GetQuery()
          .FirstOrDefaultAsync(c => c.UserId == UserId);
        if (reader == null)
            return Result.Failure(Error.ClientUserNotFound);
        var result = await messageService.Read(reader.Id, Id);
        if (!result.IsSuccess)
            return Result.Failure(result.Error ?? Error.UpdateFailed);
        var message = result.Value;
        if (message == null)
            return Result.Failure(Error.UpdateFailed);

        var clientFrom = await clientUserRepo.GetByIdAsync(message.FromClientUserId);
        var clientTo = await clientUserRepo.GetByIdAsync(message.ToClientUserId);
        if (clientFrom == null)
            return Result.Failure(Error.ClientUserNotFound);
        if (clientTo == null)
            return Result.Failure(Error.ClientUserNotFound);

        var userFromId = clientFrom.UserId;
        var userToId = clientTo.UserId;
        await Clients.Users(userFromId.ToString(), userToId.ToString()).ReadMessage(Id);
        return Result.Success();

    }

    [SignalRMethod]
    public async Task<Result> SendMessage(CreateMessageDTO messageDTO)
    {
        var UserId = Guid.Parse(Context.UserIdentifier!);

        var sender = await clientUserRepo.GetQuery()
            .FirstOrDefaultAsync(c => c.UserId == UserId);
        if (sender == null)
            return Result.Failure(Error.ClientUserNotFound);



        var toClient = await clientUserRepo.GetQuery()
            .FirstOrDefaultAsync(c => c.Id == messageDTO.ToClientUserId);

        if (toClient == null)
            return Result.Failure(Error.ClientUserNotFound);

        //Create Message
        var result = await messageService.Create(sender.Id, messageDTO);
        if (!result.IsSuccess)
            return Result.Failure(result.Error ?? Error.CreateFailed);
        var message = result.Value;
        if (message == null)
            return Result.Failure(Error.CreateFailed);
        var receiveMessage = message.Adapt<GetMessageDTO>();
        receiveMessage.UserName = sender.UserName;


        await Clients.Users(sender.UserId.ToString(), toClient.UserId.ToString()).ReceiveMessage(receiveMessage);
        return Result.Success();
    }
}
