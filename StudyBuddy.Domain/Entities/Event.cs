namespace StudyBuddy.Domain.Entities;

public partial class Event
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime Date { get; set; }

    public int ClientUserId { get; set; }

    public virtual ClientUser ClientUser { get; set; } = null!;
}
