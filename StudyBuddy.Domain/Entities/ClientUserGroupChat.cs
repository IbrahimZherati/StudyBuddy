namespace StudyBuddy.Domain.Entities;

public partial class ClientUserGroupChat
{
    public int Id { get; set; }

    public int ClientUserId { get; set; }

    public int GroupChatId { get; set; }

    public virtual ClientUser ClientUser { get; set; } = null!;

    public virtual GroupChat GroupChat { get; set; } = null!;
}
