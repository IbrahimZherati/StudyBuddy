using Mapster;
using StudyBuddy.Shared.DTOs.MajorDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Application.Services
{
     public interface IMajorService
     {
         Task<Result> Create(CreateMajorDTO majorDTO);
         Task<Result> Update(UpdateMajorDTO majorDTO);
         Task<Result<GetMajorDTO>> GetMajorById(int id);
         Task<Result> Delete(int id);
         Task<Result<DataResponse<GetMajorDTO>>> GetMajors(int skip, int take);
     }
}
     
