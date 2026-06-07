using StudyBuddy.Shared.DTOs.DayDTO;
using StudyBuddy.Shared.DTOs.StudyInterestDTO;
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

        [Required]
        public int MajorId { get; set; }

       

        public int? CityId { get; set; }

        public int? CountryId { get; set; }
        public int? UniversityId { get; set; }

        public bool? Gender { get; set; } 

        public byte[]? Photo { get; set; }

        public List<int> availableDayIds { get; set; } = new List<int>();
        public List<CreateStudyInterestDTO> studyInterests { get; set; } = new();
    }
}
