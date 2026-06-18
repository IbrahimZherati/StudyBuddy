using Mapster;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.ChatDTOs;
using StudyBuddy.Shared.DTOs.ClientUserDTO;
using StudyBuddy.Shared.DTOs.FriendRequestDTO;
using StudyBuddy.Shared.DTOs.GroupChatDTO;
using StudyBuddy.Shared.DTOs.GroupInviteDTOs;
using StudyBuddy.Shared.DTOs.MessageDTO;
using StudyBuddy.Shared.DTOs.NoteDTO;
using StudyBuddy.Shared.DTOs.NotificationDTO;
using StudyBuddy.Shared.DTOs.PostDTO;
using StudyBuddy.Shared.DTOs.PostReplyDTO;
using StudyBuddy.Shared.DTOs.TopicDTO;

public static class MapsterConfiguration
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<ClientUser, GetProfileClientUserDTO>.NewConfig()
            .Map(dest => dest.Major, src => src.Major != null ? src.Major.Name : "")
            .Map(dest => dest.University, src => src.University != null ? src.University.Name : "")
            .Map(dest => dest.City, src => src.City != null ? src.City.Name : "")
            .Map(dest => dest.Country, src => src.Country != null ? src.Country.Name : "")
            .Map(dest => dest.StudyInterests , src => src.StudyInterests.Select(s => s.Name))
            .Map(dest => dest.AvailableDays, src => src.ClientUserAvailableDays.Select(ca => ca.Day.Name))
            .Map(dest => dest.FriendCount, src => src.FirstFriends.Count() + src.SecondFriends.Count())
            .Map(dest => dest.PostCount, src => src.Posts.Count());

        TypeAdapterConfig<ClientUser, InfoClientUserDTO>.NewConfig()
            .Map(dest => dest.Major, src => src.Major != null ? src.Major.Name : "")
            .Map(dest => dest.StudyInterestsList, src => src.StudyInterests.Select(s => s.Name).ToList())
            .Map(dest => dest.AvailableDaysList, src => src.ClientUserAvailableDays.Select(s => s.Day.Name).ToList());
        TypeAdapterConfig<ClientUser, FriendInfoDTO>.NewConfig()
            .Map(dest => dest.Major, src => src.Major != null ? src.Major.Name : "")

            .Map(dest => dest.University, src => src.University != null ? src.University.Name : "");

        TypeAdapterConfig<GroupChat, InfoGroupChatDTO>.NewConfig()
          .Map(dest => dest.Major, src => src.Major.Name)
          .Map(dest => dest.MemberCount, src => src.ClientUserGroupChats.Count());

   

        TypeAdapterConfig<Message, GetMessageDTO>.NewConfig()
          .Map(dest => dest.UserName, src => src.FromClientUser.UserName);
        TypeAdapterConfig<FriendRequest, GetFriendRequestDTO>.NewConfig()
          .Map(dest => dest.From, src => src.FromClientUser.UserName);
        TypeAdapterConfig<Post, GetPostDTO>.NewConfig()
          .Map(dest => dest.ClientUserPhoto, src => src.ClientUser.Photo)
          .Map(dest => dest.UserName, src => src.ClientUser.UserName)
          .Map(dest => dest.Likes, src => src.ClientUserLikePosts.Count())
          .Map(dest => dest.Replies, src => src.PostReplys.Count())
          .Map(dest => dest.Share, src => src.ShareCount);

        TypeAdapterConfig<PostReply, GetPostReplyDTO>.NewConfig()
          .Map(dest => dest.ClientUserName, src => src.ClientUser.UserName)
          .Map(dest => dest.ClientPhoto, src => src.ClientUser.Photo);

        TypeAdapterConfig<GroupInvite, GetGroupInviteDTO>.NewConfig()
          .Map(dest => dest.From, src => src.ClientUserFrom.UserName)
          .Map(dest => dest.FromClientPhoto, src => src.ClientUserFrom.Photo);

        TypeAdapterConfig<Notification, GetNotificationDTO>.NewConfig()
          .Map(dest => dest.Type, src => src.NotificationType.Type);

        TypeAdapterConfig<Note, GetNoteDTO>.NewConfig()
          .Map(dest => dest.Topics, src => src.NoteTopics.Select(n => new GetTopicDTO { Id = n.Topic.Id,Name = n.Topic.Name}).ToList());

        TypeAdapterConfig<ClientUser, ChatDTO>.NewConfig()
          .Map(dest => dest.Name, src => src.UserName)
          .Map(dest => dest.Photo, src => src.Photo);

        TypeAdapterConfig<GroupChat, ChatDTO>.NewConfig()
          .Map(dest => dest.Name, src => src.Name)
          .Map(dest => dest.Photo, src => src.Photo);

        TypeAdapterConfig<Message, ChatMessageDTO>.NewConfig()
          .Map(dest => dest.Text, src => src.Text)
          .Map(dest => dest.CreateDate, src => src.CreateDate);
          
        
        TypeAdapterConfig<GroupMessage, ChatMessageDTO>.NewConfig()
          .Map(dest => dest.Text, src => src.Text)
          .Map(dest => dest.CreateDate, src => src.CreateDate);
          
        

    }
}
