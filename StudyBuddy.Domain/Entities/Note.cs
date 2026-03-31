namespace StudyBuddy.Domain.Entities;

public partial class Note
{
    public int Id { get; set; }

    public int ClientUserId { get; set; }

    public string Title { get; set; } = null!;

    public string Notes { get; set; } = null!;

    public virtual ClientUser ClientUser { get; set; } = null!;
}
