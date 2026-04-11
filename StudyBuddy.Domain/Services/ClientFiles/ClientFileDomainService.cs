
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.ClientFileDTO;
using StudyBuddy.Shared.Results;
using StudyBuddy.Shared.Helpers.ErrorMessages;
namespace StudyBuddy.Domain.Services.ClientFiles
{
    public class ClientFileDomainService : IClientFileDomainService
    {
        private readonly IRepo<ClientFile> clientFileRepo;
        private readonly IRepo<ClientUser> clientUserRepo;


        public ClientFileDomainService(IRepo<ClientFile> clientFileRepo
        ,IRepo<ClientUser> clientUserRepo
        )
        {
            this.clientFileRepo = clientFileRepo;
            this.clientUserRepo = clientUserRepo;

        }

        public async Task<Result> Create(CreateClientFileDTO clientFileDTO)
        {
            
            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientFileDTO.ClientUserId))
                return Result.Failure(Error.ClientUserNotFound);


            if(await clientFileRepo.ExistsAsync(a => a.Title == clientFileDTO.Title))
                return Result.Failure(Error.ClientFileAlreadyAlreadyExists);
            return Result.Success();
        }

        public async Task<Result> Delete(int Id)
        {
            if(!await clientFileRepo.ExistsAsync(a => a.Id == Id))
                return Result.Failure(Error.ClientFileNotFound);
            return Result.Success();
        }

        public async Task<Result> GetClientFilesByClientId(int clientId)
        {
            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);
            return Result.Success();
        }

        public async Task<Result> Update(UpdateClientFileDTO clientFileDTO)
        { 
            if (!await clientFileRepo.ExistsAsync(a => a.Id == clientFileDTO.Id))
                return Result.Failure(Error.ClientFileNotFound);
            
            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientFileDTO.ClientUserId))
                return Result.Failure(Error.ClientUserNotFound);


            if (await clientFileRepo.ExistsAsync(a => a.Title == clientFileDTO.Title && a.Id != clientFileDTO.Id ))
                return Result.Failure(Error.ClientFileAlreadyAlreadyExists);
            return Result.Success();
        }
    }
}
