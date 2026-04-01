using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Shared.DTOs.MajorDTO
{
    public class MajorBaseDTO
    {
        [Required]
        public string Name { get; set; } = null!;

    }
}
