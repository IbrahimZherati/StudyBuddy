using Mapster;
using StudyBuddy.Shared.DTOs.ClientUserGroupChatDTO;
using StudyBuddy.Shared.Helpers.ErrorMessages;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Domain.Entities;

public partial class ClientUserGroupChat : EntityBase<int>
{
     public int ClientUserId { get; private set; }
     public int GroupChatId { get; private set; }
     public virtual ClientUser ClientUser { get; private set; } = null!;
     public virtual GroupChat GroupChat { get; private set; } = null!;

     private ClientUserGroupChat() { }

     public static Result<ClientUserGroupChat> Create(CreateClientUserGroupChatDTO clientUserGroupChatDTO)
     {
         var newClientUserGroupChat = new ClientUserGroupChat();
         clientUserGroupChatDTO.Adapt(newClientUserGroupChat);
         newClientUserGroupChat.CreateDate = DateTime.Now;
         return Result<ClientUserGroupChat>.Success(newClientUserGroupChat);
     }

    public static ClientUserGroupChat Create(int clientUserId , int groupId)
    {
        var newClientUserGroupChat = new ClientUserGroupChat();
        newClientUserGroupChat.ClientUserId = clientUserId;
        newClientUserGroupChat.GroupChatId = groupId;
        newClientUserGroupChat.CreateDate = DateTime.Now;
        return newClientUserGroupChat;
    }

     public Result<ClientUserGroupChat> Update(UpdateClientUserGroupChatDTO clientUserGroupChatDTO)
     {
         clientUserGroupChatDTO.Adapt(this);
         ModifyDate = DateTime.Now;
         return Result<ClientUserGroupChat>.Success(this);
     }


 }
