namespace StudyBuddy.Application.DTOs.Shared
{
    public class DataResponse<TValue>
    {
        public int Count { get; set; }
        public List<TValue>? Data { get; set; }
    }
}
