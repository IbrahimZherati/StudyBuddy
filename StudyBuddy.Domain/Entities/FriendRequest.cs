namespace StudyBuddy.Domain.Entities;

public partial class FriendRequest
{
    public int Id { get; set; }

    public int ClientUserId { get; set; }

    public int FriendId { get; set; }

    public bool? IsAccepted { get; set; }

    public virtual ClientUser ClientUser { get; set; } = null!;

    public virtual ClientUser Friend { get; set; } = null!;
}
