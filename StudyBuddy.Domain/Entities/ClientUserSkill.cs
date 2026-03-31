namespace StudyBuddy.Domain.Entities;

public partial class ClientUserSkill
{
    public int Id { get; set; }

    public int ClientUserId { get; set; }

    public int SkillId { get; set; }

    public virtual ClientUser ClientUser { get; set; } = null!;

    public virtual Skill Skill { get; set; } = null!;
}
