using StudyBuddy.Application.Services.Shared.Interfaces;
using StudyBuddy.Shared.AI;
using StudyBuddy.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Application.Services.Shared.AutoGenerateSkills
{
    public class AutoGenerateSkill : IAutoGenrateSkill
    {
        private readonly IAiService aiService;

        public AutoGenerateSkill(IAiService aiService)
        {
            this.aiService = aiService;
        }
        public async Task<Result<List<string>>> GetSkillFromBio(string? bio)
        {
            if (bio == null)
                return Result<List<string>>.Success(new List<string>());

            var promt = AiPromt.GetGenerateSkillPromt(bio);
            var result = string.Empty;
            try
            {
                result = await aiService.GenerateAsync(promt);
            }
            catch
            {
              return Result<List<string>>.Failure(Error.AiServiceFailed);
            }

            List<string> skills = result.Split(',')
                                       .Select(s => s.Trim())
                                       .ToList();
            return Result<List<string>>.Success(skills);

        }
    }
}
