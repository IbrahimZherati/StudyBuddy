
using Mapster;
using StudyBuddy.Application.DTOs.Shared;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Domain.Services.StudyInterests;
using StudyBuddy.Shared.DTOs.StudyInterestDTO;
using StudyBuddy.Shared.Results;

namespace StudyBuddy.Application.Services.StudyInterests
{
    public class StudyInterestService : IStudyInterestService
    {
        private readonly IRepo<StudyInterest> studyInterestRepo;
        private readonly IStudyInterestDomainService studyInterestDomainService;


        public StudyInterestService(IRepo<StudyInterest> studyInterestRepo, IStudyInterestDomainService studyInterestDomainService)
        {
            this.studyInterestRepo = studyInterestRepo;
            this.studyInterestDomainService = studyInterestDomainService;

        }

        public async Task<Result<GetStudyInterestDTO>> Create(int clientId, CreateStudyInterestDTO studyInterestDTO)
        {
            var valid = await studyInterestDomainService.Create(clientId, studyInterestDTO);
            if (!valid.IsSuccess)
                return Result<GetStudyInterestDTO>.Failure(valid.Error!);

            var result = StudyInterest.Create(clientId, studyInterestDTO);

            if (!result.IsSuccess)
                return Result<GetStudyInterestDTO>.Failure(result.Error!);

            if (result.Value == null)
                return Result<GetStudyInterestDTO>.Failure(Error.CreateFailed);

            var studyInterest = result.Value;
            await studyInterestRepo.AddAsync(studyInterest);

            try
            {
                await studyInterestRepo.SaveAsync();
                var dto = studyInterest.Adapt<GetStudyInterestDTO>();
                return Result<GetStudyInterestDTO>.Success(dto);
            }
            catch (DbUpdateException e)
            {
                return Result<GetStudyInterestDTO>.Failure(Error.CreateFailed);
            }
        }

        public async Task<Result> Delete(int clientId, int id)
        {
            var valid = await studyInterestDomainService.Delete(clientId, id);
            if (!valid.IsSuccess)
                return Result.Failure(valid.Error!);
            var studyInterest = await studyInterestRepo.GetByIdAsync(id);
            if (studyInterest == null)
                return Result.Failure(Error.StudyInterestNotFound);
            studyInterestRepo.Remove(studyInterest);
            try
            {
                await studyInterestRepo.SaveAsync();
                return Result.Success();
            }
            catch (DbUpdateException e)
            {
                return Result.Failure(Error.DeleteFailed);
            }
        }

        public async Task<Result<GetStudyInterestDTO>> GetStudyInterestById(int clientId, int id)
        {
            var valid = await studyInterestDomainService.GetStudyInterestById(clientId, id);
            if (!valid.IsSuccess)
                return Result<GetStudyInterestDTO>.Failure(valid.Error!);
            var studyInterest = await studyInterestRepo.GetByIdAsync(id);
            if (studyInterest == null)
                return Result<GetStudyInterestDTO>.Failure(Error.StudyInterestNotFound);
            var studyInterestDTO = studyInterest.Adapt<GetStudyInterestDTO>();
            return Result<GetStudyInterestDTO>.Success(studyInterestDTO);
        }

        public async Task<Result<DataResponse<GetStudyInterestDTO>>> GetStudyInterests(int clientId ,int skip, int take)
        {
            var valid = await studyInterestDomainService.GetStudyInterests(clientId);
            if (!valid.IsSuccess)
                return Result<DataResponse<GetStudyInterestDTO>>.Failure(valid.Error!);
            var result = studyInterestRepo.GetQuery();

            var query = result.ProjectToType<GetStudyInterestDTO>();

            var data = new DataResponse<GetStudyInterestDTO>();
            data.Count = await query.CountAsync();
            data.Data = await query.OrderBy(q => q.Id).Skip(skip).Take(take).ToListAsync();
            return Result<DataResponse<GetStudyInterestDTO>>.Success(data);
        }

        public async Task<Result<GetStudyInterestDTO>> Update(int clientId ,UpdateStudyInterestDTO studyInterestDTO)
        {
            var valid = await studyInterestDomainService.Update(clientId ,studyInterestDTO);
            if (!valid.IsSuccess)
                return Result<GetStudyInterestDTO>.Failure(valid.Error!);

            var studyInterest = await studyInterestRepo.GetByIdAsync(studyInterestDTO.Id);
            if (studyInterest == null)
                return Result<GetStudyInterestDTO>.Failure(Error.StudyInterestNotFound);

            var result = studyInterest.Update(studyInterestDTO);

            if (!result.IsSuccess)
                return Result<GetStudyInterestDTO>.Failure(result.Error!);

            studyInterestRepo.Update(studyInterest);
            try
            {
                await studyInterestRepo.SaveAsync();
                var dto = studyInterest.Adapt<GetStudyInterestDTO>();
                return Result<GetStudyInterestDTO>.Success(dto);
            }
            catch (DbUpdateException e)
            {
                return Result<GetStudyInterestDTO>.Failure(Error.UpdateFailed);
            }

        }
    }
}
