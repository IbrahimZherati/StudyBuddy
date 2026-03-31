namespace StudyBuddy.Domain.Entities;

public partial class Post
{
    public int Id { get; set; }

    public int ClientUserId { get; set; }

    public byte[] Photo { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Text { get; set; } = null!;

    public DateOnly CreateDate { get; set; }

    public DateOnly? ModifyDate { get; set; }

    public virtual ClientUser ClientUser { get; set; } = null!;
}
