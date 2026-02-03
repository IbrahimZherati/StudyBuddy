namespace StudyBuddy.Application.DTOs.Shared
{
    public class DataTableResponse<TValue>
    {
        public int Count { get; set; }
        public List<TValue>? Data { get; set; }
    }
}
