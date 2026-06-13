namespace StudyBuddy.Shared.DTOs.NoteDTO;

public class UpdateNoteDTO : NoteBaseDTO
{
    public int Id { get; set; }
    public List<int> TopicIds { get; set; } = new List<int>();

}
