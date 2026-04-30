using StudyBuddy.Shared.DTOs.DayDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Shared.DTOs.ClientUserDTO
{
    public class UpdateClientUserDTO : ClientUserBaseDTO
    {
        [Required]
        public string Bio { get; set; } = null!;
    }
}
