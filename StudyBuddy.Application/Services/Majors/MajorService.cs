
using Mapster;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Domain.Services.Majors;
using StudyBuddy.Shared.DTOs.MajorDTO;
using StudyBuddy.Shared.Results;
namespace StudyBuddy.Application.Services
{
    public class MajorService : IMajorService
    {
        private readonly IRepo<Major> majorRepo;
        private readonly IMajorDomainService majorDomainService;


        public MajorService(IRepo<Major> majorRepo, IMajorDomainService majorDomainService)
        {
            this.majorRepo = majorRepo;
            this.majorDomainService = majorDomainService;

        }

        public async Task<Result<GetMajorDTO>> Create(CreateMajorDTO majorDTO)
        {
            var valid = await majorDomainService.Create(majorDTO);
            if (!valid.IsSuccess)
                return Result<GetMajorDTO>.Failure(valid.Error!);

            var result = Major.Create(majorDTO);

            if (!result.IsSuccess)
                return Result<GetMajorDTO>.Failure(result.Error!);

            if(result.Value == null)
                return Result<GetMajorDTO>.Failure(Error.CreateFailed);

            var major = result.Value;
            await majorRepo.AddAsync(major);

            try
            {
                await majorRepo.SaveAsync();
                var dto = major.Adapt<GetMajorDTO>();
                return Result<GetMajorDTO>.Success(dto);
            }
            catch(DbUpdateException e)
            {
                return Result<GetMajorDTO>.Failure(Error.CreateFailed);
            }
        }

        public async Task<Result> Delete(int id)
        {
            var valid = await majorDomainService.Delete(id);
            if(!valid.IsSuccess)
                return Result.Failure(valid.Error!);
            var major = await majorRepo.GetByIdAsync(id);
            if (major == null)
                return Result.Failure(Error.MajorNotFound);
            majorRepo.Remove(major);
            try
            {
                await majorRepo.SaveAsync();
                return Result.Success();
            }
            catch(DbUpdateException e)
            {
                return Result.Failure(Error.DeleteFailed);
            }
        }

        public async Task<Result<GetMajorDTO>> GetMajorById(int id)
        {
            var major = await majorRepo.GetByIdAsync(id);
            if (major == null)
                return Result<GetMajorDTO>.Failure(Error.MajorNotFound);
            var majorDTO = major.Adapt<GetMajorDTO>();
            return Result<GetMajorDTO>.Success(majorDTO);
        }

        public async Task<Result<DataResponse<GetMajorDTO>>> GetMajors(int skip, int take)
        {
            var result = majorRepo.GetQuery();

            var query = result.ProjectToType<GetMajorDTO>();

            var data = new DataResponse<GetMajorDTO>();
            data.Count = await query.CountAsync();
            data.Data = await query.OrderBy(q => q.Id).Skip(skip).Take(take).ToListAsync();
            return Result<DataResponse<GetMajorDTO>>.Success(data);
        }

        public async Task<Result<GetMajorDTO>> Update(UpdateMajorDTO majorDTO)
        {
            var valid = await majorDomainService.Update(majorDTO);
            if (!valid.IsSuccess)
                return Result<GetMajorDTO>.Failure(valid.Error!);

            var major = await majorRepo.GetByIdAsync(majorDTO.Id);
            if (major == null)
                return Result<GetMajorDTO>.Failure(Error.MajorNotFound);

            var result = major.Update(majorDTO);

            if (!result.IsSuccess)
                return Result<GetMajorDTO>.Failure(result.Error!);

            majorRepo.Update(major);
            try
            {
                await majorRepo.SaveAsync();
                var dto = major.Adapt<GetMajorDTO>();
                return Result<GetMajorDTO>.Success(dto);
            }
            catch(DbUpdateException e)
            {
                return Result<GetMajorDTO>.Failure(Error.UpdateFailed);
            }

        }
    }
}
