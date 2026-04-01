using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Application.Services.Shared.Interfaces
{
    public interface IAiService
    {
        Task<string> GenerateAsync(string prompt);
    }
}
