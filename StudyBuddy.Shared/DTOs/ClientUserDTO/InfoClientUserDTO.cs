using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Shared.DTOs.ClientUserDTO
{
    public class InfoClientUserDTO
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Bio { get; set; }
        public string? Major { get; set; }
        public bool? IsSkillFromMajor { get; set; }
        public string? University { get; set; }
        public byte[]? Photo { get; set; }

        public List<string> StudyInterestsList { get; set; } = new();
        public List<string> AvailableDaysList { get; set; } = new();


    }
}
