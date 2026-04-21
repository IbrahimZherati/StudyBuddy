using Mapster;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.ClientUserDTO;
using StudyBuddy.Shared.DTOs.FriendRequestDTO;
using StudyBuddy.Shared.DTOs.GroupChatDTO;
using StudyBuddy.Shared.DTOs.MessageDTO;

public static class MapsterConfiguration
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<ClientUser, GetProfileClientUserDTO>.NewConfig()
            .Map(dest => dest.Major, src => src.Major != null ? src.Major.Name : "")
            .Map(dest => dest.University, src => src.University != null ? src.University.Name : "")
            .Map(dest => dest.City, src => src.City != null ? src.City.Name : "")
            .Map(dest => dest.Country, src => src.Country != null ? src.Country.Name : "")
            .Map(dest => dest.StudyInterests, src => src.ClientUserSkills.Select(cs => cs.Skill.Name))
            .Map(dest => dest.AvailableDays, src => src.ClientUserAvailableDays.Select(ca => ca.Day.Name))
            .Map(dest => dest.FriendCount, src => src.FirstFriends.Count() + src.SecondFriends.Count())
            .Map(dest => dest.PostCount, src => src.Posts.Count());

        TypeAdapterConfig<ClientUser, InfoClientUserDTO>.NewConfig()
            .Map(dest => dest.Major, src => src.Major != null ? src.Major.Name : "")
            .Map(dest => dest.University, src => src.University != null ? src.University.Name : "");

        TypeAdapterConfig<GroupChat, InfoGroupChatDTO>.NewConfig()
          .Map(dest => dest.Major, src => src.Major.Name)
          .Map(dest => dest.MemberCount, src => src.ClientUserGroupChats.Count())
          .Map(dest => dest.University, src => src.University.Name);

   

        TypeAdapterConfig<Message, GetMessageDTO>.NewConfig()
          .Map(dest => dest.UserName, src => src.FromClientUser.UserName);
        TypeAdapterConfig<FriendRequest, GetFriendRequestDTO>.NewConfig()
          .Map(dest => dest.From, src => src.FromClientUser.UserName);

    }
}
