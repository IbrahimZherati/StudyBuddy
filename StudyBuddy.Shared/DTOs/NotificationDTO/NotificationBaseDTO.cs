namespace StudyBuddy.Shared.DTOs.NotificationDTO
{
    public class NotificationBaseDTO
    {
        public int ToClientUserId { get; set; }

        public int FromClientUserId { get; set; }

        public string Description { get; set; } = null!;

        public string Title { get; set; } = null!;



        public int NotificationTypeId { get; set; }
    }
}