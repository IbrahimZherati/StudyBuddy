using System.ComponentModel.DataAnnotations;
namespace StudyBuddy.Shared.DTOs.ClientUserSkillDTO;

public class ClientUserSkillBaseDTO
{

    [Required]
    public int ClientUserId { get; set; }
    [Required]
    public int SkillId { get; set; }
}
