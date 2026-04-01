using StudyBuddy.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Application.Services.Shared.AutoGenerateSkills
{
    public interface IAutoGenrateSkill
    {
        Task<Result<List<string>>> GetSkillFromBio(string? bio);
    }
}
