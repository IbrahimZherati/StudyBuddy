namespace StudyBuddy.Shared.DTOs.PostDTO;

public class GetPostDTO : PostBaseDTO
{
    public Guid Id { get; set; }
    public string? UserName { get; set; }
    public byte[]? ClientUserPhoto { get; set; }
    public DateTime? CreateDate { get; set; }
    public int Likes { get; set; }
    public int Replies { get; set; }

    public bool IsLiked { get; set; }

    public int Share { get; set; }
}
