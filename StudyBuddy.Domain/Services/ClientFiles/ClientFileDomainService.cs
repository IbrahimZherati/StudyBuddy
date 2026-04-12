
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

        public async Task<Result> Create(int clientId, CreateClientFileDTO clientFileDTO)
        {
            
            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);


            if(await clientFileRepo.ExistsAsync(a => a.Title == clientFileDTO.Title))
                return Result.Failure(Error.ClientFileAlreadyExists);
            return Result.Success();
        }

        public async Task<Result> Delete(int clientId, int Id)
        {
            var file = await clientFileRepo.GetByIdAsync(Id);

            if(file == null)
                return Result.Failure(Error.ClientFileNotFound);

            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);

            if (file.ClientUserId != clientId)
                return Result.Failure(Error.AccessDeniedNotOwner);
            return Result.Success();
        }

        public async Task<Result> GetClientFilesByClientId(int clientId)
        {
            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);
            return Result.Success();
        }

        public async Task<Result> Update(int clientId, UpdateClientFileDTO clientFileDTO)
        {
            var file = await clientFileRepo.GetByIdAsync(clientFileDTO.Id);

            if (file == null)
                return Result.Failure(Error.ClientFileNotFound);

            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);

            if (file.ClientUserId != clientId)
                return Result.Failure(Error.AccessDeniedNotOwner);



            if (await clientFileRepo.ExistsAsync(a => a.Title == clientFileDTO.Title && a.Id != clientFileDTO.Id ))
                return Result.Failure(Error.ClientFileAlreadyExists);
            return Result.Success();
        }
    }
}
