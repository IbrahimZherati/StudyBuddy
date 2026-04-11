using Mapster;
using StudyBuddy.Shared.DTOs.CityDTO;
using StudyBuddy.Shared.Helpers.ErrorMessages;
using StudyBuddy.Shared.Results;
using Mapster;
using StudyBuddy.Shared.DTOs.CityDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Application.Services
{
     public interface ICityService
     {
         Task<Result> Create(CreateCityDTO cityDTO);
         Task<Result> Update(UpdateCityDTO cityDTO);
         Task<Result<GetCityDTO>> GetCityById(int id);
         Task<Result> Delete(int id);
         Task<Result<List<GetCityDTO>>> GetCities(int skip, int take);
     }
}
     
