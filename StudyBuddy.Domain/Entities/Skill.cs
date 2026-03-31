namespace StudyBuddy.Domain.Entities;

public partial class Skill
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ClientUserSkill> ClientUserSkills { get; set; } = new List<ClientUserSkill>();
}
