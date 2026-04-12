using Mapster;
using StudyBuddy.Shared.DTOs.UniversityDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Application.Services
{
     public interface IUniversityService
     {
         Task<Result<GetUniversityDTO>> Create(CreateUniversityDTO universityDTO);
         Task<Result<GetUniversityDTO>> Update(UpdateUniversityDTO universityDTO);
         Task<Result<GetUniversityDTO>> GetUniversityById(int id);
         Task<Result> Delete(int id);
         Task<Result<DataResponse<GetUniversityDTO>>> GetUniversities(int skip, int take);
     }
}
     
