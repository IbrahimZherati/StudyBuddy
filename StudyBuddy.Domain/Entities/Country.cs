namespace StudyBuddy.Domain.Entities;

public partial class Country
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    public virtual ICollection<ClientUser> ClientUsers { get; set; } = new List<ClientUser>();
}
