using Mapster;
using StudyBuddy.Shared.DTOs.MajorDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Application.Services
{
     public interface IMajorService
     {
         Task<Result<GetMajorDTO>> Create(CreateMajorDTO majorDTO);
         Task<Result<GetMajorDTO>> Update(UpdateMajorDTO majorDTO);
         Task<Result<GetMajorDTO>> GetMajorById(int id);
         Task<Result> Delete(int id);
         Task<Result<DataResponse<GetMajorDTO>>> GetMajors(int skip, int take);
     }
}
     
