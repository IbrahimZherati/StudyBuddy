using System.ComponentModel.DataAnnotations;
namespace StudyBuddy.Shared.DTOs.ClientFileDTO;

public class ClientFileBaseDTO
{

    [Required]
    public int ClientUserId { get; set; }
    [Required]
    public string Title { get; set; } = null!;
    public byte[]? Bin { get; set; }
}
