namespace StudyBuddy.Domain.Entities;

public partial class Friend
{
    public int Id { get; set; }

    public int ClientUserId { get; set; }

    public int FriendId { get; set; }

    public virtual ClientUser ClientUser { get; set; } = null!;

    public virtual ClientUser FriendNavigation { get; set; } = null!;
}
