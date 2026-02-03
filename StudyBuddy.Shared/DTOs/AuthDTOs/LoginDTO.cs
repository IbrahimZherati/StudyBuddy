using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyBuddy.Shared.Helpers;

namespace StudyBuddy.Application.DTOs.AuthDTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = ValidationMessage.Required)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = ValidationMessage.Required)]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }
}
