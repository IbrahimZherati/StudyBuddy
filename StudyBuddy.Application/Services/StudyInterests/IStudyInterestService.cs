using Mapster;
using StudyBuddy.Shared.DTOs.StudyInterestDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Application.Services.StudyInterests
{
     public interface IStudyInterestService
     {
         Task<Result<GetStudyInterestDTO>> Create(int clientId ,CreateStudyInterestDTO studyinterestDTO);
         Task<Result<GetStudyInterestDTO>> Update(int clientId ,UpdateStudyInterestDTO studyinterestDTO);
         Task<Result<GetStudyInterestDTO>> GetStudyInterestById(int clientId ,int id);
         Task<Result> Delete(int clientId,int id);
         Task<Result<DataResponse<GetStudyInterestDTO>>> GetStudyInterests(int clientId ,int skip, int take);
     }
}
     
