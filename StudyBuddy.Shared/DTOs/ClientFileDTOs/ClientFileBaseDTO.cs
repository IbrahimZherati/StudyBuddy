using System.ComponentModel.DataAnnotations;
namespace StudyBuddy.Shared.DTOs.ClientFileDTO;

public class ClientFileBaseDTO
{



    [Required]
    public string Title { get; set; } = null!;

    [Required]
    public byte[]? Bin { get; set; }

    public DateTime CreateDate { get; set; }

}
