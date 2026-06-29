namespace StudyBuddy.Shared.DTOs.ClientFileDTO;

public class GetFlashCardDTO
{
    public Guid Id { get; set; }
    public string? Question { get; set; }
    public string? Answer { get; set; }
}
