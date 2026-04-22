using StudyBuddy.Application.Services.Shared.AutoGenerateSkills;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.Json;
using StudyBuddy.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StudyBuddy.Application.Services.Shared.GetTagsFromMajors
{
    public class TagsService : ITagsService
    {
        private readonly IRepo<ClientUser> clientUserRepo;
        private readonly IRepo<Skill> skillRepo;
        private readonly IAutoGenrateSkill autoGenrateSkill;

        public TagsService(
            IRepo<ClientUser> clientUserRepo,
            IRepo<Skill> skillRepo,
            IAutoGenrateSkill autoGenrateSkill
            )
        {
            this.clientUserRepo = clientUserRepo;
            this.skillRepo = skillRepo;
            this.autoGenrateSkill = autoGenrateSkill;
        }
        public async Task<Result<ClientUser>> GenerateTags(ClientUser client, string rootPath)
        {
            #region Generate Tags From AI
            var result = await autoGenrateSkill.GetSkillFromBio(client.Bio);

            if (result.IsSuccess)
            {
                client.IsSkillFromMajor = false;
                var newClientUserSkills = new List<ClientUserSkill>();
                foreach (var skill in result.Value!.Distinct())
                {
                    var skillIn = await skillRepo.GetQuery()
                   .FirstOrDefaultAsync(s => s.Name.ToLower() == skill.ToLower());
                    if (skillIn == null)
                    {
                        skillIn = Skill.Create(skill.ToLower());
                        await skillRepo.AddAsync(skillIn);
                    }

                    client.AddSkill(skillIn);
                }
                return Result<ClientUser>.Success(client);
            }

            //Create new Skills and select need skills
            #endregion
            #region Generate Tags From Major
            client.IsSkillFromMajor = true;
            var path = Path.Combine(rootPath, "data", "major_tags.json");
            if (!File.Exists(path))
                return Result<ClientUser>.Failure(Error.JsonNotFound);

            var jsonString = await File.ReadAllTextAsync(path);

            var data = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(jsonString);
            if (data == null)
                return Result<ClientUser>.Failure(Error.JsonFormatWrong);

            data.TryGetValue(client.Major.Name, out var tags);
            if (tags == null)
                return Result<ClientUser>.Failure(Error.TagsNotFound);

            foreach (var skill in tags.Distinct())
            {
                var skillIn = await skillRepo.GetQuery()
                    .FirstOrDefaultAsync(s => s.Name.ToLower() == skill.ToLower());
                if (skillIn == null)
                {
                    skillIn = Skill.Create(skill.ToLower());
                    await skillRepo.AddAsync(skillIn);
                }

                client.AddSkill(skillIn);
            }
            return Result<ClientUser>.Success(client);

            #endregion

        }
    }
}
