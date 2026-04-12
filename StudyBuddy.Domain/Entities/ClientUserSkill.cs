using Mapster;
using StudyBuddy.Shared.DTOs.ClientUserSkillDTO;
using StudyBuddy.Shared.Helpers.ErrorMessages;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Domain.Entities;

public partial class ClientUserSkill : EntityBase<int>
{
    public int ClientUserId { get; private set; }

    public int SkillId { get; private set; }

    public virtual ClientUser ClientUser { get; private set; } = null!;

    public virtual Skill Skill { get; private set; } = null!;


    private ClientUserSkill() { }

    public static Result<ClientUserSkill> Create(CreateClientUserSkillDTO clientUserSkillDTO)
    {
        var newClientUserSkill = new ClientUserSkill();
        clientUserSkillDTO.Adapt(newClientUserSkill);
        newClientUserSkill.CreateDate = DateTime.Now;
        return Result<ClientUserSkill>.Success(newClientUserSkill);
    }
    public static ClientUserSkill Create(ClientUser clientUser, Skill skill)
    {
        var newClientUserSkill = new ClientUserSkill();
        newClientUserSkill.Skill = skill;
        newClientUserSkill.ClientUser = clientUser;
        newClientUserSkill.CreateDate = DateTime.Now;
        return newClientUserSkill;
    }

    public Result<ClientUserSkill> Update(UpdateClientUserSkillDTO clientUserSkillDTO)
    {
        clientUserSkillDTO.Adapt(this);
        ModifyDate = DateTime.Now;
        return Result<ClientUserSkill>.Success(this);
    }


}
