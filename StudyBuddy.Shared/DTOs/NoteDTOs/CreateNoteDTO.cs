namespace StudyBuddy.Shared.DTOs.NoteDTO;

public class CreateNoteDTO : NoteBaseDTO
{
    public List<int> TopicIds { get; set; } = new List<int>();
}
