namespace StudyBuddy.Domain.Entities;

public partial class ClientFile
{
    public int Id { get; set; }

    public int ClientUserId { get; set; }

    public string Title { get; set; } = null!;

    public byte[]? Bin { get; set; }

    public virtual ClientUser ClientUser { get; set; } = null!;
}
