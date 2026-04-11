
using StudyBuddy.Shared.DTOs.CountryDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Domain.Services.Countries
{
    public interface ICountryDomainService
    {
        Task<Result> Create(CreateCountryDTO countryDTO);
        Task<Result> Update(UpdateCountryDTO countryDTO);
        Task<Result> Delete(int Id);
    } 
}
