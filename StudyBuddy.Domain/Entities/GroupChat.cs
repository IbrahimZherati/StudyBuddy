namespace StudyBuddy.Domain.Entities;

public partial class GroupChat
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int MajorId { get; set; }

    public int UniversityId { get; set; }

    public string Bio { get; set; } = null!;

    public byte[]? Photo { get; set; }

    public virtual ICollection<ClientUserGroupChat> ClientUserGroupChats { get; set; } = new List<ClientUserGroupChat>();

    public virtual ICollection<GroupMessage> GroupMessages { get; set; } = new List<GroupMessage>();

    public virtual Major Major { get; set; } = null!;

    public virtual University University { get; set; } = null!;
}
