using Mapster;
using StudyBuddy.Shared.DTOs.CityDTO;
using StudyBuddy.Shared.Helpers.ErrorMessages;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Domain.Entities;

public partial class City : EntityBase<int>
{
    public int CountryId { get; private set; }

    public string Name { get; private set; } = null!;

    private readonly List<ClientUser> _clientUsers = new();
    public virtual IReadOnlyCollection<ClientUser> ClientUsers => _clientUsers;

    public virtual Country Country { get; private set; } = null!;


    private City() { }

    public static Result<City> Create(CreateCityDTO cityDTO)
    {
        var newCity = new City();
        cityDTO.Adapt(newCity);
        newCity.CreateDate = DateTime.Now;
        return Result<City>.Success(newCity);
    }

    public static City Create(string Name)
    {
        var newCity = new City();
        newCity.Name = Name;
        newCity.CreateDate = DateTime.Now;
        return newCity;
    }

    public Result<City> Update(UpdateCityDTO cityDTO)
    {
        cityDTO.Adapt(this);
        ModifyDate = DateTime.Now;
        return Result<City>.Success(this);
    }


}
