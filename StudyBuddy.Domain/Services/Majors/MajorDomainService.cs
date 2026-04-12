
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.MajorDTO;
using StudyBuddy.Shared.Results;
using StudyBuddy.Shared.Helpers.ErrorMessages;
namespace StudyBuddy.Domain.Services.Majors
{
    public class MajorDomainService : IMajorDomainService
    {
        private readonly IRepo<Major> majorRepo;


        public MajorDomainService(IRepo<Major> majorRepo
        )
        {
            this.majorRepo = majorRepo;

        }

        public async Task<Result> Create(CreateMajorDTO majorDTO)
        {
            
            if(await majorRepo.ExistsAsync(a => a.Name == majorDTO.Name))
                return Result.Failure(Error.MajorAlreadyExists);
            return Result.Success();
        }

        public async Task<Result> Delete(int Id)
        {
            if(!await majorRepo.ExistsAsync(a => a.Id == Id))
                return Result.Failure(Error.MajorNotFound);
            return Result.Success();
        }

        public async Task<Result> Update(UpdateMajorDTO majorDTO)
        { 
            if (!await majorRepo.ExistsAsync(a => a.Id == majorDTO.Id))
                return Result.Failure(Error.MajorNotFound);
            
            if (await majorRepo.ExistsAsync(a => a.Name == majorDTO.Name && a.Id != majorDTO.Id ))
                return Result.Failure(Error.MajorAlreadyExists);
            return Result.Success();
        }
    }
}
