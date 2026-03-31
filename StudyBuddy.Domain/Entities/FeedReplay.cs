namespace StudyBuddy.Domain.Entities;

public partial class FeedReplay
{
    public int Id { get; set; }

    public int FeedId { get; set; }

    public virtual Feed Feed { get; set; } = null!;
}
