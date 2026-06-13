using StudyBuddy.Shared.DTOs.TopicDTO;

namespace StudyBuddy.Shared.DTOs.NoteDTO;

public class GetNoteDTO : NoteBaseDTO
{
    public int Id { get; set; }
    public List<GetTopicDTO> Topics { get; set; } = new();
}
