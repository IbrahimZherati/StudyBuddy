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

        public string? Major { get; set; }

        public string? University { get; set; }
    }
}
