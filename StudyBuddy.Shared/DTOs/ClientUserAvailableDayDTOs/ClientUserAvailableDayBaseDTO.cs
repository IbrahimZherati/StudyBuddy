using System.ComponentModel.DataAnnotations;
namespace StudyBuddy.Shared.DTOs.ClientUserAvailableDayDTO;

public class ClientUserAvailableDayBaseDTO
{

    [Required]
        public int ClientUserId { get; set; }
    [Required]
        public int DayId { get; set; }
}
