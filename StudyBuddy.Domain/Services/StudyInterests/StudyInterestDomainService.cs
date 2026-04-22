
using StudyBuddy.Domain.Entities;
using StudyBuddy.Shared.DTOs.StudyInterestDTO;
using StudyBuddy.Shared.Results;
using StudyBuddy.Shared.Helpers.ErrorMessages;
namespace StudyBuddy.Domain.Services.StudyInterests
{
    public class StudyInterestDomainService : IStudyInterestDomainService
    {
        private readonly IRepo<StudyInterest> studyInterestRepo;
        private readonly IRepo<ClientUser> clientUserRepo;

        public StudyInterestDomainService(IRepo<StudyInterest> studyInterestRepo
        , IRepo<ClientUser> clientUserRepo
        )
        {
            this.studyInterestRepo = studyInterestRepo;
            this.clientUserRepo = clientUserRepo;

        }

        public async Task<Result> Create(int clientId, CreateStudyInterestDTO studyInterestDTO)
        {

            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);


            return Result.Success();
        }

        public async Task<Result> Delete(int clientId ,int Id)
        {
            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);
            var studyIntrest = await studyInterestRepo.GetByIdAsync(Id);
            if (studyIntrest == null)
                return Result.Failure(Error.StudyInterestNotFound);

            if (studyIntrest.ClientUserId != clientId)
                return Result.Failure(Error.AccessDeniedNotOwner);
            return Result.Success();
        }

        public async Task<Result> GetStudyInterests(int clientId)
        {
            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);
            return Result.Success();
        }

        public async Task<Result> GetStudyInterestById(int clientId , int Id)
        {
            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);
            var studyIntrest = await studyInterestRepo.GetByIdAsync(Id);
            if (studyIntrest == null)
                return Result.Failure(Error.StudyInterestNotFound);

            if (studyIntrest.ClientUserId != clientId)
                return Result.Failure(Error.AccessDeniedNotOwner);
            return Result.Success();
        }

        

        public async Task<Result> Update(int clientId ,UpdateStudyInterestDTO studyInterestDTO)
        {
            if (!await clientUserRepo.ExistsAsync(c => c.Id == clientId))
                return Result.Failure(Error.ClientUserNotFound);
            var studyIntrest = await studyInterestRepo.GetByIdAsync(studyInterestDTO.Id);
            if (studyIntrest == null)
                return Result.Failure(Error.StudyInterestNotFound);

            if (studyIntrest.ClientUserId != clientId)
                return Result.Failure(Error.AccessDeniedNotOwner);
            return Result.Success();
        }
    }
}
