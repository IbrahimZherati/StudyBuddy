using Mapster;
using StudyBuddy.Shared.DTOs.CountryDTO;
using StudyBuddy.Shared.Helpers.ErrorMessages;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Domain.Entities;

public partial class Country : EntityBase<int>
{
    public string Name { get; private set; } = null!;

    private readonly List<City> _cities = new();
    public virtual IReadOnlyCollection<City> Cities => _cities;

    private readonly List<ClientUser> _clientUsers = new();
    public virtual IReadOnlyCollection<ClientUser> ClientUsers => _clientUsers;


    private Country() { }

    public static Result<Country> Create(CreateCountryDTO countryDTO)
    {
        var newCountry = new Country();
        countryDTO.Adapt(newCountry);
        newCountry.CreateDate = DateTime.Now;
        return Result<Country>.Success(newCountry);
    }

    public static Country Create(string Name)
    {
        var newCountry = new Country();
        newCountry.Name = Name;
        newCountry.CreateDate = DateTime.Now;
        return newCountry;
    }

    public Result<Country> Update(UpdateCountryDTO countryDTO)
    {
        countryDTO.Adapt(this);
        ModifyDate = DateTime.Now;
        return Result<Country>.Success(this);
    }

    public void AddCity(City city)
    {
        if (city == null)
            return;
        if(!_cities.Contains(city))
            _cities.Add(city);
        return;
    }


}
