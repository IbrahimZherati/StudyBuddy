namespace StudyBuddy.Domain.Entities;

public partial class City
{
    public int Id { get; set; }

    public int CountryId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ClientUser> ClientUsers { get; set; } = new List<ClientUser>();

    public virtual Country Country { get; set; } = null!;
}
