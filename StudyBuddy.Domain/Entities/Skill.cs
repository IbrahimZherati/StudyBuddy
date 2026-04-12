using Mapster;
using StudyBuddy.Shared.DTOs.SkillDTO;
using StudyBuddy.Shared.Helpers.ErrorMessages;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Domain.Entities;

public partial class Skill : EntityBase<int>
{
     public string Name { get; private set; } = null!;
     private readonly List<ClientUserSkill> _clientUserSkills = new();
     public virtual IReadOnlyCollection<ClientUserSkill> ClientUserSkills => _clientUserSkills;


     private Skill() { }

     public static Result<Skill> Create(CreateSkillDTO skillDTO)
     {
         var newSkill = new Skill();
         skillDTO.Adapt(newSkill);
         newSkill.CreateDate = DateTime.Now;
         return Result<Skill>.Success(newSkill);
     }

    public static Skill Create(string Name)
    {
        var newSkill = new Skill();
        newSkill.Name = Name;
        newSkill.CreateDate = DateTime.Now;
        return newSkill;
    }

     public Result<Skill> Update(UpdateSkillDTO skillDTO)
     {
         skillDTO.Adapt(this);
         ModifyDate = DateTime.Now;
         return Result<Skill>.Success(this);
     }


 }
