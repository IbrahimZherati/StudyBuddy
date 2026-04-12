using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalRSwaggerGen.Attributes;
using StudyBuddy.API.Hubs;
using StudyBuddy.Application.Services.Messages;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.MessageDTO;
using StudyBuddy.Shared.Helpers.ErrorMessages;
using StudyBuddy.Shared.Results;
using System.Text.RegularExpressions;

[SignalRHub]
[Authorize]
public class PrivateChatHub : Hub , IPrivateChatHub
{
    private readonly IRepo<ClientUser> clientUserRepo;
    private readonly IMessageService messageService;

    public PrivateChatHub(
        IRepo<ClientUser> clientUserRepo,
        IMessageService messageService)
    {
        this.clientUserRepo = clientUserRepo;
        this.messageService = messageService;
    }

    [SignalRMethod]
    public async Task<Result> SendMessage(CreateMessageDTO messageDTO)
    {
        var UserId =Guid.Parse(Context.UserIdentifier!);

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
        if(message == null)
            return Result.Failure(Error.CreateFailed);
        var receiveMessage = message.Adapt<GetMessageDTO>();
        receiveMessage.UserName = sender.UserName;


        await Clients.Users(sender.UserId.ToString() , toClient.UserId.ToString()).SendAsync("ReceiveMessage", receiveMessage);
        return Result.Success();
    }
}
