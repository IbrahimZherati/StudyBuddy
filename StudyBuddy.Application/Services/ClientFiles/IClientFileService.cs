using Mapster;
using StudyBuddy.Shared.DTOs.ClientFileDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Application.Services
{
    public interface IClientFileService
    {
        Task<Result<GetClientFileDTO>> Create(int clientId ,CreateClientFileDTO clientfileDTO);
        Task<Result<GetClientFileDTO>> Update(int clientId, UpdateClientFileDTO clientfileDTO);
        Task<Result> Delete(int clientId, int id);
        Task<Result<GetClientFileDTO>> GetClientFileById(int id);
        Task<Result<DataResponse<GetClientFileDTO>>> GetClientFiles(int clientId, int skip, int take);
    }
}

