using Mapster;
using StudyBuddy.Shared.DTOs.ClientFileDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Application.Services
{
     public interface IClientFileService
     {
         Task<Result> Create(CreateClientFileDTO clientfileDTO);
         Task<Result> Update(UpdateClientFileDTO clientfileDTO);
         Task<Result<GetClientFileDTO>> GetClientFileById(int id);
         Task<Result> Delete(int id);
         Task<Result<DataResponse<GetClientFileDTO>>> GetClientFiles(int clientId , int skip, int take);
     }
}
     
