
using Mapster;
using StudyBuddy.Application.Services.Shared.ExtartStringFromPdf;
using StudyBuddy.Application.Services.Shared.GenerateFlashCards;
using StudyBuddy.Application.Services.Shared.GenerateSummary;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Domain.Services.ClientFiles;
using StudyBuddy.Shared.DTOs.ClientFileDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Application.Services
{
    public class ClientFileService : IClientFileService
    {
        private readonly IRepo<ClientFile> clientFileRepo;
        private readonly IExtartStringFromPdf extartStringFromPdf;
        private readonly IGenerateFlashCard generateFlashCard;
        private readonly IGenerateSummary generateSummary;
        private readonly IClientFileDomainService clientFileDomainService;


        public ClientFileService(IRepo<ClientFile> clientFileRepo, IExtartStringFromPdf extartStringFromPdf, IGenerateFlashCard generateFlashCard, IGenerateSummary generateSummary, IClientFileDomainService clientFileDomainService)
        {
            this.clientFileRepo = clientFileRepo;
            this.extartStringFromPdf = extartStringFromPdf;
            this.generateFlashCard = generateFlashCard;
            this.generateSummary = generateSummary;
            this.clientFileDomainService = clientFileDomainService;

        }

        public async Task<Result<GetClientFileDTO>> Create(int clientId, CreateClientFileDTO clientFileDTO)
        {
            var valid = await clientFileDomainService.Create(clientId, clientFileDTO);
            if (!valid.IsSuccess)
                return Result<GetClientFileDTO>.Failure(valid.Error!);

            var result = ClientFile.Create(clientId, clientFileDTO);

            if (!result.IsSuccess)
                return Result<GetClientFileDTO>.Failure(result.Error!);

            if (result.Value == null)
                return Result<GetClientFileDTO>.Failure(Error.CreateFailed);

            var clientFile = result.Value;
            await clientFileRepo.AddAsync(clientFile);

            try
            {
                await clientFileRepo.SaveAsync();
                var dto = clientFile.Adapt<GetClientFileDTO>();
                return Result<GetClientFileDTO>.Success(dto);
            }
            catch (DbUpdateException e)
            {
                return Result<GetClientFileDTO>.Failure(Error.CreateFailed);
            }
        }

        public async Task<Result> Delete(int clientId, int id)
        {
            var valid = await clientFileDomainService.Delete(clientId, id);
            if (!valid.IsSuccess)
                return Result.Failure(valid.Error!);
            var clientFile = await clientFileRepo.GetByIdAsync(id);
            if (clientFile == null)
                return Result.Failure(Error.ClientFileNotFound);
            clientFileRepo.Remove(clientFile);
            try
            {
                await clientFileRepo.SaveAsync();
                return Result.Success();
            }
            catch (DbUpdateException e)
            {
                return Result.Failure(Error.DeleteFailed);
            }
        }

        public async Task<Result<GetClientFileDTO>> GetClientFileById(int id)
        {
            var clientFile = await clientFileRepo.GetByIdAsync(id);
            if (clientFile == null)
                return Result<GetClientFileDTO>.Failure(Error.ClientFileNotFound);
            var clientFileDTO = clientFile.Adapt<GetClientFileDTO>();
            return Result<GetClientFileDTO>.Success(clientFileDTO);
        }

        public async Task<Result<DataResponse<GetClientFileDTO>>> GetClientFiles(int clientId, int skip, int take)
        {
            var valid = await clientFileDomainService.GetClientFilesByClientId(clientId);
            if (!valid.IsSuccess)
                return Result<DataResponse<GetClientFileDTO>>.Failure(valid.Error!);
            var result = clientFileRepo.GetQuery().Where(c => c.ClientUserId == clientId);

            var query = result.ProjectToType<GetClientFileDTO>();

            var data = new DataResponse<GetClientFileDTO>();
            data.Count = await query.CountAsync();
            data.Data = await query.OrderByDescending(q => q.CreateDate).Skip(skip).Take(take).ToListAsync();
            return Result<DataResponse<GetClientFileDTO>>.Success(data);
        }

        public async Task<Result<List<GetFlashCardDTO>>> GetFlashCards(int id, int take)
        {
            var file = await clientFileRepo.GetByIdAsync(id);
            if (file == null)
                return Result<List<GetFlashCardDTO>>.Failure(Error.ClientFileNotFound);
            if (file.Bin == null)
                return Result<List<GetFlashCardDTO>>.Failure(Error.TheFileIsEmpty);
            var text = extartStringFromPdf.ExtractTextFromPdf(file.Bin);
            var result = await generateFlashCard.GetFlashes(text, take);
            if (!result.IsSuccess)
                return Result<List<GetFlashCardDTO>>.Failure(result.Error!);
            if (result.Value == null)
                return Result<List<GetFlashCardDTO>>.Failure(OperationErrorMessage.ItemsNotLoad);
            return Result<List<GetFlashCardDTO>>.Success(result.Value);

        }

        public async Task<Result<string>> GetSummary(int id)
        {
            var file = await clientFileRepo.GetByIdAsync(id);
            if (file == null)
                return Result<string>.Failure(Error.ClientFileNotFound);
            if (file.Bin == null)
                return Result<string>.Failure(Error.TheFileIsEmpty);
            var text = extartStringFromPdf.ExtractTextFromPdf(file.Bin);
            var result = await generateSummary.GetSummary(text);
            if (!result.IsSuccess)
                return Result<string>.Failure(result.Error!);
            if (result.Value == null)
                return Result<string>.Failure(OperationErrorMessage.ItemsNotLoad);
            return Result<string>.Success(result.Value);
        }

        public async Task<Result<GetClientFileDTO>> Update(int clientId, UpdateClientFileDTO clientFileDTO)
        {
            var valid = await clientFileDomainService.Update(clientId, clientFileDTO);
            if (!valid.IsSuccess)
                return Result<GetClientFileDTO>.Failure(valid.Error!);

            var clientFile = await clientFileRepo.GetByIdAsync(clientFileDTO.Id);
            if (clientFile == null)
                return Result<GetClientFileDTO>.Failure(Error.ClientFileNotFound);

            var result = clientFile.Update(clientFileDTO);

            if (!result.IsSuccess)
                return Result<GetClientFileDTO>.Failure(result.Error!);

            clientFileRepo.Update(clientFile);
            try
            {
                await clientFileRepo.SaveAsync();
                var dto = clientFile.Adapt<GetClientFileDTO>();
                return Result<GetClientFileDTO>.Success(dto);
            }
            catch (DbUpdateException e)
            {
                return Result<GetClientFileDTO>.Failure(Error.UpdateFailed);
            }

        }
    }
}
