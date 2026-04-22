using Mapster;
using StudyBuddy.Shared.DTOs.GroupChatDTO;
using StudyBuddy.Shared.Helpers.ErrorMessages;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Domain.Entities;

public partial class GroupChat : EntityBase<int>
{
     public string Name { get; private set; } = null!;
     public int MajorId { get; private set; }

     public string Bio { get; private set; } = null!;
     public byte[]? Photo { get; private set; }
     private readonly List<ClientUserGroupChat> _clientUserGroupChats = new();
     public virtual IReadOnlyCollection<ClientUserGroupChat> ClientUserGroupChats => _clientUserGroupChats;

     private readonly List<GroupMessage> _groupMessages = new();
     public virtual IReadOnlyCollection<GroupMessage> GroupMessages => _groupMessages;

     public virtual Major Major { get; private set; } = null!;


     private GroupChat() { }

     public static Result<GroupChat> Create(CreateGroupChatDTO groupChatDTO)
     {
         var newGroupChat = new GroupChat();
         groupChatDTO.Adapt(newGroupChat);
         newGroupChat.CreateDate = DateTime.Now;
         return Result<GroupChat>.Success(newGroupChat);
     }

     public Result<GroupChat> Update(UpdateGroupChatDTO groupChatDTO)
     {
         groupChatDTO.Adapt(this);
         ModifyDate = DateTime.Now;
         return Result<GroupChat>.Success(this);
     }


 }
