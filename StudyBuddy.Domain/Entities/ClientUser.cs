namespace StudyBuddy.Domain.Entities;

public partial class ClientUser
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public Guid UserId { get; set; }

    public int? MajorId { get; set; }

    public int? UniversityId { get; set; }

    public string? Bio { get; set; }

    public int? CityId { get; set; }

    public int? CountryId { get; set; }

    public bool Gender { get; set; } = true;

    public byte[]? Photo { get; set; }

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    public virtual City City { get; set; } = null!;

    public virtual ICollection<ClientFile> ClientFiles { get; set; } = new List<ClientFile>();

    public virtual ICollection<ClientUserGroupChat> ClientUserGroupChats { get; set; } = new List<ClientUserGroupChat>();

    public virtual ICollection<ClientUserSkill> ClientUserSkills { get; set; } = new List<ClientUserSkill>();

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Feed> Feeds { get; set; } = new List<Feed>();

    public virtual ICollection<Friend> FriendClientUsers { get; set; } = new List<Friend>();

    public virtual ICollection<Friend> FriendFriendNavigations { get; set; } = new List<Friend>();

    public virtual ICollection<FriendRequest> FriendRequestClientUsers { get; set; } = new List<FriendRequest>();

    public virtual ICollection<FriendRequest> FriendRequestFriends { get; set; } = new List<FriendRequest>();

    public virtual ICollection<GroupMessage> GroupMessages { get; set; } = new List<GroupMessage>();

    public virtual Major? Major { get; set; }

    public virtual ICollection<Message> MessageFromClientUsers { get; set; } = new List<Message>();

    public virtual ICollection<Message> MessageToClientUsers { get; set; } = new List<Message>();

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();

    public virtual ICollection<Notification> NotificationFromClientUsers { get; set; } = new List<Notification>();

    public virtual ICollection<Notification> NotificationToClientUsers { get; set; } = new List<Notification>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual University? University { get; set; }

    public virtual AspNetUser User { get; set; } = null!;
}
