using Mapster;
using StudyBuddy.Shared.DTOs.CountryDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Application.Services
{
     public interface ICountryService
     {
         Task<Result> Create(CreateCountryDTO countryDTO);
         Task<Result> Update(UpdateCountryDTO countryDTO);
         Task<Result<GetCountryDTO>> GetCountryById(int id);
         Task<Result> Delete(int id);
         Task<Result<DataResponse<GetCountryDTO>>> GetCountries(int skip, int take);
     }
}
     
