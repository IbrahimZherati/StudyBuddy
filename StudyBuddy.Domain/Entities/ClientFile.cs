using Mapster;
using StudyBuddy.Shared.DTOs.ClientFileDTO;
using StudyBuddy.Shared.Helpers.ErrorMessages;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Domain.Entities;

public partial class ClientFile : EntityBase<int>
{
     public int ClientUserId { get; private set; }
     public string Title { get; private set; } = null!;
     public byte[]? Bin { get; private set; }
     public virtual ClientUser ClientUser { get; private set; } = null!;

     private ClientFile() { }

     public static Result<ClientFile> Create(CreateClientFileDTO clientFileDTO)
     {
         var newClientFile = new ClientFile();
         clientFileDTO.Adapt(newClientFile);
         newClientFile.CreateDate = DateTime.Now;
         return Result<ClientFile>.Success(newClientFile);
     }

     public Result<ClientFile> Update(UpdateClientFileDTO clientFileDTO)
     {
         clientFileDTO.Adapt(this);
         ModifyDate = DateTime.Now;
         return Result<ClientFile>.Success(this);
     }


 }
