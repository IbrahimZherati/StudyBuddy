using Mapster;
using StudyBuddy.Shared.DTOs.ClientUserDTO;
using StudyBuddy.Shared.Helpers.ErrorMessages;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Domain.Entities;

public partial class ClientUser : EntityBase<int>
{
    public string UserName { get; private set; } = null!;

    public Guid UserId { get; private set; }

    public int? MajorId { get; private set; }

    public int? UniversityId { get; private set; }

    public string? Bio { get; private set; }

    public int? CityId { get; private set; }

    public int? CountryId { get; private set; }

    public bool Gender { get; private set; } = true;

    public byte[]? Photo { get; private set; }

    private readonly List<Article> _articles = new();
    public virtual IReadOnlyCollection<Article> Articles => _articles;

    public virtual City City { get; private set; } = null!;

    private readonly List<ClientFile> _clientFiles = new();
    public virtual IReadOnlyCollection<ClientFile> ClientFiles => _clientFiles;

    private readonly List<ClientUserAvailableDay> _clientUserAvailableDays = new();
    public virtual IReadOnlyCollection<ClientUserAvailableDay> ClientUserAvailableDays => _clientUserAvailableDays;

    private readonly List<ClientUserGroupChat> _clientUserGroupChats = new();
    public virtual IReadOnlyCollection<ClientUserGroupChat> ClientUserGroupChats => _clientUserGroupChats;

    private readonly List<ClientUserSkill> _clientUserSkills = new();
    public virtual IReadOnlyCollection<ClientUserSkill> ClientUserSkills => _clientUserSkills;

    private readonly List<ClientUserLikeFeed> _clientUserLikeFeed = new();
    public virtual IReadOnlyCollection<ClientUserLikeFeed> ClientUserLikeFeeds => _clientUserLikeFeed;

    public virtual Country Country { get; private set; } = null!;

    private readonly List<Event> _events = new();
    public virtual IReadOnlyCollection<Event> Events => _events;

    private readonly List<Feed> _feeds = new();
    public virtual IReadOnlyCollection<Feed> Feeds => _feeds;

    private readonly List<Friend> _firstFriends = new();
    public virtual IReadOnlyCollection<Friend> FirstFriends => _firstFriends;

    private readonly List<Friend> _secondFriends = new();
    public virtual IReadOnlyCollection<Friend> SecondFriends => _secondFriends;

    private readonly List<FriendRequest> _friendRequestFromClientUsers = new();
    public virtual IReadOnlyCollection<FriendRequest> FriendRequestFromClientUsers => _friendRequestFromClientUsers;

    private readonly List<FriendRequest> _friendRequestToClientUsers = new();
    public virtual IReadOnlyCollection<FriendRequest> FriendRequestToClientUsers => _friendRequestToClientUsers;

    private readonly List<GroupMessage> _groupMessages = new();
    public virtual IReadOnlyCollection<GroupMessage> GroupMessages => _groupMessages;

    public virtual Major? Major { get; private set; }

    private readonly List<Message> _messageFromClientUsers = new();
    public virtual IReadOnlyCollection<Message> MessageFromClientUsers => _messageFromClientUsers;

    private readonly List<Message> _messageToClientUsers = new();
    public virtual IReadOnlyCollection<Message> MessageToClientUsers => _messageToClientUsers;

    private readonly List<Note> _notes = new();
    public virtual IReadOnlyCollection<Note> Notes => _notes;

    private readonly List<Notification> _notificationFromClientUsers = new();
    public virtual IReadOnlyCollection<Notification> NotificationFromClientUsers => _notificationFromClientUsers;

    private readonly List<Notification> _notificationToClientUsers = new();
    public virtual IReadOnlyCollection<Notification> NotificationToClientUsers => _notificationToClientUsers;

    private readonly List<Post> _posts = new();
    public virtual IReadOnlyCollection<Post> Posts => _posts;

    private readonly List<FeedReplay> _feedReplaies = new();
    public virtual IReadOnlyCollection<FeedReplay> FeedReplaies => _feedReplaies;

    public virtual University? University { get; private set; }

    private readonly List<ClientUserLikePost> _clientUserLikePosts = new();
    public virtual IReadOnlyCollection<ClientUserLikePost> ClientUserLikePosts => _clientUserLikePosts;





    private ClientUser() { }

    public static Result<ClientUser> Create(CreateClientUserDTO clientUserDTO)
    {
        var newClientUser = new ClientUser();
        clientUserDTO.Adapt(newClientUser);
        newClientUser.CreateDate = DateTime.Now;
        return Result<ClientUser>.Success(newClientUser);
    }

    public Result<ClientUser> Update(UpdateClientUserDTO clientUserDTO)
    {
        clientUserDTO.Adapt(this);
        ModifyDate = DateTime.Now;
        return Result<ClientUser>.Success(this);
    }


}
