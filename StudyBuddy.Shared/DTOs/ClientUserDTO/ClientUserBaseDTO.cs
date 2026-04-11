using StudyBuddy.Shared.DTOs.DayDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Shared.DTOs.ClientUserDTO
{
    public class ClientUserBaseDTO
    {
        [Required]
        public string UserName { get; set; } = null!;

        public int? MajorId { get; set; }

        public string? Bio { get; set; }

        public int? CityId { get; set; }

        public int? CountryId { get; set; }
        public int? UniversityId { get; set; }

        public bool Gender { get; set; } = true;

        public byte[]? Photo { get; set; }

        public List<GetDayDTO> availableDays = new();
    }
}
