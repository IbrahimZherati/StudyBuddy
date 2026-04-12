
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.UniversityDTO;
using StudyBuddy.Shared.Results;
using StudyBuddy.Shared.Helpers.ErrorMessages;
namespace StudyBuddy.Domain.Services.Universities
{
    public class UniversityDomainService : IUniversityDomainService
    {
        private readonly IRepo<University> universityRepo;


        public UniversityDomainService(IRepo<University> universityRepo
        )
        {
            this.universityRepo = universityRepo;

        }

        public async Task<Result> Create(CreateUniversityDTO universityDTO)
        {
            
            if(await universityRepo.ExistsAsync(a => a.Name == universityDTO.Name))
                return Result.Failure(Error.UniversityAlreadyExists);
            return Result.Success();
        }

        public async Task<Result> Delete(int Id)
        {
            if(!await universityRepo.ExistsAsync(a => a.Id == Id))
                return Result.Failure(Error.UniversityNotFound);
            return Result.Success();
        }

        public async Task<Result> Update(UpdateUniversityDTO universityDTO)
        { 
            if (!await universityRepo.ExistsAsync(a => a.Id == universityDTO.Id))
                return Result.Failure(Error.UniversityNotFound);
            
            if (await universityRepo.ExistsAsync(a => a.Name == universityDTO.Name && a.Id != universityDTO.Id ))
                return Result.Failure(Error.UniversityAlreadyExists);
            return Result.Success();
        }
    }
}
