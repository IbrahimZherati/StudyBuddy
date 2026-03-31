namespace StudyBuddy.Domain.Entities;

public partial class University
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<ClientUser> ClientUsers { get; set; } = new List<ClientUser>();

    public virtual ICollection<GroupChat> GroupChats { get; set; } = new List<GroupChat>();
}
