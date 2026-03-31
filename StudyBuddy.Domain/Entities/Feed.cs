namespace StudyBuddy.Domain.Entities;

public partial class Feed
{
    public int Id { get; set; }

    public int ClientUserId { get; set; }

    public string Discription { get; set; } = null!;

    public int ShareCount { get; set; }

    public int LikeCount { get; set; }

    public virtual ClientUser ClientUser { get; set; } = null!;

    public virtual ICollection<FeedReplay> FeedReplays { get; set; } = new List<FeedReplay>();
}
